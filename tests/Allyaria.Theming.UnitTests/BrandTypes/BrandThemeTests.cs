namespace Allyaria.Theming.UnitTests.BrandTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class BrandThemeTests
{
    [Fact]
    public void Constructor_Should_GenerateElevationStates_FromDefaultSurfaceColor_WhenSurfaceIsNull()
    {
        // Arrange
        HexColor? surface = null;

        // Act
        var sut = new BrandTheme(surface: surface);

        // Assert
        var defaultSurface = StyleDefaults.SurfaceColorLight;
        sut.Elevation1.Should().Be(expected: new BrandState(color: defaultSurface.ToElevation1()));
        sut.Elevation2.Should().Be(expected: new BrandState(color: defaultSurface.ToElevation2()));
        sut.Elevation3.Should().Be(expected: new BrandState(color: defaultSurface.ToElevation3()));
        sut.Elevation4.Should().Be(expected: new BrandState(color: defaultSurface.ToElevation4()));
        sut.Elevation5.Should().Be(expected: new BrandState(color: defaultSurface.ToElevation5()));
    }

    [Fact]
    public void Constructor_Should_GenerateElevationStates_FromSurfaceColor()
    {
        // Arrange
        var surface = new HexColor(value: "#999999");

        // Act
        var sut = new BrandTheme(surface: surface);

        // Assert
        sut.Elevation1.Should().Be(expected: new BrandState(color: surface.ToElevation1()));
        sut.Elevation2.Should().Be(expected: new BrandState(color: surface.ToElevation2()));
        sut.Elevation3.Should().Be(expected: new BrandState(color: surface.ToElevation3()));
        sut.Elevation4.Should().Be(expected: new BrandState(color: surface.ToElevation4()));
        sut.Elevation5.Should().Be(expected: new BrandState(color: surface.ToElevation5()));
    }

    [Fact]
    public void Constructor_Should_UseProvidedColors_ForAllStates()
    {
        // Arrange
        var surface = new HexColor(value: "#111111");
        var primary = new HexColor(value: "#222222");
        var secondary = new HexColor(value: "#333333");
        var tertiary = new HexColor(value: "#444444");
        var error = new HexColor(value: "#555555");
        var warning = new HexColor(value: "#666666");
        var success = new HexColor(value: "#777777");
        var info = new HexColor(value: "#888888");

        // Act
        var sut = new BrandTheme(
            surface: surface, primary: primary, secondary: secondary, tertiary: tertiary, error: error,
            warning: warning, success: success, info: info
        );

        // Assert
        sut.Surface.Should().Be(expected: new BrandState(color: surface));
        sut.Primary.Should().Be(expected: new BrandState(color: primary));
        sut.Secondary.Should().Be(expected: new BrandState(color: secondary));
        sut.Tertiary.Should().Be(expected: new BrandState(color: tertiary));
        sut.Error.Should().Be(expected: new BrandState(color: error));
        sut.Warning.Should().Be(expected: new BrandState(color: warning));
        sut.Success.Should().Be(expected: new BrandState(color: success));
        sut.Info.Should().Be(expected: new BrandState(color: info));
    }

    [Fact]
    public void Constructor_Should_UseStyleDefaults_WhenColorsAreNull()
    {
        // Act
        var sut = new BrandTheme();

        // Assert
        sut.Surface.Should().Be(expected: new BrandState(color: StyleDefaults.SurfaceColorLight));
        sut.Primary.Should().Be(expected: new BrandState(color: StyleDefaults.PrimaryColorLight));
        sut.Secondary.Should().Be(expected: new BrandState(color: StyleDefaults.SecondaryColorLight));
        sut.Tertiary.Should().Be(expected: new BrandState(color: StyleDefaults.TertiaryColorLight));
        sut.Error.Should().Be(expected: new BrandState(color: StyleDefaults.ErrorColorLight));
        sut.Warning.Should().Be(expected: new BrandState(color: StyleDefaults.WarningColorLight));
        sut.Success.Should().Be(expected: new BrandState(color: StyleDefaults.SuccessColorLight));
        sut.Info.Should().Be(expected: new BrandState(color: StyleDefaults.InfoColorLight));
    }
}
