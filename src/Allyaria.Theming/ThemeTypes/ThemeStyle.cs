namespace Allyaria.Theming.ThemeTypes;

/// <summary>
/// Represents the lowest layer in the Allyaria theming hierarchy, responsible for holding and rendering individual style
/// properties (such as colors, borders, and font-related values).
/// </summary>
/// <remarks>
///     <para>
///     A <see cref="ThemeStyle" /> corresponds to a single <see cref="ComponentState" /> (e.g., Default, Hovered) and
///     manages a collection of <see cref="StyleType" /> → <see cref="IStyleValue" /> mappings.
///     </para>
///     <para>
///     This class ensures contrast compliance for color-related properties according to WCAG 2.2 AA standards and produces
///     normalized CSS through <see cref="BuildCss" />.
///     </para>
/// </remarks>
internal sealed class ThemeStyle
{
    /// <summary>Stores all style values for this theme style, keyed by <see cref="StyleType" />.</summary>
    private readonly Dictionary<StyleType, IStyleValue> _children = new();

    /// <summary>
    /// Builds the CSS output for this style definition and its contained <see cref="IStyleValue" /> instances.
    /// </summary>
    /// <param name="builder">The <see cref="CssBuilder" /> used to accumulate CSS declarations.</param>
    /// <param name="navigator">
    /// The <see cref="ThemeNavigator" /> defining which <see cref="StyleType" /> values should be included.
    /// </param>
    /// <param name="varPrefix">An optional CSS variable prefix used when generating scoped variable names.</param>
    /// <returns>A <see cref="CssBuilder" /> containing the merged style declarations.</returns>
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

    /// <summary>Appends an individual CSS property or grouped value to the provided builder.</summary>
    /// <param name="builder">The <see cref="CssBuilder" /> used for output.</param>
    /// <param name="key">The <see cref="StyleType" /> identifying the CSS property.</param>
    /// <param name="value">The value associated with the style property.</param>
    /// <param name="varPrefix">An optional CSS variable prefix for variable-scoped styles.</param>
    /// <returns>The updated <see cref="CssBuilder" />.</returns>
    private static CssBuilder BuildCssValue(CssBuilder builder, StyleType key, IStyleValue? value, string? varPrefix)
        => value is null
            ? builder
            : value is StyleGroup group
                ? group.BuildCss(builder: builder, varPrefix: varPrefix)
                : builder.Add(name: key.GetDescription(), value: value.Value, varPrefix: varPrefix);

    /// <summary>
    /// Ensures sufficient contrast between foreground and background color properties, following WCAG 2.2 AA standards for
    /// visual accessibility.
    /// </summary>
    /// <remarks>
    /// This method adjusts key color-related properties—such as text, border, and accent colors— to maintain a minimum
    /// contrast ratio of 4.5:1 against the background.
    /// </remarks>
    private void EnsureContrast()
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
            return;
        }

        accentColor = accentColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        borderColor = borderColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        caretColor = caretColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        color = color?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        outlineColor = outlineColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);
        textDecorationColor = textDecorationColor?.EnsureContrast(background: backgroundColor.Value, minimumRatio: 4.5);

        SetColor(key: StyleType.AccentColor, color: accentColor)
            .SetColor(key: StyleType.BackgroundColor, color: backgroundColor)
            .SetColor(key: StyleType.BorderColor, color: borderColor)
            .SetColor(key: StyleType.CaretColor, color: caretColor)
            .SetColor(key: StyleType.Color, color: color)
            .SetColor(key: StyleType.OutlineColor, color: outlineColor)
            .SetColor(key: StyleType.TextDecorationColor, color: textDecorationColor);
    }

    /// <summary>Retrieves the <see cref="IStyleValue" /> associated with the given <see cref="StyleType" />.</summary>
    /// <param name="key">The <see cref="StyleType" /> to retrieve.</param>
    /// <returns>The associated <see cref="IStyleValue" />, or <see langword="null" /> if not found.</returns>
    private IStyleValue? Get(StyleType key) => _children.GetValueOrDefault(key: key);

    /// <summary>Applies a <see cref="ThemeUpdater" /> to set or update style values for this theme style.</summary>
    /// <param name="updater">The <see cref="ThemeUpdater" /> containing the update definition.</param>
    /// <returns>The same <see cref="ThemeStyle" /> instance for fluent configuration.</returns>
    internal ThemeStyle Set(ThemeUpdater updater)
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

    /// <summary>Assigns a specific color value to the given <see cref="StyleType" />.</summary>
    /// <param name="key">The <see cref="StyleType" /> identifying the color property.</param>
    /// <param name="color">The <see cref="HexColor" /> to assign.</param>
    /// <returns>The current <see cref="ThemeStyle" /> instance for chaining.</returns>
    private ThemeStyle SetColor(StyleType key, HexColor? color)
        => color is null
            ? this
            : SetValue(key: key, value: new StyleColor(value: color));

    /// <summary>Sets or removes a style value for the specified <see cref="StyleType" />.</summary>
    /// <param name="key">The <see cref="StyleType" /> identifying the CSS property.</param>
    /// <param name="value">The <see cref="IStyleValue" /> representing the style value.</param>
    /// <returns>The same <see cref="ThemeStyle" /> instance for fluent chaining.</returns>
    private ThemeStyle SetValue(StyleType key, IStyleValue? value)
    {
        if (string.IsNullOrWhiteSpace(value: value?.Value))
        {
            _children.Remove(key: key);
        }
        else
        {
            _children[key: key] = value;
        }

        return this;
    }
}
