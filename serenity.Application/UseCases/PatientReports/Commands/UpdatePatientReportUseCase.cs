using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientReports;

namespace serenity.Application.UseCases.PatientReports.Commands;

public class UpdatePatientReportUseCase
{
    private readonly IPatientReportRepository _reportRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientReportUseCase(IPatientReportRepository reportRepository, IUnitOfWork unitOfWork)
    {
        _reportRepository = reportRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientReportDto> ExecuteAsync(int id, UpdatePatientReportRequest request, CancellationToken cancellationToken = default)
    {
        var report = await _reportRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException($"No se encontró el reporte con id {id}.");

        if (request.Title is not null)
        {
            report.Title = request.Title;
        }

        if (request.Diagnosis is not null)
        {
            report.Diagnosis = request.Diagnosis;
        }

        if (request.AnxietyLevel is not null)
        {
            report.AnxietyLevel = request.AnxietyLevel;
        }

        if (request.Recommendations is not null)
        {
            report.Recommendations = request.Recommendations;
        }

        report.UpdatedAt = DateTime.Now;

        await _reportRepository.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return report.ToDto();
    }
}



