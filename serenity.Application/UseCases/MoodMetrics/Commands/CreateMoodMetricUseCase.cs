using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MoodMetrics;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.MoodMetrics.Commands;

public class CreateMoodMetricUseCase
{
    private readonly IMoodMetricRepository _moodMetricRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMoodMetricUseCase(
        IMoodMetricRepository moodMetricRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _moodMetricRepository = moodMetricRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MoodMetricDto> ExecuteAsync(CreateMoodMetricRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var now = DateTime.Now;
        var moodMetric = new MoodMetric
        {
            PatientId = request.PatientId,
            Date = request.Date,
            HappyPercentage = request.HappyPercentage,
            CalmPercentage = request.CalmPercentage,
            SadPercentage = request.SadPercentage,
            AnxiousPercentage = request.AnxiousPercentage,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _moodMetricRepository.AddAsync(moodMetric, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return moodMetric.ToDto();
    }
}




