using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientReports.Commands;

namespace serenity.Application.Features.PatientReports.Commands;

public record CreatePatientReportCommand(CreatePatientReportRequest Request) : IRequest<PatientReportDto>;

public class CreatePatientReportCommandHandler : IRequestHandler<CreatePatientReportCommand, PatientReportDto>
{
    private readonly CreatePatientReportUseCase _useCase;

    public CreatePatientReportCommandHandler(CreatePatientReportUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientReportDto> Handle(CreatePatientReportCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}



