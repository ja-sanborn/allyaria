namespace Allyaria.Theming.UnitTests.BrandTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class BrandStateTests
{
    [Fact]
    public void Constructor_Should_CreateDefaultPalette_FromBaseColor()
    {
        // Arrange
        HexColor baseColor = "#336699";

        // Act
        var sut = new BrandState(color: baseColor);

        // Assert
        var expected = new BrandPalette(color: baseColor);
        sut.Default.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_CreateDisabledPalette_FromDisabledColor()
    {
        // Arrange
        HexColor baseColor = "#336699";

        // Act
        var sut = new BrandState(color: baseColor);

        // Assert
        var expectedDisabledColor = baseColor.ToDisabled();
        var expected = new BrandPalette(color: expectedDisabledColor);

        sut.Disabled.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_CreateDraggedPalette_FromDraggedColor()
    {
        // Arrange
        HexColor baseColor = "#336699";

        // Act
        var sut = new BrandState(color: baseColor);

        // Assert
        var expectedDraggedColor = baseColor.ToDragged();
        var expected = new BrandPalette(color: expectedDraggedColor);

        sut.Dragged.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_CreateFocusedPalette_FromFocusedColor()
    {
        // Arrange
        HexColor baseColor = "#336699";

        // Act
        var sut = new BrandState(color: baseColor);

        // Assert
        var expectedFocusedColor = baseColor.ToFocused();
        var expected = new BrandPalette(color: expectedFocusedColor);

        sut.Focused.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_CreateHoveredPalette_FromHoveredColor()
    {
        // Arrange
        HexColor baseColor = "#336699";

        // Act
        var sut = new BrandState(color: baseColor);

        // Assert
        var expectedHoveredColor = baseColor.ToHovered();
        var expected = new BrandPalette(color: expectedHoveredColor);

        sut.Hovered.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_CreatePressedPalette_FromPressedColor()
    {
        // Arrange
        HexColor baseColor = "#336699";

        // Act
        var sut = new BrandState(color: baseColor);

        // Assert
        var expectedPressedColor = baseColor.ToPressed();
        var expected = new BrandPalette(color: expectedPressedColor);

        sut.Pressed.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_CreateVisitedPalette_FromVisitedColor()
    {
        // Arrange
        HexColor baseColor = "#336699";

        // Act
        var sut = new BrandState(color: baseColor);

        // Assert
        var expectedVisitedColor = baseColor.ToVisited();
        var expected = new BrandPalette(color: expectedVisitedColor);

        sut.Visited.Should().Be(expected: expected);
    }
}
