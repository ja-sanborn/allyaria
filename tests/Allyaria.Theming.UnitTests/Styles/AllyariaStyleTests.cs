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
    public void ToCssVars_Should_Apply_CustomPrefix_To_Palette_And_Typography()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography(new AllyariaStringValue("Verdana"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCssVars("brand");

        // Assert
        result.Should()
            .Contain("--brand-color")
            .And.Contain("--brand-background-color")
            .And.Contain("--brand-font-family")
            .And.NotContain("--aa-");
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
            .Contain("--aa-color")
            .And.Contain("--aa-background-color")
            .And.Contain("--aa-font-weight");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("---")]
    public void ToCssVars_Should_Fallback_To_DefaultPrefix_When_Prefix_IsEmptyOrWhitespace(string prefix)
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography(new AllyariaStringValue("Georgia"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCssVars(prefix);

        // Assert
        result.Should()
            .Contain("--aa-color")
            .And.Contain("--aa-font-family");
    }

    [Fact]
    public void ToCssVars_Should_Normalize_CustomPrefix_ToLowercase_AndHyphens()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography(new AllyariaStringValue("Tahoma"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var result = sut.ToCssVars("  --My THEME  ");

        // Assert
        result.Should()
            .Contain("--my-theme-color")
            .And.Contain("--my-theme-font-family");
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
            .And.Contain("--aa-color");
    }
}
