using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Auth;

namespace serenity.Application.Features.Auth.Commands;

public record RegisterUserCommand(RegisterRequest Request) : IRequest<UserDto>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly RegisterUserUseCase _registerUserUseCase;

    public RegisterUserCommandHandler(RegisterUserUseCase registerUserUseCase)
    {
        _registerUserUseCase = registerUserUseCase;
    }

    public Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return _registerUserUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}

