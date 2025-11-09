using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class NoteSuggestion
{
    public int Id { get; set; }

    public int NoteId { get; set; }

    public string Suggestion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual PatientNote Note { get; set; } = null!;
}
