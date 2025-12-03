using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientReports;

namespace serenity.Application.UseCases.PatientReports.Queries;

public class GetAllPatientReportsUseCase
{
    private readonly IPatientReportRepository _reportRepository;

    public GetAllPatientReportsUseCase(IPatientReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<IEnumerable<PatientReportDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var reports = await _reportRepository.GetAllAsync(cancellationToken);
        return reports.Select(r => r.ToDto());
    }
}




