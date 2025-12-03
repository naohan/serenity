using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using serenity.Application.Interfaces;
using serenity.Domain.Ports.IServicios;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace serenity.Infrastructure.Adapters.Services;

/// <summary>
/// OpenAI-based assistant service implementation.
/// </summary>
public class AssistantService : serenity.Application.Interfaces.IAssistantService
{
    private readonly SerenityDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _model;

    public AssistantService(IConfiguration configuration, SerenityDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI API key not configured.");
        _model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<AIInsightResult> GenerateInsightsForPatientAsync(int patientId, CancellationToken cancellationToken = default)
    {
        var patient = await _context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken)
            ?? throw new KeyNotFoundException($"Patient with id {patientId} not found.");

        // Recopilar historial del paciente
        var history = await BuildPatientHistoryAsync(patientId, cancellationToken);

        var prompt = BuildInsightsPrompt(patient, history);

        var response = await CallOpenAIAsync(new[]
        {
            new { role = "system", content = "Eres un asistente de IA especializado en salud mental y bienestar emocional. Proporciona insights y recomendaciones basadas en datos estructurados." },
            new { role = "user", content = prompt }
        }, cancellationToken);

        return ParseInsightsResponse(response);
    }

    public async Task<AIDiagnosisResult> AnalyzeNotesAsync(int noteId, CancellationToken cancellationToken = default)
    {
        var note = await _context.PatientNotes
            .Include(n => n.Patient)
            .FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken)
            ?? throw new KeyNotFoundException($"Note with id {noteId} not found.");

        var prompt = $@"Analiza la siguiente nota del paciente y proporciona:
1. Un diagnóstico o análisis breve
2. Sugerencias específicas para mejorar el tratamiento
3. Análisis detallado

Nota del paciente:
Fecha: {note.Date}
Contenido: {note.Content}";

        var response = await CallOpenAIAsync(new[]
        {
            new { role = "system", content = "Eres un psicólogo experto que analiza notas de pacientes para proporcionar diagnósticos y sugerencias profesionales." },
            new { role = "user", content = prompt }
        }, cancellationToken);

        return ParseDiagnosisResponse(response);
    }

    public async Task<AITrendAnalysisResult> AnalyzeWellbeingTrendsAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        var patient = await _context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken)
            ?? throw new KeyNotFoundException($"Patient with id {patientId} not found.");

        var trends = await BuildTrendsDataAsync(patientId, startDate, endDate, cancellationToken);

        var prompt = $@"Analiza las siguientes tendencias de bienestar del paciente y proporciona:
1. Análisis de tendencias generales
2. Hallazgos clave
3. Recomendaciones basadas en las tendencias

Datos del paciente:
{trends}";

        var response = await CallOpenAIAsync(new[]
        {
            new { role = "system", content = "Eres un analista de datos de salud mental especializado en identificar tendencias y patrones en el bienestar emocional." },
            new { role = "user", content = prompt }
        }, cancellationToken);

        return ParseTrendsResponse(response);
    }

    private async Task<string> BuildPatientHistoryAsync(int patientId, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);

        if (patient == null) return string.Empty;

        var dailyMoods = await _context.DailyMoods
            .Where(dm => dm.PatientId == patientId)
            .OrderByDescending(dm => dm.Date)
            .Take(30)
            .ToListAsync(cancellationToken);

        var emotionalStates = await _context.EmotionalStates
            .Where(es => es.PatientId == patientId)
            .OrderByDescending(es => es.Date)
            .Take(30)
            .ToListAsync(cancellationToken);

        var moodMetrics = await _context.MoodMetrics
            .Where(mm => mm.PatientId == patientId)
            .OrderByDescending(mm => mm.Date)
            .Take(30)
            .ToListAsync(cancellationToken);

        var wellbeingMetrics = await _context.MentalWellbeingMetrics
            .Where(mwm => mwm.PatientId == patientId)
            .OrderByDescending(mwm => mwm.Date)
            .Take(30)
            .ToListAsync(cancellationToken);

        var notes = await _context.PatientNotes
            .Where(n => n.PatientId == patientId)
            .OrderByDescending(n => n.Date)
            .Take(20)
            .ToListAsync(cancellationToken);

        var history = $@"Historial del Paciente:
Nombre: {patient.User?.Name ?? "N/A"}
Edad: {patient.Age?.ToString() ?? "N/A"}
Estado: {patient.Status ?? "No especificado"}
Diagnóstico: {patient.Diagnosis ?? "No registrado"}

