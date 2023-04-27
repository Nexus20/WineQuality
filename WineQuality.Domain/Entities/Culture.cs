using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

/// <summary>
/// Specific culture.
/// </summary>
public class Culture : BaseEntity
{
    /// <summary>
    /// Culture name.
    /// </summary>
    public string CultureName { get; set; } = null!;

    /// <summary>
    /// Culture code.
    /// </summary>
    public string CultureCode { get; set; } = null!;

    public virtual List<Localization>? Localizations { get; set; }
}