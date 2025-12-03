using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.DailyMoods;

namespace serenity.Application.UseCases.DailyMoods.Queries;

public class GetAllDailyMoodsUseCase
{
    private readonly IDailyMoodRepository _dailyMoodRepository;

    public GetAllDailyMoodsUseCase(IDailyMoodRepository dailyMoodRepository)
    {
        _dailyMoodRepository = dailyMoodRepository;
    }

    public async Task<IEnumerable<DailyMoodDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var dailyMoods = await _dailyMoodRepository.GetAllAsync(cancellationToken);
        return dailyMoods.Select(d => d.ToDto());
    }
}



