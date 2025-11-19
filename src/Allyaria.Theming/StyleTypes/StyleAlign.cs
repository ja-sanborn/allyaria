namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS alignment value used within the Allyaria theming system. Provides typed access to standard CSS
/// alignment keywords (e.g., center, start, stretch) through the <see cref="Kind" /> enumeration.
/// </summary>
public sealed record StyleAlign : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleAlign" /> record using the specified alignment kind.
    /// </summary>
    /// <param name="kind">The alignment kind to represent (e.g., <see cref="Kind.Center" />).</param>
    public StyleAlign(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS alignment values.</summary>
    public enum Kind
    {
        /// <summary>Aligns the baseline of the element with the baseline of its parent.</summary>
        [Description(description: "baseline")]
        Baseline,

        /// <summary>Centers the element along the alignment axis.</summary>
        [Description(description: "center")]
        Center,

        /// <summary>Aligns the element to the end of the alignment container.</summary>
        [Description(description: "end")]
        End,

        /// <summary>Aligns the first baseline of the element with the first baseline of its parent.</summary>
        [Description(description: "first baseline")]
        FirstBaseline,

        /// <summary>Aligns the element to the end of the flex container's cross axis.</summary>
        [Description(description: "flex-end")]
        FlexEnd,

        /// <summary>Aligns the element to the start of the flex container's cross axis.</summary>
        [Description(description: "flex-start")]
        FlexStart,

        /// <summary>Aligns the last baseline of the element with the last baseline of its parent.</summary>
        [Description(description: "last baseline")]
        LastBaseline,

        /// <summary>Represents the default alignment behavior for the property.</summary>
        [Description(description: "normal")]
        Normal,

        /// <summary>Centers the element safely without overflowing its container.</summary>
        [Description(description: "safe center")]
        SafeCenter,

        /// <summary>Distributes elements evenly with space around each item.</summary>
        [Description(description: "space-around")]
        SpaceAround,

        /// <summary>Distributes elements evenly with the first element at the start and last at the end.</summary>
        [Description(description: "space-between")]
        SpaceBetween,

        /// <summary>Distributes elements evenly with equal spacing between and around them.</summary>
        [Description(description: "space-evenly")]
        SpaceEvenly,

        /// <summary>Aligns the element to the start of the alignment container.</summary>
        [Description(description: "start")]
        Start,

        /// <summary>Stretches the element to fill the container along the alignment axis.</summary>
        [Description(description: "stretch")]
        Stretch,

        /// <summary>Centers the element even if it may cause overflow (unsafe).</summary>
        [Description(description: "unsafe center")]
        UnsafeCenter
    }

    /// <summary>Parses the provided string value into a <see cref="StyleAlign" /> instance.</summary>
    /// <param name="value">The string representation of a CSS alignment value.</param>
    /// <returns>A new <see cref="StyleAlign" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not match any valid
    /// <see cref="Kind" />.
    /// </exception>
    public static StyleAlign Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleAlign(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleAlign" /> instance.</summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleAlign" /> instance or <see langword="null" /> if parsing
    /// failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing was successful; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleAlign? result)
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

    /// <summary>Implicitly converts a string to a <see cref="StyleAlign" /> instance.</summary>
    /// <param name="value">The string representation of the alignment value.</param>
    /// <returns>A <see cref="StyleAlign" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">Thrown when the string cannot be parsed into a valid alignment kind.</exception>
    public static implicit operator StyleAlign(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleAlign" /> instance to its string value.</summary>
    /// <param name="value">The <see cref="StyleAlign" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS alignment string or an empty string if <paramref name="value" /> is <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleAlign? value) => (value?.Value).OrDefaultIfEmpty();
}
