namespace WineQuality.Application.Models.Results.GrapeSorts;

public class PredictionResult
{
    public int QualityPrediction { get; set; }
    public Dictionary<string, double> PredictionExplanation { get; set; } = null!;
    public string ExplanationUri { get; set; } = null!;
}