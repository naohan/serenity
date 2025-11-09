using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Patients.Commands;

namespace serenity.Application.Features.Patients.Commands;

public record CreatePatientCommand(CreatePatientRequest Request) : IRequest<PatientDto>;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
{
    private readonly CreatePatientUseCase _useCase;

    public CreatePatientCommandHandler(CreatePatientUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}

