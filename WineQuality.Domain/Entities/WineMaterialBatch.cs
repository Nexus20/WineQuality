using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

// public enum ProcessPhase
// {
//     Harvesting,
//     Destemming,
//     Pressuring,
//     Sulfitation,
//     Fermentation,
//     Cleaning,
//     Maturation,
//     Blending,
//     Stabilization,
//     Bottling,
//     Labeling
// }

/// <summary>
/// Wine material that will go through several phases to become wine
/// </summary>
public class WineMaterialBatch : BaseEntity
{
    public string Name { get; set; } = null!;
    public DateTime HarvestDate { get; set; }
    public string HarvestLocation { get; set; } = null!;
    public virtual List<WineMaterialBatchGrapeSortPhase> Phases { get; set; } = null!;
    
    public string GrapeSortId { get; set; } = null!;
    public virtual GrapeSort GrapeSort { get; set; } = null!;
}