Estados de ánimo diarios (últimos 30):
{string.Join("\n", dailyMoods.Select(dm => $"- {dm.Date}: {dm.Mood} - {dm.Note}"))}

Estados emocionales (últimos 30):
{string.Join("\n", emotionalStates.Select(es => $"- {es.Date}: {es.EmotionalState1} - {es.Value}"))}

Métricas de ánimo (últimos 30):
{string.Join("\n", moodMetrics.Select(mm => $"- {mm.Date}: Happy {mm.HappyPercentage}%, Calm {mm.CalmPercentage}%, Sad {mm.SadPercentage}%, Anxious {mm.AnxiousPercentage}%"))}

Métricas de bienestar mental (últimos 30):
{string.Join("\n", wellbeingMetrics.Select(mwm => $"- {mwm.Date}: Stress {mwm.StressLevel}, Energy {mwm.EnergyLevel}, Concentration {mwm.ConcentrationLevel}, Satisfaction {mwm.SatisfactionLevel}, Sleep Duration {mwm.SleepDuration}h, Sleep Quality {mwm.SleepQuality}"))}

Notas del paciente (últimas 20):
{string.Join("\n", notes.Select(n => $"- {n.Date}: {n.Content}"))}";

        return history;
    }

    private string BuildInsightsPrompt(Patient patient, string history)
    {
        return $@"Analiza el siguiente historial de un paciente y genera:
1. Insights clave sobre su estado emocional y bienestar
2. Recomendaciones específicas y accionables
3. Un resumen ejecutivo

{history}

Proporciona la respuesta en formato JSON con las siguientes claves:
- insights: array de strings con insights clave
- recommendations: array de strings con recomendaciones específicas
- summary: string con resumen ejecutivo";
    }

    private async Task<string> BuildTrendsDataAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var dailyMoods = await _context.DailyMoods
            .Where(dm => dm.PatientId == patientId && dm.Date >= startDate && dm.Date <= endDate)
            .OrderBy(dm => dm.Date)
            .ToListAsync(cancellationToken);

        var moodMetrics = await _context.MoodMetrics
            .Where(mm => mm.PatientId == patientId && mm.Date >= startDate && mm.Date <= endDate)
            .OrderBy(mm => mm.Date)
            .ToListAsync(cancellationToken);

        var wellbeingMetrics = await _context.MentalWellbeingMetrics
            .Where(mwm => mwm.PatientId == patientId && mwm.Date >= startDate && mwm.Date <= endDate)
            .OrderBy(mwm => mwm.Date)
            .ToListAsync(cancellationToken);

        return $@"Tendencias de Bienestar ({startDate} a {endDate}):

Estados de ánimo diarios:
{string.Join("\n", dailyMoods.Select(dm => $"{dm.Date}: {dm.Mood}"))}

Métricas de ánimo:
{string.Join("\n", moodMetrics.Select(mm => $"{mm.Date}: Happy {mm.HappyPercentage}%, Calm {mm.CalmPercentage}%, Sad {mm.SadPercentage}%, Anxious {mm.AnxiousPercentage}%"))}

