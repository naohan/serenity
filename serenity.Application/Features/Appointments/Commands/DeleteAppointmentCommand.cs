using MediatR;
using serenity.Application.UseCases.Appointments.Commands;

namespace serenity.Application.Features.Appointments.Commands;

public record DeleteAppointmentCommand(int Id) : IRequest;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
{
    private readonly DeleteAppointmentUseCase _useCase;

    public DeleteAppointmentCommandHandler(DeleteAppointmentUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




