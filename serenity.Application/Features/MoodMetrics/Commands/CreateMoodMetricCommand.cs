using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MoodMetrics.Commands;

namespace serenity.Application.Features.MoodMetrics.Commands;

public record CreateMoodMetricCommand(CreateMoodMetricRequest Request) : IRequest<MoodMetricDto>;

public class CreateMoodMetricCommandHandler : IRequestHandler<CreateMoodMetricCommand, MoodMetricDto>
{
    private readonly CreateMoodMetricUseCase _useCase;

    public CreateMoodMetricCommandHandler(CreateMoodMetricUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MoodMetricDto> Handle(CreateMoodMetricCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}



