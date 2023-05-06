namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class WineMaterialBatchProcessPhaseReadingsResult
{
    public string ParameterId { get; set; } = null!;
    public string ParameterName { get; set; } = null!;
    public double Value { get; set; }
}