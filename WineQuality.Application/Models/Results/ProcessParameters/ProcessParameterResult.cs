using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.ProcessParameters;

public class ProcessParameterResult : BaseResult
{
    public string Name { get; set; } = null!;
}