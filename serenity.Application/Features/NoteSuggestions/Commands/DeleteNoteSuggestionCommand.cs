using MediatR;
using serenity.Application.UseCases.NoteSuggestions.Commands;

namespace serenity.Application.Features.NoteSuggestions.Commands;

public record DeleteNoteSuggestionCommand(int Id) : IRequest;

public class DeleteNoteSuggestionCommandHandler : IRequestHandler<DeleteNoteSuggestionCommand>
{
    private readonly DeleteNoteSuggestionUseCase _useCase;

    public DeleteNoteSuggestionCommandHandler(DeleteNoteSuggestionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteNoteSuggestionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



