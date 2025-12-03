using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Appointments.Commands;

public class DeleteAppointmentUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentUseCase(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró la cita con id {id}.");

        await _appointmentRepository.DeleteAsync(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

