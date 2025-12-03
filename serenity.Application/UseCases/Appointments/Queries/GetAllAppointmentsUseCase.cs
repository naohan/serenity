using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Appointments;

namespace serenity.Application.UseCases.Appointments.Queries;

public class GetAllAppointmentsUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAllAppointmentsUseCase(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<AppointmentDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var appointments = await _appointmentRepository.GetAllAsync(cancellationToken);
        return appointments.Select(a => a.ToDto());
    }
}

