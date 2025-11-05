namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>overflow-wrap</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// controlling word-breaking behavior within elements when text exceeds its container.
/// </summary>
public sealed record StyleOverflowWrap : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleOverflowWrap" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The overflow-wrap behavior to represent.</param>
    public StyleOverflowWrap(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>overflow-wrap</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Allows words to be broken at arbitrary points if necessary to prevent overflow. This mode provides maximum flexibility
        /// to avoid overflow.
        /// </summary>
        [Description(description: "anywhere")]
        Anywhere,

        /// <summary>
        /// Breaks words only if necessary to prevent overflow, similar to <c>normal</c>, but allows breaking within words when
        /// they would otherwise overflow.
        /// </summary>
        [Description(description: "break-word")]
        BreakWord,

        /// <summary>
        /// Uses default line-breaking rules; words will only wrap at allowed break points (such as spaces or hyphens).
        /// </summary>
        [Description(description: "normal")]
        Normal
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>overflow-wrap</c> value into a <see cref="StyleOverflowWrap" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the overflow-wrap value.</param>
    /// <returns>A new <see cref="StyleOverflowWrap" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleOverflowWrap Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleOverflowWrap(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleOverflowWrap" /> instance.</summary>
    /// <param name="value">The string representation of the overflow-wrap value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleOverflowWrap" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleOverflowWrap? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleOverflowWrap" /> instance.</summary>
    /// <param name="value">The string representation of the overflow-wrap value.</param>
    /// <returns>A <see cref="StyleOverflowWrap" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleOverflowWrap(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleOverflowWrap" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleOverflowWrap" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>overflow-wrap</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleOverflowWrap? value) => value?.Value ?? string.Empty;
}
