using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Appointments.Queries;

namespace serenity.Application.Features.Appointments.Queries;

public record GetAppointmentByIdQuery(int Id) : IRequest<AppointmentDto?>;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto?>
{
    private readonly GetAppointmentByIdUseCase _useCase;

    public GetAppointmentByIdQueryHandler(GetAppointmentByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<AppointmentDto?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



