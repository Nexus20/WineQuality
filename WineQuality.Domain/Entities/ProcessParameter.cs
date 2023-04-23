using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessParameter : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual List<ProcessPhaseParameter>? Phases { get; set; }
}