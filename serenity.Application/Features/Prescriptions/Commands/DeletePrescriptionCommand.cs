using MediatR;
using serenity.Application.UseCases.Prescriptions.Commands;

namespace serenity.Application.Features.Prescriptions.Commands;

public record DeletePrescriptionCommand(int Id) : IRequest;

public class DeletePrescriptionCommandHandler : IRequestHandler<DeletePrescriptionCommand>
{
    private readonly DeletePrescriptionUseCase _useCase;

    public DeletePrescriptionCommandHandler(DeletePrescriptionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



