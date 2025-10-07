using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryPaletteElevationTests
{
    [Fact]
    public void Ctor_Should_CreateDefaultPalette_When_PaletteIsNull()
    {
        // Arrange & Act
        var sut = new AryPaletteElevation(null);

        // Assert
        sut.Palette.Should().NotBeNull();

        // sanity: palettes from each layer should be obtainable without exceptions
        var state = ComponentState.Default;
        sut.Lowest.ToPalette(state).Should().NotBeNull();
        sut.Low.ToPalette(state).Should().NotBeNull();
        sut.Mid.ToPalette(state).Should().NotBeNull();
        sut.High.ToPalette(state).Should().NotBeNull();
        sut.Highest.ToPalette(state).Should().NotBeNull();
    }

    [Fact]
    public void Ctor_Should_SetProvidedPalette_When_PaletteIsNotNull()
    {
        // Arrange
        var provided = new AryPalette();

        // Act
        var sut = new AryPaletteElevation(provided);

        // Assert
        sut.Palette.Should().Be(provided);
        sut.Lowest.Should().NotBeNull();
        sut.Low.Should().NotBeNull();
        sut.Mid.Should().NotBeNull();
        sut.High.Should().NotBeNull();
        sut.Highest.Should().NotBeNull();
    }

    [Fact]
    public void ToPalette_DefaultBranch_Should_MatchMid_For_MidElevation()
    {
        // Arrange
        var sut = new AryPaletteElevation(new AryPalette());
        var state = ComponentState.Default;

        // Act
        var viaMidEnum = sut.ToPalette(ComponentElevation.Mid, state); // falls into default branch
        var viaMidLayer = sut.Mid.ToPalette(state);

        // Assert
        viaMidEnum.Should().Be(viaMidLayer);
    }

    [Fact]
    public void ToPalette_Should_NotThrow_And_ReturnPalette_For_AllEnumValues()
    {
        // Arrange
        var sut = new AryPaletteElevation(new AryPalette());
        var allStates = Enum.GetValues<ComponentState>();

        // Act
        foreach (var elevation in Enum.GetValues<ComponentElevation>())
        {
            foreach (var state in allStates)
            {
                Action act = () => _ = sut.ToPalette(elevation, state);

                // Assert
                act.Should().NotThrow();
                sut.ToPalette(elevation, state).Should().NotBeNull();
            }
        }
    }

    [Fact]
    public void ToPalette_Should_ReturnHighestLayerPalette_When_ElevationIsHighest()
    {
        // Arrange
        var sut = new AryPaletteElevation(new AryPalette());
        var state = ComponentState.Default;

        // Act
        var result = sut.ToPalette(ComponentElevation.Highest, state);

        // Assert
        var expected = sut.Highest.ToPalette(state);
        result.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnHighLayerPalette_When_ElevationIsHigh()
    {
        // Arrange
        var sut = new AryPaletteElevation(new AryPalette());
        var state = ComponentState.Disabled;

        // Act
        var result = sut.ToPalette(ComponentElevation.High, state);

        // Assert
        var expected = sut.High.ToPalette(state);
        result.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnLowestLayerPalette_When_ElevationIsLowest()
    {
        // Arrange
        var sut = new AryPaletteElevation(new AryPalette());
        var state = ComponentState.Hovered;

        // Act
        var result = sut.ToPalette(ComponentElevation.Lowest, state);

        // Assert
        var expected = sut.Lowest.ToPalette(state);
        result.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnLowLayerPalette_When_ElevationIsLow()
    {
        // Arrange
        var sut = new AryPaletteElevation(new AryPalette());
        var state = ComponentState.Pressed;

        // Act
        var result = sut.ToPalette(ComponentElevation.Low, state);

        // Assert
        var expected = sut.Low.ToPalette(state);
        result.Should().Be(expected);
    }

    [Fact]
    public void ToPalette_Should_ReturnMidLayerPalette_When_ElevationIsUnrecognized()
    {
        // Arrange
        var sut = new AryPaletteElevation(new AryPalette());
        var state = ComponentState.Hovered;
        var unknownElevation = (ComponentElevation)9999;

        // Act
        var result = sut.ToPalette(unknownElevation, state);

        // Assert
        var expected = sut.Mid.ToPalette(state);
        result.Should().Be(expected);
    }
}
