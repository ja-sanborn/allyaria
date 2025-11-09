namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a simple CSS string value within the Allyaria theming system. Provides a strongly typed wrapper for any
/// arbitrary CSS-compatible string value, ensuring consistent handling and conversion across components.
/// </summary>
public sealed record StyleString : StyleValueBase
{
    /// <summary>Initializes a new instance of the <see cref="StyleString" /> record with an optional string value.</summary>
    /// <param name="value">The string value to assign. If <see langword="null" />, an empty string is used.</param>
    public StyleString(string? value = "")
        : base(value: value ?? string.Empty) { }

    /// <summary>Parses the specified string into a <see cref="StyleString" /> instance.</summary>
    /// <param name="value">The string representation of the style value.</param>
    /// <returns>A new <see cref="StyleString" /> instance representing the provided value.</returns>
    public static StyleString Parse(string? value) => new(value: value);

    /// <summary>Attempts to parse a string into a <see cref="StyleString" /> instance.</summary>
    /// <param name="value">The string representation of the style value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleString" /> instance or <see langword="null" /> if parsing
    /// failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleString? result)
    {
        try
        {
            result = Parse(value: value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a string into a <see cref="StyleString" /> instance.</summary>
    /// <param name="value">The string value to convert.</param>
    /// <returns>A <see cref="StyleString" /> instance representing the provided string.</returns>
    public static implicit operator StyleString(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleString" /> instance to its underlying string representation.</summary>
    /// <param name="value">The <see cref="StyleString" /> instance to convert.</param>
    /// <returns>
    /// The contained string value, or an empty string if <paramref name="value" /> is <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleString? value) => (value?.Value).OrDefaultIfEmpty();
}
