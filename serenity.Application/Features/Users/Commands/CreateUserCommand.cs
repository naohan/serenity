using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Users.Commands;

namespace serenity.Application.Features.Users.Commands;

public record CreateUserCommand(CreateUserRequest Request) : IRequest<UserDto>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly CreateUserUseCase _useCase;

    public CreateUserCommandHandler(CreateUserUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}

