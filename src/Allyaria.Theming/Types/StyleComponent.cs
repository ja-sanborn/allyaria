namespace Allyaria.Theming.Types;

public readonly record struct StyleComponent
{
    public StyleComponent(StyleState style)
    {
        Default = style;
        Disabled = Default.ToDisabled();
        Dragged = Default.ToDragged();
        Focused = Default.ToFocused();
        Hovered = Default.ToHovered();
        Pressed = Default.ToPressed();
    }

    public StyleState Default { get; init; }

    public StyleState Disabled { get; init; }

    public StyleState Dragged { get; init; }

    public StyleState Focused { get; init; }

    public StyleState Hovered { get; init; }

    public StyleState Pressed { get; init; }

    public StyleComponent Cascade(StyleComponent? value = null)
        => value is null
            ? this
            : Cascade(value.Value.Default);

    public StyleComponent Cascade(StyleState? style = null)
        => style is null
            ? this
            : this with
            {
                Default = style.Value,
                Disabled = style.Value.ToDisabled(),
                Dragged = style.Value.ToDragged(),
                Focused = style.Value.ToFocused(),
                Hovered = style.Value.ToHovered(),
                Pressed = style.Value.ToPressed()
            };

    public string ToCss(ComponentState state, string? varPrefix = "")
    {
        var builder = new StringBuilder();
        var prefix = varPrefix.ToCssName();

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-{state}";
        }

        switch (state)
        {
            case ComponentState.Default:
                builder.Append(Default.ToCss(prefix));

                break;

            case ComponentState.Disabled:
                builder.Append(Disabled.ToCss(prefix));

                break;

            case ComponentState.Dragged:
                builder.Append(Dragged.ToCss(prefix));

                break;

            case ComponentState.Focused:
                builder.Append(Focused.ToCss(prefix));

                break;

            case ComponentState.Hovered:
                builder.Append(Hovered.ToCss(prefix));

                break;

            case ComponentState.Pressed:
                builder.Append(Pressed.ToCss(prefix));

                break;
        }

        return builder.ToString();
    }
}
