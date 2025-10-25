namespace Allyaria.Theming.Themes;

public sealed record Theme(
    ThemeComponent Body,
    ThemeComponent BodyVariant,
    ThemeComponent Link,
    ThemeComponent LinkVariant,
    ThemeComponent Surface,
    ThemeComponent SurfaceVariant
)
{
    public static readonly Theme Empty = new(
        Body: ThemeComponent.Empty,
        BodyVariant: ThemeComponent.Empty,
        Link: ThemeComponent.Empty,
        LinkVariant: ThemeComponent.Empty,
        Surface: ThemeComponent.Empty,
        SurfaceVariant: ThemeComponent.Empty
    );

    public CssBuilder BuildCss(CssBuilder builder,
        ComponentType type,
        ThemeType themeType,
        ComponentState state,
        string? varPrefix = null)
    {
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-{type}";
        }

        switch (type)
        {
            case ComponentType.Body:
                builder = Body.BuildCss(builder: builder, themeType: themeType, state: state, varPrefix: prefix);

                break;

            case ComponentType.BodyVariant:
                builder = BodyVariant.BuildCss(builder: builder, themeType: themeType, state: state, varPrefix: prefix);

                break;
            case ComponentType.Link:
                builder = Link.BuildCss(builder: builder, themeType: themeType, state: state, varPrefix: prefix);

                break;
            case ComponentType.LinkVariant:
                builder = LinkVariant.BuildCss(builder: builder, themeType: themeType, state: state, varPrefix: prefix);

                break;
            case ComponentType.Surface:
                builder = Surface.BuildCss(builder: builder, themeType: themeType, state: state, varPrefix: prefix);

                break;

            case ComponentType.SurfaceVariant:
                builder = SurfaceVariant.BuildCss(
                    builder: builder, themeType: themeType, state: state, varPrefix: prefix
                );

                break;
        }

        return builder;
    }

    public Theme FromDefault(PaletteType paletteType, FontType fontType)
        => new(
            Body: ThemeComponent.FromDefault(paletteType: paletteType, fontType: fontType),
            BodyVariant: ThemeComponent.FromDefault(paletteType: paletteType, fontType: fontType),
            Link: ThemeComponent.FromDefault(paletteType: paletteType, fontType: fontType),
            LinkVariant: ThemeComponent.FromDefault(paletteType: paletteType, fontType: fontType),
            Surface: ThemeComponent.FromDefault(paletteType: paletteType, fontType: fontType),
            SurfaceVariant: ThemeComponent.FromDefault(paletteType: paletteType, fontType: fontType)
        );

    public Theme Merge(Theme other)
        => SetBody(Body.Merge(other.Body))
            .SetBodyVariant(BodyVariant.Merge(other.BodyVariant))
            .SetLink(Link.Merge(other.Link))
            .SetLinkVariant(LinkVariant.Merge(other.LinkVariant))
            .SetSurface(Surface.Merge(other.Surface))
            .SetSurfaceVariant(SurfaceVariant.Merge(other.SurfaceVariant));

    public Theme SetBody(ThemeComponent value)
        => this with
        {
            Body = value
        };

    public Theme SetBodyVariant(ThemeComponent value)
        => this with
        {
            BodyVariant = value
        };

    public Theme SetLink(ThemeComponent value)
        => this with
        {
            Link = value
        };

    public Theme SetLinkVariant(ThemeComponent value)
        => this with
        {
            LinkVariant = value
        };

    public Theme SetSurface(ThemeComponent value)
        => this with
        {
            Surface = value
        };

    public Theme SetSurfaceVariant(ThemeComponent value)
        => this with
        {
            SurfaceVariant = value
        };

    public string ToCss(ComponentType component, ThemeType themeType, ComponentState state, string? varPrefix = "")
        => BuildCss(
            builder: new CssBuilder(), type: component, themeType: themeType, state: state, varPrefix: varPrefix
        ).ToString();

    public string ToCssVars(ThemeType themeType)
    {
        var builder = new CssBuilder();
        var componentItems = Enum.GetValues<ComponentType>();
        var stateItems = Enum.GetValues<ComponentState>();

        foreach (var component in componentItems)
        {
            foreach (var state in stateItems)
            {
                builder = BuildCss(
                    builder: builder, type: component, themeType: themeType, state: state, varPrefix: General.VarPrefix
                );
            }
        }

        return builder.ToString();
    }

    public Theme Update(ColorPalette colorPalette, FontDefinition fontDefinition)
        => SetBody(Body.Update(colorPalette: colorPalette, fontDefinition: fontDefinition))
            .SetBodyVariant(BodyVariant.Update(colorPalette: colorPalette, fontDefinition: fontDefinition))
            .SetLink(Link.Update(colorPalette: colorPalette, fontDefinition: fontDefinition))
            .SetLinkVariant(LinkVariant.Update(colorPalette: colorPalette, fontDefinition: fontDefinition))
            .SetSurface(Surface.Update(colorPalette: colorPalette, fontDefinition: fontDefinition))
            .SetSurfaceVariant(SurfaceVariant.Update(colorPalette: colorPalette, fontDefinition: fontDefinition));

    public Theme UpdateFontFamily(FontDefinition fontDefinition)
        => SetBody(Body.UpdateFontFamily(fontDefinition))
            .SetBodyVariant(BodyVariant.UpdateFontFamily(fontDefinition))
            .SetLink(Link.UpdateFontFamily(fontDefinition))
            .SetLinkVariant(LinkVariant.UpdateFontFamily(fontDefinition))
            .SetSurface(Surface.UpdateFontFamily(fontDefinition))
            .SetSurfaceVariant(SurfaceVariant.UpdateFontFamily(fontDefinition));

    public Theme UpdatePalette(ColorPalette colorPalette)
        => SetBody(Body.UpdatePalette(colorPalette))
            .SetBodyVariant(BodyVariant.UpdatePalette(colorPalette))
            .SetLink(Link.UpdatePalette(colorPalette))
            .SetLinkVariant(LinkVariant.UpdatePalette(colorPalette))
            .SetSurface(Surface.UpdatePalette(colorPalette))
            .SetSurfaceVariant(SurfaceVariant.UpdatePalette(colorPalette));
}
