using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaPaletteTests
{
    [Fact]
    public void BackgroundColor_Should_Return_BaseColor_When_HasBorderFalse()
    {
        // Arrange
        var baseBg = Colors.Black; // any explicit color
        var sut = new AllyariaPalette(baseBg, hasBorder: false);

        // Act
        var effective = sut.BackgroundColor;

        // Assert
        effective.Should()
            .Be(baseBg);
    }

    [Fact]
    public void BackgroundColor_Should_Return_HoverOfBase_When_HasBorderTrue()
    {
        // Arrange
        var baseBg = Colors.White;
        var sut = new AllyariaPalette(baseBg, hasBorder: true);

        // Act
        var effective = sut.BackgroundColor;

        // Assert
        effective.Should()
            .Be(baseBg.HoverColor());
    }

    [Fact]
    public void BackgroundHoverColor_Should_Default_To_Background_Hover_When_NotProvided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.Black, hasBorder: false);

        // Act
        var effectiveBg = sut.BackgroundColor;
        var hover = sut.BackgroundHoverColor;

        // Assert
        hover.Should()
            .Be(effectiveBg.HoverColor());
    }

    [Fact]
    public void BackgroundImage_Should_Be_Empty_When_HasBackgroundFalse_Even_If_Provided()
    {
        // Arrange
        var raw = "https://example.com/abc.png";

        // Act
        var sut = new AllyariaPalette(backgroundImage: raw, hasBackground: false);
        var img = sut.BackgroundImage;

        // Assert
        img.Should()
            .BeEmpty();
    }

    [Fact]
    public void BackgroundImage_Should_Be_Normalized_When_Provided_And_HasBackgroundTrue()
    {
        // Arrange
        var raw = "  HtTpS://Example.COM/Images/HeRo.PnG  ";

        // Act
        var sut = new AllyariaPalette(backgroundImage: raw, hasBackground: true);
        var normalized = sut.BackgroundImage;

        // Assert
        normalized.Should()
            .Be("https://example.com/images/hero.png");
    }

    [Fact]
    public void BorderColor_Should_Be_Transparent_When_HasBorderFalse()
    {
        // Arrange
        var sut = new AllyariaPalette(hasBorder: false);

        // Act
        var border = sut.BorderColor;

        // Assert
        border.Should()
            .Be(Colors.Transparent);
    }

    [Fact]
    public void BorderColor_Should_FallBack_To_EffectiveBackground_When_Unset_And_HasBorderTrue()
    {
        // Arrange
        var baseBg = Colors.White;
        var sut = new AllyariaPalette(baseBg, borderColor: null, hasBorder: true);

        // Act
        var border = sut.BorderColor;
        var effectiveBg = sut.BackgroundColor;

        // Assert
        border.Should()
            .Be(effectiveBg);
    }

    [Fact]
    public void ForegroundColor_Should_Default_To_Black_When_Background_Is_Light()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, hasBorder: false);

        // Act
        var fg = sut.ForegroundColor;

        // Assert
        fg.Should()
            .Be(Colors.Black);
    }

    [Fact]
    public void ForegroundColor_Should_Default_To_White_When_Background_Is_Dark()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.Black, hasBorder: false);

        // Act
        var fg = sut.ForegroundColor;

        // Assert
        fg.Should()
            .Be(Colors.White);
    }

    [Fact]
    public void ForegroundHoverColor_Should_Default_To_Foreground_Hover_When_NotProvided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, hasBorder: false);

        // Act
        var fg = sut.ForegroundColor;
        var hover = sut.ForegroundHoverColor;

        // Assert
        hover.Should()
            .Be(fg.HoverColor());
    }

    [Fact]
    public void ToCss_Should_Not_Render_Background_When_HasBackgroundFalse()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "/a.png", hasBackground: false);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .NotContain("background-image:");

        css.Should()
            .NotContain("background-position:");

        css.Should()
            .NotContain("background-repeat:");

        css.Should()
            .NotContain("background-size:");

        css.Should()
            .NotContain("background-color");
    }

    [Fact]
    public void ToCss_Should_Not_Render_BorderColor_When_HasBorderFalse()
    {
        // Arrange
        var sut = new AllyariaPalette(hasBorder: false);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .NotContain("border-color");
    }

    [Fact]
    public void ToCss_Should_Render_BackgroundColor_When_Image_Is_Whitespace()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "   ", hasBackground: true);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("background-color");

        css.Should()
            .NotContain("background-image:");
    }

    [Fact]
    public void ToCss_Should_Render_BackgroundColor_When_Image_Not_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "", hasBackground: true);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("background-color");

        css.Should()
            .NotContain("background-image:");
    }

    [Fact]
    public void ToCss_Should_Render_BackgroundImage_With_Overlay_Position_Repeat_And_Size()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "  /assets/Hero.JPG  ", hasBackground: true);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain(
                "background-image:linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url(\"/assets/hero.jpg\");"
            );

        css.Should()
            .Contain("background-position:center;");

        css.Should()
            .Contain("background-repeat:no-repeat;");

        css.Should()
            .Contain("background-size:cover;");
    }

    [Fact]
    public void ToCss_Should_Render_BorderColor_When_HasBorderTrue()
    {
        // Arrange
        var sut = new AllyariaPalette(hasBorder: true);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-color");
    }

    [Fact]
    public void ToCssVars_Should_Define_Background_Vars_When_No_Image()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "", hasBackground: true);

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-fg:");

        vars.Should()
            .Contain("--aa-fg-hover:");

        vars.Should()
            .Contain("--aa-bg:");

        vars.Should()
            .Contain("--aa-bg-hover:");

        vars.Should()
            .NotContain("--aa-bg-image:");
    }

    [Fact]
    public void ToCssVars_Should_Define_Border_Var_When_HasBorderTrue()
    {
        // Arrange
        var sut = new AllyariaPalette(hasBorder: true);

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-border:");
    }

    [Fact]
    public void ToCssVars_Should_Define_Image_Var_When_Image_Is_Present_And_HasBackgroundTrue()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "https://cdn/x.png", hasBackground: true);

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-fg:");

        vars.Should()
            .Contain("--aa-fg-hover:");

        vars.Should()
            .Contain("--aa-bg-image:");

        vars.Should()
            .NotContain("--aa-bg:");

        vars.Should()
            .NotContain("--aa-bg-hover:");
    }

    [Fact]
    public void ToCssVars_Should_Not_Define_Border_Var_When_HasBorderFalse()
    {
        // Arrange
        var sut = new AllyariaPalette(hasBorder: false);

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .NotContain("--aa-border:");
    }

    [Fact]
    public void ToString_Should_Return_Same_As_ToCss()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "/img.png", hasBackground: true, hasBorder: true);

        // Act
        var asString = sut.ToString();
        var css = sut.ToCss();

        // Assert
        asString.Should()
            .Be(css);
    }
}
