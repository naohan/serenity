using MediatR;
using serenity.Application.DTOs;
using serenity.Application.DTOs.Auth;
using serenity.Application.UseCases.Auth;

namespace serenity.Application.Features.Auth.Commands;

public record GoogleLoginCommand(GoogleLoginDto Request) : IRequest<AuthResponse>;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, AuthResponse>
{
    private readonly GoogleLoginUseCase _googleLoginUseCase;

    public GoogleLoginCommandHandler(GoogleLoginUseCase googleLoginUseCase)
    {
        _googleLoginUseCase = googleLoginUseCase;
    }

    public Task<AuthResponse> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        return _googleLoginUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
