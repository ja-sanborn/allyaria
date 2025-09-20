using System.Globalization;

namespace Allyaria.Theming.Helpers;

/// <summary>
/// Internal helper utilities for validating, normalizing, and composing CSS style values. Exposed as <c>public static</c>
/// methods to allow consumption from style structs, while the class itself remains <c>internal</c> to the theming
/// assembly. All numeric parsing uses <see cref="CultureInfo.InvariantCulture" />.
/// </summary>
internal static class StyleHelpers
{
    /// <summary>
    /// A comprehensive set of CSS length units recognized by <see cref="IsLength(string)" />. Includes absolute, relative, and
    /// viewport units.
    /// </summary>
    public static readonly string[] LengthUnits =
    {
        // Relative font & element units
        "em",
        "rem",
        "lh",
        "rlh",
        "ex",
        "ch",

        // Viewport units (legacy + logical)
        "vw",
        "vh",
        "vmin",
        "vmax",
        "vi",
        "vb",

        // Absolute units
        "px",
        "cm",
        "mm",
        "q",
        "in",
        "pt",
        "pc"
    };

    /// <summary>
    /// Determines whether the token starts with a supported CSS function name. Typical examples are <c>var(…)</c>,
    /// <c>calc(…)</c>, <c>min(…)</c>, <c>max(…)</c>, <c>clamp(…)</c>.
    /// </summary>
    /// <param name="value">The token to evaluate.</param>
    /// <param name="functionNames">Function names to check, without the trailing parenthesis (e.g., "var").</param>
    /// <returns>
    /// <c>true</c> if the token starts with any of the provided function names followed by '('; otherwise <c>false</c>.
    /// </returns>
    public static bool IsCssFunction(string? value, params string[]? functionNames)
    {
        if (value is null || functionNames is null || functionNames.Length == 0)
        {
            return false;
        }

        return functionNames.Any(name => value.StartsWith(name, StringComparison.OrdinalIgnoreCase) &&
            value.Length > name.Length &&
            value[name.Length] == '('
        );
    }

    /// <summary>
    /// Determines whether the provided token is a CSS length value using a known unit. The check is case-sensitive and assumes
    /// the input is already trimmed/lowercased when appropriate.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when the token is a length; otherwise <c>false</c>.</returns>
    public static bool IsLength(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        return LengthUnits.Any(u => s.EndsWith(u, StringComparison.Ordinal) &&
            s.Length > u.Length &&
            double.TryParse(s[..^u.Length], NumberStyles.Float, CultureInfo.InvariantCulture, out _)
        );
    }

    /// <summary>Determines whether the provided token is a CSS length or percentage value.</summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when the token is a length or percentage; otherwise <c>false</c>.</returns>
    public static bool IsLengthOrPercentage(string s) => IsLength(s) || IsPercentage(s);

    /// <summary>
    /// Determines if the value represents a number using invariant culture. Accepts integer and floating-point forms and
    /// optional sign.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when numeric; otherwise <c>false</c>.</returns>
    public static bool IsNumeric(string s)
        => double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

    /// <summary>
    /// Determines whether the provided token is a CSS percentage value (ends with '%' and the preceding part is numeric).
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when the token is a percentage; otherwise <c>false</c>.</returns>
    public static bool IsPercentage(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        if (!s.EndsWith('%'))
        {
            return false;
        }

        var number = s[..^1];

        return double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Determines if the value represents a positive (greater than zero) unitless number. Useful for <c>line-height</c>
    /// unitless values.
    /// </summary>
    /// <param name="s">The token to evaluate.</param>
    /// <returns><c>true</c> when unitless and positive; otherwise <c>false</c>.</returns>
    public static bool IsUnitlessPositive(string s)
        => double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d) && d > 0;
}
