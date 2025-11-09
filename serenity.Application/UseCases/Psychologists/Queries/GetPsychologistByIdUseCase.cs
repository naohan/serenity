using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Psychologists.Queries;

public class GetPsychologistByIdUseCase
{
    private readonly IPsychologistRepository _psychologistRepository;

    public GetPsychologistByIdUseCase(IPsychologistRepository psychologistRepository)
    {
        _psychologistRepository = psychologistRepository;
    }

    public async Task<PsychologistDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var psychologist = await _psychologistRepository.GetByIdAsync(id, cancellationToken);
        return psychologist?.ToDto();
    }
}

