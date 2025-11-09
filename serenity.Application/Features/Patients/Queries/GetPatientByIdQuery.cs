using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Patients.Queries;

namespace serenity.Application.Features.Patients.Queries;

public record GetPatientByIdQuery(int Id) : IRequest<PatientDto?>;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto?>
{
    private readonly GetPatientByIdUseCase _useCase;

    public GetPatientByIdQueryHandler(GetPatientByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientDto?> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}

