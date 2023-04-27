using System.ComponentModel.DataAnnotations.Schema;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessPhaseType : BaseEntity, ILocalizable
{
    public string Name { get; set; } = null!;
    public virtual ProcessPhaseType? NextPhase {get; set;}
    public virtual ProcessPhaseType? PreviousPhase {get; set;}
    public virtual List<ProcessPhaseParameter>? Parameters { get; set; }
    public virtual List<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; set; }
    
    [NotMapped]
    public string? LocalName { get; set; }
    public virtual List<Localization>? Localizations { get; set; }
}