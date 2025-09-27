using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaPaletteTests
{
    [Fact]
    public void BorderColor_Should_Default_To_BackgroundColor_Hover_When_Border_Present_And_No_Explicit_BorderColor()
    {
        // Arrange
        var darkBg = new AllyariaColorValue("#202020FF");

        var sut = new AllyariaPalette(
            darkBg,
            Colors.White,
            backgroundImage: "",
            borderWidth: 2,
            borderColor: null
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:#535353FF");
    }

    [Fact]
    public void BorderColor_Should_Use_Explicit_BorderColor_When_Provided_And_Border_Present()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "",
            borderWidth: 1,
            borderColor: Colors.Red
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:#FF0000FF");
    }

    [Fact]
    public void Ctor_Should_Default_BackgroundColor_To_White_When_Null_Is_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(
            null,
            Colors.Black,
            backgroundImage: "",
            borderWidth: 0
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("background-color:#FFFFFFFF");
    }

    [Fact]
    public void Ctor_Should_Use_Provided_BackgroundColor_When_Not_Null()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.Black,
            Colors.White,
            backgroundImage: "",
            borderWidth: 0
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("background-color:#000000FF");
    }

    [Fact]
    public void ForegroundColor_Should_Default_To_Black_When_Background_Is_Light()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("color:#000000FF");
    }

    [Fact]
    public void ForegroundColor_Should_Default_To_White_When_Background_Is_Dark()
    {
        // Arrange
        var sut = new AllyariaPalette(new AllyariaColorValue("#202020FF"));

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("color:#FFFFFFFF");
    }

    [Fact]
    public void ForegroundColor_Should_Use_Explicit_Value_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Red,
            backgroundImage: "",
            borderWidth: 0
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("color:#FF0000FF");
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
    public void ToCss_Should_Include_BorderRadius_When_Radius_Is_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(borderRadius: new AllyariaStringValue("8px"));

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-radius:8px;");
    }

    [Fact]
    public void ToCss_Should_Not_Include_BorderRadius_When_Radius_Is_Null()
    {
        // Arrange
        var sut = new AllyariaPalette();

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .NotContain("border-radius:");
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
            .And.NotContain("border-width");
    }

    [Fact]
    public void ToCssHover_Should_EmitBackgroundHoverColor_When_NoImageAndNoBorder()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "",
            borderWidth: 0
        );

        // Act
        var css = sut.ToCssHover();

        // Assert
        css.Should()
            .Contain("color:")
            .And.Contain("background-color:")
            .And.NotContain("background-image")
            .And.NotContain("border-");
    }

    [Fact]
    public void ToCssHover_Should_EmitBackgroundImageAndNoBackgroundHoverColor_When_ImageProvided()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            Colors.Red, // explicit hover color, should be ignored due to image
            backgroundImage: "https://example.com/pic.png"
        );

        // Act
        var css = sut.ToCssHover();

        // Assert
        css.Should()
            .Contain("background-image:")
            .And.Contain("linear-gradient(")
            .And.Contain("url(\"https://example.com/pic.png\")")
            .And.Contain("background-position:center;")
            .And.Contain("background-repeat:no-repeat;")
            .And.Contain("background-size:cover;")
            .And.NotContain("background-color:");
    }

    [Fact]
    public void ToCssHover_Should_EmitBorderDeclarations_When_BorderWidthPositive()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "",
            borderWidth: 2,
            borderColor: Colors.Black,
            borderStyle: new AllyariaStringValue("dotted"),
            borderRadius: new AllyariaStringValue("5px")
        );

        // Act
        var css = sut.ToCssHover();

        // Assert
        css.Should()
            .Contain("border-color:")
            .And.Contain("border-style:dotted")
            .And.Contain("border-width:2px")
            .And.Contain("border-radius:5px");
    }

    [Fact]
    public void ToCssHover_Should_Include_BorderRadius_When_Radius_Is_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(borderRadius: new AllyariaStringValue("12px"));

        // Act
        var css = sut.ToCssHover();

        // Assert
        css.Should()
            .Contain("border-radius:12px;");
    }

    [Fact]
    public void ToCssHover_Should_Not_Include_BorderRadius_When_Radius_Is_Null()
    {
        // Arrange
        var sut = new AllyariaPalette();

        // Act
        var css = sut.ToCssHover();

        // Assert
        css.Should()
            .NotContain("border-radius:");
    }

    [Fact]
    public void ToCssHover_Should_OmitBorderDeclarations_When_BorderWidthZero()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            borderWidth: 0,
            borderRadius: new AllyariaStringValue("10px")
        );

        // Act
        var css = sut.ToCssHover();

        // Assert
        css.Should()
            .NotContain("border-color")
            .And.NotContain("border-style")
            .And.NotContain("border-width");
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
    public void ToCssVars_Should_Include_BorderRadius_Var_When_Radius_Is_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(borderRadius: new AllyariaStringValue("4px"));

        // Act
        var cssVars = sut.ToCssVars();

        // Assert
        cssVars.Should()
            .Contain("--aa-border-radius:4px;");
    }

    [Fact]
    public void ToCssVars_Should_Not_Include_BorderRadius_Var_When_Radius_Is_Null()
    {
        // Arrange
        var sut = new AllyariaPalette();

        // Act
        var cssVars = sut.ToCssVars();

        // Assert
        cssVars.Should()
            .NotContain("--aa-border-radius:");
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
            .And.NotContain("--aa-border-width");
    }
}
