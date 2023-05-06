namespace WineQuality.Application.Models.Results.GrapeSorts;

public class PredictionDetailsResult
{
    public string Quality { get; set; } = null!;
    public List<QualityExplanationItem>? ExplanationItems { get; set; }
}

public class QualityExplanationItem
{
    public string Reason { get; set; }
    public double Value { get; set; }
    public double Severity { get; set; }
}