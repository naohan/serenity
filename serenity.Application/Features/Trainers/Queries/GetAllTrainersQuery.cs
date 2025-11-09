using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Trainers.Queries;

namespace serenity.Application.Features.Trainers.Queries;

public record GetAllTrainersQuery() : IRequest<IEnumerable<TrainerDto>>;

public class GetAllTrainersQueryHandler : IRequestHandler<GetAllTrainersQuery, IEnumerable<TrainerDto>>
{
    private readonly GetAllTrainersUseCase _useCase;

    public GetAllTrainersQueryHandler(GetAllTrainersUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<TrainerDto>> Handle(GetAllTrainersQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}

