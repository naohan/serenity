using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Appointments;

namespace serenity.Application.UseCases.Appointments.Commands;

public class UpdateAppointmentUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentUseCase(
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

    public async Task<AppointmentDto> ExecuteAsync(int id, UpdateAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró la cita con id {id}.");

        if (request.PatientId.HasValue)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId.Value, cancellationToken);
            if (patient is null)
            {
                throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId.Value}.");
            }
            appointment.PatientId = request.PatientId.Value;
        }

        if (request.PsychologistId.HasValue)
        {
            var psychologist = await _psychologistRepository.GetByIdAsync(request.PsychologistId.Value, cancellationToken);
            if (psychologist is null)
            {
                throw new KeyNotFoundException($"No se encontró el psicólogo con id {request.PsychologistId.Value}.");
            }
            appointment.PsychologistId = request.PsychologistId.Value;
        }

        if (request.AppointmentDate.HasValue)
        {
            appointment.AppointmentDate = request.AppointmentDate.Value;
        }

        if (request.AppointmentTime.HasValue)
        {
            appointment.AppointmentTime = request.AppointmentTime.Value;
        }

        if (request.Duration.HasValue)
        {
            appointment.Duration = request.Duration.Value;
        }

        if (request.Type is not null)
        {
            appointment.Type = request.Type;
        }

        if (request.Status is not null)
        {
            appointment.Status = request.Status;
        }

        if (request.Notes is not null)
        {
            appointment.Notes = request.Notes;
        }

        appointment.UpdatedAt = DateTime.Now;

        await _appointmentRepository.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.ToDto();
    }
}

