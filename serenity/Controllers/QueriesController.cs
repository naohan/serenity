using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Infrastructure.Adapters.Repositories;
using serenity.Infrastructure.Data;

namespace serenity.Controllers;

/// <summary>
/// Controlador para consultas especializadas de Serenity
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QueriesController : ControllerBase
{
    private readonly SerenityDbContext _db;

    public QueriesController(SerenityDbContext db)
    {
        _db = db;
    }

    // ============================================
    // 1. USERS
    // ============================================

    /// <summary>
    /// Verificar si un email ya existe
    /// </summary>
    [HttpGet("users/email-exists/{email}")]
    [AllowAnonymous] // Permitir sin autenticación para verificar emails
    public async Task<ActionResult<bool>> EmailExists(string email, CancellationToken cancellationToken = default)
    {
        var exists = await SerenityQueries.EmailExistsAsync(_db, email, cancellationToken);
        return Ok(exists);
    }

    /// <summary>
    /// Listar usuarios activos por rol
    /// </summary>
    [HttpGet("users/active-by-role/{role}")]
    public async Task<ActionResult> GetActiveUsersByRole(sbyte role, CancellationToken cancellationToken = default)
    {
        var users = await SerenityQueries.GetActiveUsersByRoleAsync(_db, role, cancellationToken);
        return Ok(users);
    }

    // ============================================
    // 2. PSYCHOLOGISTS
    // ============================================

    /// <summary>
    /// Listar psicólogos con datos de usuario
    /// </summary>
    [HttpGet("psychologists/with-user")]
    public async Task<ActionResult> GetPsychologistsWithUser(CancellationToken cancellationToken = default)
    {
        var psychologists = await SerenityQueries.GetPsychologistsWithUserAsync(_db, cancellationToken);
        return Ok(psychologists);
    }

    /// <summary>
    /// Top N psicólogos por weekly_score
    /// </summary>
    [HttpGet("psychologists/top/{top}")]
    public async Task<ActionResult> GetTopPsychologists(int top, CancellationToken cancellationToken = default)
    {
        var psychologists = await SerenityQueries.GetTopPsychologistsAsync(_db, top, cancellationToken);
        return Ok(psychologists);
    }

    // ============================================
    // 3. PATIENTS
    // ============================================

    /// <summary>
    /// Listar pacientes de un psicólogo
    /// </summary>
    [HttpGet("patients/by-psychologist/{psychologistId}")]
    public async Task<ActionResult> GetPatientsByPsychologist(int psychologistId, CancellationToken cancellationToken = default)
    {
        var patients = await SerenityQueries.GetPatientsByPsychologistAsync(_db, psychologistId, cancellationToken);
        return Ok(patients);
    }

    /// <summary>
    /// Buscar pacientes por nombre o email
    /// </summary>
    [HttpGet("patients/search")]
    public async Task<ActionResult> SearchPatients([FromQuery] string search, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return BadRequest(new { message = "El parámetro 'search' es requerido" });
        }
        var patients = await SerenityQueries.SearchPatientsAsync(_db, search, cancellationToken);
        return Ok(patients);
    }

    /// <summary>
    /// Obtener perfil completo de paciente
    /// </summary>
    [HttpGet("patients/{patientId}/full-profile")]
    public async Task<ActionResult> GetFullPatientProfile(int patientId, CancellationToken cancellationToken = default)
    {
        var patient = await SerenityQueries.GetFullPatientProfileAsync(_db, patientId, cancellationToken);
        if (patient == null)
        {
            return NotFound(new { message = $"Paciente con ID {patientId} no encontrado" });
        }
        return Ok(patient);
    }

    /// <summary>
    /// Obtener todos los pacientes activos con usuario y psicólogo
    /// </summary>
    [HttpGet("patients/active-with-details")]
    public async Task<ActionResult> GetActivePatientsWithDetails(CancellationToken cancellationToken = default)
    {
        var patients = await SerenityQueries.GetActivePatientsWithUserAndPsychologistAsync(_db, cancellationToken);
        return Ok(patients);
    }

    // ============================================
    // 4. APPOINTMENTS
    // ============================================

    /// <summary>
    /// Próximas citas de un paciente
    /// </summary>
    [HttpGet("appointments/patient/{patientId}/upcoming")]
    public async Task<ActionResult> GetUpcomingAppointmentsForPatient(int patientId, CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var appointments = await SerenityQueries.GetUpcomingAppointmentsForPatientAsync(_db, patientId, today, cancellationToken);
        return Ok(appointments);
    }

    /// <summary>
    /// Citas de un psicólogo en rango de fechas
    /// </summary>
    [HttpGet("appointments/psychologist/{psychologistId}/range")]
    public async Task<ActionResult> GetPsychologistAppointmentsInRange(
        int psychologistId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var appointments = await SerenityQueries.GetPsychologistAppointmentsInRangeAsync(_db, psychologistId, start, end, cancellationToken);
        return Ok(appointments);
    }

    /// <summary>
    /// Citas por día y estado
    /// </summary>
    [HttpGet("appointments/by-date-status")]
    public async Task<ActionResult> GetAppointmentsByDateAndStatus(
        [FromQuery] DateOnly date,
        [FromQuery] string status,
        CancellationToken cancellationToken = default)
    {
        var appointments = await SerenityQueries.GetAppointmentsByDateAndStatusAsync(_db, date, status, cancellationToken);
        return Ok(appointments);
    }

    // ============================================
    // 5. CHAT_MESSAGES
    // ============================================

    /// <summary>
    /// Obtener conversación completa entre paciente y psicólogo
    /// </summary>
    [HttpGet("chat/conversation")]
    public async Task<ActionResult> GetConversation(
        [FromQuery] int patientId,
        [FromQuery] int psychologistId,
        CancellationToken cancellationToken = default)
    {
        var messages = await SerenityQueries.GetConversationAsync(_db, patientId, psychologistId, cancellationToken);
        return Ok(messages);
    }

    /// <summary>
    /// Contar mensajes no leídos para un psicólogo
    /// </summary>
    [HttpGet("chat/psychologist/{psychologistId}/unread-count")]
    public async Task<ActionResult<int>> CountUnreadMessagesForPsychologist(int psychologistId, CancellationToken cancellationToken = default)
    {
        var count = await SerenityQueries.CountUnreadMessagesForPsychologistAsync(_db, psychologistId, cancellationToken);
        return Ok(count);
    }

    /// <summary>
    /// Contar mensajes no leídos para un paciente
    /// </summary>
    [HttpGet("chat/patient/{patientId}/unread-count")]
    public async Task<ActionResult<int>> CountUnreadMessagesForPatient(int patientId, CancellationToken cancellationToken = default)
    {
        var count = await SerenityQueries.CountUnreadMessagesForPatientAsync(_db, patientId, cancellationToken);
        return Ok(count);
    }

    // ============================================
    // 6. DAILY_MOODS
    // ============================================

    /// <summary>
    /// Último daily mood de un paciente
    /// </summary>
    [HttpGet("daily-moods/patient/{patientId}/last")]
    public async Task<ActionResult> GetLastDailyMood(int patientId, CancellationToken cancellationToken = default)
    {
        var mood = await SerenityQueries.GetLastDailyMoodAsync(_db, patientId, cancellationToken);
        if (mood == null)
        {
            return NotFound(new { message = $"No se encontró daily mood para el paciente {patientId}" });
        }
        return Ok(mood);
    }

    /// <summary>
    /// Serie de moods en rango de fechas
    /// </summary>
    [HttpGet("daily-moods/patient/{patientId}/range")]
    public async Task<ActionResult> GetDailyMoodsInRange(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var moods = await SerenityQueries.GetDailyMoodsInRangeAsync(_db, patientId, start, end, cancellationToken);
        return Ok(moods);
    }

    /// <summary>
    /// Pacientes sin mood en los últimos N días
    /// </summary>
    [HttpGet("daily-moods/patients-without-mood")]
    public async Task<ActionResult> GetPatientsWithoutMoodLastDays(
        [FromQuery] int days = 7,
        CancellationToken cancellationToken = default)
    {
        var patients = await SerenityQueries.GetPatientsWithoutMoodLastDaysAsync(_db, days, cancellationToken);
        return Ok(patients);
    }

    // ============================================
    // 7. EMOTIONAL_STATES
    // ============================================

    /// <summary>
    /// Último estado emocional por tipo
    /// </summary>
    [HttpGet("emotional-states/patient/{patientId}/last-by-type")]
    public async Task<ActionResult> GetLastEmotionalStatesByType(int patientId, CancellationToken cancellationToken = default)
    {
        var states = await SerenityQueries.GetLastEmotionalStatesByTypeAsync(_db, patientId, cancellationToken);
        return Ok(states);
    }

    /// <summary>
    /// Estados emocionales en rango de fechas
    /// </summary>
    [HttpGet("emotional-states/patient/{patientId}/range")]
    public async Task<ActionResult> GetEmotionalStatesInRange(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var states = await SerenityQueries.GetEmotionalStatesInRangeAsync(_db, patientId, start, end, cancellationToken);
        return Ok(states);
    }

    // ============================================
    // 8. METRICS
    // ============================================

    /// <summary>
    /// Métricas de bienestar de un paciente por rango
    /// </summary>
    [HttpGet("metrics/wellbeing/patient/{patientId}/range")]
    public async Task<ActionResult> GetWellbeingMetrics(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var metrics = await SerenityQueries.GetWellbeingMetricsAsync(_db, patientId, start, end, cancellationToken);
        return Ok(metrics);
    }

    /// <summary>
    /// Métricas de ánimo por rango
    /// </summary>
    [HttpGet("metrics/mood/patient/{patientId}/range")]
    public async Task<ActionResult> GetMoodMetrics(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var metrics = await SerenityQueries.GetMoodMetricsAsync(_db, patientId, start, end, cancellationToken);
        return Ok(metrics);
    }

    /// <summary>
    /// Resumen semanal promedio de bienestar
    /// </summary>
    [HttpGet("metrics/wellbeing/patient/{patientId}/weekly-summary")]
    public async Task<ActionResult> GetWeeklyWellbeingSummary(int patientId, CancellationToken cancellationToken = default)
    {
        var summary = await SerenityQueries.GetWeeklyWellbeingSummaryAsync(_db, patientId, cancellationToken);
        return Ok(summary);
    }

    /// <summary>
    /// Métricas combinadas de bienestar y ánimo
    /// </summary>
    [HttpGet("metrics/combined/patient/{patientId}/range")]
    public async Task<ActionResult> GetCombinedMetrics(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var metrics = await SerenityQueries.GetCombinedMetricsForPatientAsync(_db, patientId, start, end, cancellationToken);
        return Ok(metrics);
    }

    // ============================================
    // 9. MEDITATION_SESSIONS
    // ============================================

    /// <summary>
    /// Total de minutos de meditación en rango
    /// </summary>
    [HttpGet("meditation/patient/{patientId}/minutes")]
    public async Task<ActionResult<int>> GetMeditationMinutes(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var minutes = await SerenityQueries.GetMeditationMinutesAsync(_db, patientId, start, end, cancellationToken);
        return Ok(minutes);
    }

    /// <summary>
    /// Sesiones de meditación en rango de fechas
    /// </summary>
    [HttpGet("meditation/patient/{patientId}/sessions")]
    public async Task<ActionResult> GetMeditationSessionsInRange(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var sessions = await SerenityQueries.GetMeditationSessionsInRangeAsync(_db, patientId, start, end, cancellationToken);
        return Ok(sessions);
    }

    /// <summary>
    /// Estadísticas de uso de meditación
    /// </summary>
    [HttpGet("meditation/patient/{patientId}/statistics")]
    public async Task<ActionResult> GetMeditationStatistics(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var stats = await SerenityQueries.GetMeditationStatisticsAsync(_db, patientId, start, end, cancellationToken);
        return Ok(stats);
    }

    // ============================================
    // 10. PATIENT_NOTES + INSIGHTS
    // ============================================

    /// <summary>
    /// Notas de paciente con sugerencias
    /// </summary>
    [HttpGet("patient-notes/{patientId}/with-suggestions")]
    public async Task<ActionResult> GetPatientNotesWithSuggestions(int patientId, CancellationToken cancellationToken = default)
    {
        var notes = await SerenityQueries.GetPatientNotesWithSuggestionsAsync(_db, patientId, cancellationToken);
        return Ok(notes);
    }

    /// <summary>
    /// Notas que requieren seguimiento
    /// </summary>
    [HttpGet("patient-notes/psychologist/{psychologistId}/requiring-followup")]
    public async Task<ActionResult> GetNotesRequiringFollowUp(int psychologistId, CancellationToken cancellationToken = default)
    {
        var notes = await SerenityQueries.GetNotesRequiringFollowUpAsync(_db, psychologistId, cancellationToken);
        return Ok(notes);
    }

    /// <summary>
    /// Insights por paciente
    /// </summary>
    [HttpGet("insights/patient/{patientId}")]
    public async Task<ActionResult> GetInsightsForPatient(int patientId, CancellationToken cancellationToken = default)
    {
        var insights = await SerenityQueries.GetInsightsForPatientAsync(_db, patientId, cancellationToken);
        return Ok(insights);
    }

    /// <summary>
    /// Insights por psicólogo
    /// </summary>
    [HttpGet("insights/psychologist/{psychologistId}")]
    public async Task<ActionResult> GetInsightsByPsychologist(int psychologistId, CancellationToken cancellationToken = default)
    {
        var insights = await SerenityQueries.GetInsightsByPsychologistAsync(_db, psychologistId, cancellationToken);
        return Ok(insights);
    }

    // ============================================
    // 11. PATIENT_REPORTS
    // ============================================

    /// <summary>
    /// Último informe de un paciente
    /// </summary>
    [HttpGet("patient-reports/patient/{patientId}/last")]
    public async Task<ActionResult> GetLastReportForPatient(int patientId, CancellationToken cancellationToken = default)
    {
        var report = await SerenityQueries.GetLastReportForPatientAsync(_db, patientId, cancellationToken);
        if (report == null)
        {
            return NotFound(new { message = $"No se encontró reporte para el paciente {patientId}" });
        }
        return Ok(report);
    }

    /// <summary>
    /// Informes de un paciente ordenados por fecha
    /// </summary>
    [HttpGet("patient-reports/patient/{patientId}")]
    public async Task<ActionResult> GetPatientReports(int patientId, CancellationToken cancellationToken = default)
    {
        var reports = await SerenityQueries.GetPatientReportsAsync(_db, patientId, cancellationToken);
        return Ok(reports);
    }

    // ============================================
    // 12. PRESCRIPTIONS
    // ============================================

    /// <summary>
    /// Prescripciones activas de un paciente
    /// </summary>
    [HttpGet("prescriptions/patient/{patientId}/active")]
    public async Task<ActionResult> GetActivePrescriptionsForPatient(int patientId, CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var prescriptions = await SerenityQueries.GetActivePrescriptionsForPatientAsync(_db, patientId, today, cancellationToken);
        return Ok(prescriptions);
    }

    /// <summary>
    /// Todas las prescripciones de un paciente
    /// </summary>
    [HttpGet("prescriptions/patient/{patientId}")]
    public async Task<ActionResult> GetAllPrescriptionsForPatient(int patientId, CancellationToken cancellationToken = default)
    {
        var prescriptions = await SerenityQueries.GetAllPrescriptionsForPatientAsync(_db, patientId, cancellationToken);
        return Ok(prescriptions);
    }

    // ============================================
    // 13. STRESS_LEVELS_BY_TIME
    // ============================================

    /// <summary>
    /// Promedio de estrés por franja horaria
    /// </summary>
    [HttpGet("stress/patient/{patientId}/by-time-of-day")]
    public async Task<ActionResult> GetStressByTimeOfDay(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var stress = await SerenityQueries.GetStressByTimeOfDayAsync(_db, patientId, start, end, cancellationToken);
        return Ok(stress);
    }

    /// <summary>
    /// Días con estrés alto
    /// </summary>
    [HttpGet("stress/patient/{patientId}/high-stress-days")]
    public async Task<ActionResult> GetHighStressDays(
        int patientId,
        [FromQuery] sbyte threshold = 8,
        CancellationToken cancellationToken = default)
    {
        var days = await SerenityQueries.GetHighStressDaysAsync(_db, patientId, threshold, cancellationToken);
        return Ok(days);
    }

    /// <summary>
    /// Niveles de estrés en rango de fechas
    /// </summary>
    [HttpGet("stress/patient/{patientId}/range")]
    public async Task<ActionResult> GetStressLevelsInRange(
        int patientId,
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken = default)
    {
        var levels = await SerenityQueries.GetStressLevelsInRangeAsync(_db, patientId, start, end, cancellationToken);
        return Ok(levels);
    }

    /// <summary>
    /// Pacientes con niveles altos de estrés
    /// </summary>
    [HttpGet("stress/patients-with-high-stress")]
    public async Task<ActionResult> GetPatientsWithHighStress(
        [FromQuery] sbyte threshold = 8,
        CancellationToken cancellationToken = default)
    {
        var patients = await SerenityQueries.GetPatientsWithHighStressAsync(_db, threshold, cancellationToken);
        return Ok(patients);
    }
}

