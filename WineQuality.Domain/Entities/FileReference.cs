using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class FileReference : BaseEntity
{
    public string ContainerName { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public virtual List<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; set; }
}