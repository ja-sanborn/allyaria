using Allyaria.Abstractions.Types;
using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a strongly-typed, immutable color value used by Allyaria theming. Supports construction from RGBA
/// components, hex strings, Material Design color names, and CSS web color names. The canonical string form is
/// <c>#RRGGBBAA</c>.
/// </summary>
public sealed class AryColorValue : ValueBase
{
    /// <summary>Initializes a new instance of the <see cref="AryColorValue" /> class from RGBA components.</summary>
    /// <param name="red">The red component (0–255).</param>
    /// <param name="green">The green component (0–255).</param>
    /// <param name="blue">The blue component (0–255).</param>
    /// <param name="alpha">The alpha component (0.0–1.0). Values are clamped by <see cref="HexByte.FromNormalized" />.</param>
    public AryColorValue(byte red, byte green, byte blue, double alpha = 1.0)
        : base(string.Empty)
        => Color = new HexColor(new HexByte(red), new HexByte(green), new HexByte(blue), HexByte.FromNormalized(alpha));

    /// <summary>
    /// Initializes a new instance of the <see cref="AryColorValue" /> class from an existing <see cref="HexColor" /> without
    /// additional parsing or normalization.
    /// </summary>
    /// <param name="color">
    /// The fully-initialized <see cref="HexColor" /> to wrap. The resulting canonical string exposed via <see cref="Value" />
    /// remains in the form <c>#RRGGBBAA</c>.
    /// </param>
    /// <remarks>
    /// This overload directly assigns the provided <paramref name="color" /> to <see cref="Color" />. Use the string-based
    /// constructor when you need to parse Material or CSS color names.
    /// </remarks>
    public AryColorValue(HexColor color) => Color = color;

    /// <summary>Gets the underlying strongly-typed color components.</summary>
    public HexColor Color { get; }

    /// <summary>Gets the canonical string representation of the color in the form <c>#RRGGBBAA</c>.</summary>
    public override string Value => Color.ToString();

    /// <summary>Parses a color string into an <see cref="AryColorValue" />.</summary>
    /// <param name="value">The color value to parse.</param>
    /// <returns>The parsed <see cref="AryColorValue" />.</returns>
    /// <exception cref="AryArgumentException">Thrown when parsing fails.</exception>
    public static AryColorValue Parse(string value) => new(value);

    /// <summary>
    /// Returns a new <see cref="AryColorValue" /> representing a desaturated version of this color to be used for disabled UI
    /// states.
    /// </summary>
    /// <returns>A new <see cref="AryColorValue" /> instance with reduced saturation.</returns>
    public AryColorValue ToDisabled() => new(Color.Desaturate(0.6));

    /// <summary>
    /// Returns a new <see cref="AryColorValue" /> representing a brightened version of this color to be used for dragged UI
    /// states.
    /// </summary>
    /// <returns>A new <see cref="AryColorValue" /> instance with increased lightness.</returns>
    public AryColorValue ToDragged() => new(Color.ShiftLightness(0.18));

    /// <summary>Gets a new <see cref="AryColorValue" /> representing a slightly elevated color (Elevation 1).</summary>
    public AryColorValue ToElevation1() => new(Color.ShiftLightness(0.04));

    /// <summary>Gets a new <see cref="AryColorValue" /> representing a moderately elevated color (Elevation 2).</summary>
    public AryColorValue ToElevation2() => new(Color.ShiftLightness(0.08));

    /// <summary>Gets a new <see cref="AryColorValue" /> representing a higher elevated color (Elevation 3).</summary>
    public AryColorValue ToElevation3() => new(Color.ShiftLightness(0.12));

    /// <summary>Gets a new <see cref="AryColorValue" /> representing a strongly elevated color (Elevation 4).</summary>
    public AryColorValue ToElevation4() => new(Color.ShiftLightness(0.16));

    /// <summary>Gets a new <see cref="AryColorValue" /> representing the highest elevated color (Elevation 5).</summary>
    public AryColorValue ToElevation5() => new(Color.ShiftLightness(0.20));

    /// <summary>
    /// Returns a new <see cref="AryColorValue" /> representing a moderately lighter version of this color to be used for
    /// focused UI states.
    /// </summary>
    /// <returns>A new <see cref="AryColorValue" /> instance with increased lightness.</returns>
    public AryColorValue ToFocused() => new(Color.ShiftLightness(0.1));

    /// <summary>
    /// Returns a new <see cref="AryColorValue" /> representing a slightly lighter version of this color to be used for hovered
    /// UI states.
    /// </summary>
    /// <returns>A new <see cref="AryColorValue" /> instance with increased lightness.</returns>
    public AryColorValue ToHovered() => new(Color.ShiftLightness(0.06));

    /// <summary>
    /// Returns a new <see cref="AryColorValue" /> representing a noticeably lighter version of this color to be used for
    /// pressed UI states.
    /// </summary>
    /// <returns>A new <see cref="AryColorValue" /> instance with increased lightness.</returns>
    public AryColorValue ToPressed() => new(Color.ShiftLightness(0.14));

    /// <summary>Attempts to parse a color string into an <see cref="AryColorValue" />.</summary>
    /// <param name="value">The color value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed color if successful; otherwise <c>null</c>.</param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise <see langword="false" />.</returns>
    public static bool TryParse(string value, out AryColorValue? result)
    {
        try
        {
            result = new AryColorValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a <see cref="HexColor" /> to an <see cref="AryColorValue" />.</summary>
    /// <param name="value">The <see cref="HexColor" /> instance to wrap.</param>
    /// <returns>A new <see cref="AryColorValue" /> representing the same color.</returns>
    /// <remarks>
    /// This conversion allows seamless usage of <see cref="HexColor" /> values wherever an <see cref="AryColorValue" /> is
    /// expected, without explicit casting.
    /// </remarks>
    public static implicit operator AryColorValue(HexColor value) => new(value);

    /// <summary>Implicitly converts an <see cref="AryColorValue" /> to its underlying <see cref="HexColor" />.</summary>
    /// <param name="value">The <see cref="AryColorValue" /> instance to extract the color from.</param>
    /// <returns>The underlying <see cref="HexColor" /> representation.</returns>
    /// <remarks>
    /// Enables direct interoperation with APIs that require <see cref="HexColor" /> values, preserving full RGBA fidelity.
    /// </remarks>
    public static implicit operator HexColor(AryColorValue value) => value.Color;

    /// <summary>Implicitly converts a color string into an <see cref="AryColorValue" />.</summary>
    /// <param name="value">
    /// A color expressed as a hex string (e.g., <c>#RRGGBB</c> or <c>#RRGGBBAA</c>), a Material Design name (e.g.,
    /// <c>"Deep Purple 200"</c>), or a CSS web color name (e.g., <c>"dodgerblue"</c>).
    /// </param>
    /// <returns>A new <see cref="AryColorValue" /> parsed from the provided string.</returns>
    /// <remarks>
    /// This implicit conversion simplifies creation of themed color values from literal strings in markup or code.
    /// </remarks>
    public static implicit operator AryColorValue(string value) => new(value);

    /// <summary>
    /// Implicitly converts an <see cref="AryColorValue" /> to its canonical string representation (<c>#RRGGBBAA</c>).
    /// </summary>
    /// <param name="value">The color value to convert.</param>
    /// <returns>The canonical uppercase <c>#RRGGBBAA</c> string representation of the color.</returns>
    /// <remarks>
    /// Enables direct binding or serialization of <see cref="AryColorValue" /> instances to their string form without calling
    /// <see cref="Value" /> explicitly.
    /// </remarks>
    public static implicit operator string(AryColorValue value) => value.Value;
}
