using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaPaletteTests
{
    [Fact]
    public void BackgroundImage_Should_BeNull_When_EmptyOrWhitespace()
    {
        // Arrange
        var sutEmpty = new AllyariaPalette(backgroundImage: "");
        var sutWhitespace = new AllyariaPalette(backgroundImage: "   ");

        // Act
        var imgEmpty = sutEmpty.BackgroundImage;
        var imgWhitespace = sutWhitespace.BackgroundImage;

        // Assert
        imgEmpty.Should()
            .BeNull();

        imgWhitespace.Should()
            .BeNull();
    }

    [Fact]
    public void BackgroundImage_Should_ContainLowerCasedTrimmedUrl_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(backgroundImage: "  HTTPS://CDN.EXAMPLE.COM/Hero.JPG  ");

        // Act
        var img = sut.BackgroundImage;

        // Assert
        img.Should()
            .NotBeNull();

        img.ToCss("background-image")
            .Should()
            .Contain("url(\"https://cdn.example.com/hero.jpg\")")
            .And.Contain("linear-gradient(");
    }

    [Fact]
    public void ToCss_Should_DefaultBorderStyleToSolid_When_StyleNotSupplied()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            borderWidth: 1,
            borderColor: Colors.Black
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-style:solid");
    }

    [Fact]
    public void ToCss_Should_EmitBackgroundImageAndNoBackgroundColor_When_ImageProvided()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "  HTTPS://EXAMPLE.COM/Img.Png  "
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("background-image:")
            .And.Contain("linear-gradient(")
            .And.Contain("url(\"https://example.com/img.png\")") // lowercased + trimmed
            .And.Contain("background-position:center;")
            .And.Contain("background-repeat:no-repeat;")
            .And.Contain("background-size:cover;")
            .And.NotContain("background-color:");
    }

    [Fact]
    public void ToCss_Should_EmitBorderDeclarations_When_BorderWidthPositive()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "",
            borderWidth: 2,
            borderColor: Colors.Black,
            borderStyle: new AllyariaStringValue("dashed"),
            borderRadius: new AllyariaStringValue("4px")
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:")
            .And.Contain("border-style:dashed")
            .And.Contain("border-width:2px")
            .And.Contain("border-radius:4px");
    }

    [Fact]
    public void ToCss_Should_EmitColorAndBackgroundColor_When_NoImageAndNoBorder()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "",
            borderWidth: 0
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("color:")
            .And.Contain("background-color:")
            .And.NotContain("background-image")
            .And.NotContain("border-");
    }

    [Fact]
    public void ToCss_Should_OmitBorderDeclarations_When_BorderWidthZero()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "",
            borderWidth: 0,
            borderRadius: new AllyariaStringValue("12px") // radius should still be ignored without a border
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .NotContain("border-color")
            .And.NotContain("border-style")
            .And.NotContain("border-width")
            .And.NotContain("border-radius");
    }

    [Fact]
    public void ToCssVars_Should_EmitBorderVars_When_BorderPresent()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            borderWidth: 3,
            borderColor: Colors.Black,
            borderStyle: new AllyariaStringValue("dotted"),
            borderRadius: new AllyariaStringValue("8px")
        );

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-border-color")
            .And.Contain("--aa-border-style:dotted")
            .And.Contain("--aa-border-width:3px")
            .And.Contain("--aa-border-radius:8px");
    }

    [Fact]
    public void ToCssVars_Should_EmitFgAndBgVars_When_NoImage()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            Colors.Black, // explicit to avoid relying on HoverColor() implementation
            Colors.White // explicit to avoid relying on HoverColor() implementation
        );

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-fg")
            .And.Contain("--aa-fg-hover")
            .And.Contain("--aa-bg")
            .And.Contain("--aa-bg-hover")
            .And.NotContain("--aa-bg-image");
    }

    [Fact]
    public void ToCssVars_Should_EmitFgAndImageVars_When_ImagePresent()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            foregroundHoverColor: Colors.White,
            backgroundImage: "Example.png",
            borderWidth: 0
        );

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-fg")
            .And.Contain("--aa-fg-hover")
            .And.Contain("--aa-bg-image")
            .And.NotContain("--aa-bg;") // ensure no plain bg variables
            .And.NotContain("--aa-bg-hover");
    }

    [Fact]
    public void ToCssVars_Should_OmitBorderVars_When_BorderAbsent()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            borderWidth: 0
        );

        // Act
        var vars = sut.ToCssVars();

        // Assert
        vars.Should()
            .NotContain("--aa-border-color")
            .And.NotContain("--aa-border-style")
            .And.NotContain("--aa-border-width")
            .And.NotContain("--aa-border-radius");
    }
}
