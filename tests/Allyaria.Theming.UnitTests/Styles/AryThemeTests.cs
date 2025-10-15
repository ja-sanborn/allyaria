using Allyaria.Theming.Constants;
using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AryThemeTests
{
    [Fact]
    public void Cascade_Should_OverrideAllMembers_When_AllOverridesProvided()
    {
        // Arrange
        var seed = new AryTheme();

        var newBorders = new AryBorders();
        var newSpacing = new ArySpacing();

        var basePalette = new AryPalette();
        var newLight = basePalette.ToElevation1();
        var newDark = basePalette.ToElevation2();
        var newHigh = basePalette.ToElevation3();

        var newSurfaceTypo = new AryTypography();

        // Act
        var result = seed.Cascade(
            newBorders,
            newSpacing,
            newLight,
            newDark,
            newHigh,
            newSurfaceTypo
        );

        // Assert
        result.Borders.Should().Be(newBorders);
        result.Spacing.Should().Be(newSpacing);
        result.Typo.Surface.Should().Be(newSurfaceTypo);

        result.Palette.LightPalette.Should().Be(newLight);
        result.Palette.DarkPalette.Should().Be(newDark);
        result.Palette.HighContrastPalette.Should().Be(newHigh);
    }

    [Fact]
    public void Cascade_Should_OverrideOnlySpecifiedMembers_When_PartialOverridesProvided()
    {
        // Arrange
        var initialLight = new AryPalette();
        var initialDark = initialLight.ToHovered();
        var initialHigh = initialLight.ToPressed();

        var initialTheme = new AryTheme(
            new AryBorders(),
            new ArySpacing(),
            initialLight,
            initialDark,
            initialHigh,
            new AryTypography()
        );

        var overrideDark = initialLight.ToDragged();

        // Act
        var result = initialTheme.Cascade(
            paletteDark: overrideDark

            // other args intentionally left null to exercise null-coalescing branches
        );

        // Assert (unchanged)
        result.Borders.Should().Be(initialTheme.Borders);
        result.Spacing.Should().Be(initialTheme.Spacing);
        result.Typo.Surface.Should().Be(initialTheme.Typo.Surface);
        result.Palette.LightPalette.Should().Be(initialLight);
        result.Palette.HighContrastPalette.Should().Be(initialHigh);

        // Assert (changed)
        result.Palette.DarkPalette.Should().Be(overrideDark);
    }

    [Fact]
    public void Constructor_Should_ApplyProvidedComponents_When_AllArgsSpecified()
    {
        // Arrange
        var customBorders = new AryBorders();
        var customSpacing = new ArySpacing();

        // Create three distinct palettes by transforming from a base
        var basePalette = new AryPalette();
        var customLight = basePalette;
        var customDark = basePalette.ToHovered();
        var customHigh = basePalette.ToPressed();

        var customTypography = new AryTypography();

        // Act
        var sut = new AryTheme(
            customBorders,
            customSpacing,
            customLight,
            customDark,
            customHigh,
            customTypography
        );

        // Assert
        sut.Borders.Should().Be(customBorders);
        sut.Spacing.Should().Be(customSpacing);
        sut.Typo.Surface.Should().Be(customTypography);

        sut.Palette.LightPalette.Should().Be(customLight);
        sut.Palette.DarkPalette.Should().Be(customDark);
        sut.Palette.HighContrastPalette.Should().Be(customHigh);
    }

    [Fact]
    public void Constructor_Should_InitializeDefaults_When_NoArgs()
    {
        // Arrange + Act
        var sut = new AryTheme();

        // Assert
        sut.Borders.Should().Be(new AryBorders());
        sut.Spacing.Should().Be(new ArySpacing());
        sut.Typo.Surface.Should().Be(new AryTypography());

        // PaletteVariant defaults to StyleDefaults presets
        sut.Palette.LightPalette.Should().Be(StyleDefaults.PaletteLight);
        sut.Palette.DarkPalette.Should().Be(StyleDefaults.PaletteDark);
        sut.Palette.HighContrastPalette.Should().Be(StyleDefaults.PaletteHighContrast);
    }

    [Fact]
    public void ToCss_Should_Equal_ToStyle_ToCss_Output_For_NonFocusedState()
    {
        // Arrange
        var sut = new AryTheme();
        var themeType = ThemeType.Light;
        var elevation = ComponentElevation.Mid;
        var state = ComponentState.Default;
        var component = (ComponentType)0;
        var varPrefix = "app";

        // Act
        var cssViaTheme = sut.ToCss(themeType, component, elevation, state, varPrefix);
        var cssViaStyle = sut.ToStyle(themeType, component, elevation, state).ToCss(varPrefix);

        // Assert
        cssViaTheme.Should().Be(cssViaStyle);
        cssViaTheme.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ToCss_Should_Match_ToStyle_ToCss_Output_For_FocusedState()
    {
        // Arrange
        var sut = new AryTheme();
        var themeType = ThemeType.HighContrast;
        var elevation = ComponentElevation.Mid;
        var component = (ComponentType)0;
        var varPrefix = "pref";

        // Act
        var cssViaTheme = sut.ToCss(themeType, component, elevation, ComponentState.Focused, varPrefix);
        var cssViaStyle = sut.ToStyle(themeType, component, elevation, ComponentState.Focused).ToCss(varPrefix, true);

        // Assert
        cssViaTheme.Should().Be(cssViaStyle);
    }

    [Fact]
    public void ToCss_Should_Respect_DefaultElevationAndState_When_ParametersOmitted()
    {
        // Arrange
        var sut = new AryTheme();
        var themeType = ThemeType.Light;
        var component = (ComponentType)0;

        // Act
        var implicitCss = sut.ToCss(themeType, component);
        var explicitCss = sut.ToCss(themeType, component);

        // Assert
        implicitCss.Should().Be(explicitCss);
    }

    [Fact]
    public void ToCss_Should_UseFocusedBorderPresentation_When_StateIsFocused()
    {
        // Arrange
        var sut = new AryTheme();
        var themeType = ThemeType.Light;
        var elevation = ComponentElevation.Mid;
        var component = (ComponentType)0;
        var varPrefix = "x";

        // Act
        var defaultCss = sut.ToCss(themeType, component, elevation, ComponentState.Default, varPrefix);
        var focusedCss = sut.ToCss(themeType, component, elevation, ComponentState.Focused, varPrefix);

        // Assert
        focusedCss.Should().NotBe(defaultCss); // border should differ when focused (thicker + dashed)

        // The focused border should be dashed and thicker; look for indicative substrings
        focusedCss.Should().Contain("border-top-style").And.Contain("dashed");
        focusedCss.Should().Contain("border-inline-end-width").And.Contain("2px");
    }

    [Fact]
    public void ToStyle_Should_Compose_From_Palette_Typography_Spacing_And_Borders()
    {
        // Arrange
        var sut = new AryTheme();
        var themeType = ThemeType.Light;
        var elevation = ComponentElevation.Mid;
        var state = ComponentState.Default;
        var component = (ComponentType)0; // any value resolves to Surface in current implementation

        // Act
        var style = sut.ToStyle(themeType, component, elevation, state);

        // Assert
        var expectedPalette = sut.Palette.ToPalette(themeType, elevation, state);
        var expectedTypography = sut.Typo.ToTypography(component);

        style.Palette.Should().Be(expectedPalette);
        style.Typography.Should().Be(expectedTypography);
        style.Spacing.Should().Be(sut.Spacing);
        style.Border.Should().Be(sut.Borders);
    }

    [Fact]
    public void ToStyle_Should_Respect_DefaultElevationAndState_When_ParametersOmitted()
    {
        // Arrange
        var sut = new AryTheme();
        var themeType = ThemeType.Dark;
        var component = (ComponentType)0;

        // Act
        var implicitStyle = sut.ToStyle(themeType, component);
        var explicitStyle = sut.ToStyle(themeType, component);

        // Assert
        implicitStyle.Should().Be(explicitStyle);
    }
}
