using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Trainers.Queries;

public class GetAllTrainersUseCase
{
    private readonly ITrainerRepository _trainerRepository;

    public GetAllTrainersUseCase(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public async Task<IEnumerable<TrainerDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var trainers = await _trainerRepository.GetAllAsync(cancellationToken);
        return trainers.Select(t => t.ToDto());
    }
}

