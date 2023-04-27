using System.ComponentModel.DataAnnotations.Schema;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessParameter : BaseEntity, ILocalizable
{
    public string Name { get; set; } = null!;
    public virtual List<ProcessPhaseParameter>? Phases { get; set; }
    
    [NotMapped]
    public string? LocalName { get; set; }
    public virtual List<Localization>? Localizations { get; set; }
}