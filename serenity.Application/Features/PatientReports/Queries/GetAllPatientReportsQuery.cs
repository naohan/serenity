using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientReports.Queries;

namespace serenity.Application.Features.PatientReports.Queries;

public record GetAllPatientReportsQuery : IRequest<IEnumerable<PatientReportDto>>;

public class GetAllPatientReportsQueryHandler : IRequestHandler<GetAllPatientReportsQuery, IEnumerable<PatientReportDto>>
{
    private readonly GetAllPatientReportsUseCase _useCase;

    public GetAllPatientReportsQueryHandler(GetAllPatientReportsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<PatientReportDto>> Handle(GetAllPatientReportsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




