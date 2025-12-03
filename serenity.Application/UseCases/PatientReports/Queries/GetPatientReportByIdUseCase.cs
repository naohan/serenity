using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientReports;

namespace serenity.Application.UseCases.PatientReports.Queries;

public class GetPatientReportByIdUseCase
{
    private readonly IPatientReportRepository _reportRepository;

    public GetPatientReportByIdUseCase(IPatientReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<PatientReportDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var report = await _reportRepository.GetByIdAsync(id, cancellationToken);
        return report?.ToDto();
    }
}



