namespace Allyaria.Theming.Types;

public readonly record struct StyleColorVariants
{
    public ThemeColor Color { get; init; }

    public ThemeColor Disabled { get; init; }

    public ThemeColor Dragged { get; init; }

    public ThemeColor Elevation1 { get; init; }

    public ThemeColor Elevation2 { get; init; }

    public ThemeColor Elevation3 { get; init; }

    public ThemeColor Elevation4 { get; init; }

    public bool IsForeground{ get; init; }

    public bool IsHighContrast{ get; init; }

    public ThemeColor Focused { get; init; }

    public ThemeColor Hovered { get; init; }

    public ThemeColor Pressed { get; init; }

    public StyleColorVariants(ThemeColor color, bool isForeground = true, bool isHighContrast = false)
    {
        IsForeground = isForeground;
        IsHighContrast = isHighContrast;

        if (color.Color == Colors.Transparent)
        {
            Color = color;
            Disabled = color;
            Dragged = color;
            Elevation1 = color;
            Elevation2 = color;
            Elevation3 = color;
            Elevation4 = color;
            Focused = color;
            Hovered = color;
            Pressed = color;

            return;
        }

        Color = color;
        Disabled = color = Color.ToDisabled();
        Dragged = color.ToDragged(IsHighContrast);
        Elevation1 = color.ToElevation1(IsHighContrast && !IsForeground);
        Elevation2 = color.ToElevation2(IsHighContrast && !IsForeground);
        Elevation3 = color.ToElevation3(IsHighContrast && !IsForeground);
        Elevation4 = color.ToElevation4(IsHighContrast && !IsForeground);
        Focused = color.ToFocused(IsHighContrast);
        Hovered = color.ToHovered(IsHighContrast);
        Pressed = color.ToPressed(IsHighContrast);
    }

    public StyleColorVariants Cascade(StyleColorVariants? value = null)
        => value is null
            ? this
            : Cascade(value.Value.Color, value.Value.IsForeground, value.Value.IsHighContrast);

    public StyleColorVariants Cascade(ThemeColor? value = null, bool? isForeground = null, bool? isHighContrast = null)
    {
        if (value is null)
        {
            return this;
        }

        var newForeground = isForeground ?? IsForeground;
        var newHighContrast = isHighContrast ?? IsHighContrast;

        return new StyleColorVariants(value, newForeground, newHighContrast);
    }
}
