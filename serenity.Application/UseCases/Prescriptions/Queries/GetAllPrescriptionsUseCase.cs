using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Prescriptions;

namespace serenity.Application.UseCases.Prescriptions.Queries;

public class GetAllPrescriptionsUseCase
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetAllPrescriptionsUseCase(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<IEnumerable<PrescriptionDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var prescriptions = await _prescriptionRepository.GetAllAsync(cancellationToken);
        return prescriptions.Select(p => p.ToDto());
    }
}




