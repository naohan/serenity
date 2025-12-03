using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.StressLevelsByTime.Commands;

public class DeleteStressLevelsByTimeUseCase
{
    private readonly IStressLevelsByTimeRepository _stressLevelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStressLevelsByTimeUseCase(IStressLevelsByTimeRepository stressLevelRepository, IUnitOfWork unitOfWork)
    {
        _stressLevelRepository = stressLevelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var stressLevel = await _stressLevelRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró el nivel de estrés con id {id}.");

        await _stressLevelRepository.DeleteAsync(stressLevel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}



