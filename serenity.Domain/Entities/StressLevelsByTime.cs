using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class StressLevelsByTime
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly TimeOfDay { get; set; }

    public sbyte StressLevel { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
