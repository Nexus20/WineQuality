namespace WineQuality.Application.Models.Requests.GrapeSorts;

public class MlServicePredictQualityRequest
{
    public string DatasetUri { get; set; } = null!;
    public string ModelUri { get; set; } = null!;
    public Dictionary<string, double> ParametersValues { get; set; } = null!;
}