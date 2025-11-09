using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Psychologists.Queries;

public class GetAllPsychologistsUseCase
{
    private readonly IPsychologistRepository _psychologistRepository;

    public GetAllPsychologistsUseCase(IPsychologistRepository psychologistRepository)
    {
        _psychologistRepository = psychologistRepository;
    }

    public async Task<IEnumerable<PsychologistDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var psychologists = await _psychologistRepository.GetAllAsync(cancellationToken);
        return psychologists.Select(p => p.ToDto());
    }
}

