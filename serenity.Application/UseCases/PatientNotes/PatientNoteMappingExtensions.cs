using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.PatientNotes;

internal static class PatientNoteMappingExtensions
{
    public static PatientNoteDto ToDto(this PatientNote note)
    {
        return new PatientNoteDto
        {
            Id = note.Id,
            PatientId = note.PatientId,
            PsychologistId = note.PsychologistId,
            Date = note.Date,
            Content = note.Content,
            Mood = note.Mood,
            AiDiagnosis = note.AiDiagnosis,
            NeedsFollowUp = note.NeedsFollowUp,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }
}



