namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class WineMaterialBatchProcessPhaseReadingsResult
{
    public DateTime CreatedAt { get; set; }
    public string ParameterId { get; set; } = null!;
    public string ParameterName { get; set; } = null!;
    public double Value { get; set; }
}