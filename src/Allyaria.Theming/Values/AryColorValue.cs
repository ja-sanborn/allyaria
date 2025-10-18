using Allyaria.Theming.Contracts;
using Allyaria.Theming.Types;

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
    public AryColorValue(HexColor color)
        : base(string.Empty)
        => Color = color;

    /// <summary>Gets the underlying strongly-typed color components.</summary>
    public HexColor Color { get; }

    /// <summary>Gets the canonical string representation of the color in the form <c>#RRGGBBAA</c>.</summary>
    public override string Value => Color.ToString();

    /// <summary>
    /// Ensures this color meets a minimum WCAG contrast ratio against the specified background, currently enforcing 4.5:1 (AA
    /// for normal text), returning an adjusted foreground color when necessary.
    /// </summary>
    /// <param name="backgroundColor">The background color to contrast against.</param>
    /// <returns>
    /// An <see cref="AryColorValue" /> that meets or exceeds the default minimum non-text contrast ratio (4.5:1) versus
    /// <paramref name="backgroundColor" />. If the current color already meets the requirement, the original color is returned
    /// unchanged.
    /// </returns>
    public AryColorValue EnsureContrast(AryColorValue backgroundColor)
        => new(Color.EnsureMinimumContrast(backgroundColor.Color, 4.5));

    /// <summary>Parses a color string into an <see cref="AryColorValue" />.</summary>
    /// <param name="value">The color value to parse.</param>
    /// <returns>The parsed <see cref="AryColorValue" />.</returns>
    /// <exception cref="AryArgumentException">Thrown when parsing fails.</exception>
    public static AryColorValue Parse(string value) => new(new HexColor(value));

    /// <summary>
    /// Computes the appropriate border color for the element based on its background context, meeting WCAG non-text contrast
    /// (≥ 3:1) and preserving hue where possible.
    /// </summary>
    /// <param name="outerBackground">The surrounding background surface color.</param>
    /// <param name="componentFill">
    /// Optional fill color of the component; if <see langword="null" />, the border is treated as a divider on the background.
    /// </param>
    /// <param name="highContrast">
    /// When <see langword="true" />, returns a strong high-contrast outline (black on light surfaces or white on dark).
    /// </param>
    /// <returns>The resolved <see cref="AryColorValue" /> representing the border color.</returns>
    public AryColorValue ToBorder(AryColorValue outerBackground,
        AryColorValue? componentFill = null,
        bool highContrast = false)
        => new(Color.ToComponentBorderColor(outerBackground.Color, componentFill?.Color, 3.0, highContrast));

    /// <summary>
    /// Returns a desaturated version of this color for disabled UI states. This effect is preserved even in high-contrast mode
    /// so that disabled elements remain visibly distinct.
    /// </summary>
    /// <returns>A new <see cref="AryColorValue" /> instance with reduced saturation.</returns>
    public AryColorValue ToDisabled() => new(Color.Desaturate(0.6));

    /// <summary>
    /// Returns a brightened version of this color for dragged UI states. In high-contrast mode, returns this color unchanged.
    /// </summary>
    /// <param name="highContrast">If true, bypasses brightening.</param>
    /// <returns>A new <see cref="AryColorValue" /> for dragged state.</returns>
    public AryColorValue ToDragged(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.18));

    /// <summary>
    /// Returns a slightly elevated version of this color (Elevation 1). In high-contrast mode, returns this color unchanged.
    /// </summary>
    public AryColorValue ToElevation1(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.04));

    /// <summary>
    /// Returns a moderately elevated version of this color (Elevation 2). In high-contrast mode, returns this color unchanged.
    /// </summary>
    public AryColorValue ToElevation2(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.08));

    /// <summary>
    /// Returns a higher elevated version of this color (Elevation 3). In high-contrast mode, returns this color unchanged.
    /// </summary>
    public AryColorValue ToElevation3(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.12));

    /// <summary>
    /// Returns a strongly elevated version of this color (Elevation 4). In high-contrast mode, returns this color unchanged.
    /// </summary>
    public AryColorValue ToElevation4(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.16));

    /// <summary>
    /// Returns a slightly lighter version of this color for focused UI states. In high-contrast mode, returns this color
    /// unchanged.
    /// </summary>
    /// <param name="highContrast">If true, bypasses lightening.</param>
    /// <returns>A new <see cref="AryColorValue" /> for focused state.</returns>
    public AryColorValue ToFocused(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.1));

    /// <summary>
    /// Returns a slightly lighter version of this color for hovered UI states. In high-contrast mode, returns this color
    /// unchanged.
    /// </summary>
    /// <param name="highContrast">If true, bypasses lightening.</param>
    /// <returns>A new <see cref="AryColorValue" /> for hovered state.</returns>
    public AryColorValue ToHovered(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.06));

    /// <summary>
    /// Returns a noticeably lighter version of this color for pressed UI states. In high-contrast mode, returns this color
    /// unchanged.
    /// </summary>
    /// <param name="highContrast">If true, bypasses lightening.</param>
    /// <returns>A new <see cref="AryColorValue" /> for pressed state.</returns>
    public AryColorValue ToPressed(bool highContrast = false)
        => highContrast
            ? this
            : new AryColorValue(Color.ShiftLightness(0.14));

    /// <summary>Attempts to parse a color string into an <see cref="AryColorValue" />.</summary>
    /// <param name="value">The color value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed color if successful; otherwise <c>null</c>.</param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise <see langword="false" />.</returns>
    public static bool TryParse(string value, out AryColorValue? result)
    {
        try
        {
            result = new AryColorValue(new HexColor(value));

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
