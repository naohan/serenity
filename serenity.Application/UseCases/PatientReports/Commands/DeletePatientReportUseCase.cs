using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.PatientReports.Commands;

public class DeletePatientReportUseCase
{
    private readonly IPatientReportRepository _reportRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientReportUseCase(IPatientReportRepository reportRepository, IUnitOfWork unitOfWork)
    {
        _reportRepository = reportRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var report = await _reportRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException($"No se encontró el reporte con id {id}.");

        await _reportRepository.DeleteAsync(report);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}




