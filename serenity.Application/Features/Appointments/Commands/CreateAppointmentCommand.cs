using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Appointments.Commands;

namespace serenity.Application.Features.Appointments.Commands;

public record CreateAppointmentCommand(CreateAppointmentRequest Request) : IRequest<AppointmentDto>;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
{
    private readonly CreateAppointmentUseCase _useCase;

    public CreateAppointmentCommandHandler(CreateAppointmentUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}




