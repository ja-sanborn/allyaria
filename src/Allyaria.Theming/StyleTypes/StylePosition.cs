namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>position</c> value within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining how an element is positioned in the document layout.
/// </summary>
public sealed record StylePosition : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StylePosition" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The position behavior to represent.</param>
    public StylePosition(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>position</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Positions the element relative to its nearest positioned ancestor. The element is removed from the normal document
        /// flow.
        /// </summary>
        [Description(description: "absolute")]
        Absolute,

        /// <summary>
        /// Positions the element relative to the viewport. The element remains fixed in the same position even when the page is
        /// scrolled.
        /// </summary>
        [Description(description: "fixed")]
        Fixed,

        /// <summary>
        /// Positions the element relative to its normal position. Offsets can be applied using <c>top</c>, <c>right</c>,
        /// <c>bottom</c>, and <c>left</c>.
        /// </summary>
        [Description(description: "relative")]
        Relative,

        /// <summary>Uses the normal document flow (default). Offsets have no effect when <c>position: static</c>.</summary>
        [Description(description: "static")]
        Static,

        /// <summary>
        /// Positions the element based on the user's scroll position. The element toggles between relative and fixed positioning
        /// depending on scroll boundaries.
        /// </summary>
        [Description(description: "sticky")]
        Sticky
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>position</c> value into a <see cref="StylePosition" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the position value.</param>
    /// <returns>A new <see cref="StylePosition" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StylePosition Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StylePosition(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StylePosition" /> instance.</summary>
    /// <param name="value">The string representation of the position value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StylePosition" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StylePosition? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StylePosition" /> instance.</summary>
    /// <param name="value">The string representation of the position value.</param>
    /// <returns>A <see cref="StylePosition" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StylePosition(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StylePosition" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StylePosition" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>position</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StylePosition? value) => (value?.Value).OrDefaultIfEmpty();
}
