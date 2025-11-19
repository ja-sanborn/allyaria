namespace Allyaria.Theming.Helpers;

/// <summary>
/// Provides the core logic for constructing Allyaria themes by combining colors, typography, and layout rules into a
/// complete <see cref="Theme" />.
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="ThemeBuilder" /> orchestrates creation of default themes for light, dark, and high-contrast modes by
///     applying a series of <see cref="ThemeApplierBase" />-derived builders.
///     </para>
///     <para>
///     It supports programmatic customization through <see cref="Set(ThemeUpdater)" /> and can optionally accept a
///     <see cref="Brand" /> definition to influence default colors and fonts.
///     </para>
/// </remarks>
internal sealed class ThemeBuilder
{
    /// <summary>Indicates whether the builder has completed an initialization pass.</summary>
    private bool _isReady;

    /// <summary>Provides color, font, and palette mappings for the current theme context.</summary>
    private ThemeMapper _mapper = new();

    /// <summary>The underlying <see cref="Theme" /> being composed.</summary>
    private Theme _theme = new();

    /// <summary>Applies all <see cref="ThemeUpdater" /> entries from the specified applier to the current theme.</summary>
    /// <param name="applier">A <see cref="ThemeApplierBase" /> instance providing theme updates.</param>
    private void ApplyTheme(ThemeApplierBase applier)
    {
        foreach (var updater in applier)
        {
            _theme.Set(updater: updater);
        }
    }

    /// <summary>Finalizes and returns the built <see cref="Theme" />, resetting the builder for reuse.</summary>
    /// <returns>A completed <see cref="Theme" /> instance representing the composed theme.</returns>
    public Theme Build()
    {
        if (!_isReady)
        {
            Create();
        }

        var theme = _theme;

        _mapper = new ThemeMapper();
        _theme = new Theme();
        _isReady = false;

        return theme;
    }

    /// <summary>Initializes a new base theme structure using the provided <see cref="Brand" /> configuration.</summary>
    /// <param name="brand">
    /// An optional <see cref="Brand" /> instance providing color and font defaults. If omitted, system defaults are used.
    /// </param>
    /// <returns>The current <see cref="ThemeBuilder" /> instance for fluent configuration.</returns>
    public ThemeBuilder Create(Brand? brand = null)
    {
        _mapper = new ThemeMapper(brand: brand);
        _theme = new Theme();

        for (var contrast = 0; contrast < 2; contrast++)
        {
            var isHighContrast = contrast is 1;

            CreateGlobalBody(isHighContrast: isHighContrast);
            CreateGlobalFocus(isHighContrast: isHighContrast);
            CreateGlobalHtml(isHighContrast: isHighContrast);

            CreateHeading1(isHighContrast: isHighContrast);
            CreateHeading2(isHighContrast: isHighContrast);
            CreateHeading3(isHighContrast: isHighContrast);
            CreateHeading4(isHighContrast: isHighContrast);
            CreateHeading5(isHighContrast: isHighContrast);
            CreateHeading6(isHighContrast: isHighContrast);

            CreateLink(isHighContrast: isHighContrast);
            CreateSurface(isHighContrast: isHighContrast);
            CreateText(isHighContrast: isHighContrast);
        }

        _isReady = true;

        return this;
    }

