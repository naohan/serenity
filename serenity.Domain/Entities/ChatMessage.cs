using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class ChatMessage
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int PsychologistId { get; set; }

    public string MessageText { get; set; } = null!;

    public bool IsFromPsychologist { get; set; }

    public DateTime SentAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Psychologist Psychologist { get; set; } = null!;
}
