using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Users.Queries;

namespace serenity.Application.Features.Users.Queries;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly GetAllUsersUseCase _useCase;

    public GetAllUsersQueryHandler(GetAllUsersUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}

