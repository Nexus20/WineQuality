namespace WineQuality.Application.Constants.Localization;

public static class QualityLocalizationConstants
{
    private const string GoodQualityWhine = nameof(GoodQualityWhine);
    private const string BadQualityWhine = nameof(BadQualityWhine);

    public static readonly Dictionary<int, string> QualityLocalizations = new()
    {
        { QualityConstants.BadQuality, BadQualityWhine },
        { QualityConstants.GoodQuality, GoodQualityWhine }
    };
}