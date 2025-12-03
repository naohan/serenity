using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientReports.Commands;

namespace serenity.Application.Features.PatientReports.Commands;

public record UpdatePatientReportCommand(int Id, UpdatePatientReportRequest Request) : IRequest<PatientReportDto>;

public class UpdatePatientReportCommandHandler : IRequestHandler<UpdatePatientReportCommand, PatientReportDto>
{
    private readonly UpdatePatientReportUseCase _useCase;

    public UpdatePatientReportCommandHandler(UpdatePatientReportUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientReportDto> Handle(UpdatePatientReportCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}




