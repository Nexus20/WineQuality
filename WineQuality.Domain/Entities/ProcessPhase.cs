using System.ComponentModel.DataAnnotations.Schema;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessPhase : BaseEntity, ILocalizable
{
    public string Name { get; set; } = null!;
    
    public virtual List<GrapeSortPhase>? GrapeSorts { get; set; }
    public virtual List<ProcessPhaseParameter>? Parameters { get; set; }

    [NotMapped]
    public string? LocalName { get; set; }
    public virtual List<Localization>? Localizations { get; set; }
}