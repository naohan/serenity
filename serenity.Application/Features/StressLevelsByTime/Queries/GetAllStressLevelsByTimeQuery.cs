using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.StressLevelsByTime.Queries;

namespace serenity.Application.Features.StressLevelsByTime.Queries;

public record GetAllStressLevelsByTimeQuery : IRequest<IEnumerable<StressLevelsByTimeDto>>;

public class GetAllStressLevelsByTimeQueryHandler : IRequestHandler<GetAllStressLevelsByTimeQuery, IEnumerable<StressLevelsByTimeDto>>
{
    private readonly GetAllStressLevelsByTimeUseCase _useCase;

    public GetAllStressLevelsByTimeQueryHandler(GetAllStressLevelsByTimeUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<StressLevelsByTimeDto>> Handle(GetAllStressLevelsByTimeQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




