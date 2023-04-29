using WineQuality.Application.Models.Dtos.Files;
using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.GrapeSorts;

public class GrapeSortPhaseDatasetResult : BaseResult
{
    public FileNameWithUrlDto DatasetInfo { get; set; } = null!;
}