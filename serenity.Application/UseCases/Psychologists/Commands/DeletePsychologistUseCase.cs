using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Psychologists.Commands;

public class DeletePsychologistUseCase
{
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePsychologistUseCase(IPsychologistRepository psychologistRepository, IUnitOfWork unitOfWork)
    {
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var psychologist = await _psychologistRepository.GetByIdAsync(id, cancellationToken)
                           ?? throw new KeyNotFoundException($"No se encontró el psicólogo con id {id}.");

        await _psychologistRepository.DeleteAsync(psychologist);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

