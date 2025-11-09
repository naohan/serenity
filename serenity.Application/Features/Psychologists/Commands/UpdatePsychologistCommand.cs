using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Psychologists.Commands;

namespace serenity.Application.Features.Psychologists.Commands;

public record UpdatePsychologistCommand(int Id, UpdatePsychologistRequest Request) : IRequest<PsychologistDto>;

public class UpdatePsychologistCommandHandler : IRequestHandler<UpdatePsychologistCommand, PsychologistDto>
{
    private readonly UpdatePsychologistUseCase _useCase;

    public UpdatePsychologistCommandHandler(UpdatePsychologistUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PsychologistDto> Handle(UpdatePsychologistCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}

