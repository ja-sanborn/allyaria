namespace Allyaria.Theming.Styles;

public readonly record struct StyleGroupBorderRadius : IStyleGroup
{
    public StyleGroupBorderRadius(StyleValueNumber value)
    {
        EndEnd = value;
        EndStart = value;
        StartEnd = value;
        StartStart = value;
    }

    public StyleGroupBorderRadius(StyleValueNumber start, StyleValueNumber end)
    {
        EndEnd = end;
        EndStart = end;
        StartEnd = start;
        StartStart = start;
    }

    public StyleGroupBorderRadius(StyleValueNumber startStart,
        StyleValueNumber startEnd,
        StyleValueNumber endStart,
        StyleValueNumber endEnd)
    {
        EndEnd = endEnd;
        EndStart = endStart;
        StartEnd = startEnd;
        StartStart = startStart;
    }

    public StyleValueNumber EndEnd { get; init; }

    public StyleValueNumber EndStart { get; init; }

    public StyleValueNumber StartEnd { get; init; }

    public StyleValueNumber StartStart { get; init; }

    public CssBuilder BuildCss(CssBuilder builder, string? varPrefix = "")
    {
        builder
            .Add<StyleValueNumber>(propertyName: "border-end-end-radius", value: EndEnd, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "border-end-start-radius", value: EndStart, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "border-start-end-radius", value: StartEnd, varPrefix: varPrefix)
            .Add<StyleValueNumber>(propertyName: "border-start-start-radius", value: StartStart, varPrefix: varPrefix);

        return builder;
    }

    public StyleGroupBorderRadius SetEndEnd(StyleValueNumber value)
        => this with
        {
            EndEnd = value
        };

    public StyleGroupBorderRadius SetEndStart(StyleValueNumber value)
        => this with
        {
            EndStart = value
        };

    public StyleGroupBorderRadius SetStartEnd(StyleValueNumber value)
        => this with
        {
            StartEnd = value
        };

    public StyleGroupBorderRadius SetStartStart(StyleValueNumber value)
        => this with
        {
            StartStart = value
        };

    public string ToCss(string? varPrefix = "") => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
