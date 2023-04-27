using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

/// <summary>
/// Localization entry for a list element value.
/// </summary>
public class Localization : BaseEntity
{
    /// <summary>
    /// Localized name of the value.
    /// </summary>
    public string LocalName { get; set; } = null!;

    /// <summary>
    /// Culture which the list localization is associated.
    /// </summary>
    public virtual Culture Culture { get; set; } = null!;

    /// <summary>
    /// Culture Id.
    /// </summary>
    /// <value>Foreign key property.</value>
    public string CultureId { get; set; } = null!;
}