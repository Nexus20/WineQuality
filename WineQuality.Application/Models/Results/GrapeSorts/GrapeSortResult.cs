using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.GrapeSorts;

public class GrapeSortResult : BaseResult
{
    public string Name { get; set; } = null!;
}