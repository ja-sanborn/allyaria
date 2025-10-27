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

        if (!string.IsNullOrWhiteSpace(value: prefix))
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

    public static Theme FromBrand(BrandTheme brand,
        FontType fontType,
        PaletteType paletteType)
    {
        var body = ThemeComponent.FromBrand(brand: brand, fontType: fontType, paletteType: paletteType);
        var bodyVariant = ThemeComponent.FromBrand(brand: brand, fontType: fontType, paletteType: paletteType);
        var link = ThemeComponent.FromBrand(brand: brand, fontType: fontType, paletteType: paletteType);
        var linkVariant = ThemeComponent.FromBrand(brand: brand, fontType: fontType, paletteType: paletteType);
        var surface = ThemeComponent.FromBrand(brand: brand, fontType: fontType, paletteType: paletteType);
        var surfaceVariant = ThemeComponent.FromBrand(brand: brand, fontType: fontType, paletteType: paletteType);

        TODO remove background color (cascade down) - Replace instead of Merge, to cascade down?

        return new Theme(
            Body: body,
            BodyVariant: bodyVariant,
            Link: link,
            LinkVariant: linkVariant,
            Surface: surface,
            SurfaceVariant: surfaceVariant
        );
    }

    public Theme Merge(Theme other)
        => SetBody(value: Body.Merge(other: other.Body))
            .SetBodyVariant(value: BodyVariant.Merge(other: other.BodyVariant))
            .SetLink(value: Link.Merge(other: other.Link))
            .SetLinkVariant(value: LinkVariant.Merge(other: other.LinkVariant))
            .SetSurface(value: Surface.Merge(other: other.Surface))
            .SetSurfaceVariant(value: SurfaceVariant.Merge(other: other.SurfaceVariant));

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
}
