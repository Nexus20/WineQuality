using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class GrapeSort : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual List<WineMaterialBatch>? WineMaterialBatches { get; set; }

    public virtual List<GrapeSortPhase>? Phases { get; set; }
}