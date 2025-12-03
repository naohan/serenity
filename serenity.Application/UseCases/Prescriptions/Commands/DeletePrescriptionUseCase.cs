using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Prescriptions.Commands;

public class DeletePrescriptionUseCase
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePrescriptionUseCase(IPrescriptionRepository prescriptionRepository, IUnitOfWork unitOfWork)
    {
        _prescriptionRepository = prescriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró la prescripción con id {id}.");

        await _prescriptionRepository.DeleteAsync(prescription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}




