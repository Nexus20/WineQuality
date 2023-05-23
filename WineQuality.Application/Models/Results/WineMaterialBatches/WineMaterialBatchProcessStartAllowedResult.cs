namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class WineMaterialBatchProcessStartAllowedResult
{
    public bool StartAllowed { get; set; }
    public List<string> StartNotAllowedReasons { get; set; }

    public WineMaterialBatchProcessStartAllowedResult()
    {
        StartNotAllowedReasons = new List<string>();
        StartAllowed = false;
    }
}

public class WineMaterialBatchPhaseParameterChartDataResult
{
    public List<DateTime> Labels { get; set; }
    public List<double> Values { get; set; }

    public WineMaterialBatchPhaseParameterChartDataResult()
    {
        Labels = new List<DateTime>();
        Values = new List<double>();
    }
}