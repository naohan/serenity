using MediatR;
using serenity.Application.UseCases.MeditationSessions.Commands;

namespace serenity.Application.Features.MeditationSessions.Commands;

public record DeleteMeditationSessionCommand(int Id) : IRequest;

public class DeleteMeditationSessionCommandHandler : IRequestHandler<DeleteMeditationSessionCommand>
{
    private readonly DeleteMeditationSessionUseCase _useCase;

    public DeleteMeditationSessionCommandHandler(DeleteMeditationSessionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteMeditationSessionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




