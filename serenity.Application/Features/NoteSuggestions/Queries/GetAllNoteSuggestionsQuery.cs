using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.NoteSuggestions.Queries;

namespace serenity.Application.Features.NoteSuggestions.Queries;

public record GetAllNoteSuggestionsQuery : IRequest<IEnumerable<NoteSuggestionDto>>;

public class GetAllNoteSuggestionsQueryHandler : IRequestHandler<GetAllNoteSuggestionsQuery, IEnumerable<NoteSuggestionDto>>
{
    private readonly GetAllNoteSuggestionsUseCase _useCase;

    public GetAllNoteSuggestionsQueryHandler(GetAllNoteSuggestionsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<NoteSuggestionDto>> Handle(GetAllNoteSuggestionsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}



