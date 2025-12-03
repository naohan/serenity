using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Appointments;

namespace serenity.Application.UseCases.Appointments.Queries;

public class GetAppointmentByIdUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentByIdUseCase(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<AppointmentDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        return appointment?.ToDto();
    }
}

