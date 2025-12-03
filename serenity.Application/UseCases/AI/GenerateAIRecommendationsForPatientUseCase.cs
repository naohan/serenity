using serenity.Application.DTOs;
using serenity.Application.Interfaces;

using serenity.Infrastructure;

namespace serenity.Application.UseCases.AI;

public class GenerateAIRecommendationsForPatientUseCase
{
    private readonly IAssistantService _assistantService;
    private readonly IPatientRepository _patientRepository;
    private readonly IInsightsAndRecommendationRepository _insightsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateAIRecommendationsForPatientUseCase(
        IAssistantService assistantService,
        IPatientRepository patientRepository,
        IInsightsAndRecommendationRepository insightsRepository,
        IUnitOfWork unitOfWork)
    {
        _assistantService = assistantService;
        _patientRepository = patientRepository;
        _insightsRepository = insightsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InsightResponseDto> ExecuteAsync(int patientId, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el paciente con id {patientId}.");

        var result = await _assistantService.GenerateInsightsForPatientAsync(patientId, cancellationToken);

        // Guardar insights en la base de datos
        var now = DateTime.Now;
        foreach (var insight in result.Insights)
        {
            var insightEntity = new InsightsAndRecommendation
            {
                PatientId = patientId,
                Title = "Insight generado por IA",
                Description = insight,
                Type = "insight",
                Priority = null,
                CreatedAt = now,
                UpdatedAt = now
            };
            await _insightsRepository.AddAsync(insightEntity, cancellationToken);
        }

        if (result.Recommendations.Any())
        {
            foreach (var recommendation in result.Recommendations.Skip(1))
            {
                var recommendationEntity = new InsightsAndRecommendation
                {
                    PatientId = patientId,
                    Title = "Recomendación generada por IA",
                    Description = recommendation,
                    Type = "recommendation",
                    Priority = null,
                    CreatedAt = now,
                    UpdatedAt = now
                };
                await _insightsRepository.AddAsync(recommendationEntity, cancellationToken);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new InsightResponseDto
        {
            PatientId = patientId,
            Insights = result.Insights,
            Recommendations = result.Recommendations,
            Summary = result.Summary,
            GeneratedAt = now
        };
    }
}

