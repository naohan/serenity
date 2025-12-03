using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.DailyMoods;

namespace serenity.Application.UseCases.DailyMoods.Commands;

public class UpdateDailyMoodUseCase
{
    private readonly IDailyMoodRepository _dailyMoodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDailyMoodUseCase(IDailyMoodRepository dailyMoodRepository, IUnitOfWork unitOfWork)
    {
        _dailyMoodRepository = dailyMoodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DailyMoodDto> ExecuteAsync(int id, UpdateDailyMoodRequest request, CancellationToken cancellationToken = default)
    {
        var dailyMood = await _dailyMoodRepository.GetByIdAsync(id, cancellationToken)
                       ?? throw new KeyNotFoundException($"No se encontró el estado de ánimo con id {id}.");

        if (request.Date.HasValue)
        {
            dailyMood.Date = request.Date.Value;
        }

        if (request.Mood.HasValue)
        {
            dailyMood.Mood = request.Mood.Value;
        }

        if (request.Note is not null)
        {
            dailyMood.Note = request.Note;
        }

        await _dailyMoodRepository.UpdateAsync(dailyMood);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return dailyMood.ToDto();
    }
}




