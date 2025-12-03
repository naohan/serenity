using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientReports.Queries;

namespace serenity.Application.Features.PatientReports.Queries;

public record GetPatientReportByIdQuery(int Id) : IRequest<PatientReportDto?>;

public class GetPatientReportByIdQueryHandler : IRequestHandler<GetPatientReportByIdQuery, PatientReportDto?>
{
    private readonly GetPatientReportByIdUseCase _useCase;

    public GetPatientReportByIdQueryHandler(GetPatientReportByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientReportDto?> Handle(GetPatientReportByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



