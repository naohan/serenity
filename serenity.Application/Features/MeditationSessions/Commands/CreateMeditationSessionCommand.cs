using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MeditationSessions.Commands;

namespace serenity.Application.Features.MeditationSessions.Commands;

public record CreateMeditationSessionCommand(CreateMeditationSessionRequest Request) : IRequest<MeditationSessionDto>;

public class CreateMeditationSessionCommandHandler : IRequestHandler<CreateMeditationSessionCommand, MeditationSessionDto>
{
    private readonly CreateMeditationSessionUseCase _useCase;

    public CreateMeditationSessionCommandHandler(CreateMeditationSessionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MeditationSessionDto> Handle(CreateMeditationSessionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}




