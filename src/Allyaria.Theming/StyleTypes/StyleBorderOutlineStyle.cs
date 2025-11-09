namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>border-style</c> or <c>outline-style</c> value used within the Allyaria theming system. Provides a
/// strongly typed wrapper around standard CSS line style keywords (e.g., solid, dashed, dotted).
/// </summary>
public sealed record StyleBorderOutlineStyle : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleBorderOutlineStyle" /> record using the specified border or outline
    /// style kind.
    /// </summary>
    /// <param name="kind">The border or outline style kind to represent.</param>
    public StyleBorderOutlineStyle(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>border-style</c> and <c>outline-style</c> values.</summary>
    public enum Kind
    {
        /// <summary>Specifies a dashed line style.</summary>
        [Description(description: "dashed")]
        Dashed,

        /// <summary>Specifies a dotted line style.</summary>
        [Description(description: "dotted")]
        Dotted,

        /// <summary>Specifies a double-line border style.</summary>
        [Description(description: "double")]
        Double,

        /// <summary>Specifies a 3D grooved border style that appears carved into the page.</summary>
        [Description(description: "groove")]
        Groove,

        /// <summary>Specifies an inset 3D border style that appears embedded.</summary>
        [Description(description: "inset")]
        Inset,

        /// <summary>Specifies that no border or outline is drawn.</summary>
        [Description(description: "none")]
        None,

        /// <summary>Specifies an outset 3D border style that appears raised.</summary>
        [Description(description: "outset")]
        Outset,

        /// <summary>Specifies a 3D ridged border style that appears raised from the surface.</summary>
        [Description(description: "ridge")]
        Ridge,

        /// <summary>Specifies a single solid line border or outline.</summary>
        [Description(description: "solid")]
        Solid
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>border-style</c> or <c>outline-style</c> value into a
    /// <see cref="StyleBorderOutlineStyle" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the style value.</param>
    /// <returns>A new <see cref="StyleBorderOutlineStyle" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleBorderOutlineStyle Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleBorderOutlineStyle(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleBorderOutlineStyle" /> instance.</summary>
    /// <param name="value">The string representation of the border or outline style to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleBorderOutlineStyle" /> instance or
    /// <see langword="null" /> if parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleBorderOutlineStyle? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleBorderOutlineStyle" /> instance.</summary>
    /// <param name="value">The string representation of the border or outline style.</param>
    /// <returns>A <see cref="StyleBorderOutlineStyle" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the string cannot be parsed into a valid border or outline style
    /// kind.
    /// </exception>
    public static implicit operator StyleBorderOutlineStyle(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleBorderOutlineStyle" /> instance to its string value.</summary>
    /// <param name="value">The <see cref="StyleBorderOutlineStyle" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS border or outline style string, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleBorderOutlineStyle? value) => (value?.Value).OrDefaultIfEmpty();
}
