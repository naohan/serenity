using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Appointments.Queries;

namespace serenity.Application.Features.Appointments.Queries;

public record GetAllAppointmentsQuery : IRequest<IEnumerable<AppointmentDto>>;

public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, IEnumerable<AppointmentDto>>
{
    private readonly GetAllAppointmentsUseCase _useCase;

    public GetAllAppointmentsQueryHandler(GetAllAppointmentsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}



