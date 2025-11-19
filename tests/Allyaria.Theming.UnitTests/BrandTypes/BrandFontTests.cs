namespace Allyaria.Theming.UnitTests.BrandTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class BrandFontTests
{
    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_FallbackToDefaultMonospace_When_InputIsNullOrWhitespace(string? input)
    {
        // Arrange
        const string expected =
            "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, 'Liberation Mono', 'Courier New', monospace";

        // Act
        var sut = new BrandFont(monospace: input);

        // Assert
        sut.Monospace.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_FallbackToDefaultSansSerif_When_InputIsNullOrWhitespace(string? input)
    {
        // Arrange
        const string expected =
            "system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif";

        // Act
        var sut = new BrandFont(sansSerif: input);

        // Assert
        sut.SansSerif.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_FallbackToDefaultSerif_When_InputIsNullOrWhitespace(string? input)
    {
        // Arrange
        const string expected = "ui-serif, Georgia, Cambria, 'Times New Roman', Times, serif";

        // Act
        var sut = new BrandFont(serif: input);

        // Assert
        sut.Serif.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_TrimFontNames_When_Provided()
    {
        // Arrange
        const string sansSerif = "  Roboto  ";
        const string serif = "  Georgia ";
        const string monospace = "  Courier New ";

        // Act
        var sut = new BrandFont(sansSerif: sansSerif, serif: serif, monospace: monospace);

        // Assert
        sut.SansSerif.Should().Be(expected: "Roboto");
        sut.Serif.Should().Be(expected: "Georgia");
        sut.Monospace.Should().Be(expected: "Courier New");
    }

    [Fact]
    public void Constructor_Should_UseProvidedFontNames_When_NotNullOrWhitespace()
    {
        // Arrange
        const string sansSerif = "Open Sans";
        const string serif = "Times New Roman";
        const string monospace = "Consolas";

        // Act
        var sut = new BrandFont(sansSerif: sansSerif, serif: serif, monospace: monospace);

        // Assert
        sut.SansSerif.Should().Be(expected: sansSerif);
        sut.Serif.Should().Be(expected: serif);
        sut.Monospace.Should().Be(expected: monospace);
    }
}
