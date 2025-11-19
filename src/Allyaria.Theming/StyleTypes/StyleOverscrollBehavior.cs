namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>overscroll-behavior</c>, <c>overscroll-behavior-x</c>, or <c>overscroll-behavior-y</c> value within
/// the Allyaria theming system. Provides a strongly typed wrapper for controlling browser scroll chaining and overscroll
/// behavior.
/// </summary>
public sealed record StyleOverscrollBehavior : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleOverscrollBehavior" /> record using the specified <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The overscroll behavior to represent.</param>
    public StyleOverscrollBehavior(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>overscroll-behavior</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Allows the default scroll chaining behavior. Scrolls will propagate to parent containers or cause browser gestures
        /// (such as pull-to-refresh).
        /// </summary>
        [Description(description: "auto")]
        Auto,

        /// <summary>
        /// Prevents scroll chaining to parent containers, but allows default browser gestures (e.g., pull-to-refresh).
        /// </summary>
        [Description(description: "contain")]
        Contain,

        /// <summary>Completely disables scroll chaining and default browser gestures caused by reaching scroll limits.</summary>
        [Description(description: "none")]
        None
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>overscroll-behavior</c> value into a <see cref="StyleOverscrollBehavior" />
    /// instance.
    /// </summary>
    /// <param name="value">The string representation of the overscroll behavior value.</param>
    /// <returns>A new <see cref="StyleOverscrollBehavior" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleOverscrollBehavior Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleOverscrollBehavior(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleOverscrollBehavior" /> instance.</summary>
    /// <param name="value">The string representation of the overscroll behavior value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleOverscrollBehavior" /> instance or
    /// <see langword="null" /> if parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleOverscrollBehavior? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleOverscrollBehavior" /> instance.</summary>
    /// <param name="value">The string representation of the overscroll behavior value.</param>
    /// <returns>A <see cref="StyleOverscrollBehavior" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleOverscrollBehavior(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleOverscrollBehavior" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleOverscrollBehavior" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>overscroll-behavior</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleOverscrollBehavior? value) => (value?.Value).OrDefaultIfEmpty();
}
