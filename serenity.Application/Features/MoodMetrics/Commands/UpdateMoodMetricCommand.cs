using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MoodMetrics.Commands;

namespace serenity.Application.Features.MoodMetrics.Commands;

public record UpdateMoodMetricCommand(int Id, UpdateMoodMetricRequest Request) : IRequest<MoodMetricDto>;

public class UpdateMoodMetricCommandHandler : IRequestHandler<UpdateMoodMetricCommand, MoodMetricDto>
{
    private readonly UpdateMoodMetricUseCase _useCase;

    public UpdateMoodMetricCommandHandler(UpdateMoodMetricUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MoodMetricDto> Handle(UpdateMoodMetricCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}



