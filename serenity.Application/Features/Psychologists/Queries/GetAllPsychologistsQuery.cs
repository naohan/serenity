using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Psychologists.Queries;

namespace serenity.Application.Features.Psychologists.Queries;

public record GetAllPsychologistsQuery() : IRequest<IEnumerable<PsychologistDto>>;

public class GetAllPsychologistsQueryHandler : IRequestHandler<GetAllPsychologistsQuery, IEnumerable<PsychologistDto>>
{
    private readonly GetAllPsychologistsUseCase _useCase;

    public GetAllPsychologistsQueryHandler(GetAllPsychologistsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<PsychologistDto>> Handle(GetAllPsychologistsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}

