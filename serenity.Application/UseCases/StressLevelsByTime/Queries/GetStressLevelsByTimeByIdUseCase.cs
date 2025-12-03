using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.StressLevelsByTime;

namespace serenity.Application.UseCases.StressLevelsByTime.Queries;

public class GetStressLevelsByTimeByIdUseCase
{
    private readonly IStressLevelsByTimeRepository _stressLevelRepository;

    public GetStressLevelsByTimeByIdUseCase(IStressLevelsByTimeRepository stressLevelRepository)
    {
        _stressLevelRepository = stressLevelRepository;
    }

    public async Task<StressLevelsByTimeDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var stressLevel = await _stressLevelRepository.GetByIdAsync(id, cancellationToken);
        return stressLevel?.ToDto();
    }
}




