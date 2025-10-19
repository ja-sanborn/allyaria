namespace Allyaria.Theming.Types;

public readonly record struct StyleVariant(StyleComponent Surface)
{
    public StyleVariant Cascade(StyleVariant? value = null)
        => value is null
            ? this
            : Cascade(value.Value.Surface);

    public StyleVariant Cascade(StyleComponent? surface = null)
        => this with
        {
            Surface = Surface.Cascade(surface)
        };

    public string ToCss(ComponentType component, ComponentState state, string? varPrefix = "")
    {
        var builder = new StringBuilder();
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-{component}";
        }

        switch (component)
        {
            case ComponentType.Surface:
                builder.Append(Surface.ToCss(state, prefix));
                break;
        }

        builder.Append(Surface.ToCss(state, prefix));

        return builder.ToString();
    }
}
