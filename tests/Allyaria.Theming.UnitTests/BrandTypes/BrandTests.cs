namespace Allyaria.Theming.UnitTests.BrandTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class BrandTests
{
    [Fact]
    public void Constructor_Should_CreateVariant_When_ThemesAreNotProvided()
    {
        // Act
        var sut = new Brand();

        // Assert
        sut.Variant.Should().NotBeNull();
        sut.Variant.Light.Should().NotBeNull();
        sut.Variant.Dark.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_Should_UseDefaultFont_When_NullProvided()
    {
        // Act
        var sut = new Brand();

        // Assert
        sut.Font.Should().Be(expected: new BrandFont());
    }

    [Fact]
    public void Constructor_Should_UseProvidedFontAndThemes_When_NotNull()
    {
        // Arrange
        var font = new BrandFont(sansSerif: "Roboto", serif: "Georgia", monospace: "Consolas");
        var lightTheme = new BrandTheme(surface: "#FFFFFF");
        var darkTheme = new BrandTheme(surface: "#000000");

        // Act
        var sut = new Brand(font: font, lightTheme: lightTheme, darkTheme: darkTheme);

        // Assert
        sut.Font.Should().Be(expected: font);
        sut.Variant.Light.Should().Be(expected: lightTheme);
        sut.Variant.Dark.Should().Be(expected: darkTheme);
    }

    [Fact]
    public void CreateHighContrastBrand_Should_CreateBrandWithExpectedHighContrastThemes()
    {
        // Act
        var sut = Brand.CreateHighContrastBrand();

        // Assert
        sut.Font.Should().Be(expected: new BrandFont());

        // Light theme expectations
        sut.Variant.Light.Surface.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastSurfaceColorLight));

        sut.Variant.Light.Primary.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastPrimaryColorLight));

        sut.Variant.Light.Secondary.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastSecondaryColorLight));

        sut.Variant.Light.Tertiary.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastTertiaryColorLight));

        sut.Variant.Light.Error.Should().Be(expected: new BrandState(color: StyleDefaults.HighContrastErrorColorLight));

        sut.Variant.Light.Warning.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastWarningColorLight));

        sut.Variant.Light.Success.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastSuccessColorLight));

        sut.Variant.Light.Info.Should().Be(expected: new BrandState(color: StyleDefaults.HighContrastInfoColorLight));

        // Dark theme expectations
        sut.Variant.Dark.Surface.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastSurfaceColorDark));

        sut.Variant.Dark.Primary.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastPrimaryColorDark));

        sut.Variant.Dark.Secondary.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastSecondaryColorDark));

        sut.Variant.Dark.Tertiary.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastTertiaryColorDark));

        sut.Variant.Dark.Error.Should().Be(expected: new BrandState(color: StyleDefaults.HighContrastErrorColorDark));

        sut.Variant.Dark.Warning.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastWarningColorDark));

        sut.Variant.Dark.Success.Should()
            .Be(expected: new BrandState(color: StyleDefaults.HighContrastSuccessColorDark));

        sut.Variant.Dark.Info.Should().Be(expected: new BrandState(color: StyleDefaults.HighContrastInfoColorDark));
    }
}
