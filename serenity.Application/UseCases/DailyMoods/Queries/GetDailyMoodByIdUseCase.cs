using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.DailyMoods;

namespace serenity.Application.UseCases.DailyMoods.Queries;

public class GetDailyMoodByIdUseCase
{
    private readonly IDailyMoodRepository _dailyMoodRepository;

    public GetDailyMoodByIdUseCase(IDailyMoodRepository dailyMoodRepository)
    {
        _dailyMoodRepository = dailyMoodRepository;
    }

    public async Task<DailyMoodDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var dailyMood = await _dailyMoodRepository.GetByIdAsync(id, cancellationToken);
        return dailyMood?.ToDto();
    }
}



