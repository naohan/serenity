using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Users;

namespace serenity.Application.UseCases.Users.Queries;

public class GetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return users.Select(user => user.ToDto());
    }
}

