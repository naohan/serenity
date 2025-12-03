using MediatR;
using serenity.Application.UseCases.PatientReports.Commands;

namespace serenity.Application.Features.PatientReports.Commands;

public record DeletePatientReportCommand(int Id) : IRequest;

public class DeletePatientReportCommandHandler : IRequestHandler<DeletePatientReportCommand>
{
    private readonly DeletePatientReportUseCase _useCase;

    public DeletePatientReportCommandHandler(DeletePatientReportUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeletePatientReportCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




