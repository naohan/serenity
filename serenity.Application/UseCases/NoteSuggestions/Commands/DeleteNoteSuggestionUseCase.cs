using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.NoteSuggestions.Commands;

public class DeleteNoteSuggestionUseCase
{
    private readonly INoteSuggestionRepository _suggestionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNoteSuggestionUseCase(INoteSuggestionRepository suggestionRepository, IUnitOfWork unitOfWork)
    {
        _suggestionRepository = suggestionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var suggestion = await _suggestionRepository.GetByIdAsync(id, cancellationToken)
                        ?? throw new KeyNotFoundException($"No se encontró la sugerencia con id {id}.");

        await _suggestionRepository.DeleteAsync(suggestion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}




