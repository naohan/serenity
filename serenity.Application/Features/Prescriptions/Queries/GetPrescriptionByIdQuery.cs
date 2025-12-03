using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Prescriptions.Queries;

namespace serenity.Application.Features.Prescriptions.Queries;

public record GetPrescriptionByIdQuery(int Id) : IRequest<PrescriptionDto?>;

public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionDto?>
{
    private readonly GetPrescriptionByIdUseCase _useCase;

    public GetPrescriptionByIdQueryHandler(GetPrescriptionByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PrescriptionDto?> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



