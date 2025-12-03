using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MeditationSessions.Commands;

namespace serenity.Application.Features.MeditationSessions.Commands;

public record UpdateMeditationSessionCommand(int Id, UpdateMeditationSessionRequest Request) : IRequest<MeditationSessionDto>;

public class UpdateMeditationSessionCommandHandler : IRequestHandler<UpdateMeditationSessionCommand, MeditationSessionDto>
{
    private readonly UpdateMeditationSessionUseCase _useCase;

    public UpdateMeditationSessionCommandHandler(UpdateMeditationSessionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MeditationSessionDto> Handle(UpdateMeditationSessionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}




