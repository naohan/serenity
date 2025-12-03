using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.AI;

namespace serenity.Application.Features.AI.Commands;

public record AnalyzeNotesCommand(int NoteId) : IRequest<DiagnosisResponseDto>;

public class AnalyzeNotesCommandHandler : IRequestHandler<AnalyzeNotesCommand, DiagnosisResponseDto>
{
    private readonly GenerateAIDiagnosisFromNotesUseCase _useCase;

    public AnalyzeNotesCommandHandler(GenerateAIDiagnosisFromNotesUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<DiagnosisResponseDto> Handle(AnalyzeNotesCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.NoteId, cancellationToken);
    }
}


