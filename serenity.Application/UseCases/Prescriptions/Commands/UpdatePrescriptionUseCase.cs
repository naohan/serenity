using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Prescriptions;

namespace serenity.Application.UseCases.Prescriptions.Commands;

public class UpdatePrescriptionUseCase
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePrescriptionUseCase(IPrescriptionRepository prescriptionRepository, IUnitOfWork unitOfWork)
    {
        _prescriptionRepository = prescriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PrescriptionDto> ExecuteAsync(int id, UpdatePrescriptionRequest request, CancellationToken cancellationToken = default)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró la prescripción con id {id}.");

        if (request.MedicationName is not null)
        {
            prescription.MedicationName = request.MedicationName;
        }

        if (request.Dosage is not null)
        {
            prescription.Dosage = request.Dosage;
        }

        if (request.Frequency is not null)
        {
            prescription.Frequency = request.Frequency;
        }

        if (request.StartDate.HasValue)
        {
            prescription.StartDate = request.StartDate.Value;
        }

        if (request.EndDate.HasValue)
        {
            prescription.EndDate = request.EndDate;
        }

        if (request.Instructions is not null)
        {
            prescription.Instructions = request.Instructions;
        }

        if (request.Status is not null)
        {
            prescription.Status = request.Status;
        }

        prescription.UpdatedAt = DateTime.Now;

        await _prescriptionRepository.UpdateAsync(prescription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return prescription.ToDto();
    }
}



