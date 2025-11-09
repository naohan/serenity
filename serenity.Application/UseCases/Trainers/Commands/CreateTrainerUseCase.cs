using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Trainers.Commands;

public class CreateTrainerUseCase
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTrainerUseCase(ITrainerRepository trainerRepository, IUnitOfWork unitOfWork)
    {
        _trainerRepository = trainerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TrainerDto> ExecuteAsync(CreateTrainerRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UserId <= 0)
        {
            throw new ArgumentException("UserId debe ser válido.", nameof(request.UserId));
        }

        var existing = await _trainerRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (existing is not null)
        {
            throw new InvalidOperationException("El usuario ya cuenta con un registro de entrenador.");
        }

        var trainer = new Trainer
        {
            UserId = request.UserId,
            Bio = request.Bio,
            Specialization = request.Specialization,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _trainerRepository.AddAsync(trainer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return trainer.ToDto();
    }
}

