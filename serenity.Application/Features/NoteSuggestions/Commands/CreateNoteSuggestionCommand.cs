using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.NoteSuggestions.Commands;

namespace serenity.Application.Features.NoteSuggestions.Commands;

public record CreateNoteSuggestionCommand(CreateNoteSuggestionRequest Request) : IRequest<NoteSuggestionDto>;

public class CreateNoteSuggestionCommandHandler : IRequestHandler<CreateNoteSuggestionCommand, NoteSuggestionDto>
{
    private readonly CreateNoteSuggestionUseCase _useCase;

    public CreateNoteSuggestionCommandHandler(CreateNoteSuggestionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<NoteSuggestionDto> Handle(CreateNoteSuggestionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}




