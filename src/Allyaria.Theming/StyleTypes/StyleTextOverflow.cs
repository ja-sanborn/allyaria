namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>text-overflow</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining how overflowing inline text is rendered when it exceeds its container.
/// </summary>
public sealed record StyleTextOverflow : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleTextOverflow" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The text overflow behavior to represent.</param>
    public StyleTextOverflow(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>text-overflow</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Clips overflowing text content without adding any visual indicator.</summary>
        [Description(description: "clip")]
        Clip,

        /// <summary>Displays an ellipsis ("…") to represent clipped text when content overflows.</summary>
        [Description(description: "ellipsis")]
        Ellipsis
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>text-overflow</c> value into a <see cref="StyleTextOverflow" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the text-overflow value.</param>
    /// <returns>A new <see cref="StyleTextOverflow" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleTextOverflow Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleTextOverflow(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleTextOverflow" /> instance.</summary>
    /// <param name="value">The string representation of the text-overflow value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleTextOverflow" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleTextOverflow? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleTextOverflow" /> instance.</summary>
    /// <param name="value">The string representation of the text-overflow value.</param>
    /// <returns>A <see cref="StyleTextOverflow" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleTextOverflow(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleTextOverflow" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleTextOverflow" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>text-overflow</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleTextOverflow? value) => value?.Value ?? string.Empty;
}
