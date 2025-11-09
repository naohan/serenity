using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Patients.Queries;

namespace serenity.Application.Features.Patients.Queries;

public record GetAllPatientsQuery() : IRequest<IEnumerable<PatientDto>>;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientDto>>
{
    private readonly GetAllPatientsUseCase _useCase;

    public GetAllPatientsQueryHandler(GetAllPatientsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}

