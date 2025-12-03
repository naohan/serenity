using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.AI.Commands;
using serenity.Application.Features.AI.Queries;
using serenity.Application.Interfaces;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AIController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IInsightsAndRecommendationRepository _insightsRepository;

    public AIController(IMediator mediator, IInsightsAndRecommendationRepository insightsRepository)
    {
        _mediator = mediator;
        _insightsRepository = insightsRepository;
    }

    /// <summary>
    /// Generates AI-powered insights and recommendations for a patient.
    /// </summary>
    [HttpPost("patient/{id}/generate-insights")]
    public async Task<ActionResult<InsightResponseDto>> GenerateInsights(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GenerateInsightsCommand(id), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (HttpRequestException ex) when (ex.Data.Contains("StatusCode") && ex.Data["StatusCode"] is System.Net.HttpStatusCode statusCode && statusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            return StatusCode(429, new { message = "Demasiadas solicitudes a la API de IA. Por favor, espera un momento antes de intentar nuevamente.", error = ex.Message });
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { message = "Error al comunicarse con la API de IA", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al generar insights con IA", error = ex.Message });
        }
    }

    /// <summary>
    /// Analyzes patient notes using AI to generate diagnosis and suggestions.
    /// </summary>
    [HttpPost("notes/analyze")]
    public async Task<ActionResult<DiagnosisResponseDto>> AnalyzeNotes([FromBody] AnalyzeNotesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new AnalyzeNotesCommand(request.NoteId), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (HttpRequestException ex) when (ex.Data.Contains("StatusCode") && ex.Data["StatusCode"] is System.Net.HttpStatusCode statusCode && statusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            return StatusCode(429, new { message = "Demasiadas solicitudes a la API de IA. Por favor, espera un momento antes de intentar nuevamente.", error = ex.Message });
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { message = "Error al comunicarse con la API de IA", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al analizar notas con IA", error = ex.Message });
        }
    }

    /// <summary>
    /// Analyzes wellbeing trends for a patient over a date range.
    /// </summary>
    [HttpGet("patient/{id}/trends")]
    public async Task<ActionResult<TrendAnalysisResponseDto>> AnalyzeTrends(
        int id,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new AnalyzeTrendsQuery(id, startDate, endDate), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (HttpRequestException ex) when (ex.Data.Contains("StatusCode") && ex.Data["StatusCode"] is System.Net.HttpStatusCode statusCode && statusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            return StatusCode(429, new { message = "Demasiadas solicitudes a la API de IA. Por favor, espera un momento antes de intentar nuevamente.", error = ex.Message });
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { message = "Error al comunicarse con la API de IA", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al analizar tendencias con IA", error = ex.Message });
        }
    }

    /// <summary>
    /// Gets all AI-generated insights for a patient.
    /// </summary>
    [HttpGet("patient/{id}/insights")]
    public async Task<ActionResult<IEnumerable<InsightResponseDto>>> GetPatientInsights(int id, CancellationToken cancellationToken)
    {
        try
        {
            var insights = await _insightsRepository.GetByPatientIdAsync(id, cancellationToken);
            var result = insights.Select(i => new InsightResponseDto
            {
                PatientId = id,
                Insights = !string.IsNullOrWhiteSpace(i.Description) ? new List<string> { i.Description } : new List<string>(),
                Recommendations = new List<string>(), // Las recomendaciones detalladas no se distinguen a nivel de entidad
                Summary = i.Title,
                GeneratedAt = i.CreatedAt
            }).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener insights", error = ex.Message });
        }
    }
}

