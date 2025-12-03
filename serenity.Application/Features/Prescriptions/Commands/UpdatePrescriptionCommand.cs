using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Prescriptions.Commands;

namespace serenity.Application.Features.Prescriptions.Commands;

public record UpdatePrescriptionCommand(int Id, UpdatePrescriptionRequest Request) : IRequest<PrescriptionDto>;

public class UpdatePrescriptionCommandHandler : IRequestHandler<UpdatePrescriptionCommand, PrescriptionDto>
{
    private readonly UpdatePrescriptionUseCase _useCase;

    public UpdatePrescriptionCommandHandler(UpdatePrescriptionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PrescriptionDto> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}



