using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientNotes;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.PatientNotes.Commands;

public class CreatePatientNoteUseCase
{
    private readonly IPatientNoteRepository _noteRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientNoteUseCase(
        IPatientNoteRepository noteRepository,
        IPatientRepository patientRepository,
        IPsychologistRepository psychologistRepository,
        IUnitOfWork unitOfWork)
    {
        _noteRepository = noteRepository;
        _patientRepository = patientRepository;
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientNoteDto> ExecuteAsync(CreatePatientNoteRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var psychologist = await _psychologistRepository.GetByIdAsync(request.PsychologistId, cancellationToken);
        if (psychologist is null)
        {
            throw new KeyNotFoundException($"No se encontró el psicólogo con id {request.PsychologistId}.");
        }

        var now = DateTime.Now;
        var note = new PatientNote
        {
            PatientId = request.PatientId,
            PsychologistId = request.PsychologistId,
            Date = request.Date,
            Content = request.Content,
            Mood = request.Mood,
            AiDiagnosis = request.AiDiagnosis,
            NeedsFollowUp = request.NeedsFollowUp ?? false,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _noteRepository.AddAsync(note, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return note.ToDto();
    }
}



