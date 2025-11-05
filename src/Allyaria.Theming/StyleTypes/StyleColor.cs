namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>color</c> value within the Allyaria theming system. Provides a strongly typed wrapper around color
/// values supporting parsing and conversion to and from <see cref="HexColor" /> and string representations.
/// </summary>
public sealed record StyleColor : StyleValueBase
{
    /// <summary>Initializes a new instance of the <see cref="StyleColor" /> record using a string color value.</summary>
    /// <param name="value">The color value as a string, typically in hexadecimal, RGB(A), or named CSS color format.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="value" /> cannot be parsed into a valid <see cref="HexColor" />.
    /// </exception>
    public StyleColor(string value)
        : this(color: new HexColor(value: value)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleColor" /> record using a <see cref="HexColor" /> instance.
    /// </summary>
    /// <param name="color">The <see cref="HexColor" /> instance to represent.</param>
    public StyleColor(HexColor color)
        : base(value: color.ToString())
        => Color = color;

    /// <summary>Gets the strongly typed <see cref="HexColor" /> value represented by this instance.</summary>
    public HexColor Color { get; }

    /// <summary>Parses a string representation of a CSS color into a <see cref="StyleColor" /> instance.</summary>
    /// <param name="value">The string representation of the color to parse. Supports hexadecimal, RGB(A), and named colors.</param>
    /// <returns>A new <see cref="StyleColor" /> instance representing the parsed color.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="value" /> cannot be parsed into a valid
    /// color format.
    /// </exception>
    public static StyleColor Parse(string? value) => new(value: value ?? string.Empty);

    /// <summary>Attempts to parse a string representation of a CSS color into a <see cref="StyleColor" /> instance.</summary>
    /// <param name="value">The string representation of the color value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleColor" /> instance or <see langword="null" /> if parsing
    /// failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleColor? result)
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

    /// <summary>Implicitly converts a string value into a <see cref="StyleColor" /> instance.</summary>
    /// <param name="value">The string representation of the color value.</param>
    /// <returns>A <see cref="StyleColor" /> instance representing the provided value.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided string cannot be parsed into a valid color format.</exception>
    public static implicit operator StyleColor(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleColor" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleColor" /> instance to convert.</param>
    /// <returns>
    /// The CSS string representation of the color (typically in hex format), or an empty string if <paramref name="value" />
    /// is <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleColor? value) => value?.Value ?? string.Empty;
}
