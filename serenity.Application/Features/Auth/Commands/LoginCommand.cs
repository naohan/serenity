using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Auth;

namespace serenity.Application.Features.Auth.Commands;

public record LoginCommand(LoginRequest Request) : IRequest<AuthResponse>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly LoginUseCase _loginUseCase;

    public LoginCommandHandler(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    public Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return _loginUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}

