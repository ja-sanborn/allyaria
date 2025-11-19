namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>text-orientation</c> value within the Allyaria theming system. Provides a strongly typed wrapper
/// for defining the orientation of text characters in vertical writing modes.
/// </summary>
public sealed record StyleTextOrientation : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleTextOrientation" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The text orientation type to represent.</param>
    public StyleTextOrientation(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>text-orientation</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Uses mixed orientation for characters: upright for ideographic and vertical forms, sideways for others such as Latin
        /// characters.
        /// </summary>
        [Description(description: "mixed")]
        Mixed,

        /// <summary>Rotates all characters sideways, as if in horizontal layout, within a vertical writing mode.</summary>
        [Description(description: "sideways")]
        Sideways,

        /// <summary>Displays all characters upright within a vertical writing mode.</summary>
        [Description(description: "upright")]
        Upright
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>text-orientation</c> value into a <see cref="StyleTextOrientation" />
    /// instance.
    /// </summary>
    /// <param name="value">The string representation of the text-orientation value.</param>
    /// <returns>A new <see cref="StyleTextOrientation" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleTextOrientation Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleTextOrientation(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleTextOrientation" /> instance.</summary>
    /// <param name="value">The string representation of the text-orientation value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleTextOrientation" /> instance or <see langword="null" />
    /// if parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleTextOrientation? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleTextOrientation" /> instance.</summary>
    /// <param name="value">The string representation of the text-orientation value.</param>
    /// <returns>A <see cref="StyleTextOrientation" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleTextOrientation(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleTextOrientation" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleTextOrientation" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>text-orientation</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleTextOrientation? value) => (value?.Value).OrDefaultIfEmpty();
}
