namespace Allyaria.Theming.Helpers;

public static class StyleHelpers
{
    public static string ToCssName(this string? value)
        => Regex.Replace(
                input: (value ?? string.Empty).Replace(oldChar: '_', newChar: '-'), pattern: @"[\s-]+", replacement: "-"
            ).Trim(trimChar: '-')
            .ToLowerInvariant();

    public static string ToCssProperty<TStyle>(this TStyle? style, string propertyName)
        where TStyle : struct, IStyleValue
    {
        if (string.IsNullOrWhiteSpace(value: propertyName) || string.IsNullOrWhiteSpace(value: style?.Value))
        {
            return string.Empty;
        }

        var name = propertyName.ToCssName();

        return string.IsNullOrWhiteSpace(value: name)
            ? string.Empty
            : $"{name}:{style.Value};";
    }

    public static bool TryValidateInput(this string? value, out string result)
    {
        result = string.Empty;

        if (string.IsNullOrWhiteSpace(value: value))
        {
            return false;
        }

        result = value.Trim();

        return !result.Any(predicate: static c => char.IsControl(c: c));
    }

    public static string ValidateInput(this string? value)
    {
        AryArgumentException.ThrowIfNullOrWhiteSpace(argValue: value, argName: nameof(value));

        var trimmed = value!.Trim();

        return trimmed.Any(predicate: static c => char.IsControl(c: c))
            ? throw new AryArgumentException(
                message: "Value contains control characters.", argName: nameof(value), argValue: value
            )
            : trimmed;
    }
}
