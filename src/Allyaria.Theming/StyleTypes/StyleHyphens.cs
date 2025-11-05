namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>hyphens</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining how words may be hyphenated when text wraps.
/// </summary>
public sealed record StyleHyphens : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleHyphens" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The hyphenation behavior kind to represent.</param>
    public StyleHyphens(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>hyphens</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// The browser automatically inserts hyphens where appropriate according to the language rules and available hyphenation
        /// dictionary.
        /// </summary>
        [Description(description: "auto")]
        Auto,

        /// <summary>
        /// Hyphens are inserted only where the author has explicitly specified soft hyphens (<c>&amp;shy;</c>) in the text.
        /// </summary>
        [Description(description: "manual")]
        Manual,

        /// <summary>
        /// Hyphenation is disabled entirely; words will not be broken and will overflow or wrap at spaces only.
        /// </summary>
        [Description(description: "none")]
        None
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>hyphens</c> value into a <see cref="StyleHyphens" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the hyphens value.</param>
    /// <returns>A new <see cref="StyleHyphens" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleHyphens Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleHyphens(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleHyphens" /> instance.</summary>
    /// <param name="value">The string representation of the hyphens value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleHyphens" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleHyphens? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleHyphens" /> instance.</summary>
    /// <param name="value">The string representation of the hyphens value.</param>
    /// <returns>A <see cref="StyleHyphens" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleHyphens(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleHyphens" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleHyphens" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>hyphens</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleHyphens? value) => value?.Value ?? string.Empty;
}
