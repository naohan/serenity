using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.NoteSuggestions.Queries;

namespace serenity.Application.Features.NoteSuggestions.Queries;

public record GetNoteSuggestionByIdQuery(int Id) : IRequest<NoteSuggestionDto?>;

public class GetNoteSuggestionByIdQueryHandler : IRequestHandler<GetNoteSuggestionByIdQuery, NoteSuggestionDto?>
{
    private readonly GetNoteSuggestionByIdUseCase _useCase;

    public GetNoteSuggestionByIdQueryHandler(GetNoteSuggestionByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<NoteSuggestionDto?> Handle(GetNoteSuggestionByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



