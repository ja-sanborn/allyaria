namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>scroll-behavior</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// controlling the scrolling animation behavior for in-page navigation and programmatic scrolls.
/// </summary>
public sealed record StyleScrollBehavior : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleScrollBehavior" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The scroll behavior kind to represent.</param>
    public StyleScrollBehavior(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>scroll-behavior</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Uses the default instant scrolling behavior without animation.</summary>
        [Description(description: "auto")]
        Auto,

        /// <summary>Enables smooth animated scrolling behavior for in-page navigation or element scrolling.</summary>
        [Description(description: "smooth")]
        Smooth
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>scroll-behavior</c> value into a <see cref="StyleScrollBehavior" />
    /// instance.
    /// </summary>
    /// <param name="value">The string representation of the scroll-behavior value.</param>
    /// <returns>A new <see cref="StyleScrollBehavior" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleScrollBehavior Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleScrollBehavior(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleScrollBehavior" /> instance.</summary>
    /// <param name="value">The string representation of the scroll-behavior value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleScrollBehavior" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleScrollBehavior? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleScrollBehavior" /> instance.</summary>
    /// <param name="value">The string representation of the scroll-behavior value.</param>
    /// <returns>A <see cref="StyleScrollBehavior" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleScrollBehavior(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleScrollBehavior" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleScrollBehavior" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>scroll-behavior</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleScrollBehavior? value) => value?.Value ?? string.Empty;
}
