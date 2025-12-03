using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.StressLevelsByTime;

namespace serenity.Application.UseCases.StressLevelsByTime.Commands;

public class UpdateStressLevelsByTimeUseCase
{
    private readonly IStressLevelsByTimeRepository _stressLevelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStressLevelsByTimeUseCase(IStressLevelsByTimeRepository stressLevelRepository, IUnitOfWork unitOfWork)
    {
        _stressLevelRepository = stressLevelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StressLevelsByTimeDto> ExecuteAsync(int id, UpdateStressLevelsByTimeRequest request, CancellationToken cancellationToken = default)
    {
        var stressLevel = await _stressLevelRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró el nivel de estrés con id {id}.");

        if (request.Date.HasValue)
        {
            stressLevel.Date = request.Date.Value;
        }

        if (request.TimeOfDay.HasValue)
        {
            stressLevel.TimeOfDay = request.TimeOfDay.Value;
        }

        if (request.StressLevel.HasValue)
        {
            stressLevel.StressLevel = request.StressLevel.Value;
        }

        await _stressLevelRepository.UpdateAsync(stressLevel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return stressLevel.ToDto();
    }
}



