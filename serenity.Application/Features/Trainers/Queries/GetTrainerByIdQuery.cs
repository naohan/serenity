using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Trainers.Queries;

namespace serenity.Application.Features.Trainers.Queries;

public record GetTrainerByIdQuery(int Id) : IRequest<TrainerDto?>;

public class GetTrainerByIdQueryHandler : IRequestHandler<GetTrainerByIdQuery, TrainerDto?>
{
    private readonly GetTrainerByIdUseCase _useCase;

    public GetTrainerByIdQueryHandler(GetTrainerByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<TrainerDto?> Handle(GetTrainerByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}

