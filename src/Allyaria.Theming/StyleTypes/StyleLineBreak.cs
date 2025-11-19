namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>line-break</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// controlling line-breaking behavior for text content.
/// </summary>
public sealed record StyleLineBreak : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleLineBreak" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The line-breaking behavior kind to represent.</param>
    public StyleLineBreak(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>line-break</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Allows unrestrained line breaks between any two characters for maximum flexibility. Useful for text with long words or
        /// URLs.
        /// </summary>
        [Description(description: "anywhere")]
        Anywhere,

        /// <summary>Uses the default line-breaking rules based on the document’s language and script.</summary>
        [Description(description: "auto")]
        Auto,

        /// <summary>
        /// Provides looser line-breaking rules that may allow additional break opportunities, often used for East Asian text to
        /// improve readability.
        /// </summary>
        [Description(description: "loose")]
        Loose,

        /// <summary>Applies standard line-breaking rules following the language’s usual behavior.</summary>
        [Description(description: "normal")]
        Normal,

        /// <summary>Uses the most restrictive line-breaking rules, allowing breaks only where absolutely necessary.</summary>
        [Description(description: "strict")]
        Strict
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>line-break</c> value into a <see cref="StyleLineBreak" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the line-break value.</param>
    /// <returns>A new <see cref="StyleLineBreak" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleLineBreak Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleLineBreak(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleLineBreak" /> instance.</summary>
    /// <param name="value">The string representation of the line-break value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleLineBreak" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleLineBreak? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleLineBreak" /> instance.</summary>
    /// <param name="value">The string representation of the line-break value.</param>
    /// <returns>A <see cref="StyleLineBreak" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleLineBreak(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleLineBreak" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleLineBreak" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>line-break</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleLineBreak? value) => (value?.Value).OrDefaultIfEmpty();
}
