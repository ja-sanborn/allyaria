namespace Allyaria.Theming.Enumerations;

/// <summary>
/// Defines the tonal elevation levels used in Allyaria components, aligned with the Material Design 3 elevation model.
/// Elevation in this context represents perceived depth through surface tone and shadow blending.
/// </summary>
public enum ComponentElevation
{
    /// <summary>Represents a mid-level, default, tonal palette (e.g. <c>SurfaceContainer</c>).</summary>
    Mid,

    /// <summary>Represents the lowest tonal palette (e.g. <c>SurfaceContainerLowest</c>).</summary>
    Lowest,

    /// <summary>Represents a slightly lowered tonal palette (e.g. <c>SurfaceContainerLow</c>).</summary>
    Low,

    /// <summary>Represents a slightly elevated tonal palette (e.g. <c>SurfaceContainerHigh</c>).</summary>
    High,

    /// <summary>Represents the highest tonal palette (e.g. <c>SurfaceContainerHighest</c>).</summary>
    Highest
}
