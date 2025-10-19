namespace Allyaria.Theming.Types;

/// <summary>Represents a theming string theme with enforced normalization (trimmed, non-null, non-whitespace).</summary>
public sealed class ThemeString : ThemeBase
{
    /// <summary>Initializes a new instance of the <see cref="ThemeString" /> class.</summary>
    /// <param name="value">The string theme to normalize and store.</param>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, whitespace, or contains disallowed control characters.
    /// </exception>
    public ThemeString(string value)
        : base(Normalize(value)) { }

    /// <summary>Normalizes the input string by trimming and validating it (including control character checks).</summary>
    /// <param name="value">The string theme to normalize.</param>
    /// <returns>The normalized (trimmed) string.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, whitespace, or contains control characters.
    /// </exception>
    private static string Normalize(string value) => ValidateInput(value);

    /// <summary>Parses the specified string into an <see cref="ThemeString" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <returns>A new <see cref="ThemeString" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, whitespace, or contains disallowed control characters.
    /// </exception>
    public static ThemeString Parse(string value) => new(value);

    /// <summary>Attempts to parse the specified string into an <see cref="ThemeString" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="ThemeString" /> if parsing succeeded, or <c>null</c> if it
    /// failed.
    /// </param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string value, out ThemeString? result)
    {
        try
        {
            result = new ThemeString(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Defines an implicit conversion from <see cref="string" /> to <see cref="ThemeString" />.</summary>
    /// <param name="value">The string theme to convert.</param>
    /// <returns>A new <see cref="ThemeString" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, empty, whitespace, or contains disallowed control characters.
    /// </exception>
    public static implicit operator ThemeString(string value) => new(value);

    /// <summary>Defines an implicit conversion from <see cref="ThemeString" /> to <see cref="string" />.</summary>
    /// <param name="theme">The <see cref="ThemeString" /> instance.</param>
    /// <returns>The underlying normalized string theme.</returns>
    public static implicit operator string(ThemeString theme) => theme.Value;
}
