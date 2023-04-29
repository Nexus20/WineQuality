namespace WineQuality.Application.Extensions;

public static class StringExtensions
{
    public static string TruncateLongString(this string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return str[..Math.Min(str.Length, maxLength)];
    }
}