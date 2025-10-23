namespace Allyaria.Theming.Styles;

public sealed record StyleGroupBorderRadius : IStyleGroup
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
            .Add<StyleValueNumber>("border-end-end-radius", EndEnd, varPrefix)
            .Add<StyleValueNumber>("border-end-start-radius", EndStart, varPrefix)
            .Add<StyleValueNumber>("border-start-end-radius", StartEnd, varPrefix)
            .Add<StyleValueNumber>("border-start-start-radius", StartStart, varPrefix);

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

    public string ToCss(string? varPrefix = "") => BuildCss(new CssBuilder(), varPrefix).ToString();
}
