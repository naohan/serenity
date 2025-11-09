using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Trainers.Commands;

public class UpdateTrainerUseCase
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTrainerUseCase(ITrainerRepository trainerRepository, IUnitOfWork unitOfWork)
    {
        _trainerRepository = trainerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TrainerDto> ExecuteAsync(int id, UpdateTrainerRequest request, CancellationToken cancellationToken = default)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken)
                      ?? throw new KeyNotFoundException($"No se encontró el entrenador con id {id}.");

        trainer.Bio = request.Bio;
        trainer.Specialization = request.Specialization;
        trainer.UpdatedAt = DateTime.UtcNow;

        await _trainerRepository.UpdateAsync(trainer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return trainer.ToDto();
    }
}

