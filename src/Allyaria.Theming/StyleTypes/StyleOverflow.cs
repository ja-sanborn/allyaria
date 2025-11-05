namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>overflow</c>, <c>overflow-x</c>, or <c>overflow-y</c> value within the Allyaria theming system.
/// Provides a strongly typed wrapper around standard overflow behaviors controlling how content exceeding its container is
/// handled.
/// </summary>
public sealed record StyleOverflow : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleOverflow" /> record using the specified overflow <see cref="Kind" />
    /// value.
    /// </summary>
    /// <param name="kind">The overflow behavior to represent.</param>
    public StyleOverflow(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>overflow</c> property values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Displays scrollbars when content overflows the element’s box. The user agent determines whether to clip or show
        /// scrollbars automatically.
        /// </summary>
        [Description(description: "auto")]
        Auto,

        /// <summary>
        /// Clips the overflowing content without adding scrollbars. Hidden content cannot be scrolled or accessed visually.
        /// </summary>
        [Description(description: "clip")]
        Clip,

        /// <summary>
        /// Clips the content and hides the overflowed portion, similar to <c>clip</c>, but scrollbars may still be rendered in
        /// some browsers for backward compatibility.
        /// </summary>
        [Description(description: "hidden")]
        Hidden,

        /// <summary>Always shows scrollbars, allowing users to scroll overflowing content.</summary>
        [Description(description: "scroll")]
        Scroll,

        /// <summary>Content is not clipped and may overflow the element’s box visibly.</summary>
        [Description(description: "visible")]
        Visible
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>overflow</c> value into a <see cref="StyleOverflow" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the overflow value.</param>
    /// <returns>A new <see cref="StyleOverflow" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleOverflow Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleOverflow(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleOverflow" /> instance.</summary>
    /// <param name="value">The string representation of the overflow value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleOverflow" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleOverflow? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleOverflow" /> instance.</summary>
    /// <param name="value">The string representation of the overflow value.</param>
    /// <returns>A <see cref="StyleOverflow" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleOverflow(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleOverflow" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleOverflow" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>overflow</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleOverflow? value) => value?.Value ?? string.Empty;
}
