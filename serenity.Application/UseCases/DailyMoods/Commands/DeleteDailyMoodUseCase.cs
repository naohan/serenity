using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.DailyMoods.Commands;

public class DeleteDailyMoodUseCase
{
    private readonly IDailyMoodRepository _dailyMoodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDailyMoodUseCase(IDailyMoodRepository dailyMoodRepository, IUnitOfWork unitOfWork)
    {
        _dailyMoodRepository = dailyMoodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var dailyMood = await _dailyMoodRepository.GetByIdAsync(id, cancellationToken)
                       ?? throw new KeyNotFoundException($"No se encontró el estado de ánimo con id {id}.");

        await _dailyMoodRepository.DeleteAsync(dailyMood);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}



