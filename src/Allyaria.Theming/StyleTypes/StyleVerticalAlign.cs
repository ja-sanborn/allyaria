namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>vertical-align</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// controlling the vertical alignment of inline or table-cell elements.
/// </summary>
public sealed record StyleVerticalAlign : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleVerticalAlign" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The vertical alignment kind to represent.</param>
    public StyleVerticalAlign(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>vertical-align</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Aligns the baseline of the element with the baseline of its parent.</summary>
        [Description(description: "baseline")]
        Baseline,

        /// <summary>Aligns the bottom of the element with the lowest element on the line.</summary>
        [Description(description: "bottom")]
        Bottom,

        /// <summary>Aligns the middle of the element with the baseline plus half the x-height of the parent.</summary>
        [Description(description: "middle")]
        Middle,

        /// <summary>Aligns the element as subscript text, lowering it below the baseline.</summary>
        [Description(description: "sub")]
        Sub,

        /// <summary>Aligns the element as superscript text, raising it above the baseline.</summary>
        [Description(description: "super")]
        Super,

        /// <summary>Aligns the bottom of the element with the bottom of the parent element’s text.</summary>
        [Description(description: "text-bottom")]
        TextBottom,

        /// <summary>Aligns the top of the element with the top of the parent element’s text.</summary>
        [Description(description: "text-top")]
        TextTop,

        /// <summary>Aligns the top of the element with the highest element on the line.</summary>
        [Description(description: "top")]
        Top
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>vertical-align</c> value into a <see cref="StyleVerticalAlign" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the vertical-align value.</param>
    /// <returns>A new <see cref="StyleVerticalAlign" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleVerticalAlign Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleVerticalAlign(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleVerticalAlign" /> instance.</summary>
    /// <param name="value">The string representation of the vertical-align value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleVerticalAlign" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleVerticalAlign? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleVerticalAlign" /> instance.</summary>
    /// <param name="value">The string representation of the vertical-align value.</param>
    /// <returns>A <see cref="StyleVerticalAlign" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleVerticalAlign(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleVerticalAlign" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleVerticalAlign" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>vertical-align</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleVerticalAlign? value) => (value?.Value).OrDefaultIfEmpty();
}
