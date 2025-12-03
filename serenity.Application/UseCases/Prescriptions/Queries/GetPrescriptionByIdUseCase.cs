using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Prescriptions;

namespace serenity.Application.UseCases.Prescriptions.Queries;

public class GetPrescriptionByIdUseCase
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetPrescriptionByIdUseCase(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<PrescriptionDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(id, cancellationToken);
        return prescription?.ToDto();
    }
}



