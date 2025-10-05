using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaThemeVariantTests
{
    [Fact]
    public void Ctor_Default_Should_Create_All_Three_Variants_With_Project_Defaults()
    {
        // Arrange + Act
        var sut = new AllyariaThemeVariant();

        // Assert (Light defaults)
        sut.Light.Default.Palette.BackgroundColor.Should().Be(StyleDefaults.BackgroundColorLight);
        sut.Light.Default.Palette.ForegroundColor.Should().Be(StyleDefaults.ForegroundColorLight);

        // Assert (Dark derived from Light with Dark defaults)
        sut.Dark.Default.Palette.BackgroundColor.Should().Be(StyleDefaults.BackgroundColorDark);
        sut.Dark.Default.Palette.ForegroundColor.Should().Be(StyleDefaults.ForegroundColorDark);

        // Assert (HighContrast derived from Light with HC defaults)
        sut.HighContrast.Default.Palette.BackgroundColor.Should().Be(StyleDefaults.BackgroundColorHighContrast);
        sut.HighContrast.Default.Palette.ForegroundColor.Should().Be(StyleDefaults.ForegroundColorHighContrast);

        // sanity: variants are distinct palettes (not all equal)
        sut.Light.Default.Palette.BackgroundColor.Should().NotBe(sut.Dark.Default.Palette.BackgroundColor);
        sut.Light.Default.Palette.BackgroundColor.Should().NotBe(sut.HighContrast.Default.Palette.BackgroundColor);
    }

    [Fact]
    public void Ctor_Should_Derive_Dark_And_HighContrast_From_Provided_Light_When_Omitted()
    {
        // Arrange
        var customLight = new AllyariaStyle(
            new AllyariaStyleVariant(new AllyariaPalette(Colors.Orange500, Colors.Purple900, Colors.Orange500))
        );

        // Act
        var sut = new AllyariaThemeVariant(light: customLight); // dark/highContrast omitted => derived

        // Assert – Light is exactly what we passed
        sut.Light.Default.Palette.BackgroundColor.Should().Be(Colors.Orange500);
        sut.Light.Default.Palette.ForegroundColor.Should().Be(Colors.Purple900);

        // Assert – Dark derived uses dark defaults for bg/fg (not Orange/Purple)
        sut.Dark.Default.Palette.BackgroundColor.Should().Be(StyleDefaults.BackgroundColorDark);
        sut.Dark.Default.Palette.ForegroundColor.Should().Be(StyleDefaults.ForegroundColorDark);

        // Assert – HighContrast derived uses HC defaults
        sut.HighContrast.Default.Palette.BackgroundColor.Should().Be(StyleDefaults.BackgroundColorHighContrast);
        sut.HighContrast.Default.Palette.ForegroundColor.Should().Be(StyleDefaults.ForegroundColorHighContrast);
    }

    [Fact]
    public void Ctor_Should_Respect_Explicit_Dark_And_HighContrast_When_Provided()
    {
        // Arrange
        var explicitDark = new AllyariaStyle(
            new AllyariaStyleVariant(new AllyariaPalette(Colors.Blue900, Colors.Yellow500, Colors.Blue900))
        );

        var explicitHighContrast = new AllyariaStyle(
            new AllyariaStyleVariant(new AllyariaPalette(Colors.Black, Colors.White, Colors.Black))
        );

        // Act
        var sut = new AllyariaThemeVariant(explicitDark, explicitHighContrast);

        // Assert
        sut.Dark.Default.Palette.BackgroundColor.Should().Be(Colors.Blue900);
        sut.Dark.Default.Palette.ForegroundColor.Should().Be(Colors.Yellow500);

        sut.HighContrast.Default.Palette.BackgroundColor.Should().Be(Colors.Black);
        sut.HighContrast.Default.Palette.ForegroundColor.Should().Be(Colors.White);

        // Light should still be project default (since not provided)
        sut.Light.Default.Palette.BackgroundColor.Should().Be(StyleDefaults.BackgroundColorLight);
        sut.Light.Default.Palette.ForegroundColor.Should().Be(StyleDefaults.ForegroundColorLight);
    }

    [Fact]
    public void Ctor_Should_Use_Explicit_Light_And_Leave_Other_Explicit_Values_Unchanged()
    {
        // Arrange
        var explicitLight = new AllyariaStyle(
            new AllyariaStyleVariant(new AllyariaPalette(Colors.Green500, Colors.Black, Colors.Green700))
        );

        var explicitDark = new AllyariaStyle(
            new AllyariaStyleVariant(new AllyariaPalette(Colors.Red700, Colors.White, Colors.Red900))
        );

        // Act
        var sut = new AllyariaThemeVariant(explicitDark, null, explicitLight);

        // Assert – Light is explicit
        sut.Light.Default.Palette.BackgroundColor.Should().Be(Colors.Green500);
        sut.Light.Default.Palette.ForegroundColor.Should().Be(Colors.Black);
        sut.Light.Default.Palette.BorderColor.Should().Be(Colors.Green700);

        // Assert – Dark remains explicit (not derived from new Light)
        sut.Dark.Default.Palette.BackgroundColor.Should().Be(Colors.Red700);
        sut.Dark.Default.Palette.ForegroundColor.Should().Be(Colors.White);
        sut.Dark.Default.Palette.BorderColor.Should().Be(Colors.Red900);

        // Assert – HighContrast was omitted, so derived from Light using HC defaults
        sut.HighContrast.Default.Palette.BackgroundColor.Should().Be(StyleDefaults.BackgroundColorHighContrast);
        sut.HighContrast.Default.Palette.ForegroundColor.Should().Be(StyleDefaults.ForegroundColorHighContrast);
    }
}
