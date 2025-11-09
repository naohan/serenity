using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Psychologists.Commands;

public class UpdatePsychologistUseCase
{
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePsychologistUseCase(IPsychologistRepository psychologistRepository, IUnitOfWork unitOfWork)
    {
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PsychologistDto> ExecuteAsync(int id, UpdatePsychologistRequest request, CancellationToken cancellationToken = default)
    {
        var psychologist = await _psychologistRepository.GetByIdAsync(id, cancellationToken)
                           ?? throw new KeyNotFoundException($"No se encontró el psicólogo con id {id}.");

        psychologist.Country = request.Country;
        psychologist.Location = request.Location;
        psychologist.Specialization = request.Specialization;
        psychologist.WeeklyScore = request.WeeklyScore;
        psychologist.UpdatedAt = DateTime.UtcNow;

        await _psychologistRepository.UpdateAsync(psychologist);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return psychologist.ToDto();
    }
}

