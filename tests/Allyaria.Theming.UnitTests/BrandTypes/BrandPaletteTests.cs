namespace Allyaria.Theming.UnitTests.BrandTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class BrandPaletteTests
{
    [Fact]
    public void Constructor_Should_InitializePaletteColors_When_BaseColorIsProvided()
    {
        // Arrange
        HexColor baseColor = default;

        // Act
        var sut = new BrandPalette(color: baseColor);

        // Assert
        var expectedBackground = baseColor.SetAlpha(alpha: 255);
        var expectedForeground = expectedBackground.ToForeground().EnsureContrast(background: expectedBackground);
        var expectedAccentFromForeground = expectedForeground.ToAccent().EnsureContrast(background: expectedBackground);
        var expectedAccentFromBackground = expectedBackground.ToAccent().EnsureContrast(background: expectedBackground);

        sut.BackgroundColor.Should().Be(expected: expectedBackground);
        sut.ForegroundColor.Should().Be(expected: expectedForeground);
        sut.AccentColor.Should().Be(expected: expectedAccentFromForeground);
        sut.BorderColor.Should().Be(expected: expectedAccentFromBackground);
    }

    [Fact]
    public void Constructor_Should_SetCaretColorEqualToForegroundColor_When_BaseColorIsProvided()
    {
        // Arrange
        HexColor baseColor = default;

        // Act
        var sut = new BrandPalette(color: baseColor);

        // Assert
        sut.CaretColor.Should().Be(expected: sut.ForegroundColor);
    }

    [Fact]
    public void Constructor_Should_SetOutlineAndTextDecorationColorsEqualToAccentColor_When_BaseColorIsProvided()
    {
        // Arrange
        HexColor baseColor = default;

        // Act
        var sut = new BrandPalette(color: baseColor);

        // Assert
        sut.OutlineColor.Should().Be(expected: sut.AccentColor);
        sut.TextDecorationColor.Should().Be(expected: sut.AccentColor);
    }
}