    /// <summary>Configures base body styles such as colors, fonts, layout, and overflow behavior.</summary>
    /// <param name="isHighContrast">Indicates whether high-contrast adjustments are applied.</param>
    private void CreateGlobalBody(bool isHighContrast)
    {
        ApplyTheme(
            applier: new ThemeColorApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalBody,
                paletteType: PaletteType.Surface,
                isVariant: false,
                hasBackground: true,
                isOutline: false
            )
        );

        ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalBody,
                fontFace: FontFaceType.SansSerif,
                fontSize: Sizing.Relative,
                lineHeight: "1.5"
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalBody,
                styleType: StyleType.Margin,
                value: new StyleLength(value: Sizing.Size0)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalBody,
                styleType: StyleType.Padding,
                value: new StyleLength(value: Sizing.Size0)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalBody,
                styleType: StyleType.MinHeight,
                value: new StyleLength(value: Sizing.Full)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalBody,
                styleType: StyleType.OverflowBlock,
                value: new StyleOverflow(kind: StyleOverflow.Kind.Clip)
            )
        );
    }

    /// <summary>Configures global focus outline behavior.</summary>
    /// <param name="isHighContrast">Indicates whether high-contrast adjustments are applied.</param>
    private void CreateGlobalFocus(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeOutlineApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalFocus,
                paletteType: PaletteType.Surface
            )
        );

    /// <summary>Configures root HTML-level styles including sizing, box model, and scrolling behavior.</summary>
    /// <param name="isHighContrast">Indicates whether high-contrast adjustments are applied.</param>
    private void CreateGlobalHtml(bool isHighContrast)
    {
        ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalHtml,
                fontSize: Sizing.Size3,
                lineHeight: "1.5"
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalHtml,
                styleType: StyleType.Margin,
                value: new StyleLength(value: Sizing.Size0)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalHtml,
                styleType: StyleType.Padding,
                value: new StyleLength(value: Sizing.Size0)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalHtml,
                styleType: StyleType.BoxSizing,
                value: new StyleBoxSizing(kind: StyleBoxSizing.Kind.BorderBox)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalHtml,
                styleType: StyleType.MinHeight,
                value: new StyleLength(value: Sizing.Full)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalHtml,
                styleType: StyleType.ScrollBehavior,
                value: new StyleScrollBehavior(kind: StyleScrollBehavior.Kind.Smooth)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalHtml,
                styleType: StyleType.TextSizeAdjust,
                value: new StyleLength(value: Sizing.Full)
            )
        );
    }

    /// <summary>Creates typographic rules for &lt;h1&gt; headings.</summary>
    private void CreateHeading1(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading1,
                fontSize: Sizing.RelativeLarge4,
                fontWeight: StyleFontWeight.Kind.Weight700,
                lineHeight: "1.2",
                marginBottom: Sizing.RelativeLarge1
            )
        );

    /// <summary>Creates typographic rules for &lt;h2&gt; headings.</summary>
    private void CreateHeading2(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading2,
                fontSize: Sizing.RelativeLarge3,
                fontWeight: StyleFontWeight.Kind.Weight700,
                lineHeight: "1.25",
                marginBottom: Sizing.Relative
            )
        );

    /// <summary>Creates typographic rules for &lt;h3&gt; headings.</summary>
    private void CreateHeading3(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading3,
                fontSize: Sizing.RelativeLarge2,
                fontWeight: StyleFontWeight.Kind.Weight600,
                lineHeight: "1.3",
                marginBottom: Sizing.RelativeSmall2
            )
        );

    /// <summary>Creates typographic rules for &lt;h4&gt; headings.</summary>
    private void CreateHeading4(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading4,
                fontSize: Sizing.RelativeLarge1,
                fontWeight: StyleFontWeight.Kind.Weight600,
                lineHeight: "1.4",
                marginBottom: Sizing.RelativeSmall3
            )
        );

    /// <summary>Creates typographic rules for &lt;h5&gt; headings.</summary>
    private void CreateHeading5(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading5,
                fontSize: Sizing.Relative,
                fontWeight: StyleFontWeight.Kind.Weight600,
                lineHeight: "1.5",
                marginBottom: Sizing.RelativeSmall4
            )
        );

    /// <summary>Creates typographic rules for &lt;h6&gt; headings.</summary>
    private void CreateHeading6(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Heading6,
                fontSize: Sizing.RelativeSmall1,
                fontWeight: StyleFontWeight.Kind.Weight500,
                lineHeight: "1.5",
                marginBottom: Sizing.RelativeSmall4
            )
        );

    /// <summary>Creates link styles, including color, text decoration, and thickness.</summary>
    private void CreateLink(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Link,
                paletteType: PaletteType.Primary,
                textDecorationLine: StyleTextDecorationLine.Kind.Underline,
                textDecorationStyle: StyleTextDecorationStyle.Kind.Solid,
                textDecorationThickness: Sizing.Thin
            )
        );

    /// <summary>Configures background and spacing for surface containers.</summary>
    private void CreateSurface(bool isHighContrast)
    {
        ApplyTheme(
            applier: new ThemeColorApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Surface,
                paletteType: PaletteType.Elevation1,
                isVariant: false,
                hasBackground: true,
                isOutline: false
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Surface,
                styleType: StyleType.Margin,
                value: new StyleLength(value: Sizing.Size2)
            )
        );

        ApplyTheme(
            applier: new ThemeApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Surface,
                styleType: StyleType.Padding,
                value: new StyleLength(value: Sizing.Size3)
            )
        );
    }

    /// <summary>Creates typography settings for general text content.</summary>
    private void CreateText(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeFontApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.Text,
                fontSize: Sizing.Relative,
                lineHeight: "1.5",
                marginBottom: Sizing.Relative
            )
        );

    /// <summary>Applies a <see cref="ThemeUpdater" /> to modify an existing theme definition.</summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> containing the desired updates.</param>
    /// <returns>The same <see cref="ThemeBuilder" /> instance for fluent chaining.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when an invalid update is attempted, such as modifying system or high-contrast themes, or restricted component
    /// states.
    /// </exception>
    public ThemeBuilder Set(ThemeUpdater updater)
    {
        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.System))
        {
            throw new AryArgumentException(
                message: "System theme cannot be set directly.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ComponentStates.Contains(value: ComponentState.Hidden) ||
            updater.Navigator.ComponentStates.Contains(value: ComponentState.ReadOnly))
        {
            throw new AryArgumentException(
                message: "Hidden and read-only states cannot be set directly.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastDark) ||
            updater.Navigator.ThemeTypes.Contains(value: ThemeType.HighContrastLight))
        {
            throw new AryArgumentException(
                message: "Cannot alter High Contrast themes.", argName: nameof(updater.Value)
            );
        }

        if (updater.Navigator.ComponentStates.Contains(value: ComponentState.Focused) &&
            (updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineOffset) ||
                updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineStyle) ||
                updater.Navigator.StyleTypes.Contains(value: StyleType.OutlineWidth)))
        {
            throw new AryArgumentException(
                message: "Cannot change focused outline offset, style or width.", argName: nameof(updater.Value)
            );
        }

        _theme = _theme.Set(updater: updater);

        return this;
    }
}
