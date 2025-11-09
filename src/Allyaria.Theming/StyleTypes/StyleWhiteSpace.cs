namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>white-space</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// controlling how whitespace inside an element is handled, including collapsing, preservation, and line-breaking
/// behavior.
/// </summary>
public sealed record StyleWhiteSpace : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleWhiteSpace" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The white-space handling behavior to represent.</param>
    public StyleWhiteSpace(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>white-space</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Preserves spaces, tabs, and line breaks; text may wrap to a new line only at line breaks or explicit wrap points.
        /// Trailing spaces are preserved.
        /// </summary>
        [Description(description: "break-spaces")]
        BreakSpaces,

        /// <summary>Collapses consecutive spaces and tabs into a single space; line breaks may occur where allowed.</summary>
        [Description(description: "collapse")]
        Collapse,

        /// <summary>
        /// Collapses consecutive whitespace characters and allows text wrapping at normal break points (default behavior).
        /// </summary>
        [Description(description: "normal")]
        Normal,

        /// <summary>Collapses whitespace but prevents text from wrapping onto multiple lines.</summary>
        [Description(description: "nowrap")]
        Nowrap,

        /// <summary>
        /// Preserves spaces and line breaks exactly as they appear in the source. Text does not wrap automatically.
        /// </summary>
        [Description(description: "pre")]
        Pre,

        /// <summary>Collapses spaces as in normal, but preserves line breaks.</summary>
        [Description(description: "pre-line")]
        PreLine,

        /// <summary>
        /// Preserves spaces and tabs but prevents automatic line wrapping. Equivalent to legacy <c>white-space: pre nowrap</c>
        /// combination.
        /// </summary>
        [Description(description: "preserve nowrap")]
        PreserveNowrap,

        /// <summary>Preserves whitespace but allows text to wrap when needed.</summary>
        [Description(description: "pre-wrap")]
        PreWrap
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>white-space</c> value into a <see cref="StyleWhiteSpace" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the white-space value.</param>
    /// <returns>A new <see cref="StyleWhiteSpace" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleWhiteSpace Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleWhiteSpace(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleWhiteSpace" /> instance.</summary>
    /// <param name="value">The string representation of the white-space value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleWhiteSpace" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleWhiteSpace? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleWhiteSpace" /> instance.</summary>
    /// <param name="value">The string representation of the white-space value.</param>
    /// <returns>A <see cref="StyleWhiteSpace" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleWhiteSpace(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleWhiteSpace" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleWhiteSpace" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>white-space</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleWhiteSpace? value) => (value?.Value).OrDefaultIfEmpty();
}
