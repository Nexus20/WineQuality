using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class QualityPrediction : BaseEntity
{
    public bool Prediction { get; set; } // 1 - good, 0 - bad
    public Dictionary<string, double>? PredictionExplanation { get; set; }
    public Dictionary<string, double>? ParametersValues { get; set; }
    public string FileReferenceId { get; set; } = null!;
    public virtual FileReference FileReference { get; set; } = null!;
    public string WineMaterialBatchGrapeSortPhaseId { get; set; } = null!;
    public virtual WineMaterialBatchGrapeSortPhase WineMaterialBatchGrapeSortPhase { get; set; } = null!;
}