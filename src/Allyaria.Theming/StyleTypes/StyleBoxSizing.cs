namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>box-sizing</c> value used within the Allyaria theming system. Provides a strongly typed wrapper for
/// defining how element dimensions are calculated (content-box or border-box).
/// </summary>
public sealed record StyleBoxSizing : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleBoxSizing" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The box-sizing kind that determines how width and height are calculated.</param>
    public StyleBoxSizing(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>box-sizing</c> property values.</summary>
    public enum Kind
    {
        /// <summary>Includes padding and border within the total width and height of the element.</summary>
        [Description(description: "border-box")]
        BorderBox,

        /// <summary>Calculates width and height based only on the content box, excluding padding and border.</summary>
        [Description(description: "content-box")]
        ContentBox,

        /// <summary>Inherits the box-sizing value from the parent element.</summary>
        [Description(description: "inherit")]
        Inherit
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>box-sizing</c> value into a <see cref="StyleBoxSizing" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the box-sizing value to parse.</param>
    /// <returns>A new <see cref="StyleBoxSizing" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleBoxSizing Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleBoxSizing(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleBoxSizing" /> instance.</summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleBoxSizing" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleBoxSizing? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleBoxSizing" /> instance.</summary>
    /// <param name="value">The string representation of the box-sizing value.</param>
    /// <returns>A <see cref="StyleBoxSizing" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleBoxSizing(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleBoxSizing" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleBoxSizing" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>box-sizing</c> string, or an empty string if <paramref name="value" /> is <see langword="null" />
    /// .
    /// </returns>
    public static implicit operator string(StyleBoxSizing? value) => (value?.Value).OrDefaultIfEmpty();
}