Métricas de bienestar mental:
{string.Join("\n", wellbeingMetrics.Select(mwm => $"{mwm.Date}: Stress {mwm.StressLevel}, Energy {mwm.EnergyLevel}, Concentration {mwm.ConcentrationLevel}, Satisfaction {mwm.SatisfactionLevel}, Sleep Duration {mwm.SleepDuration}h, Sleep Quality {mwm.SleepQuality}"))}";
    }

    private AIInsightResult ParseInsightsResponse(string content)
    {
        var result = new AIInsightResult();

        // Intentar parsear JSON, si falla, parsear texto plano
        try
        {
            // Buscar arrays de insights y recommendations
            var insightsMatch = Regex.Match(content, @"""insights""\s*:\s*\[(.*?)\]", RegexOptions.Singleline);
            var recommendationsMatch = Regex.Match(content, @"""recommendations""\s*:\s*\[(.*?)\]", RegexOptions.Singleline);
            var summaryMatch = Regex.Match(content, @"""summary""\s*:\s*""(.*?)""", RegexOptions.Singleline);

            if (insightsMatch.Success)
            {
                var insightsContent = insightsMatch.Groups[1].Value;
                result.Insights = ExtractListItems(insightsContent);
            }

            if (recommendationsMatch.Success)
            {
                var recommendationsContent = recommendationsMatch.Groups[1].Value;
                result.Recommendations = ExtractListItems(recommendationsContent);
            }

            if (summaryMatch.Success)
            {
                result.Summary = summaryMatch.Groups[1].Value;
            }
        }
        catch
        {
            // Fallback: parsear como texto plano
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.Contains("insight", StringComparison.OrdinalIgnoreCase) || line.Contains("observación", StringComparison.OrdinalIgnoreCase))
                {
                    result.Insights.Add(line.Trim());
                }
                else if (line.Contains("recomendación", StringComparison.OrdinalIgnoreCase) || line.Contains("sugerencia", StringComparison.OrdinalIgnoreCase))
                {
                    result.Recommendations.Add(line.Trim());
                }
            }
        }

        if (!result.Insights.Any() && !result.Recommendations.Any())
        {
            result.Insights.Add(content);
            result.Summary = content.Substring(0, Math.Min(200, content.Length));
        }

        return result;
    }

    private AIDiagnosisResult ParseDiagnosisResponse(string content)
    {
        var result = new AIDiagnosisResult();

        // Buscar diagnóstico, sugerencias y análisis
        var diagnosisMatch = Regex.Match(content, @"(?:diagnóstico|diagnosis)[:\-]\s*(.*?)(?:\n|$)", RegexOptions.IgnoreCase);
        var suggestionsMatch = Regex.Match(content, @"(?:sugerencias|suggestions)[:\-]\s*(.*?)(?:\n|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        if (diagnosisMatch.Success)
        {
            result.Diagnosis = diagnosisMatch.Groups[1].Value.Trim();
        }

        if (suggestionsMatch.Success)
        {
            result.Suggestions = ExtractListItems(suggestionsMatch.Groups[1].Value);
        }

        result.Analysis = content;

        return result;
    }

    private AITrendAnalysisResult ParseTrendsResponse(string content)
    {
        var result = new AITrendAnalysisResult();

        var findingsMatch = Regex.Match(content, @"(?:hallazgos|findings|puntos clave)[:\-]\s*(.*?)(?:\n|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        var recommendationsMatch = Regex.Match(content, @"(?:recomendaciones|recommendations)[:\-]\s*(.*?)(?:\n|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        if (findingsMatch.Success)
        {
            result.KeyFindings = ExtractListItems(findingsMatch.Groups[1].Value);
        }

        if (recommendationsMatch.Success)
        {
            result.Recommendations = ExtractListItems(recommendationsMatch.Groups[1].Value);
        }

        result.TrendAnalysis = content;

        return result;
    }

    private List<string> ExtractListItems(string content)
    {
        var items = new List<string>();
        var matches = Regex.Matches(content, @"(?:^|\n)[\-\*•]\s*(.+?)(?=\n|$)", RegexOptions.Multiline);
        foreach (Match match in matches)
        {
            var item = match.Groups[1].Value.Trim();
            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
        }

        // Si no se encontraron items con bullets, dividir por líneas
        if (!items.Any())
        {
            items = content.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();
        }

        return items;
    }

    private async Task<string> CallOpenAIAsync(object[] messages, CancellationToken cancellationToken, int maxTokens = 2000)
    {
        // Delay para evitar spam y rate limiting de OpenAI
        await Task.Delay(1500, cancellationToken); // 1.5 segundos entre solicitudes

        var requestBody = new
        {
            model = _model,
            messages = messages,
            temperature = 0.7,
            max_tokens = maxTokens
        };

        var jsonContent = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("chat/completions", content, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var statusCode = (int)response.StatusCode;
            
            string errorMessage = statusCode switch
            {
                429 => "OpenAI API: Demasiadas solicitudes. Por favor, espera un momento antes de intentar nuevamente.",
                401 => "OpenAI API: Clave de API inválida o no autorizada. Verifica la configuración.",
                400 => $"OpenAI API: Solicitud inválida. {errorContent}",
                500 => "OpenAI API: Error interno del servidor de OpenAI. Intenta más tarde.",
                503 => "OpenAI API: Servicio no disponible temporalmente. Intenta más tarde.",
                _ => $"OpenAI API: Error {statusCode}. {errorContent}"
            };
            
            var exception = new HttpRequestException(errorMessage, null, response.StatusCode);
            exception.Data["StatusCode"] = response.StatusCode;
            throw exception;
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var jsonDoc = JsonDocument.Parse(responseContent);

        return jsonDoc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? string.Empty;
    }
}

