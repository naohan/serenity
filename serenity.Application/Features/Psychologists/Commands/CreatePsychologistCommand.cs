using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Psychologists.Commands;

namespace serenity.Application.Features.Psychologists.Commands;

public record CreatePsychologistCommand(CreatePsychologistRequest Request) : IRequest<PsychologistDto>;

public class CreatePsychologistCommandHandler : IRequestHandler<CreatePsychologistCommand, PsychologistDto>
{
    private readonly CreatePsychologistUseCase _useCase;

    public CreatePsychologistCommandHandler(CreatePsychologistUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PsychologistDto> Handle(CreatePsychologistCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}

