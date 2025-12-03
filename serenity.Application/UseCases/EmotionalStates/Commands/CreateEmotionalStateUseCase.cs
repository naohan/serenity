using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.EmotionalStates;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.EmotionalStates.Commands;

public class CreateEmotionalStateUseCase
{
    private readonly IEmotionalStateRepository _emotionalStateRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmotionalStateUseCase(
        IEmotionalStateRepository emotionalStateRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _emotionalStateRepository = emotionalStateRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EmotionalStateDto> ExecuteAsync(CreateEmotionalStateRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var emotionalState = new EmotionalState
        {
            PatientId = request.PatientId,
            Date = request.Date,
            EmotionalState1 = request.EmotionalState1,
            Value = request.Value,
            CreatedAt = DateTime.Now
        };

        await _emotionalStateRepository.AddAsync(emotionalState, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return emotionalState.ToDto();
    }
}




