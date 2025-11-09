using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Trainers.Queries;

public class GetTrainerByIdUseCase
{
    private readonly ITrainerRepository _trainerRepository;

    public GetTrainerByIdUseCase(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public async Task<TrainerDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken);
        return trainer?.ToDto();
    }
}

