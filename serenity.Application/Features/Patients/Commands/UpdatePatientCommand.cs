using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Patients.Commands;

namespace serenity.Application.Features.Patients.Commands;

public record UpdatePatientCommand(int Id, UpdatePatientRequest Request) : IRequest<PatientDto>;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, PatientDto>
{
    private readonly UpdatePatientUseCase _useCase;

    public UpdatePatientCommandHandler(UpdatePatientUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientDto> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}

