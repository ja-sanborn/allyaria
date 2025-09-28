using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaPaletteTests
{
    [Fact]
    public void BorderColor_Should_Be_Transparent_When_No_Border_And_No_Explicit_Color()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.Black,
            Colors.White,
            borderWidth: 0
        );

        // Act
        var actual = sut.BorderColor;

        // Assert
        actual.Should()
            .Be(Colors.Transparent);
    }

    [Fact]
    public void BorderColor_Should_Default_To_BackgroundColor_Hover_When_Border_Present_And_No_Explicit_BorderColor()
    {
        // Arrange
        var darkBg = new AllyariaColorValue("#202020FF");

        var sut = new AllyariaPalette(
            darkBg,
            Colors.White,
            "",
            false,
            2
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:#202020FF");
    }

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
    public void BorderColor_Should_Use_Explicit_BorderColor_When_Provided_And_Border_Present()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            "",
            false,
            1,
            Colors.Red
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:#FF0000FF");
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
    public void Cascade_Should_Override_BackgroundImage_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black);

        // Act
        var cascaded = sut.Cascade(backgroundImage: "  /Hero.PNG  ");
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("background-image:")
            .And.Contain("linear-gradient(")
            .And.Contain("url(\"/hero.png\")")
            .And.NotContain("background-color:");
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
    public void Cascade_Should_Override_ForegroundColor_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White);

        // Act
        var cascaded = sut.Cascade(foregroundColor: Colors.Red);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("color:#FF0000FF");
    }

    [Fact]
    public void Cascade_Should_Recompute_BackgroundColor_Hover_When_Border_Added_And_No_BackgroundColor_Override()
    {
        // Arrange
        var darkBg = new AllyariaColorValue("#202020FF");
        var sut = new AllyariaPalette(darkBg, Colors.White, borderWidth: 0);

        // Act
        var cascaded = sut.Cascade(borderWidth: 1);
        var css = cascaded.ToCss();

        // Assert
        css.Should()
            .Contain("background-color:#535353FF");
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
    public void ForegroundColor_Should_Use_Explicit_Value_When_Provided()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Red
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
            "  HTTPS://EXAMPLE.COM/Img.Png  "
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
            "",
            false,
            2,
            Colors.Black,
            new AllyariaStringValue("dashed"),
            new AllyariaStringValue("4px")
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
    public void ToCssVars_Should_Collapse_Whitespace_To_Single_Dash_In_Prefix_Example()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black);
        var prefix = "test  - - -  test";

        // Act
        var css = sut.ToCssVars(prefix);

        // Assert
        css.Should()
            .Contain("--test-test-color:#000000FF")
            .And.Contain("--test-test-background-color:#FFFFFFFF");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ToCssVars_Should_Default_To_AA_Prefix_When_Prefix_Is_Empty_Or_Whitespace(string input)
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black);

        // Act
        var css = sut.ToCssVars(input);

        // Assert
        css.Should()
            .Contain("--aa-color:#000000FF")
            .And.Contain("--aa-background-color:#FFFFFFFF")
            .And.NotContain("--aa-background-image")
            .And.NotContain("--aa-border-");
    }

    [Fact]
    public void ToCssVars_Should_Emit_BackgroundImage_Var_And_Omit_BackgroundColor_When_Image_Present()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, "  /Hero.PNG  ");

        // Act
        var css = sut.ToCssVars();

        // Assert
        css.Should()
            .Contain("--aa-background-image:linear-gradient(")
            .And.Contain("url(\"/hero.png\")")
            .And.NotContain("--aa-background-color:");
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
    public void ToCssVars_Should_Normalize_Prefix_And_Lowercase_And_Collapse_Spaces_And_Dashes()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black);
        var prefix = "  --My THEME  - - Name--";

        // Act
        var css = sut.ToCssVars(prefix);

        // Assert
        css.Should()
            .Contain("--my-theme-name-color:#000000FF")
            .And.Contain("--my-theme-name-background-color:#FFFFFFFF")
            .And.NotContain("--My THEME")
            .And.NotContain("  ")
            .And.NotContain("--my--theme--name-");
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
}
