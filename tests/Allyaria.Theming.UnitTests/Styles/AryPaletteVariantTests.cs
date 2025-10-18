namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryPaletteVariantTests
{
    [Fact]
    public void Cascade_Should_Override_Only_Dark_When_Only_Dark_Is_Specified()
    {
        // Arrange
        var original = new AryPaletteVariant();

        var customDark = new AryPalette(
            StyleDefaults.BackgroundColorDark,
            StyleDefaults.BackgroundColorDark,
            StyleDefaults.ForegroundColorDark,
            StyleDefaults.ForegroundColorDark
        );

        // Act
        var sut = original.Cascade(paletteDark: customDark);

        // Assert
        sut.DarkPalette.Should().Be(customDark);
        sut.LightPalette.Should().Be(original.LightPalette);
        sut.HighContrastPalette.Should().Be(original.HighContrastPalette);

        sut.DarkElevation.Lowest.Default.Should().Be(customDark);
        sut.LightElevation.Lowest.Default.Should().Be(original.LightPalette);
        sut.HighContrastElevation.Lowest.Default.Should().Be(original.HighContrastPalette);
    }

    [Fact]
    public void Cascade_Should_Override_Only_HighContrast_When_Only_HighContrast_Is_Specified()
    {
        // Arrange
        var original = new AryPaletteVariant();

        var customHigh = new AryPalette(
            StyleDefaults.BackgroundColorHighContrast,
            StyleDefaults.BackgroundColorHighContrast,
            StyleDefaults.ForegroundColorHighContrast,
            StyleDefaults.ForegroundColorHighContrast,
            true
        );

        // Act
        var sut = original.Cascade(paletteHighContrast: customHigh);

        // Assert
        sut.HighContrastPalette.Should().Be(customHigh);
        sut.LightPalette.Should().Be(original.LightPalette);
        sut.DarkPalette.Should().Be(original.DarkPalette);

        sut.HighContrastElevation.Lowest.Default.Should().Be(customHigh);
        sut.LightElevation.Lowest.Default.Should().Be(original.LightPalette);
        sut.DarkElevation.Lowest.Default.Should().Be(original.DarkPalette);
    }

    [Fact]
    public void Cascade_Should_Override_Only_Light_When_Only_Light_Is_Specified()
    {
        // Arrange
        var original = new AryPaletteVariant();

        var customLight = new AryPalette(
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.ForegroundColorLight,
            StyleDefaults.ForegroundColorLight
        );

        // Act
        var sut = original.Cascade(customLight);

        // Assert
        sut.LightPalette.Should().Be(customLight);
        sut.DarkPalette.Should().Be(original.DarkPalette);
        sut.HighContrastPalette.Should().Be(original.HighContrastPalette);

        // Elevations must be rebuilt from the overridden palette
        sut.LightElevation.Lowest.Default.Should().Be(customLight);

        // Unchanged ones still align with their original base palettes
        sut.DarkElevation.Lowest.Default.Should().Be(original.DarkPalette);
        sut.HighContrastElevation.Lowest.Default.Should().Be(original.HighContrastPalette);
    }

    [Fact]
    public void Cascade_Should_Return_New_Instance_With_All_Overrides_When_All_Specified()
    {
        // Arrange
        var original = new AryPaletteVariant();

        var customLight = new AryPalette(
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.ForegroundColorLight,
            StyleDefaults.ForegroundColorLight
        );

        var customDark = new AryPalette(
            StyleDefaults.BackgroundColorDark,
            StyleDefaults.BackgroundColorDark,
            StyleDefaults.ForegroundColorDark,
            StyleDefaults.ForegroundColorDark
        );

        var customHigh = new AryPalette(
            StyleDefaults.BackgroundColorHighContrast,
            StyleDefaults.BackgroundColorHighContrast,
            StyleDefaults.ForegroundColorHighContrast,
            StyleDefaults.ForegroundColorHighContrast,
            true
        );

        // Act
        var sut = original.Cascade(customLight, customDark, customHigh);

        // Assert
        sut.Should().NotBe(original); // record struct value should differ due to different contents

        sut.LightPalette.Should().Be(customLight);
        sut.DarkPalette.Should().Be(customDark);
        sut.HighContrastPalette.Should().Be(customHigh);

        // Elevation roots must match their corresponding overridden palettes
        sut.LightElevation.Lowest.Default.Should().Be(customLight);
        sut.DarkElevation.Lowest.Default.Should().Be(customDark);
        sut.HighContrastElevation.Lowest.Default.Should().Be(customHigh);
    }

    [Fact]
    public void Ctor_Should_Use_Default_Palettes_And_Build_Elevations_When_No_Overrides()
    {
        // Arrange & Act
        var sut = new AryPaletteVariant();

        // Assert
        sut.LightPalette.Should().Be(StyleDefaults.PaletteLight);
        sut.DarkPalette.Should().Be(StyleDefaults.PaletteDark);
        sut.HighContrastPalette.Should().Be(StyleDefaults.PaletteHighContrast);

        // Elevation hierarchies should be derived from the respective base palettes.
        sut.LightElevation.Lowest.Default.Should().Be(sut.LightPalette);
        sut.DarkElevation.Lowest.Default.Should().Be(sut.DarkPalette);
        sut.HighContrastElevation.Lowest.Default.Should().Be(sut.HighContrastPalette);
    }

    [Fact]
    public void ToPalette_Should_Default_To_Light_Elevation_For_Unrecognized_ThemeType()
    {
        // Arrange
        var sut = new AryPaletteVariant();
        var bogusTheme = (ThemeType)1234;

        var expected = sut.LightElevation.ToPalette(ComponentElevation.Mid, ComponentState.Default);

        // Act
        var actual = sut.ToPalette(bogusTheme, ComponentElevation.Mid, ComponentState.Default);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(ThemeType.Light, ComponentElevation.Lowest, ComponentState.Default)]
    [InlineData(ThemeType.Dark, ComponentElevation.Lowest, ComponentState.Default)]
    [InlineData(ThemeType.HighContrast, ComponentElevation.Lowest, ComponentState.Default)]
    [InlineData(ThemeType.Light, ComponentElevation.Mid, ComponentState.Hovered)]
    [InlineData(ThemeType.Dark, ComponentElevation.High, ComponentState.Focused)]
    [InlineData(ThemeType.HighContrast, ComponentElevation.Highest, ComponentState.Pressed)]
    public void ToPalette_Should_Resolve_From_Corresponding_Elevation_Based_On_Theme(ThemeType theme,
        ComponentElevation elevation,
        ComponentState state)
    {
        // Arrange
        // Use explicit distinct bases to ensure different elevation trees are actually used
        var light = new AryPalette(
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.ForegroundColorLight,
            StyleDefaults.ForegroundColorLight
        );

        var dark = new AryPalette(
            StyleDefaults.BackgroundColorDark,
            StyleDefaults.BackgroundColorDark,
            StyleDefaults.ForegroundColorDark,
            StyleDefaults.ForegroundColorDark
        );

        var high = new AryPalette(
            StyleDefaults.BackgroundColorHighContrast,
            StyleDefaults.BackgroundColorHighContrast,
            StyleDefaults.ForegroundColorHighContrast,
            StyleDefaults.ForegroundColorHighContrast,
            true
        );

        var sut = new AryPaletteVariant(light, dark, high);

        // Expected palette is taken from the selected theme's elevation/state
        var expected = theme switch
        {
            ThemeType.Dark => sut.DarkElevation.ToPalette(elevation, state),
            ThemeType.HighContrast => sut.HighContrastElevation.ToPalette(elevation, state),
            _ => sut.LightElevation.ToPalette(elevation, state)
        };

        // Act
        var actual = sut.ToPalette(theme, elevation, state);

        // Assert
        actual.Should().Be(expected);
    }
}
