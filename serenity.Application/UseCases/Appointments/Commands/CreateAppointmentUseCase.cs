using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Appointments;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Appointments.Commands;

public class CreateAppointmentUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentUseCase(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IPsychologistRepository psychologistRepository,
        IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AppointmentDto> ExecuteAsync(CreateAppointmentRequest request, CancellationToken cancellationToken = default)
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
        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            PsychologistId = request.PsychologistId,
            AppointmentDate = request.AppointmentDate,
            AppointmentTime = request.AppointmentTime,
            Duration = request.Duration,
            Type = request.Type,
            Status = request.Status ?? "Pendiente",
            Notes = request.Notes,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _appointmentRepository.AddAsync(appointment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.ToDto();
    }
}

