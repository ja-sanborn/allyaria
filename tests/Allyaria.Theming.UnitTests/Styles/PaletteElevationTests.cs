namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class PaletteElevationTests
{
    [Fact]
    public void Ctor_Should_Initialize_AllElevationLayers_From_BasePalette()
    {
        // Arrange
        var basePalette = new Palette();

        // Act
        var sut = new PaletteElevation(basePalette);

        // Assert
        sut.Lowest.Default.Should().Be(basePalette);
        sut.Low.Default.Should().Be(basePalette.ToElevation1());
        sut.Mid.Default.Should().Be(basePalette.ToElevation2());
        sut.High.Default.Should().Be(basePalette.ToElevation3());
        sut.Highest.Default.Should().Be(basePalette.ToElevation4());
    }

    [Fact]
    public void ToPalette_Should_FallBackToMidLayer_When_ElevationIsUnrecognized()
    {
        // Arrange
        var basePalette = new Palette();
        var sut = new PaletteElevation(basePalette);
        var state = ComponentState.Pressed;
        var expected = basePalette.ToElevation2().ToPressed(); // Mid.Pressed

        // Act
        var actual = sut.ToPalette((ComponentElevation)12345, state);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnHighestLayer_When_ElevationIsHighest()
    {
        // Arrange
        var basePalette = new Palette();
        var sut = new PaletteElevation(basePalette);
        var expected = basePalette.ToElevation4(); // Highest.Default

        // Act
        var actual = sut.ToPalette(ComponentElevation.Highest, ComponentState.Default);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnHighLayer_With_StateRouting_When_ElevationIsHigh()
    {
        // Arrange
        var basePalette = new Palette();
        var sut = new PaletteElevation(basePalette);
        var expected = basePalette.ToElevation3().ToHovered(); // High.Hovered

        // Act
        var actual = sut.ToPalette(ComponentElevation.High, ComponentState.Hovered);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnLowestLayer_When_ElevationIsLowest()
    {
        // Arrange
        var basePalette = new Palette();
        var sut = new PaletteElevation(basePalette);
        var expected = basePalette; // Lowest.Default

        // Act
        var actual = sut.ToPalette(ComponentElevation.Lowest, ComponentState.Default);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnLowLayer_When_ElevationIsLow()
    {
        // Arrange
        var basePalette = new Palette();
        var sut = new PaletteElevation(basePalette);
        var expected = basePalette.ToElevation1(); // Low.Default

        // Act
        var actual = sut.ToPalette(ComponentElevation.Low, ComponentState.Default);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnMidLayer_When_ElevationIsMid()
    {
        // Arrange
        var basePalette = new Palette();
        var sut = new PaletteElevation(basePalette);
        var expected = basePalette.ToElevation2(); // Mid.Default

        // Act
        var actual = sut.ToPalette(ComponentElevation.Mid, ComponentState.Default);

        // Assert
        actual.Should().Be(expected);
    }
}
