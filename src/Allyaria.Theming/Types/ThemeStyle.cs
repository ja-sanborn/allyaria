namespace Allyaria.Theming.Types;

public sealed class ThemeStyle
{
    private readonly Dictionary<StyleType, IStyleValue> _children = new();

    public CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
    {
        if (navigator.StyleTypes.Count is 0)
        {
            foreach (var child in _children)
            {
                builder = BuildCssValue(builder: builder, key: child.Key, value: child.Value, varPrefix: varPrefix);
            }
        }
        else
        {
            foreach (var key in navigator.StyleTypes)
            {
                builder = BuildCssValue(builder: builder, key: key, value: Get(key: key), varPrefix: varPrefix);
            }
        }

        return builder;
    }

    private CssBuilder BuildCssValue(CssBuilder builder, StyleType key, IStyleValue? value, string? varPrefix)
        => value is null
            ? builder
            : value is StyleGroup group
                ? group.BuildCss(builder: builder, varPrefix: varPrefix)
                : builder.Add(name: key.GetDescription(), value: value?.Value, varPrefix: varPrefix);

    public ThemeStyle EnsureContrast()
    {
        var accentColor = ((StyleColor?)Get(key: StyleType.AccentColor))?.Color;
        var backgroundColor = ((StyleColor?)Get(key: StyleType.BackgroundColor))?.Color;
        var borderColor = ((StyleColor?)Get(key: StyleType.BorderColor))?.Color;
        var caretColor = ((StyleColor?)Get(key: StyleType.CaretColor))?.Color;
        var color = ((StyleColor?)Get(key: StyleType.Color))?.Color;
        var outlineColor = ((StyleColor?)Get(key: StyleType.OutlineColor))?.Color;
        var textDecorationColor = ((StyleColor?)Get(key: StyleType.TextDecorationColor))?.Color;

        if (backgroundColor?.IsTransparent() ?? true)
        {
            return this;
        }

        accentColor = accentColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        borderColor = borderColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        caretColor = caretColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        color = color?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        outlineColor = outlineColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        textDecorationColor = textDecorationColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);

        return SetColor(key: StyleType.AccentColor, color: accentColor)
            .SetColor(key: StyleType.BackgroundColor, color: backgroundColor)
            .SetColor(key: StyleType.BorderColor, color: borderColor)
            .SetColor(key: StyleType.CaretColor, color: caretColor)
            .SetColor(key: StyleType.Color, color: color)
            .SetColor(key: StyleType.OutlineColor, color: outlineColor)
            .SetColor(key: StyleType.TextDecorationColor, color: textDecorationColor);
    }

    public IStyleValue? Get(StyleType key)
        => _children.TryGetValue(key: key, value: out var value)
            ? value
            : null;

    public ThemeStyle Set(ThemeUpdater updater)
    {
        var isColor = false;

        foreach (var key in updater.Navigator.StyleTypes)
        {
            SetValue(key: key, value: updater.Value);

            if (!isColor && updater.Value is StyleColor)
            {
                isColor = true;
            }
        }

        if (isColor)
        {
            EnsureContrast();
        }

        return this;
    }

    private ThemeStyle SetColor(StyleType key, HexColor? color)
        => color is null
            ? this
            : SetValue(key: key, value: new StyleColor(value: color));

    private ThemeStyle SetValue(StyleType key, IStyleValue? value)
    {
        if (_children.ContainsKey(key: key))
        {
            if (string.IsNullOrWhiteSpace(value: value?.Value))
            {
                _children.Remove(key: key);
            }
            else
            {
                _children[key: key] = value;
            }
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(value: value?.Value))
            {
                _children.Add(key: key, value: value);
            }
        }

        return this;
    }
}
