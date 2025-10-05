using Allyaria.Theming.Constants;

namespace Allyaria.Theming.Styles;

/// <summary>Represents a variant of the Allyaria theme that includes Light, Dark, and HighContrast styles.</summary>
public readonly record struct AllyariaThemeVariant
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaThemeVariant" /> struct with default styles.</summary>
    public AllyariaThemeVariant()
        : this(null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaThemeVariant" /> struct with optional theme variants.
    /// </summary>
    /// <param name="dark">
    /// The dark mode style variant. If <see langword="null" />, it will be derived from <paramref name="light" />.
    /// </param>
    /// <param name="highContrast">
    /// The high contrast style variant. If <see langword="null" />, it will be derived from <paramref name="light" />.
    /// </param>
    /// <param name="light">
    /// The light mode style variant. If <see langword="null" />, a new <see cref="AllyariaStyle" /> will be created.
    /// </param>
    public AllyariaThemeVariant(AllyariaStyle? dark = null,
        AllyariaStyle? highContrast = null,
        AllyariaStyle? light = null)
    {
        Light = light ?? new AllyariaStyle();

        Dark = dark ?? Light.Cascade(
            Light.Default.Palette.Cascade(
                StyleDefaults.BackgroundColorDark,
                StyleDefaults.ForegroundColorDark
            )
        );

        HighContrast = highContrast ?? Light.Cascade(
            Light.Default.Palette.Cascade(
                StyleDefaults.BackgroundColorHighContrast,
                StyleDefaults.ForegroundColorHighContrast
            )
        );
    }

    /// <summary>Gets the dark mode style variant.</summary>
    public AllyariaStyle Dark { get; init; }

    /// <summary>Gets the high contrast style variant.</summary>
    public AllyariaStyle HighContrast { get; init; }

    /// <summary>Gets the light mode style variant.</summary>
    public AllyariaStyle Light { get; init; }
}
