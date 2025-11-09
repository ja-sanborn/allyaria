namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>text-transform</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining text casing transformations applied to content.
/// </summary>
public sealed record StyleTextTransform : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleTextTransform" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The text transformation behavior to represent.</param>
    public StyleTextTransform(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>text-transform</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Transforms the first character of each word to uppercase while leaving the rest unchanged.</summary>
        [Description(description: "capitalize")]
        Capitalize,

        /// <summary>Transforms all characters to lowercase.</summary>
        [Description(description: "lowercase")]
        Lowercase,

        /// <summary>Prevents any case transformation; text appears as originally authored.</summary>
        [Description(description: "none")]
        None,

        /// <summary>Transforms all characters to uppercase.</summary>
        [Description(description: "uppercase")]
        Uppercase
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>text-transform</c> value into a <see cref="StyleTextTransform" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the text-transform value.</param>
    /// <returns>A new <see cref="StyleTextTransform" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleTextTransform Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleTextTransform(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleTextTransform" /> instance.</summary>
    /// <param name="value">The string representation of the text-transform value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleTextTransform" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleTextTransform? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleTextTransform" /> instance.</summary>
    /// <param name="value">The string representation of the text-transform value.</param>
    /// <returns>A <see cref="StyleTextTransform" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleTextTransform(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleTextTransform" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleTextTransform" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>text-transform</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleTextTransform? value) => (value?.Value).OrDefaultIfEmpty();
}
