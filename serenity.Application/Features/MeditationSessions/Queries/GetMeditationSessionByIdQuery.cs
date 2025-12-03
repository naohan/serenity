using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MeditationSessions.Queries;

namespace serenity.Application.Features.MeditationSessions.Queries;

public record GetMeditationSessionByIdQuery(int Id) : IRequest<MeditationSessionDto?>;

public class GetMeditationSessionByIdQueryHandler : IRequestHandler<GetMeditationSessionByIdQuery, MeditationSessionDto?>
{
    private readonly GetMeditationSessionByIdUseCase _useCase;

    public GetMeditationSessionByIdQueryHandler(GetMeditationSessionByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MeditationSessionDto?> Handle(GetMeditationSessionByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




