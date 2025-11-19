namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>justify-content</c>, <c>justify-items</c>, or <c>justify-self</c> value within the Allyaria theming
/// system. Provides a strongly typed wrapper for defining how items are distributed or aligned along the main axis of a
/// flex or grid container.
/// </summary>
public sealed record StyleJustify : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleJustify" /> record using the specified <see cref="Kind" /> value.
    /// </summary>
    /// <param name="kind">The justification kind that determines how elements are distributed along the main axis.</param>
    public StyleJustify(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS justification values for flexbox and grid layouts.</summary>
    public enum Kind
    {
        /// <summary>Centers items along the main axis.</summary>
        [Description(description: "center")]
        Center,

        /// <summary>Aligns items toward the end of the main axis.</summary>
        [Description(description: "end")]
        End,

        /// <summary>Aligns flex items to the end of the container’s main axis.</summary>
        [Description(description: "flex-end")]
        FlexEnd,

        /// <summary>Aligns flex items to the start of the container’s main axis.</summary>
        [Description(description: "flex-start")]
        FlexStart,

        /// <summary>Uses the default alignment behavior of the container.</summary>
        [Description(description: "normal")]
        Normal,

        /// <summary>Centers items without causing overflow; items are aligned safely within the container.</summary>
        [Description(description: "safe center")]
        SafeCenter,

        /// <summary>Distributes items evenly with space around each item.</summary>
        [Description(description: "space-around")]
        SpaceAround,

        /// <summary>
        /// Distributes items evenly with the first item at the start and last item at the end of the container.
        /// </summary>
        [Description(description: "space-between")]
        SpaceBetween,

        /// <summary>Distributes items evenly with equal spacing between and around them.</summary>
        [Description(description: "space-evenly")]
        SpaceEvenly,

        /// <summary>Aligns items toward the start of the main axis.</summary>
        [Description(description: "start")]
        Start,

        /// <summary>Stretches items to fill the available space along the main axis.</summary>
        [Description(description: "stretch")]
        Stretch,

        /// <summary>Centers items even if doing so may cause overflow (unsafe centering).</summary>
        [Description(description: "unsafe center")]
        UnsafeCenter
    }

    /// <summary>
    /// Parses a string representation of a CSS justification value into a <see cref="StyleJustify" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the justify value.</param>
    /// <returns>A new <see cref="StyleJustify" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleJustify Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleJustify(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleJustify" /> instance.</summary>
    /// <param name="value">The string representation of the justification value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleJustify" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleJustify? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleJustify" /> instance.</summary>
    /// <param name="value">The string representation of the justify value.</param>
    /// <returns>A <see cref="StyleJustify" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleJustify(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleJustify" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleJustify" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS justification string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleJustify? value) => value?.Value ?? string.Empty;
}
