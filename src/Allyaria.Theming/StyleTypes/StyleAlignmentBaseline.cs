namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>alignment-baseline</c> value used within the Allyaria theming system. Provides a strongly typed
/// wrapper around standard CSS alignment-baseline keywords.
/// </summary>
public sealed record StyleAlignmentBaseline : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleAlignmentBaseline" /> record with the specified baseline alignment
    /// kind.
    /// </summary>
    /// <param name="kind">The baseline alignment kind to represent.</param>
    public StyleAlignmentBaseline(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>alignment-baseline</c> values.</summary>
    public enum Kind
    {
        /// <summary>
        /// Aligns the element with the alphabetic baseline of its parent. Typically used for Latin-based scripts.
        /// </summary>
        [Description(description: "alphabetic")]
        Alphabetic,

        /// <summary>Aligns the element with the dominant baseline of its parent.</summary>
        [Description(description: "baseline")]
        Baseline,

        /// <summary>
        /// Aligns the element so that the geometric center of its box aligns with the parent's alignment baseline.
        /// </summary>
        [Description(description: "central")]
        Central,

        /// <summary>Aligns the element with the ideographic baseline used for East Asian text.</summary>
        [Description(description: "ideographic")]
        Ideographic,

        /// <summary>Aligns the element with the mathematical baseline, used for math formulas or similar content.</summary>
        [Description(description: "mathematical")]
        Mathematical,

        /// <summary>Aligns the element with the vertical middle of its parent’s box.</summary>
        [Description(description: "middle")]
        Middle,

        /// <summary>Aligns the element with the bottom of the parent's text content area.</summary>
        [Description(description: "text-bottom")]
        TextBottom,

        /// <summary>Aligns the element with the top of the parent's text content area.</summary>
        [Description(description: "text-top")]
        TextTop
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>alignment-baseline</c> value into a <see cref="StyleAlignmentBaseline" />
    /// instance.
    /// </summary>
    /// <param name="value">The string representation of the alignment-baseline value.</param>
    /// <returns>A new <see cref="StyleAlignmentBaseline" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleAlignmentBaseline Parse(string? value)
        => value.TryParseEnum<Kind>(result: out var kind)
            ? new StyleAlignmentBaseline(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleAlignmentBaseline" /> instance.</summary>
    /// <param name="value">The string representation of the alignment-baseline value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleAlignmentBaseline" /> instance or <see langword="null" />
    /// if parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleAlignmentBaseline? result)
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

    /// <summary>Implicitly converts a string into a <see cref="StyleAlignmentBaseline" /> instance.</summary>
    /// <param name="value">The string value representing the baseline alignment.</param>
    /// <returns>A <see cref="StyleAlignmentBaseline" /> instance equivalent to the provided value.</returns>
    /// <exception cref="AryArgumentException">Thrown when the string cannot be parsed into a valid alignment baseline kind.</exception>
    public static implicit operator StyleAlignmentBaseline(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleAlignmentBaseline" /> instance into its string value.</summary>
    /// <param name="value">The <see cref="StyleAlignmentBaseline" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>alignment-baseline</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleAlignmentBaseline? value) => (value?.Value).OrDefaultIfEmpty();
}
