using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.StressLevelsByTime.Queries;

namespace serenity.Application.Features.StressLevelsByTime.Queries;

public record GetStressLevelsByTimeByIdQuery(int Id) : IRequest<StressLevelsByTimeDto?>;

public class GetStressLevelsByTimeByIdQueryHandler : IRequestHandler<GetStressLevelsByTimeByIdQuery, StressLevelsByTimeDto?>
{
    private readonly GetStressLevelsByTimeByIdUseCase _useCase;

    public GetStressLevelsByTimeByIdQueryHandler(GetStressLevelsByTimeByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<StressLevelsByTimeDto?> Handle(GetStressLevelsByTimeByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



