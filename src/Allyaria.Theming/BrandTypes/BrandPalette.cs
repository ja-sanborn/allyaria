namespace Allyaria.Theming.BrandTypes;

/// <summary>
/// Represents a complete color palette for a brand theme, including background, foreground, accent, border, and decoration
/// colors derived from a base background color.
/// </summary>
public readonly record struct BrandPalette
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrandPalette" /> struct using the specified background color.
    /// </summary>
    /// <param name="backgroundColor">
    /// The base background <see cref="HexColor" /> from which all related palette colors are
    /// derived.
    /// </param>
    public BrandPalette(HexColor backgroundColor)
    {
        BackgroundColor = backgroundColor;
        ForegroundColor = BackgroundColor.ToForeground().EnsureContrast(background: BackgroundColor);
        CaretColor = ForegroundColor;
        AccentColor = ForegroundColor.ToAccent().EnsureContrast(background: BackgroundColor);
        BorderColor = BackgroundColor.ToAccent();
        OutlineColor = AccentColor;
        TextDecorationColor = AccentColor;
    }

    /// <summary>Gets the accent color used for highlights or emphasized elements.</summary>
    public HexColor AccentColor { get; }

    /// <summary>Gets the base background color of the palette.</summary>
    public HexColor BackgroundColor { get; }

    /// <summary>Gets the border color, typically derived from the background’s accent tone.</summary>
    public HexColor BorderColor { get; }

    /// <summary>Gets the caret color used for text input cursors, matching the foreground for consistency.</summary>
    public HexColor CaretColor { get; }

    /// <summary>Gets the primary foreground color that provides sufficient contrast against the background.</summary>
    public HexColor ForegroundColor { get; }

    /// <summary>
    /// Gets the outline color, typically aligned with the accent color for accessibility focus indicators.
    /// </summary>
    public HexColor OutlineColor { get; }

    /// <summary>Gets the text decoration color (e.g., underline) used for links and emphasized text.</summary>
    public HexColor TextDecorationColor { get; }
}
