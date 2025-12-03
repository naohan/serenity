using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .ThenByDescending(a => a.AppointmentTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(a => a.PsychologistId == psychologistId)
            .OrderByDescending(a => a.AppointmentDate)
            .ThenByDescending(a => a.AppointmentTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
            .OrderBy(a => a.AppointmentDate)
            .ThenBy(a => a.AppointmentTime)
            .ToListAsync(cancellationToken);
    }
}

