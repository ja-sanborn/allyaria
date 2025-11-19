namespace Allyaria.Theming.Enumerations;

/// <summary>
/// Defines the palette categories used in the Allyaria theming system to represent distinct color groups and visual
/// elevation layers.
/// </summary>
public enum PaletteType
{
    /// <summary>Represents the lowest elevation layer for UI elements such as base surfaces or containers.</summary>
    Elevation1,

    /// <summary>Represents the second elevation layer, slightly raised relative to <see cref="Elevation1" />.</summary>
    Elevation2,

    /// <summary>Represents the third elevation layer, providing additional visual depth.</summary>
    Elevation3,

    /// <summary>Represents the fourth elevation layer, typically used for higher-level UI containers.</summary>
    Elevation4,

    /// <summary>Represents the highest elevation layer, commonly used for dialogs or overlays.</summary>
    Elevation5,

    /// <summary>Represents the error palette used for validation and critical error feedback.</summary>
    Error,

    /// <summary>Represents the informational palette, used for neutral notifications and hints.</summary>
    Info,

    /// <summary>Represents the primary color palette, used for major actions, links, or key highlights.</summary>
    Primary,

    /// <summary>Represents the success palette, used to convey positive or completed actions.</summary>
    Success,

    /// <summary>Represents the secondary color palette, supporting accents or less prominent elements.</summary>
    Secondary,

    /// <summary>Represents the base surface palette, defining general background and container colors.</summary>
    Surface,

    /// <summary>Represents the tertiary palette, typically used for subtle accents or decorative purposes.</summary>
    Tertiary,

    /// <summary>Represents the warning palette, used for cautionary or attention-required states.</summary>
    Warning
}
