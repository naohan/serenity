using Microsoft.EntityFrameworkCore;
using serenity.Infrastructure;
using serenity.Infrastructure.Data;

namespace serenity.Infrastructure.Adapters.Repositories;

/// <summary>
/// Colección de consultas LINQ especializadas para Serenity usando EF Core.
/// Todas las consultas están adaptadas a las entidades reales del proyecto.
/// </summary>
public static class SerenityQueries
{
    // ============================================
    // 1. USERS – Autenticación, Google, etc.
    // ============================================

    /// <summary>
    /// 1.1 Obtener usuario por email (login normal)
    /// </summary>
    public static async Task<User?> GetUserByEmailAsync(SerenityDbContext db, string email, CancellationToken cancellationToken = default)
    {
        return await db.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// 1.2 Verificar si ya existe email
    /// </summary>
    public static async Task<bool> EmailExistsAsync(SerenityDbContext db, string email, CancellationToken cancellationToken = default)
    {
        return await db.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// 1.3 Obtener usuario por GoogleId (login Google)
    /// </summary>
    public static async Task<User?> GetUserByGoogleIdAsync(SerenityDbContext db, string googleId, CancellationToken cancellationToken = default)
    {
        return await db.Users
            .Where(u => u.GoogleId == googleId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// 1.4 Listar usuarios activos por rol
    /// </summary>
    public static async Task<List<User>> GetActiveUsersByRoleAsync(SerenityDbContext db, sbyte role, CancellationToken cancellationToken = default)
    {
        return await db.Users
            .Where(u => u.IsActive == true && u.Role == role)
            .OrderBy(u => u.Name)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 2. PSYCHOLOGISTS – Panel, ranking, etc.
    // ============================================

    /// <summary>
    /// 2.1 Listar psicólogos con datos básicos de usuario
    /// </summary>
    public static async Task<List<Psychologist>> GetPsychologistsWithUserAsync(SerenityDbContext db, CancellationToken cancellationToken = default)
    {
        return await db.Psychologists
            .Include(p => p.User)
            .OrderByDescending(p => p.WeeklyScore ?? 0)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 2.2 Top N psicólogos por weekly_score
    /// </summary>
    public static async Task<List<Psychologist>> GetTopPsychologistsAsync(SerenityDbContext db, int top, CancellationToken cancellationToken = default)
    {
        return await db.Psychologists
            .Include(p => p.User)
            .OrderByDescending(p => p.WeeklyScore ?? 0)
            .Take(top)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 3. PATIENTS – Búsqueda, panel, psicólogo responsable
    // ============================================

    /// <summary>
    /// 3.1 Listar pacientes de un psicólogo
    /// </summary>
    public static async Task<List<Patient>> GetPatientsByPsychologistAsync(
        SerenityDbContext db, int psychologistId, CancellationToken cancellationToken = default)
    {
        return await db.Patients
            .Include(p => p.User)
            .Where(p => p.PsychologistId == psychologistId && p.Status == "Activo")
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 3.2 Buscar pacientes por nombre o email
    /// </summary>
    public static async Task<List<Patient>> SearchPatientsAsync(
        SerenityDbContext db, string search, CancellationToken cancellationToken = default)
    {
        var searchLower = search.ToLower();

        return await db.Patients
            .Include(p => p.User)
            .Where(p =>
                p.Name.ToLower().Contains(searchLower) ||
                (p.User != null && p.User.Email.ToLower().Contains(searchLower)))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 3.3 Obtener perfil completo de paciente
    /// </summary>
    public static async Task<Patient?> GetFullPatientProfileAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.Patients
            .Include(p => p.User)
            .Include(p => p.Psychologist)
                .ThenInclude(ps => ps != null ? ps.User : null!)
            .Include(p => p.Appointments)
            .Include(p => p.DailyMoods)
            .Include(p => p.MentalWellbeingMetrics)
            .Include(p => p.MoodMetrics)
            .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);
    }

    // ============================================
    // 4. APPOINTMENTS – Agenda, próximos, por fecha
    // ============================================

    /// <summary>
    /// 4.1 Próximas citas de un paciente
    /// </summary>
    public static async Task<List<Appointment>> GetUpcomingAppointmentsForPatientAsync(
        SerenityDbContext db, int patientId, DateOnly today, CancellationToken cancellationToken = default)
    {
        return await db.Appointments
            .Where(a => a.PatientId == patientId &&
                        a.AppointmentDate >= today &&
                        a.Status != "Cancelada")
            .OrderBy(a => a.AppointmentDate)
            .ThenBy(a => a.AppointmentTime)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 4.2 Próximas citas de un psicólogo en rango de fechas
    /// </summary>
    public static async Task<List<Appointment>> GetPsychologistAppointmentsInRangeAsync(
        SerenityDbContext db, int psychologistId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.Appointments
            .Include(a => a.Patient)
            .Where(a => a.PsychologistId == psychologistId &&
                        a.AppointmentDate >= start &&
                        a.AppointmentDate <= end)
            .OrderBy(a => a.AppointmentDate)
            .ThenBy(a => a.AppointmentTime)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 4.3 Citas por día y estado
    /// </summary>
    public static async Task<List<Appointment>> GetAppointmentsByDateAndStatusAsync(
        SerenityDbContext db, DateOnly date, string status, CancellationToken cancellationToken = default)
    {
        return await db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Psychologist)
            .Where(a => a.AppointmentDate == date && a.Status == status)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 5. CHAT_MESSAGES – Chat paciente–psicólogo
    // ============================================

    /// <summary>
    /// 5.1 Obtener conversación completa (ordenada)
    /// </summary>
    public static async Task<List<ChatMessage>> GetConversationAsync(
        SerenityDbContext db, int patientId, int psychologistId, CancellationToken cancellationToken = default)
    {
        return await db.ChatMessages
            .Where(m => m.PatientId == patientId &&
                        m.PsychologistId == psychologistId)
            .OrderBy(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 5.2 Contar mensajes no leídos (por psicólogo)
    /// </summary>
    public static async Task<int> CountUnreadMessagesForPsychologistAsync(
        SerenityDbContext db, int psychologistId, CancellationToken cancellationToken = default)
    {
        return await db.ChatMessages
            .Where(m => m.PsychologistId == psychologistId &&
                        !m.IsFromPsychologist &&
                        m.ReadAt == null)
            .CountAsync(cancellationToken);
    }

    /// <summary>
    /// 5.3 Contar mensajes no leídos (por paciente)
    /// </summary>
    public static async Task<int> CountUnreadMessagesForPatientAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.ChatMessages
            .Where(m => m.PatientId == patientId &&
                        m.IsFromPsychologist &&
                        m.ReadAt == null)
            .CountAsync(cancellationToken);
    }

    // ============================================
    // 6. DAILY_MOODS – Último mood, series, faltantes
    // ============================================

    /// <summary>
    /// 6.1 Último daily mood de un paciente
    /// </summary>
    public static async Task<DailyMood?> GetLastDailyMoodAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.DailyMoods
            .Where(d => d.PatientId == patientId)
            .OrderByDescending(d => d.Date)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// 6.2 Serie de moods en rango de fechas
    /// </summary>
    public static async Task<List<DailyMood>> GetDailyMoodsInRangeAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.DailyMoods
            .Where(d => d.PatientId == patientId &&
                        d.Date >= start &&
                        d.Date <= end)
            .OrderBy(d => d.Date)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 6.3 Pacientes sin mood en los últimos N días
    /// </summary>
    public static async Task<List<Patient>> GetPatientsWithoutMoodLastDaysAsync(
        SerenityDbContext db, int days, CancellationToken cancellationToken = default)
    {
        var limit = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-days));

        return await db.Patients
            .Where(p => !db.DailyMoods
                .Any(d => d.PatientId == p.Id && d.Date >= limit))
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 7. EMOTIONAL_STATES – Valores y tipos
    // ============================================

    /// <summary>
    /// 7.1 Último estado emocional por tipo
    /// </summary>
    public static async Task<List<EmotionalState>> GetLastEmotionalStatesByTypeAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.EmotionalStates
            .Where(e => e.PatientId == patientId && e.EmotionalState1 != null)
            .GroupBy(e => e.EmotionalState1)
            .Select(g => g.OrderByDescending(x => x.Date).First())
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 7.2 Estados emocionales en rango de fechas
    /// </summary>
    public static async Task<List<EmotionalState>> GetEmotionalStatesInRangeAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.EmotionalStates
            .Where(e => e.PatientId == patientId &&
                        e.Date >= start &&
                        e.Date <= end)
            .OrderBy(e => e.Date)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 8. METRICS – mental_wellbeing + mood_metrics
    // ============================================

    /// <summary>
    /// 8.1 Métricas de bienestar de un paciente por rango
    /// </summary>
    public static async Task<List<MentalWellbeingMetric>> GetWellbeingMetricsAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.MentalWellbeingMetrics
            .Where(m => m.PatientId == patientId &&
                        m.Date >= start &&
                        m.Date <= end)
            .OrderBy(m => m.Date)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 8.2 Métricas de ánimo (mood_metrics) por rango
    /// </summary>
    public static async Task<List<MoodMetric>> GetMoodMetricsAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.MoodMetrics
            .Where(m => m.PatientId == patientId &&
                        m.Date >= start &&
                        m.Date <= end)
            .OrderBy(m => m.Date)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 8.3 Resumen semanal promedio (wellbeing)
    /// </summary>
    public static async Task<List<object>> GetWeeklyWellbeingSummaryAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        var metrics = await db.MentalWellbeingMetrics
            .Where(m => m.PatientId == patientId)
            .ToListAsync(cancellationToken);

        return metrics
            .GroupBy(m => GetWeekStart(m.Date))
            .Select(g => new
            {
                Week = g.Key,
                AvgStress = g.Average(x => (double?)x.StressLevel),
                AvgEnergy = g.Average(x => (double?)x.EnergyLevel),
                AvgConcentration = g.Average(x => (double?)x.ConcentrationLevel),
                AvgSatisfaction = g.Average(x => (double?)x.SatisfactionLevel)
            })
            .OrderBy(r => r.Week)
            .ToList<object>();
    }

    private static DateOnly GetWeekStart(DateOnly date)
    {
        var daysSinceMonday = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        return date.AddDays(-daysSinceMonday);
    }

    // ============================================
    // 9. MEDITATION_SESSIONS – Práctica de meditación
    // ============================================

    /// <summary>
    /// 9.1 Total de minutos de meditación en rango
    /// </summary>
    public static async Task<int> GetMeditationMinutesAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.MeditationSessions
            .Where(s => s.PatientId == patientId &&
                        s.SessionDate >= start &&
                        s.SessionDate <= end)
            .SumAsync(s => (int?)s.DurationMinutes, cancellationToken) ?? 0;
    }

    /// <summary>
    /// 9.2 Sesiones de meditación en rango de fechas
    /// </summary>
    public static async Task<List<MeditationSession>> GetMeditationSessionsInRangeAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.MeditationSessions
            .Where(s => s.PatientId == patientId &&
                        s.SessionDate >= start &&
                        s.SessionDate <= end)
            .OrderBy(s => s.SessionDate)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 10. PATIENT_NOTES + NOTE_SUGGESTIONS + INSIGHTS
    // ============================================

    /// <summary>
    /// 10.1 Notas de paciente con sugerencias
    /// </summary>
    public static async Task<List<PatientNote>> GetPatientNotesWithSuggestionsAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.PatientNotes
            .Include(n => n.NoteSuggestions)
            .Where(n => n.PatientId == patientId)
            .OrderByDescending(n => n.Date)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 10.2 Notas que requieren seguimiento
    /// </summary>
    public static async Task<List<PatientNote>> GetNotesRequiringFollowUpAsync(
        SerenityDbContext db, int psychologistId, CancellationToken cancellationToken = default)
    {
        return await db.PatientNotes
            .Include(n => n.Patient)
            .Where(n => n.PsychologistId == psychologistId &&
                        n.NeedsFollowUp == true)
            .OrderByDescending(n => n.Date)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 10.3 Insights por paciente (panel de recomendaciones)
    /// </summary>
    public static async Task<List<InsightsAndRecommendation>> GetInsightsForPatientAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.InsightsAndRecommendations
            .Where(i => i.PatientId == patientId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 10.4 Insights por psicólogo
    /// </summary>
    public static async Task<List<InsightsAndRecommendation>> GetInsightsByPsychologistAsync(
        SerenityDbContext db, int psychologistId, CancellationToken cancellationToken = default)
    {
        return await db.InsightsAndRecommendations
            .Include(i => i.Patient)
            .Where(i => i.PsychologistId == psychologistId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 11. PATIENT_REPORTS – Informes
    // ============================================

    /// <summary>
    /// 11.1 Último informe de un paciente
    /// </summary>
    public static async Task<PatientReport?> GetLastReportForPatientAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.PatientReports
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// 11.2 Informes de un paciente ordenados por fecha
    /// </summary>
    public static async Task<List<PatientReport>> GetPatientReportsAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.PatientReports
            .Include(r => r.Psychologist)
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 12. PRESCRIPTIONS – Prescripciones activas
    // ============================================

    /// <summary>
    /// 12.1 Prescripciones activas de un paciente
    /// </summary>
    public static async Task<List<Prescription>> GetActivePrescriptionsForPatientAsync(
        SerenityDbContext db, int patientId, DateOnly today, CancellationToken cancellationToken = default)
    {
        return await db.Prescriptions
            .Where(p => p.PatientId == patientId &&
                        p.Status == "Activa" &&
                        p.StartDate <= today &&
                        (p.EndDate == null || p.EndDate >= today))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 12.2 Todas las prescripciones de un paciente
    /// </summary>
    public static async Task<List<Prescription>> GetAllPrescriptionsForPatientAsync(
        SerenityDbContext db, int patientId, CancellationToken cancellationToken = default)
    {
        return await db.Prescriptions
            .Include(p => p.Psychologist)
            .Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.StartDate)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // 13. STRESS_LEVELS_BY_TIME – Análisis de estrés por hora
    // ============================================

    /// <summary>
    /// 13.1 Promedio de estrés por franja horaria
    /// </summary>
    public static async Task<List<object>> GetStressByTimeOfDayAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.StressLevelsByTimes
            .Where(s => s.PatientId == patientId &&
                        s.Date >= start &&
                        s.Date <= end)
            .GroupBy(s => s.TimeOfDay)
            .Select(g => new
            {
                TimeOfDay = g.Key,
                AvgStress = g.Average(x => (double?)x.StressLevel)
            })
            .OrderBy(r => r.TimeOfDay)
            .ToListAsync<object>(cancellationToken);
    }

    /// <summary>
    /// 13.2 Días con estrés alto (por ejemplo ≥ 8)
    /// </summary>
    public static async Task<List<DateOnly>> GetHighStressDaysAsync(
        SerenityDbContext db, int patientId, sbyte threshold = 8, CancellationToken cancellationToken = default)
    {
        return await db.StressLevelsByTimes
            .Where(s => s.PatientId == patientId && s.StressLevel >= threshold)
            .Select(s => s.Date)
            .Distinct()
            .OrderBy(d => d)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 13.3 Niveles de estrés en rango de fechas
    /// </summary>
    public static async Task<List<StressLevelsByTime>> GetStressLevelsInRangeAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        return await db.StressLevelsByTimes
            .Where(s => s.PatientId == patientId &&
                        s.Date >= start &&
                        s.Date <= end)
            .OrderBy(s => s.Date)
            .ThenBy(s => s.TimeOfDay)
            .ToListAsync(cancellationToken);
    }

    // ============================================
    // CONSULTAS ADICIONALES ÚTILES
    // ============================================

    /// <summary>
    /// Obtener todos los pacientes activos con el nombre de su usuario y el dato básico de su psicólogo
    /// </summary>
    public static async Task<List<Patient>> GetActivePatientsWithUserAndPsychologistAsync(
        SerenityDbContext db, CancellationToken cancellationToken = default)
    {
        return await db.Patients
            .Include(p => p.User)
            .Include(p => p.Psychologist)
                .ThenInclude(ps => ps != null ? ps.User : null!)
            .Where(p => p.Status == "Activo")
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtener métricas combinadas de bienestar y ánimo de un paciente en un rango de fechas
    /// </summary>
    public static async Task<object> GetCombinedMetricsForPatientAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        var wellbeing = await GetWellbeingMetricsAsync(db, patientId, start, end, cancellationToken);
        var mood = await GetMoodMetricsAsync(db, patientId, start, end, cancellationToken);

        return new
        {
            WellbeingMetrics = wellbeing,
            MoodMetrics = mood,
            StartDate = start,
            EndDate = end
        };
    }

    /// <summary>
    /// Obtener pacientes que tengan niveles altos de estrés en stress_levels_by_time
    /// </summary>
    public static async Task<List<Patient>> GetPatientsWithHighStressAsync(
        SerenityDbContext db, sbyte threshold = 8, CancellationToken cancellationToken = default)
    {
        var highStressPatientIds = await db.StressLevelsByTimes
            .Where(s => s.StressLevel >= threshold)
            .Select(s => s.PatientId)
            .Distinct()
            .ToListAsync(cancellationToken);

        return await db.Patients
            .Include(p => p.User)
            .Where(p => highStressPatientIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtener estadísticas de uso de meditación por paciente
    /// </summary>
    public static async Task<object> GetMeditationStatisticsAsync(
        SerenityDbContext db, int patientId, DateOnly start, DateOnly end, CancellationToken cancellationToken = default)
    {
        var sessions = await GetMeditationSessionsInRangeAsync(db, patientId, start, end, cancellationToken);
        var totalMinutes = await GetMeditationMinutesAsync(db, patientId, start, end, cancellationToken);

        return new
        {
            TotalSessions = sessions.Count,
            TotalMinutes = totalMinutes,
            AverageMinutesPerSession = sessions.Count > 0 ? totalMinutes / sessions.Count : 0,
            Sessions = sessions
        };
    }
}

