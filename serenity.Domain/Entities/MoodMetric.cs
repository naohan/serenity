using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class MoodMetric
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateOnly Date { get; set; }

    public decimal? HappyPercentage { get; set; }

    public decimal? CalmPercentage { get; set; }

    public decimal? SadPercentage { get; set; }

    public decimal? AnxiousPercentage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
