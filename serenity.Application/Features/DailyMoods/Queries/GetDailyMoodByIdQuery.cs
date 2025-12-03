using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.DailyMoods.Queries;

namespace serenity.Application.Features.DailyMoods.Queries;

public record GetDailyMoodByIdQuery(int Id) : IRequest<DailyMoodDto?>;

public class GetDailyMoodByIdQueryHandler : IRequestHandler<GetDailyMoodByIdQuery, DailyMoodDto?>
{
    private readonly GetDailyMoodByIdUseCase _useCase;

    public GetDailyMoodByIdQueryHandler(GetDailyMoodByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<DailyMoodDto?> Handle(GetDailyMoodByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



