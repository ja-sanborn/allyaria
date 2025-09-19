using Allyaria.Theming.Palette;

namespace Allyaria.Theming.UnitTests.Palette;

public sealed class AllyariaPaletteItemTests
{
    // ----- Background / Foreground precedence -----

    [Fact]
    public void BackgroundColor_HasBorderTrue_UsesHoverVariantOfBase()
    {
        // Arrange
        var baseBg = new AllyariaColor("black");

        var p = new AllyariaPaletteItem(baseBg)
        {
            HasBorder = true
        };

        // Act
        var effective = p.BackgroundColor;

        // Assert (intentional design: background shifts to hover when HasBorder == true)
        effective.Should()
            .Be(baseBg.HoverColor());
    }

    [Fact]
    public void BackgroundColor_Setter_ReturnsSameValue()
    {
        // Arrange
        var color = new AllyariaColor("red");
        var item = new AllyariaPaletteItem();

        // Act
        item.BackgroundColor = color;

        // Assert
        item.BackgroundColor.Should()
            .Be(color);
    }

    [Fact]
    public void BackgroundHoverColor_Setter_ReturnsSameValue()
    {
        // Arrange
        var color = new AllyariaColor("blue");
        var item = new AllyariaPaletteItem();

        // Act
        item.BackgroundHoverColor = color;

        // Assert
        item.BackgroundHoverColor.Should()
            .Be(color);
    }

    // ----- Hover fallbacks -----

    [Fact]
    public void BackgroundHoverColor_WhenUnset_DerivesFromBackground()
    {
        // Arrange
        var bg = new AllyariaColor("black");
        var p = new AllyariaPaletteItem(bg);

        // Act
        var hover = p.BackgroundHoverColor;

        // Assert
        hover.Should()
            .Be(p.BackgroundColor.HoverColor());
    }

    [Fact]
    public void BackgroundImage_Setter_ReturnsSameValue()
    {
        // Arrange
        var url = "https://example.com/image.png";
        var item = new AllyariaPaletteItem();

        // Act
        item.BackgroundImage = url;

        // Assert
        item.BackgroundImage.Should()
            .Be(url);
    }

    // ----- BackgroundImage handling -----

    [Fact]
    public void BackgroundImage_WhenWhitespace_IsNormalizedToNull()
    {
        // Arrange
        var p = new AllyariaPaletteItem();

        // Act
        p.BackgroundImage = "   ";

        // Assert
        p.BackgroundImage.Should()
            .BeNull();
    }

    [Fact]
    public void BorderColor_Setter_ReturnsSameValue()
    {
        // Arrange
        var color = new AllyariaColor("green");
        var item = new AllyariaPaletteItem();

        // Act
        item.BorderColor = color;

        // Assert
        item.BorderColor.Should()
            .Be(color);
    }

    [Fact]
    public void BorderColor_WhenUnset_FallsBackToBaseBackground_PreShift()
    {
        // Arrange
        var baseBg = new AllyariaColor("black");

        var p = new AllyariaPaletteItem(baseBg)
        {
            HasBorder = true // shifts EFFECTIVE background, but border fallback uses PRE-SHIFT base
        };

        // Act
        var border = p.BorderColor;
        var effectiveBg = p.BackgroundColor;

        // Assert
        border.Should()
            .Be(baseBg); // pre-shift

        effectiveBg.Should()
            .Be(baseBg.HoverColor()); // shifted

        border.Should()
            .NotBe(effectiveBg); // validate the distinction in fallback logic
    }

    [Fact]
    public void Ctor_BackgroundAndForeground_SetsBothBackings()
    {
        // Arrange
        var bgBase = new AllyariaColor("black");
        var fg = new AllyariaColor("white");

        // Act
        var p = new AllyariaPaletteItem(bgBase, fg);

        // Assert
        p.BackgroundColor.Should()
            .Be(bgBase);

        p.ForegroundColor.Should()
            .Be(fg);
    }

