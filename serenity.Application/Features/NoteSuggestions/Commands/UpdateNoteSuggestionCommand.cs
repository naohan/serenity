using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.NoteSuggestions.Commands;

namespace serenity.Application.Features.NoteSuggestions.Commands;

public record UpdateNoteSuggestionCommand(int Id, UpdateNoteSuggestionRequest Request) : IRequest<NoteSuggestionDto>;

public class UpdateNoteSuggestionCommandHandler : IRequestHandler<UpdateNoteSuggestionCommand, NoteSuggestionDto>
{
    private readonly UpdateNoteSuggestionUseCase _useCase;

    public UpdateNoteSuggestionCommandHandler(UpdateNoteSuggestionUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<NoteSuggestionDto> Handle(UpdateNoteSuggestionCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}




