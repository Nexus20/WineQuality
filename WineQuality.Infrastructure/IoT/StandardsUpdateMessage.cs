namespace WineQuality.Infrastructure.IoT;

public class StandardsUpdateMessage
{
    public double? UpperBound { get; set; }
    public double? LowerBound { get; set; }
    public string? ParameterName { get; set; }
    public string? PhaseName { get; set; }
    public string? GrapeSortName { get; set; }
    public string? WineMaterialBatchId { get; set; }
    public string? WineMaterialBatchName { get; set; }
}