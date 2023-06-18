using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.Cultures;

public class CultureResult : BaseResult
{
    /// <summary>
    /// Culture name.
    /// </summary>
    public string CultureName { get; set; } = null!;

    /// <summary>
    /// Culture code.
    /// </summary>
    public string CultureCode { get; set; } = null!;
}