using Allyaria.Theming.Constants;
using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryPaletteVariantTests
{
    [Fact]
    public void Cascade_Should_Preserve_ExistingPalettes_When_OverridesAreNull()
    {
        // Arrange
        var baseLight = new AryPalette(StyleDefaults.BackgroundColorLight, StyleDefaults.BackgroundColorDark);
        var baseDark = new AryPalette(StyleDefaults.BackgroundColorDark, StyleDefaults.ForegroundColorDark);

        var baseHc = new AryPalette(
            StyleDefaults.BackgroundColorHighContrast, StyleDefaults.ForegroundColorHighContrast
        );

        var baseline = new AryPaletteVariant(baseLight, baseDark, baseHc);

        // Act
        var sut = baseline.Cascade();

        // Assert
        sut.LightPalette.Should().Be(baseLight);
        sut.DarkPalette.Should().Be(baseDark);
        sut.HighContrastPalette.Should().Be(baseHc);
    }

    [Fact]
    public void Cascade_Should_Return_NewInstance_With_Provided_Overrides_And_Preserve_Original()
    {
        // Arrange
        var original = new AryPaletteVariant();

        var newLight = new AryPalette(
            StyleDefaults.BackgroundColorHighContrast, StyleDefaults.ForegroundColorHighContrast
        );

        var newDark = new AryPalette(StyleDefaults.BackgroundColorLight, StyleDefaults.ForegroundColorDark);
        var newHc = new AryPalette(StyleDefaults.BackgroundColorDark, StyleDefaults.BackgroundColorLight);

        // Act
        var sut = original.Cascade(newLight, newDark, newHc);

        // Assert - new instance has overrides
        sut.LightPalette.Should().Be(newLight);
        sut.DarkPalette.Should().Be(newDark);
        sut.HighContrastPalette.Should().Be(newHc);

        // and elevations are consistent (can produce palettes without throwing)
        sut.LightElevation.ToPalette(ComponentElevation.Lowest, ComponentState.Hovered).Should().NotBeNull();
        sut.DarkElevation.ToPalette(ComponentElevation.Low, ComponentState.Focused).Should().NotBeNull();
        sut.HighContrastElevation.ToPalette(ComponentElevation.High, ComponentState.Disabled).Should().NotBeNull();

        // original remains unchanged (immutability)
        original.LightPalette.Should().NotBe(newLight);
        original.DarkPalette.Should().NotBe(newDark);
        original.HighContrastPalette.Should().NotBe(newHc);
    }

    [Fact]
    public void Ctor_Should_Initialize_DefaultPalettes_And_Elevations_When_AllNull()
    {
        // Arrange & Act
        var sut = new AryPaletteVariant(null);

        // Assert
        sut.LightPalette.Should().Be(
            new AryPalette(StyleDefaults.BackgroundColorLight, StyleDefaults.BackgroundColorDark)
        );

        sut.DarkPalette.Should().Be(
            new AryPalette(StyleDefaults.BackgroundColorDark, StyleDefaults.ForegroundColorDark)
        );

        sut.HighContrastPalette.Should().Be(
            new AryPalette(StyleDefaults.BackgroundColorHighContrast, StyleDefaults.ForegroundColorHighContrast)
        );

        // sanity: elevations exist and can yield a palette at a common elevation/state
        var lightBase = sut.LightElevation.ToPalette(ComponentElevation.Mid, ComponentState.Default);
        lightBase.Should().NotBeNull();

        var darkBase = sut.DarkElevation.ToPalette(ComponentElevation.Mid, ComponentState.Default);
        darkBase.Should().NotBeNull();

        var hcBase = sut.HighContrastElevation.ToPalette(ComponentElevation.Mid, ComponentState.Default);
        hcBase.Should().NotBeNull();
    }

    [Theory]
    [InlineData(ComponentElevation.Mid, ComponentState.Default)]
    [InlineData(ComponentElevation.Lowest, ComponentState.Hovered)]
    [InlineData(ComponentElevation.High, ComponentState.Focused)]
    public void ToPalette_Should_Use_DarkElevation_When_ThemeIsDark(ComponentElevation elevation, ComponentState state)
    {
        // Arrange
        var sut = new AryPaletteVariant();

        // Act
        var actual = sut.ToPalette(ThemeType.Dark, elevation, state);

        // Assert
        var expected = sut.DarkElevation.ToPalette(elevation, state);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(ComponentElevation.Mid, ComponentState.Default)]
    [InlineData(ComponentElevation.Low, ComponentState.Disabled)]
    [InlineData(ComponentElevation.High, ComponentState.Pressed)]
    public void ToPalette_Should_Use_HighContrastElevation_When_ThemeIsHighContrast(ComponentElevation elevation,
        ComponentState state)
    {
        // Arrange
        var sut = new AryPaletteVariant();

        // Act
        var actual = sut.ToPalette(ThemeType.HighContrast, elevation, state);

        // Assert
        var expected = sut.HighContrastElevation.ToPalette(elevation, state);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(ThemeType.Light, ComponentElevation.Mid, ComponentState.Default)]
    [InlineData(ThemeType.Light, ComponentElevation.Highest, ComponentState.Hovered)]
    [InlineData(ThemeType.Light, ComponentElevation.Low, ComponentState.Focused)]
    public void ToPalette_Should_Use_LightElevation_When_ThemeIsLight(ThemeType theme,
        ComponentElevation elevation,
        ComponentState state)
    {
        // Arrange
        var sut = new AryPaletteVariant();

        // Act
        var actual = sut.ToPalette(theme, elevation, state);

        // Assert (compute expected from the same elevation to ensure correct routing)
        var expected = sut.LightElevation.ToPalette(elevation, state);
        actual.Should().Be(expected);
    }
}
