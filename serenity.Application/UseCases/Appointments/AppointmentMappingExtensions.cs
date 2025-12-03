using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Appointments;

internal static class AppointmentMappingExtensions
{
    public static AppointmentDto ToDto(this Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            PsychologistId = appointment.PsychologistId,
            AppointmentDate = appointment.AppointmentDate,
            AppointmentTime = appointment.AppointmentTime,
            Duration = appointment.Duration,
            Type = appointment.Type,
            Status = appointment.Status,
            Notes = appointment.Notes,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = appointment.UpdatedAt
        };
    }
}




