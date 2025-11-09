using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Psychologists.Queries;

namespace serenity.Application.Features.Psychologists.Queries;

public record GetPsychologistByIdQuery(int Id) : IRequest<PsychologistDto?>;

public class GetPsychologistByIdQueryHandler : IRequestHandler<GetPsychologistByIdQuery, PsychologistDto?>
{
    private readonly GetPsychologistByIdUseCase _useCase;

    public GetPsychologistByIdQueryHandler(GetPsychologistByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PsychologistDto?> Handle(GetPsychologistByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}

