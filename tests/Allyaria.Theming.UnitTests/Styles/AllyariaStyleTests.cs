using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaStyleTests
{
    [Fact]
    public void Constructor_Should_Use_Derived_DisabledPalette_When_PaletteDisabled_NotProvided()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));

        // Act
        var sut = new AllyariaStyle(palette, typography);

        // Assert
        sut.PaletteDisabled.Should()
            .NotBeNull();

        sut.PaletteDisabled.Should()
            .NotBeEquivalentTo(palette); // disabled derived with different colors
    }

    [Fact]
    public void Constructor_Should_Use_Derived_HoverPalette_When_PaletteHover_NotProvided()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));

        // Act
        var sut = new AllyariaStyle(palette, typography);

        // Assert
        sut.PaletteHover.Should()
            .NotBeNull();

        sut.PaletteHover.Should()
            .NotBeEquivalentTo(palette); // hover derived with different colors

        sut.TypographyHover.Should()
            .BeEquivalentTo(typography);
    }

    [Fact]
    public void Constructor_Should_Use_Provided_DisabledPalette_When_Specified()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));
        var disabledPalette = new AllyariaPalette(Colors.Red, Colors.Blue);

        // Act
        var sut = new AllyariaStyle(palette, typography, null, null, disabledPalette);

        // Assert
        sut.PaletteDisabled.Should()
            .BeEquivalentTo(disabledPalette);
    }

    [Fact]
    public void Constructor_Should_Use_Provided_HoverPalette_And_HoverTypography_When_Specified()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));

        var hoverPalette = new AllyariaPalette(Colors.Red, Colors.Blue);
        var hoverTypography = new AllyariaTypography(new AllyariaStringValue("Segoe UI"));

        // Act
        var sut = new AllyariaStyle(palette, typography, hoverPalette, hoverTypography);

        // Assert
        sut.PaletteHover.Should()
            .BeEquivalentTo(hoverPalette);

        sut.TypographyHover.Should()
            .BeEquivalentTo(hoverTypography);
    }

    [Fact]
    public void ToCss_Should_Combine_Palette_And_Typography_Css()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("color:") // palette emits color
            .And.Contain("font-family:Arial"); // typography emits family
    }

    [Fact]
    public void ToCssHover_Should_Combine_HoverPalette_And_HoverTypography_Css()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));
        var hoverPalette = new AllyariaPalette(Colors.Green, Colors.Yellow);
        var hoverTypography = new AllyariaTypography(new AllyariaStringValue("Tahoma"));

        var sut = new AllyariaStyle(palette, typography, hoverPalette, hoverTypography);

        // Act
        var css = sut.ToCssHover();

        // Assert
        css.Should()
            .Contain("color:") // palette emits color
            .And.Contain("font-family:Tahoma");
    }

    [Fact]
    public void ToCssVars_Should_Combine_All_Base_And_Hover_Variables_With_NormalizedPrefix()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"), new AllyariaStringValue("16px"));
        var hoverPalette = new AllyariaPalette(Colors.Red, Colors.Blue);

        var hoverTypography = new AllyariaTypography(
            new AllyariaStringValue("Tahoma"),
            new AllyariaStringValue("14px")
        );

        var sut = new AllyariaStyle(palette, typography, hoverPalette, hoverTypography);

        // Act
        var css = sut.ToCssVars("My Theme");

        // Assert
        css.Should()
            .Contain("--my-theme-color:")
            .And.Contain("--my-theme-font-family:Arial")
            .And.Contain("--my-theme-hover-color:")
            .And.Contain("--my-theme-hover-font-family:Tahoma");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ToCssVars_Should_Default_To_AA_Prefix_When_Input_Empty_OrWhitespace(string input)
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var css = sut.ToCssVars(input);

        // Assert
        css.Should()
            .Contain("--aa-color:")
            .And.Contain("--aa-hover-color:");
    }

    [Fact]
    public void ToCssVars_Should_Normalize_Whitespace_And_Dashes_To_SingleDash()
    {
        // Arrange
        var palette = new AllyariaPalette(Colors.White, Colors.Black);
        var typography = new AllyariaTypography(new AllyariaStringValue("Arial"));
        var sut = new AllyariaStyle(palette, typography);

        // Act
        var css = sut.ToCssVars("test  - - - test");

        // Assert
        css.Should()
            .Contain("--test-test-color:")
            .And.Contain("--test-test-hover-color:")
            .And.NotContain("  ");
    }
}
