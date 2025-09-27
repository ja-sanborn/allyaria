using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaStyleTests
{
    [Fact]
    public void ToCss_Should_CombinePaletteAndTypographyCss_When_BothProvided()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography(new AllyariaStringValue("Inter"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCss();

        // Assert
        result.Should()
            .Contain("color:")
            .And.Contain("background-color")
            .And.Contain("font-family");
    }

    [Fact]
    public void ToCss_Should_ReturnOnlyPaletteCss_When_TypographyIsDefault()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography();
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCss();

        // Assert
        result.Should()
            .Contain("color:")
            .And.Contain("background-color");
    }

    [Fact]
    public void ToCssHover_Should_CombinePaletteHoverAndTypographyCss_When_BothProvided()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography(fontSize: new AllyariaStringValue("16px"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCssHover();

        // Assert
        result.Should()
            .Contain("color:")
            .And.Contain("background-color")
            .And.Contain("font-size");
    }

    [Fact]
    public void ToCssVars_Should_CombinePaletteAndTypographyCssVars_When_BothProvided()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography(fontWeight: new AllyariaStringValue("bold"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCssVars();

        // Assert
        result.Should()
            .Contain("--aa-fg")
            .And.Contain("--aa-bg")
            .And.Contain("--aa-font-weight");
    }

    [Fact]
    public void ToCssVars_Should_ReturnOnlyTypographyVars_When_PaletteIsDefault()
    {
        // Arrange
        var palette = new AllyariaPalette(); // default palette
        var typography = new AllyariaTypography(fontStyle: new AllyariaStringValue("italic"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCssVars();

        // Assert
        result.Should()
            .Contain("--aa-font-style")
            .And.Contain("--aa-fg");
    }
}
