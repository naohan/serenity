using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientReports;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.PatientReports.Commands;

public class CreatePatientReportUseCase
{
    private readonly IPatientReportRepository _reportRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientReportUseCase(
        IPatientReportRepository reportRepository,
        IPatientRepository patientRepository,
        IPsychologistRepository psychologistRepository,
        IUnitOfWork unitOfWork)
    {
        _reportRepository = reportRepository;
        _patientRepository = patientRepository;
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientReportDto> ExecuteAsync(CreatePatientReportRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var psychologist = await _psychologistRepository.GetByIdAsync(request.PsychologistId, cancellationToken);
        if (psychologist is null)
        {
            throw new KeyNotFoundException($"No se encontró el psicólogo con id {request.PsychologistId}.");
        }

        var now = DateTime.Now;
        var report = new PatientReport
        {
            PatientId = request.PatientId,
            PsychologistId = request.PsychologistId,
            Title = request.Title,
            Diagnosis = request.Diagnosis,
            AnxietyLevel = request.AnxietyLevel,
            Recommendations = request.Recommendations,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _reportRepository.AddAsync(report, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return report.ToDto();
    }
}



