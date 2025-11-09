namespace Allyaria.Theming.UnitTests.BrandTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class BrandVariantTests
{
    [Fact]
    public void Constructor_Should_CreateConsistentVariantThemes_When_UsingDefaultThemes()
    {
        // Act
        var sut = new BrandVariant();

        // Assert
        sut.DarkVariant.Surface.Default.BackgroundColor.Should()
            .Be(expected: sut.Dark.Surface.Default.ForegroundColor);

        sut.LightVariant.Surface.Default.BackgroundColor.Should()
            .Be(expected: sut.Light.Surface.Default.ForegroundColor);
    }

    [Fact]
    public void Constructor_Should_CreateDarkVariant_FromDarkForegroundColors()
    {
        // Arrange
        var darkTheme = new BrandTheme(
            surface: "#111111",
            primary: "#222222",
            secondary: "#333333",
            tertiary: "#444444",
            error: "#555555",
            warning: "#666666",
            success: "#777777",
            info: "#888888"
        );

        // Act
        var sut = new BrandVariant(darkTheme: darkTheme);

        // Assert
        var expectedSurface = darkTheme.Surface.Default.ForegroundColor;
        var expectedPrimary = darkTheme.Primary.Default.ForegroundColor;

        sut.DarkVariant.Surface.Default.BackgroundColor.Should().Be(expected: expectedSurface);
        sut.DarkVariant.Primary.Default.BackgroundColor.Should().Be(expected: expectedPrimary);
    }

    [Fact]
    public void Constructor_Should_CreateDefaultThemes_When_ParametersAreNull()
    {
        // Act
        var sut = new BrandVariant();

        // Assert
        sut.Dark.Should().NotBeNull();
        sut.Light.Should().NotBeNull();

        sut.Dark.Surface.Should().Be(expected: new BrandState(color: StyleDefaults.SurfaceColorDark));
        sut.Light.Surface.Should().Be(expected: new BrandState(color: StyleDefaults.SurfaceColorLight));
    }

    [Fact]
    public void Constructor_Should_CreateLightVariant_FromLightForegroundColors()
    {
        // Arrange
        var lightTheme = new BrandTheme(
            surface: "#EEEEEE",
            primary: "#DDDDDD",
            secondary: "#CCCCCC",
            tertiary: "#BBBBBB",
            error: "#AAAAAA",
            warning: "#999999",
            success: "#888888",
            info: "#777777"
        );

        // Act
        var sut = new BrandVariant(lightTheme: lightTheme);

        // Assert
        var expectedSurface = lightTheme.Surface.Default.ForegroundColor;
        var expectedPrimary = lightTheme.Primary.Default.ForegroundColor;

        sut.LightVariant.Surface.Default.BackgroundColor.Should().Be(expected: expectedSurface);
        sut.LightVariant.Primary.Default.BackgroundColor.Should().Be(expected: expectedPrimary);
    }

    [Fact]
    public void Constructor_Should_UseProvidedThemes_When_NotNull()
    {
        // Arrange
        var lightTheme = new BrandTheme(surface: "#FFFFFF", primary: "#AAAAAA");
        var darkTheme = new BrandTheme(surface: "#000000", primary: "#111111");

        // Act
        var sut = new BrandVariant(lightTheme: lightTheme, darkTheme: darkTheme);

        // Assert
        sut.Light.Should().Be(expected: lightTheme);
        sut.Dark.Should().Be(expected: darkTheme);
    }
}
