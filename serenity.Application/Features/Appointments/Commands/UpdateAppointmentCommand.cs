using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Appointments.Commands;

namespace serenity.Application.Features.Appointments.Commands;

public record UpdateAppointmentCommand(int Id, UpdateAppointmentRequest Request) : IRequest<AppointmentDto>;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentDto>
{
    private readonly UpdateAppointmentUseCase _useCase;

    public UpdateAppointmentCommandHandler(UpdateAppointmentUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<AppointmentDto> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}



