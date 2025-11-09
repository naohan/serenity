using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Trainers.Commands;

public class DeleteTrainerUseCase
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTrainerUseCase(ITrainerRepository trainerRepository, IUnitOfWork unitOfWork)
    {
        _trainerRepository = trainerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken)
                      ?? throw new KeyNotFoundException($"No se encontró el entrenador con id {id}.");

        await _trainerRepository.DeleteAsync(trainer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

