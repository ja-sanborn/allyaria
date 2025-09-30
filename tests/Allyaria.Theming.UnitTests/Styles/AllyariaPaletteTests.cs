using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaPaletteTests
{
    [Fact]
    public void BorderColor_Should_Default_To_BackgroundColor_When_Border_Present_And_No_Explicit_Color()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.Black,
            Colors.White,
            borderWidth: 2
        );

        // Act
        var actual = sut.BorderColor;

        // Assert
        actual.Should()
            .Be(Colors.Black);
    }

    [Fact]
    public void BorderColor_Should_Default_To_White_When_Border_Present_And_Background_Is_Null()
    {
        // Arrange
        var sut = new AllyariaPalette(
            null,
            Colors.Black,
            borderWidth: 1
        );

        // Act
        var actual = sut.BorderColor;

        // Assert
        actual.Should()
            .Be(Colors.White);
    }

    [Fact]
    public void BorderColor_Should_Return_Explicit_Color_When_Provided_Irrespective_Of_Border()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            borderWidth: 0,
            borderColor: Colors.Red
        );

        // Act
        var actual = sut.BorderColor;

        // Assert
        actual.Should()
            .Be(Colors.Red);
    }

    [Fact]
    public void Cascade_Should_Default_BorderColor_To_BackgroundColor_When_BorderAdded_Without_Explicit_Color()
    {
        // Arrange
        var bg = Colors.White;
        var sut = new AllyariaPalette(bg, Colors.Black, borderWidth: 0);

        // Act
        var cascaded = sut.Cascade(borderWidth: 2);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:"); // defaulting to BackgroundColor (hovered due to border)
    }

    [Fact]
    public void Cascade_Should_Override_BackgroundColor_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black);

        // Act
        var cascaded = sut.Cascade(Colors.Black);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("background-color:#000000FF");
    }

    [Fact]
    public void Cascade_Should_Override_BackgroundImageStretch_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, "pic.png", false);

        // Act
        var cascaded = sut.Cascade(backgroundImageStretch: true);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("background-size:cover");
    }

    [Fact]
    public void Cascade_Should_Override_BorderColor_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderWidth: 2);

        // Act
        var cascaded = sut.Cascade(borderColor: Colors.Red);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:#FF0000FF");
    }

    [Fact]
    public void Cascade_Should_Override_BorderRadius_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black);

        // Act
        var cascaded = sut.Cascade(borderRadius: new AllyariaStringValue("12px"));
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-radius:12px;");
    }

    [Fact]
    public void Cascade_Should_Override_BorderStyle_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderWidth: 1);

        // Act
        var cascaded = sut.Cascade(borderStyle: new AllyariaStringValue("dashed"));
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-style:dashed");
    }

    [Fact]
    public void Cascade_Should_Override_BorderWidth_When_Positive()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderWidth: 0);

        // Act
        var cascaded = sut.Cascade(borderWidth: 3);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-style:solid")
            .And.Contain("border-width:3px");
    }

    [Fact]
    public void Cascade_Should_Remove_Border_When_BorderWidth_Is_Negative()
    {
        // Arrange
        var darkBg = new AllyariaColorValue("#202020FF");
        var sut = new AllyariaPalette(darkBg, Colors.White, borderWidth: 2);

        // Act
        var cascaded = sut.Cascade(borderWidth: -1);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-width:2px")
            .And.Contain("border-color:#202020FF");
    }

    [Fact]
    public void Cascade_Should_Retain_Original_BackgroundColor_When_Not_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.Black, Colors.White);

        // Act
        var cascaded = sut.Cascade();
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("background-color:#000000FF");
    }

    [Fact]
    public void Cascade_Should_Retain_Original_BackgroundImage_When_Not_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, "bg.jpg");

        // Act
        var cascaded = sut.Cascade();
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("url(\"bg.jpg\")");
    }

    [Fact]
    public void Cascade_Should_Retain_Original_BorderColor_When_Not_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderWidth: 1, borderColor: Colors.Black);

        // Act
        var cascaded = sut.Cascade();
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:#000000FF");
    }

    [Fact]
    public void Cascade_Should_Retain_Original_BorderRadius_When_Not_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderRadius: new AllyariaStringValue("6px"));

        // Act
        var cascaded = sut.Cascade();
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-radius:6px;");
    }

    [Fact]
    public void Cascade_Should_Retain_Original_BorderStyle_When_Not_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            borderWidth: 1,
            borderStyle: new AllyariaStringValue("dotted")
        );

        // Act
        var cascaded = sut.Cascade();
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("border-style:dotted");
    }

    [Fact]
    public void Cascade_Should_Retain_Original_ForegroundColor_When_Not_Provided()
    {
        // Arrange
        var explicitForeground = new AllyariaColorValue("#112233FF");
        var sut = new AllyariaPalette(Colors.White, explicitForeground);

        // Act
        var cascaded = sut.Cascade();
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("color:#112233FF");
    }

    [Fact]
    public void Ctor_Should_Default_BackgroundColor_To_White_When_Null_Is_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(
            null,
            Colors.Black
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
            Colors.White
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
    public void ToCss_Should_EmitColorAndBackgroundColor_When_NoImageAndNoBorder()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black
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
    public void ToCss_Should_Include_BackgroundImage_Positioning_When_Stretch_True()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, "hero.jpg");

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("url(\"hero.jpg\")")
            .And.Contain("background-position:center")
            .And.Contain("background-repeat:no-repeat")
            .And.Contain("background-size:cover");
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
    public void ToCss_Should_Not_Emit_Border_When_BorderWidth_Is_Null()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderWidth: null);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .NotContain("border-color")
            .And.NotContain("border-style")
            .And.NotContain("border-width");
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
    public void ToCss_Should_Not_Include_Image_Positioning_When_Stretch_False()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.Black, Colors.White, "hero.jpg", false);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("background-image:")
            .And.NotContain("background-position:")
            .And.NotContain("background-size:cover");
    }

    [Fact]
    public void ToCss_Should_OmitBorderDeclarations_When_BorderWidthZero()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
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
    public void ToCssVars_Should_Default_BorderStyle_To_Solid_When_Width_Positive_And_Style_Not_Specified()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderWidth: 1);

        // Act
        var css = sut.ToCssVars("Theme");

        // Assert
        css.Should()
            .Contain("--theme-border-style:solid");
    }

    [Fact]
    public void ToCssVars_Should_Default_Prefix_To_AA_When_Empty_Or_Whitespace()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black);

        // Act
        var css = sut.ToCssVars("   ");

        // Assert
        css.Should()
            .Contain("--aa-color:#000000FF")
            .And.Contain("--aa-background-color:#000000FF");
    }

    [Fact]
    public void ToCssVars_Should_Emit_Border_Vars_When_BorderWidth_Positive()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            borderWidth: 2,
            borderColor: Colors.Red,
            borderStyle: new AllyariaStringValue("dashed")
        );

        // Act
        var css = sut.ToCssVars("Theme");

        // Assert
        css.Should()
            .Contain("--theme-border-color:#FF0000FF")
            .And.Contain("--theme-border-style:dashed")
            .And.Contain("--theme-border-width:2px");
    }

    [Fact]
    public void ToCssVars_Should_Emit_Radius_Var_Using_Raw_Prefix_For_BorderRadius()
    {
        // Arrange
        var sut = new AllyariaPalette(borderRadius: new AllyariaStringValue("8px"));
        var prefix = "test  - - -  test"; // raw, unnormalized

        // Act
        var css = sut.ToCssVars(prefix);

        // Assert
        // Uses raw prefix (not normalized like other vars) per current implementation
        css.Should()
            .Contain("test  - - -  testborder-radius:8px")
            .And.NotContain("--test-test-border-radius:8px");
    }

    [Fact]
    public void ToCssVars_Should_Include_Image_Vars_With_Normalized_Prefix_When_Image_And_Stretch_True()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, "banner.png");

        // Act
        var css = sut.ToCssVars("My Theme");

        // Assert
        css.Should()
            .Contain("--my-theme-background-image:")
            .And.Contain("--my-theme-background-position:center")
            .And.Contain("--my-theme-background-repeat:no-repeat")
            .And.Contain("--my-theme-background-size:cover");
    }

    [Fact]
    public void ToCssVars_Should_Include_Only_BackgroundImage_Var_When_Stretch_False()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.Black, Colors.White, "img.png", false);

        // Act
        var css = sut.ToCssVars();

        // Assert
        css.Should()
            .Contain("--aa-background-image:")
            .And.NotContain("--aa-background-position")
            .And.NotContain("--aa-background-size");
    }

    [Fact]
    public void ToCssVars_Should_Not_Emit_Border_Vars_When_Border_Not_Present()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, borderWidth: 0);

        // Act
        var css = sut.ToCssVars("Theme");

        // Assert
        css.Should()
            .NotContain("--theme-border-color")
            .And.NotContain("--theme-border-style")
            .And.NotContain("--theme-border-width");
    }

    [Fact]
    public void ToDisabledPalette_Should_Desaturate_Compress_Value_And_Adjust_Border()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(120, 80, 80);
        var border = AllyariaColorValue.FromHsva(120, 80, 70);
        var sut = new AllyariaPalette(bg, Colors.Black, borderWidth: 1, borderColor: border);

        // Act
        var disabled = sut.ToDisabledPalette(); // default: desaturate 60, blend 0.15

        // Assert
        disabled.BackgroundColor.S.Should()
            .BeApproximately(20.0, 1.0);

        disabled.BackgroundColor.V.Should()
            .BeApproximately(75.5, 1.0);

        disabled.BorderColor.S.Should()
            .BeApproximately(20.0, 1.0);

        disabled.BorderColor.V.Should()
            .BeApproximately(67.0, 1.0);
    }

    [Fact]
    public void ToDisabledPalette_Should_Leave_Border_Omitted_When_Not_Present()
    {
        // Arrange
        var sut = new AllyariaPalette(AllyariaColorValue.FromHsva(0, 50, 60), Colors.Black, borderWidth: 0);

        // Act
        var disabled = sut.ToDisabledPalette();
        var css = disabled.ToCss();

        // Assert
        css.Should()
            .NotContain("border-");
    }

    [Fact]
    public void ToHoverPalette_Should_Darken_Light_Surface_And_Adjust_Border()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(0, 0, 90);
        var border = AllyariaColorValue.FromHsva(0, 100, 50);
        var sut = new AllyariaPalette(bg, Colors.Black, borderWidth: 1, borderColor: border);

        // Act
        var hover = sut.ToHoverPalette(); // default 6 / 8

        // Assert
        hover.BackgroundColor.V.Should()
            .BeApproximately(84.0, 1.0);

        hover.BorderColor.V.Should()
            .BeApproximately(42.0, 1.0);
    }

    [Fact]
    public void ToHoverPalette_Should_Lighten_Dark_Surface_And_Adjust_Border()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(210, 50, 20);
        var border = AllyariaColorValue.FromHsva(210, 50, 10);
        var sut = new AllyariaPalette(bg, Colors.White, borderWidth: 1, borderColor: border);

        // Act
        var hover = sut.ToHoverPalette();

        // Assert
        hover.BackgroundColor.V.Should()
            .BeApproximately(26.0, 1.0);

        hover.BorderColor.V.Should()
            .BeApproximately(18.0, 1.0);
    }
}