    [Fact]
    public void Ctor_BackgroundOnly_SetsBackgroundBacking()
    {
        // Arrange
        var bgBase = new AllyariaColor("black");

        // Act
        var p = new AllyariaPaletteItem(bgBase);

        // Assert (HasBorder default false -> effective bg is base)
        p.BackgroundColor.Should()
            .Be(bgBase);
    }

    // ----- Constructors -----

    [Fact]
    public void Ctor_Default_SetsExpectedDefaults()
    {
        // Arrange / Act
        var p = new AllyariaPaletteItem();

        // Assert
        // HasBackground default = true; HasBorder default = false
        p.HasBackground.Should()
            .BeTrue();

        p.HasBorder.Should()
            .BeFalse();

        // Effective colors (no inputs):
        // BackgroundColor -> White (fallback), ForegroundColor -> Black (auto from high V),
        // BorderColor -> Background base (white)
        var bg = p.BackgroundColor;
        var fg = p.ForegroundColor;
        var bc = p.BorderColor;

        bg.ToCss("background-color")
            .Should()
            .Contain("background-color");

        fg.ToCss("color")
            .Should()
            .Contain("color");

        bc.ToCss("border-color")
            .Should()
            .Contain("border-color");
    }

    [Fact]
    public void ForegroundColor_Setter_ReturnsSameValue()
    {
        // Arrange
        var color = new AllyariaColor("purple");
        var item = new AllyariaPaletteItem();

        // Act
        item.ForegroundColor = color;

        // Assert
        item.ForegroundColor.Should()
            .Be(color);
    }

