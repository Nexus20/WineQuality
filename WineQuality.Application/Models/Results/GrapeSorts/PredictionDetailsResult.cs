using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.GrapeSorts;

public class PredictionDetailsResult : BaseResult
{
    public string Quality { get; set; } = null!;
    public List<QualityExplanationItem>? ExplanationItems { get; set; }
    public string ExplanationUri { get; set; } = null!;
}

public class QualityExplanationItem
{
    public string Reason { get; set; }
    public double Value { get; set; }
    public double Severity { get; set; }
}