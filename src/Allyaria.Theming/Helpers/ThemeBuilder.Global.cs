namespace Allyaria.Theming.Helpers;

internal sealed partial class ThemeBuilder
{
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

    private void CreateGlobalFocus(bool isHighContrast)
        => ApplyTheme(
            applier: new ThemeOutlineApplier(
                themeMapper: _mapper,
                isHighContrast: isHighContrast,
                componentType: ComponentType.GlobalFocus,
                paletteType: PaletteType.Surface
            )
        );

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
}