    [Theory]
    [InlineData("black", "white")] // V < 50 => White
    [InlineData("white", "black")] // V >= 50 => Black
    public void ForegroundColor_WhenUnset_ComputedFromEffectiveBackground(string bg, string expectedFg)
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor(bg)); // default HasBorder=false

        // Act
        var fg = p.ForegroundColor;

        // Assert
        fg.Should()
            .Be(new AllyariaColor(expectedFg));
    }

    [Fact]
    public void ForegroundHoverColor_Setter_ReturnsSameValue()
    {
        // Arrange
        var color = new AllyariaColor("orange");
        var item = new AllyariaPaletteItem();

        // Act
        item.ForegroundHoverColor = color;

        // Assert
        item.ForegroundHoverColor.Should()
            .Be(color);
    }

    [Fact]
    public void ForegroundHoverColor_WhenUnset_DerivesFromForeground()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white")); // effective fg -> black

        // Act
        var hover = p.ForegroundHoverColor;

        // Assert
        hover.Should()
            .Be(p.ForegroundColor.HoverColor());
    }

    [Fact]
    public void ToCss_ImageWithQuotes_IsEscaped()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = true,
            BackgroundImage = "https://ex\"am\"ple.com/bg.png"
        };

        // Act
        var css = p.ToCss();

        // Assert
        css.Should()
            .Contain("url(\"https://ex\\\"am\\\"ple.com/bg.png\")");
    }

    [Fact]
    public void ToCss_WhenHasBackgroundFalse_EmitsNoBackgroundDeclarations()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = false,
            BackgroundImage = "https://example.com/bg.png"
        };

        // Act
        var css = p.ToCss();

        // Assert
        css.Should()
            .Contain("color:"); // foreground still emitted

        css.Should()
            .NotContain("background-color:");

        css.Should()
            .NotContain("background-image:");
    }

    [Fact]
    public void ToCss_WhenHasBackgroundTrueAndImageSet_EmitsLinearGradientImageOverlay()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = true,
            BackgroundImage = "https://example.com/bg.png"
        };

        // Act
        var css = p.ToCss();

        // Assert
        css.Should()
            .Contain("color:");

        css.Should()
            .Contain("background-image:linear-gradient(");

        css.Should()
            .Contain("url(\"https://example.com/bg.png\")");

        css.Should()
            .NotContain("background-color:"); // image chosen instead of color
    }

    [Fact]
    public void ToCss_WhenHasBackgroundTrueAndNoImage_EmitsBackgroundColor()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("black"))
        {
            HasBackground = true
        };

        // Act
        var css = p.ToCss();

        // Assert
        css.Should()
            .Contain("color:"); // foreground always present

        css.Should()
            .Contain("background-color:"); // no image => use background-color

        css.Should()
            .NotContain("background-image:"); // no image set
    }

    [Fact]
    public void ToCss_WhenHasBorderFalse_DoesNotEmitBorderColor()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBorder = false
        };

        // Act
        var css = p.ToCss();

        // Assert
        css.Should()
            .NotContain("border-color:");
    }

    // ----- Border emission -----

    [Fact]
    public void ToCss_WhenHasBorderTrue_EmitsBorderColor()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBorder = true
        };

        // Act
        var css = p.ToCss();

        // Assert
        css.Should()
            .Contain("border-color:");
    }

    // ----- ToCssVars (custom properties) -----

    [Fact]
    public void ToCssVars_EmitsAllExpectedVariables_NoImageCase()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = true,
            BackgroundImage = null,
            HasBorder = true
        };

        // Act
        var vars = p.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-bg:");

        vars.Should()
            .Contain("--aa-fg:");

        vars.Should()
            .Contain("--aa-bg-hover:");

        vars.Should()
            .Contain("--aa-fg-hover:");

        vars.Should()
            .Contain("--aa-border:");

        vars.Should()
            .Contain("--aa-bg-image: none;");

        vars.Should()
            .NotContain("url(");
    }

    [Fact]
    public void ToCssVars_EmitsEscapedUrl_WhenImageSet()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = true,
            BackgroundImage = "https://ex\"am\"ple.com/bg.png"
        };

        // Act
        var vars = p.ToCssVars();

        // Assert
        vars.Should()
            .Contain("--aa-bg-image: url(\"https://ex\\\"am\\\"ple.com/bg.png\");");
    }

    [Fact]
    public void ToHoverCss_WhenHasBackgroundFalse_EmitsNoBackgroundDeclarations()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = false,
            BackgroundImage = "https://example.com/bg.png"
        };

        // Act
        var css = p.ToHoverCss();

        // Assert
        css.Should()
            .Contain("color:"); // foreground hover still emitted

        css.Should()
            .NotContain("background-color:");

        css.Should()
            .NotContain("background-image:");
    }

    [Fact]
    public void ToHoverCss_WhenHasBackgroundTrueAndImageSet_UsesLinearGradientImage()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = true,
            BackgroundImage = "https://example.com/bg.png"
        };

        // Act
        var css = p.ToHoverCss();

        // Assert
        css.Should()
            .Contain("color:");

        css.Should()
            .Contain("background-image:linear-gradient(");

        css.Should()
            .Contain("url(\"https://example.com/bg.png\")");

        css.Should()
            .NotContain("background-color:");
    }

    // ----- ToHoverCss -----

    [Fact]
    public void ToHoverCss_WhenHasBackgroundTrueAndNoImage_UsesBackgroundHoverColor()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBackground = true
        };

        // Act
        var css = p.ToHoverCss();

        // Assert
        css.Should()
            .Contain("color:");

        css.Should()
            .Contain("background-color:");

        css.Should()
            .NotContain("background-image:");
    }

    [Fact]
    public void ToHoverCss_WhenHasBorderTrue_EmitsBorderColor()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBorder = true
        };

        // Act
        var css = p.ToHoverCss();

        // Assert
        css.Should()
            .Contain("border-color:");
    }

    // ----- ToString delegates to ToCss -----

    [Fact]
    public void ToString_DelegatesTo_ToCss()
    {
        // Arrange
        var p = new AllyariaPaletteItem(new AllyariaColor("white"))
        {
            HasBorder = true,
            HasBackground = true
        };

        // Act
        var s = p.ToString();
        var css = p.ToCss();

        // Assert
        s.Should()
            .Be(css);
    }
}
