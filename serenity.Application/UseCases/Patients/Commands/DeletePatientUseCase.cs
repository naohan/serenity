using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Patients.Commands;

public class DeletePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientUseCase(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken)
                      ?? throw new KeyNotFoundException($"No se encontró el paciente con id {id}.");

        await _patientRepository.DeleteAsync(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

