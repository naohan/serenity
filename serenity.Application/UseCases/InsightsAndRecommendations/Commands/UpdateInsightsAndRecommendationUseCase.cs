using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.InsightsAndRecommendations;

namespace serenity.Application.UseCases.InsightsAndRecommendations.Commands;

public class UpdateInsightsAndRecommendationUseCase
{
    private readonly IInsightsAndRecommendationRepository _insightRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInsightsAndRecommendationUseCase(
        IInsightsAndRecommendationRepository insightRepository,
        IPsychologistRepository psychologistRepository,
        IUnitOfWork unitOfWork)
    {
        _insightRepository = insightRepository;
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InsightsAndRecommendationDto> ExecuteAsync(int id, UpdateInsightsAndRecommendationRequest request, CancellationToken cancellationToken = default)
    {
        var insight = await _insightRepository.GetByIdAsync(id, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el insight con id {id}.");

        if (request.PsychologistId.HasValue)
        {
            var psychologist = await _psychologistRepository.GetByIdAsync(request.PsychologistId.Value, cancellationToken);
            if (psychologist is null)
            {
                throw new KeyNotFoundException($"No se encontró el psicólogo con id {request.PsychologistId.Value}.");
            }
            insight.PsychologistId = request.PsychologistId;
        }

        if (request.Title is not null)
        {
            insight.Title = request.Title;
        }

        if (request.Description is not null)
        {
            insight.Description = request.Description;
        }

        if (request.Type is not null)
        {
            insight.Type = request.Type;
        }

        if (request.Priority is not null)
        {
            insight.Priority = request.Priority;
        }

        insight.UpdatedAt = DateTime.Now;

        await _insightRepository.UpdateAsync(insight);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return insight.ToDto();
    }
}



