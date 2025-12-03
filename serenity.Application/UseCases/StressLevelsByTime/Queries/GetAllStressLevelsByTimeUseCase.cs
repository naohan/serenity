using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.StressLevelsByTime;

namespace serenity.Application.UseCases.StressLevelsByTime.Queries;

public class GetAllStressLevelsByTimeUseCase
{
    private readonly IStressLevelsByTimeRepository _stressLevelRepository;

    public GetAllStressLevelsByTimeUseCase(IStressLevelsByTimeRepository stressLevelRepository)
    {
        _stressLevelRepository = stressLevelRepository;
    }

    public async Task<IEnumerable<StressLevelsByTimeDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var stressLevels = await _stressLevelRepository.GetAllAsync(cancellationToken);
        return stressLevels.Select(s => s.ToDto());
    }
}




