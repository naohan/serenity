using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Psychologists.Commands;

public class CreatePsychologistUseCase
{
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePsychologistUseCase(IPsychologistRepository psychologistRepository, IUnitOfWork unitOfWork)
    {
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PsychologistDto> ExecuteAsync(CreatePsychologistRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UserId <= 0)
        {
            throw new ArgumentException("UserId debe ser válido.", nameof(request.UserId));
        }

        var existingByUser = await _psychologistRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (existingByUser is not null)
        {
            throw new InvalidOperationException("El usuario ya cuenta con un registro de psicólogo.");
        }

        if (!string.IsNullOrWhiteSpace(request.CollegeNumber))
        {
            var existingByCollege = await _psychologistRepository.GetByCollegeNumberAsync(request.CollegeNumber, cancellationToken);
            if (existingByCollege is not null)
            {
                throw new InvalidOperationException("El número de colegiatura ya está registrado.");
            }
        }

        var psychologist = new Psychologist
        {
            UserId = request.UserId,
            CollegeNumber = request.CollegeNumber,
            Country = request.Country,
            Location = request.Location,
            Specialization = request.Specialization,
            WeeklyScore = request.WeeklyScore,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _psychologistRepository.AddAsync(psychologist, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return psychologist.ToDto();
    }
}

