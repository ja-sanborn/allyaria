using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaPaletteTests
{
    [Fact]
    public void Cascade_Should_ApplyOverrides_TrimImage_And_EmptyOnWhitespace()
    {
        // Arrange
        var basePalette = new AllyariaPalette(Colors.Grey100, Colors.Grey900, Colors.Grey100, "url(x)");
        var newBg = Colors.White;
        var newFg = Colors.Black;
        var newBorder = Colors.Grey300;

        // Act
        var next1 = basePalette.Cascade(newBg, newFg, newBorder, "   wall.png   ", true);
        var next2 = basePalette.Cascade(backgroundImage: "   ");

        // Assert
        next1.BackgroundColor.Should().BeSameAs(newBg);
        next1.BorderColor.Should().BeSameAs(newBorder);
        next1.BackgroundImage.Should().Be("wall.png");
        next1.BackgroundImageStretch.Should().BeTrue();
        ContrastRatio(next1.ForegroundColor, next1.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);

        next2.BackgroundImage.Should().BeEmpty();
    }

    [Fact]
    public void Cascade_Should_PreserveExisting_When_NullsPassed_And_RecomputeContrast()
    {
        // Arrange
        var basePalette = new AllyariaPalette(
            Colors.Grey200,
            Colors.Grey100,
            Colors.Grey300,
            ""
        );

        // Act
        var next = basePalette.Cascade();

        // Assert
        next.Should().NotBeSameAs(basePalette);
        next.BackgroundColor.Should().Be(basePalette.BackgroundColor);
        next.BorderColor.Should().Be(basePalette.BorderColor);
        next.BackgroundImage.Should().Be(basePalette.BackgroundImage);
        next.BackgroundImageStretch.Should().Be(basePalette.BackgroundImageStretch);

        ContrastRatio(next.ForegroundColor, next.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void Cascade_Should_RetainExistingImage_When_NullPassed()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: "photo.jpg",
            backgroundImageStretch: false
        );

        // Act
        var cascaded = sut.Cascade(backgroundImage: null, backgroundImageStretch: true);

        // Assert
        cascaded.BackgroundImage.Should().Be("photo.jpg");
        cascaded.BackgroundImageStretch.Should().BeTrue();
    }

    private static double ContrastRatio(AllyariaColorValue fg, AllyariaColorValue bg)
    {
        var lf = RelativeLuminance(fg);
        var lb = RelativeLuminance(bg);
        var lighter = Math.Max(lf, lb);
        var darker = Math.Min(lf, lb);

        return (lighter + 0.05) / (darker + 0.05);
    }

    [Fact]
    public void Ctor_Should_TrimBackgroundImage_And_DefaultBorderToBackground_When_ProvidedValues()
    {
        // Arrange
        var bg = Colors.Blue100;
        var fg = Colors.Blue50;
        var img = "   https://example.com/hero.jpg   ";

        // Act
        var sut = new AllyariaPalette(bg, fg, null, img, true);

        // Assert
        sut.BackgroundColor.Should().BeSameAs(bg);
        sut.BorderColor.Should().BeSameAs(bg);
        sut.BackgroundImage.Should().Be("https://example.com/hero.jpg");
        sut.BackgroundImageStretch.Should().BeTrue();
        ContrastRatio(sut.ForegroundColor, sut.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void Ctor_Should_TurnWhitespaceImageIntoEmptyString_When_BackgroundImageIsWhitespace()
    {
        // Arrange & Act
        var sut = new AllyariaPalette(backgroundImage: "   \t   ");

        // Assert
        sut.BackgroundImage.Should().BeEmpty();
    }

    [Fact]
    public void Ctor_Should_UseDefaultsAndFixContrast_When_NoArgs()
    {
        // Arrange & Act
        var sut = new AllyariaPalette();

        // Assert
        sut.BackgroundColor.Should().BeSameAs(StyleDefaults.BackgroundColorLight);
        sut.BorderColor.Should().BeSameAs(StyleDefaults.BackgroundColorLight);
        sut.ForegroundColor.Should().BeSameAs(StyleDefaults.ForegroundColorLight);
        sut.BackgroundImage.Should().BeEmpty();
        sut.BackgroundImageStretch.Should().BeFalse();

        ContrastRatio(sut.ForegroundColor, sut.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);
    }

    private static double RelativeLuminance(AllyariaColorValue c)
    {
        static double SrgbToLinear(byte v)
        {
            var x = v / 255.0;

            return x <= 0.03928
                ? x / 12.92
                : Math.Pow((x + 0.055) / 1.055, 2.4);
        }

        var r = SrgbToLinear(c.R);
        var g = SrgbToLinear(c.G);
        var b = SrgbToLinear(c.B);

        return 0.2126 * r + 0.7152 * g + 0.0722 * b;
    }

    [Fact]
    public void ToCss_Should_EmitColor_Background_Border_InOrder_When_NoImage()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.Grey200, Colors.Grey900, Colors.Grey400);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().NotBeNullOrEmpty();
        var idxColor = css.IndexOf("color:", StringComparison.OrdinalIgnoreCase);
        var idxBg = css.IndexOf("background-color:", StringComparison.OrdinalIgnoreCase);
        var idxBorder = css.IndexOf("border-color:", StringComparison.OrdinalIgnoreCase);

        idxColor.Should().BeGreaterThanOrEqualTo(0);
        idxBg.Should().BeGreaterThan(idxColor);
        idxBorder.Should().BeGreaterThan(idxBg);

        css.Should().Contain("color:");
        css.Should().Contain(Colors.Grey900.Value);
        css.Should().Contain(Colors.Grey200.Value);
        css.Should().Contain(Colors.Grey400.Value);
        css.Should().NotContain("background-image:");
    }

    [Fact]
    public void ToCss_Should_IncludeImage_And_StretchDeclarations_When_ImageSet()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.Grey50,
            Colors.Grey900,
            Colors.Grey200,
            "banner.png",
            true
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().Contain("background-image:");
        css.Should().MatchRegex(@"background-image:url\(");
        css.Should().Contain("linear-gradient(");
        css.Should().Contain("background-position:center;");
        css.Should().Contain("background-repeat:no-repeat;");
        css.Should().Contain("background-size:cover;");
    }

    [Fact]
    public void ToCss_Should_NotEmitImageDeclarations_When_ImageIsEmpty()
    {
        // Arrange
        var sut = new AllyariaPalette(
            Colors.White,
            Colors.Black,
            backgroundImage: ""
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().NotContain("background-image:");
        css.Should().NotContain("background-position:");
        css.Should().NotContain("background-repeat:");
        css.Should().NotContain("background-size:");
    }

    [Theory]
    [InlineData(null, "color:")]
    [InlineData("", "color:")]
    [InlineData("AppTheme", "--apptheme-color:")]
    [InlineData("  app-- theme  ", "--app-theme-color:")]
    public void ToCss_Should_RespectVarPrefix_When_PrefixProvided(string? prefix, string expectedPropertyPrefix)
    {
        // Arrange
        var sut = new AllyariaPalette();

        // Act
        var css = sut.ToCss(prefix);

        // Assert
        css.Should().Contain(expectedPropertyPrefix);
    }
}
