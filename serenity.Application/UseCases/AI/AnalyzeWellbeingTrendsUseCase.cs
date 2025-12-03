using serenity.Application.DTOs;
using serenity.Application.Interfaces;


namespace serenity.Application.UseCases.AI;

public class AnalyzeWellbeingTrendsUseCase
{
    private readonly IAssistantService _assistantService;
    private readonly IPatientRepository _patientRepository;

    public AnalyzeWellbeingTrendsUseCase(
        IAssistantService assistantService,
        IPatientRepository patientRepository)
    {
        _assistantService = assistantService;
        _patientRepository = patientRepository;
    }

    public async Task<TrendAnalysisResponseDto> ExecuteAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el paciente con id {patientId}.");

        var result = await _assistantService.AnalyzeWellbeingTrendsAsync(patientId, startDate, endDate, cancellationToken);

        return new TrendAnalysisResponseDto
        {
            PatientId = patientId,
            StartDate = startDate,
            EndDate = endDate,
            TrendAnalysis = result.TrendAnalysis,
            KeyFindings = result.KeyFindings,
            Recommendations = result.Recommendations,
            GeneratedAt = DateTime.Now
        };
    }
}


