namespace serenity.Application.DTOs;

public class UpdateMoodMetricRequest
{
    public DateOnly? Date { get; set; }
    public decimal? HappyPercentage { get; set; }
    public decimal? CalmPercentage { get; set; }
    public decimal? SadPercentage { get; set; }
    public decimal? AnxiousPercentage { get; set; }
}



