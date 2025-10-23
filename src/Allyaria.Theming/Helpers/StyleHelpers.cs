namespace Allyaria.Theming.Helpers;

public static class StyleHelpers
{
    public static string ToCssName(this string? value)
        => Regex.Replace((value ?? string.Empty).Replace('_', '-'), @"[\s-]+", "-").Trim('-').ToLowerInvariant();

    public static string ToCssProperty<TStyle>(this TStyle? style, string propertyName)
        where TStyle : struct, IStyleValue
    {
        if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(style?.Value))
        {
            return string.Empty;
        }

        var name = propertyName.ToCssName();

        return string.IsNullOrWhiteSpace(name)
            ? string.Empty
            : $"{name}:{style.Value};";
    }

    public static bool TryValidateInput(this string? value, out string result)
    {
        result = string.Empty;

        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        result = value.Trim();

        return !result.Any(static c => char.IsControl(c));
    }

    public static string ValidateInput(this string? value)
    {
        AryArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var trimmed = value!.Trim();

        return trimmed.Any(static c => char.IsControl(c))
            ? throw new AryArgumentException("Value contains control characters.", nameof(value), value)
            : trimmed;
    }
}
