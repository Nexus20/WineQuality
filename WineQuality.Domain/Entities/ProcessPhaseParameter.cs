using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessPhaseParameter : BaseEntity
{
    public string PhaseTypeId { get; set; } = null!;
    public virtual ProcessPhaseType PhaseType { get; set; } = null!;

    public string ParameterId { get; set; } = null!;
    public virtual ProcessParameter Parameter { get; set; } = null!;
}