namespace Allyaria.Theming.Types;

internal sealed class ThemeStyle
{
    private readonly Dictionary<StyleType, IStyleValue> _children = new();

    internal CssBuilder BuildCss(CssBuilder builder, ThemeNavigator navigator, string? varPrefix = "")
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

    private static CssBuilder BuildCssValue(CssBuilder builder, StyleType key, IStyleValue? value, string? varPrefix)
        => value is null
            ? builder
            : value is StyleGroup group
                ? group.BuildCss(builder: builder, varPrefix: varPrefix)
                : builder.Add(name: key.GetDescription(), value: value?.Value, varPrefix: varPrefix);

    private ThemeStyle EnsureContrast()
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

    private IStyleValue? Get(StyleType key) => _children.GetValueOrDefault(key: key);

    internal ThemeStyle Set(ThemeUpdater updater, bool isFocused = false)
    {
        var isColor = false;

        foreach (var key in updater.Navigator.StyleTypes)
        {
            SetValue(key: key, value: updater.Value, isFocused: isFocused);

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
            : SetValue(key: key, value: new StyleColor(value: color), isFocused: false);

    private ThemeStyle SetValue(StyleType key, IStyleValue? value, bool isFocused)
    {
        if (string.IsNullOrWhiteSpace(value: value?.Value))
        {
            _children.Remove(key: key);
        }
        else
        {
            var newValue = value;

            newValue = ValidateOutlineStyle(key: key, value: newValue, isFocused: isFocused);
            newValue = ValidateOutlineWidth(key: key, value: newValue, isFocused: isFocused);

            _children[key: key] = newValue;
        }

        return this;
    }

    private IStyleValue ValidateOutlineStyle(StyleType key, IStyleValue value, bool isFocused)
        => isFocused && key is StyleType.OutlineStyle &&
            StyleOutlineStyle.Kind.None.GetDescription().Equals(
                value: value.Value, comparisonType: StringComparison.OrdinalIgnoreCase
            )
                ? new StyleOutlineStyle(kind: StyleOutlineStyle.Kind.Solid)
                : value;

    private IStyleValue ValidateOutlineWidth(StyleType key, IStyleValue value, bool isFocused)
        => isFocused && key is StyleType.OutlineWidth &&
            !Sizing.Thick.Equals(value: value.Value, comparisonType: StringComparison.OrdinalIgnoreCase)
                ? new StyleNumber(value: Sizing.Thick)
                : value;
}
