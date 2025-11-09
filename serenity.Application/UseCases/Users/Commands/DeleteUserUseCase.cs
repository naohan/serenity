using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Users.Commands;

public class DeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken)
                   ?? throw new KeyNotFoundException($"No se encontró el usuario con id {id}.");

        await _userRepository.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

