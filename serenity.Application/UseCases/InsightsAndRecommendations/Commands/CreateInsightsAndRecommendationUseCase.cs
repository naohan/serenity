using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.InsightsAndRecommendations;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.InsightsAndRecommendations.Commands;

public class CreateInsightsAndRecommendationUseCase
{
    private readonly IInsightsAndRecommendationRepository _insightRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInsightsAndRecommendationUseCase(
        IInsightsAndRecommendationRepository insightRepository,
        IPatientRepository patientRepository,
        IPsychologistRepository psychologistRepository,
        IUnitOfWork unitOfWork)
    {
        _insightRepository = insightRepository;
        _patientRepository = patientRepository;
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InsightsAndRecommendationDto> ExecuteAsync(CreateInsightsAndRecommendationRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        if (request.PsychologistId.HasValue)
        {
            var psychologist = await _psychologistRepository.GetByIdAsync(request.PsychologistId.Value, cancellationToken);
            if (psychologist is null)
            {
                throw new KeyNotFoundException($"No se encontró el psicólogo con id {request.PsychologistId.Value}.");
            }
        }

        var now = DateTime.Now;
        var insight = new InsightsAndRecommendation
        {
            PatientId = request.PatientId,
            PsychologistId = request.PsychologistId,
            Title = request.Title,
            Description = request.Description,
            Type = request.Type,
            Priority = request.Priority,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _insightRepository.AddAsync(insight, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return insight.ToDto();
    }
}




