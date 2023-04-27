using System.ComponentModel.DataAnnotations.Schema;

namespace WineQuality.Domain.Entities.Abstract;

/// <summary>
/// Localizable entity interface.
/// </summary>
public interface ILocalizable
{
    /// <summary>
    /// Local name of the entity in a specific culture.
    /// </summary>
    public string? LocalName { get; set; }

    /// <summary>
    /// Localizations entries which contain the translation of the entity into different cultures. 
    /// </summary>
    public List<Localization>? Localizations { get; set; }
}