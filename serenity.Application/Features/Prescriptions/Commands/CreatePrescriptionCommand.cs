using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Prescriptions.Commands;

namespace serenity.Application.Features.Prescriptions.Commands;

public record CreatePrescriptionCommand(CreatePrescriptionRequest Request) : IRequest<PrescriptionDto>;

public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, PrescriptionDto>
{
    private readonly CreatePrescriptionUseCase _useCase;

    public CreatePrescriptionCommandHandler(CreatePrescriptionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PrescriptionDto> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}




