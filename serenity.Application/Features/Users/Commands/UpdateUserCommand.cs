using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Users.Commands;

namespace serenity.Application.Features.Users.Commands;

public record UpdateUserCommand(int Id, UpdateUserRequest Request) : IRequest<UserDto>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly UpdateUserUseCase _useCase;

    public UpdateUserCommandHandler(UpdateUserUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}

