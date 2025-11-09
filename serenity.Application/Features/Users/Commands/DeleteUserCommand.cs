using MediatR;
using serenity.Application.UseCases.Users.Commands;

namespace serenity.Application.Features.Users.Commands;

public record DeleteUserCommand(int Id) : IRequest<Unit>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly DeleteUserUseCase _useCase;

    public DeleteUserCommandHandler(DeleteUserUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _useCase.ExecuteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}

