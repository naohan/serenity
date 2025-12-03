using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MeditationSessions;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.MeditationSessions.Commands;

public class CreateMeditationSessionUseCase
{
    private readonly IMeditationSessionRepository _sessionRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMeditationSessionUseCase(
        IMeditationSessionRepository sessionRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MeditationSessionDto> ExecuteAsync(CreateMeditationSessionRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var session = new MeditationSession
        {
            PatientId = request.PatientId,
            SessionDate = request.SessionDate,
            DurationMinutes = request.DurationMinutes,
            Type = request.Type,
            Notes = request.Notes,
            CreatedAt = DateTime.Now
        };

        await _sessionRepository.AddAsync(session, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return session.ToDto();
    }
}




