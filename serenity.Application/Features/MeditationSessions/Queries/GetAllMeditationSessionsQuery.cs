using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MeditationSessions.Queries;

namespace serenity.Application.Features.MeditationSessions.Queries;

public record GetAllMeditationSessionsQuery : IRequest<IEnumerable<MeditationSessionDto>>;

public class GetAllMeditationSessionsQueryHandler : IRequestHandler<GetAllMeditationSessionsQuery, IEnumerable<MeditationSessionDto>>
{
    private readonly GetAllMeditationSessionsUseCase _useCase;

    public GetAllMeditationSessionsQueryHandler(GetAllMeditationSessionsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<MeditationSessionDto>> Handle(GetAllMeditationSessionsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




