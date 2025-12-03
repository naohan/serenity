using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.DailyMoods.Queries;

namespace serenity.Application.Features.DailyMoods.Queries;

public record GetAllDailyMoodsQuery : IRequest<IEnumerable<DailyMoodDto>>;

public class GetAllDailyMoodsQueryHandler : IRequestHandler<GetAllDailyMoodsQuery, IEnumerable<DailyMoodDto>>
{
    private readonly GetAllDailyMoodsUseCase _useCase;

    public GetAllDailyMoodsQueryHandler(GetAllDailyMoodsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<DailyMoodDto>> Handle(GetAllDailyMoodsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




