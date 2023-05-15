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