using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Prescriptions.Queries;

namespace serenity.Application.Features.Prescriptions.Queries;

public record GetAllPrescriptionsQuery : IRequest<IEnumerable<PrescriptionDto>>;

public class GetAllPrescriptionsQueryHandler : IRequestHandler<GetAllPrescriptionsQuery, IEnumerable<PrescriptionDto>>
{
    private readonly GetAllPrescriptionsUseCase _useCase;

    public GetAllPrescriptionsQueryHandler(GetAllPrescriptionsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<PrescriptionDto>> Handle(GetAllPrescriptionsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




