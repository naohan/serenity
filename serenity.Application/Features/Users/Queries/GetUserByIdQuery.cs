using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Users.Queries;

namespace serenity.Application.Features.Users.Queries;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly GetUserByIdUseCase _useCase;

    public GetUserByIdQueryHandler(GetUserByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}

