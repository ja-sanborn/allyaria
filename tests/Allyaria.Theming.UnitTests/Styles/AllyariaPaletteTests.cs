using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaPaletteTests
{
    [Fact]
    public void BackgroundColor_Should_DefaultToWhite_When_NotProvided()
    {
        // Arrange
        var sut = new AllyariaPalette();

        // Act
        var result = sut.BackgroundColor;

        // Assert
        result.Should().BeEquivalentTo(Colors.White);
    }

    [Fact]
    public void BorderColor_Should_DefaultToBackgroundColor_When_NotProvided()
    {
        // Arrange
        var background = AllyariaColorValue.FromHsva(200, 20, 90);
        var sut = new AllyariaPalette(background);

        // Act
        var result = sut.BorderColor;

        // Assert
        result.Should().BeEquivalentTo(background);
    }

    [Fact]
    public void Cascade_Should_ApplyOverrides_When_ValuesProvided()
    {
        // Arrange
        var sut = new AllyariaPalette();
        var newBg = new AllyariaColorValue("#12345678");
        var newFg = new AllyariaColorValue("#FFCC9966");
        var newBorder = new AllyariaColorValue("#98765432");

        // Act
        var cascaded = sut.Cascade(newBg, newFg, newBorder, null, false);

        // Assert
        cascaded.BackgroundColor.Should().Be(newBg);
        cascaded.ForegroundColor.Should().Be(newFg);
        cascaded.BorderColor.Should().Be(newBorder);
    }

    [Fact]
    public void Cascade_Should_InheritExistingValues_When_OverridesAreNull()
    {
        // Arrange
        var background = AllyariaColorValue.FromHsva(330, 40, 40);
        var foreground = AllyariaColorValue.FromHsva(330, 15, 90);
        var border = AllyariaColorValue.FromHsva(330, 40, 30);
        var sut = new AllyariaPalette(background, foreground, border);

        // Act
        var cascaded = sut.Cascade();

        // Assert
        cascaded.BackgroundColor.Should().BeEquivalentTo(sut.BackgroundColor);
        cascaded.ForegroundColor.Should().BeEquivalentTo(sut.ForegroundColor);
        cascaded.BorderColor.Should().BeEquivalentTo(sut.BorderColor);
        cascaded.BackgroundImage.Should().BeNull();
    }

    [Fact]
    public void Cascade_Should_Preserve_BackgroundImage_And_Stretch_When_NotOverridden()
    {
        // Arrange
        var basePalette = new AllyariaPalette(Colors.White, Colors.Black, Colors.Black);

        var withImage = basePalette.Cascade(
            backgroundImage: new AllyariaImageValue("url(\"/img.png\")"), backgroundImageStretch: false
        );

        // Act
        var cascaded = withImage.Cascade();

        // Assert
        cascaded.BackgroundImage.Should().NotBeNull();

        // Ensure we didn't accidentally drop the image when overrides are null.
        cascaded.BackgroundImage!.Value.Should().Contain("/img.png");
    }

    [Fact]
    public void ForegroundColor_Should_AdjustExplicitForeground_ToMeetContrast()
    {
        // Arrange
        var background = AllyariaColorValue.FromHsva(210, 20, 80);
        var weakForeground = AllyariaColorValue.FromHsva(210, 22, 78);
        var sut = new AllyariaPalette(background, weakForeground);

        // Act
        var result = sut.ForegroundColor;

        // Assert
        ColorHelper.ContrastRatio(result, background).Should().BeGreaterThanOrEqualTo(4.5);
        Math.Abs(result.H - weakForeground.H).Should().BeLessThanOrEqualTo(2.0);
    }

    [Fact]
    public void ForegroundColor_Should_ChooseBlack_When_BackgroundIsWhite()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White);

        // Act
        var result = sut.ForegroundColor;

        // Assert
        result.Should().BeEquivalentTo(Colors.Black);

        ColorHelper.ContrastRatio(result, sut.BackgroundColor).Should()
            .BeGreaterThan(ColorHelper.ContrastRatio(Colors.White, sut.BackgroundColor));
    }

    [Fact]
    public void ForegroundColor_Should_ChooseWhite_When_BackgroundIsBlack()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.Black);

        // Act
        var result = sut.ForegroundColor;

        // Assert
        result.Should().BeEquivalentTo(Colors.White);

        ColorHelper.ContrastRatio(result, sut.BackgroundColor).Should()
            .BeGreaterThan(ColorHelper.ContrastRatio(Colors.Black, sut.BackgroundColor));
    }

    [Fact]
    public void ToCss_Should_AppendBackgroundImageCss_When_ImageProvided_StretchTrue()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(0, 0, 100);
        var fg = AllyariaColorValue.FromHsva(0, 0, 0);
        var border = AllyariaColorValue.FromHsva(0, 0, 50);
        var image = new AllyariaImageValue("url(\"/img.png\")");
        var sut = new AllyariaPalette(bg, fg, border, image);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().Contain("background-color")
            .And.Contain("color")
            .And.Contain("border-color");

        css.Should().Contain("/img.png");
        css.Trim().Should().EndWith(";");
    }

    [Fact]
    public void ToCss_Should_IncludeColorAndBorderDeclarations()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(0, 0, 100); // white
        var fg = AllyariaColorValue.FromHsva(0, 0, 0); // black
        var border = AllyariaColorValue.FromHsva(0, 0, 50);
        var sut = new AllyariaPalette(bg, fg, border);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().Contain("background-color");
        css.Should().Contain("color");
        css.Should().Contain("border-color");
        css.Trim().Should().EndWith(";");
    }

    [Fact]
    public void ToCss_Should_ReflectStretchFlag_InOutput_When_TogglingStretch()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(210, 20, 80);
        var fg = AllyariaColorValue.FromHsva(210, 10, 10);
        var border = AllyariaColorValue.FromHsva(210, 20, 60);
        var image = new AllyariaImageValue("url(\"/bg-pattern.svg\")");
        var sutStretch = new AllyariaPalette(bg, fg, border, image);
        var sutTile = new AllyariaPalette(bg, fg, border, image, false);

        // Act
        var cssStretch = sutStretch.ToCss();
        var cssTile = sutTile.ToCss();

        // Assert
        cssStretch.Should().Contain("/bg-pattern.svg");
        cssTile.Should().Contain("/bg-pattern.svg");
        cssStretch.Should().NotBe(cssTile);
    }

    [Fact]
    public void ToCssVars_Should_AppendBackgroundImageVars_When_ImageProvided_WithDefaultPrefix()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(0, 0, 100);
        var fg = AllyariaColorValue.FromHsva(0, 0, 0);
        var border = AllyariaColorValue.FromHsva(0, 0, 50);
        var image = new AllyariaImageValue("url(\"/texture.png\")");
        var sutNoImg = new AllyariaPalette(bg, fg, border);
        var sutWithImg = new AllyariaPalette(bg, fg, border, image);

        // Act
        var cssNoImg = sutNoImg.ToCssVars();
        var cssWithImg = sutWithImg.ToCssVars();

        // Assert
        cssWithImg.Should().Contain("--aa-color")
            .And.Contain("--aa-background-color")
            .And.Contain("--aa-border-color");

        cssWithImg.Should().Contain("/texture.png");
        cssWithImg.Length.Should().BeGreaterThan(cssNoImg.Length);
        cssWithImg.Should().NotBe(cssNoImg);
    }

    [Fact]
    public void ToCssVars_Should_AppendBackgroundImageVars_WithNormalizedCustomPrefix()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(30, 30, 90);
        var fg = AllyariaColorValue.FromHsva(30, 10, 10);
        var border = AllyariaColorValue.FromHsva(30, 25, 70);
        var image = new AllyariaImageValue("url(\"/hero/banner@2x.jpg\")");
        var sut = new AllyariaPalette(bg, fg, border, image, false);

        // Act
        var css = sut.ToCssVars("  Editor  Theme  ");

        // Assert
        css.Should().Contain("--editor-theme-color")
            .And.Contain("--editor-theme-background-color")
            .And.Contain("--editor-theme-border-color");

        css.Should().Contain("/hero/banner@2x.jpg");
    }

    [Theory]
    [InlineData("Editor", "--editor-")]
    [InlineData(" editor  theme ", "--editor-theme-")]
    [InlineData("EDITOR---THEME", "--editor-theme-")]
    public void ToCssVars_Should_NormalizeAndLowercasePrefix_When_PrefixProvided(string input, string expectedPrefix)
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, Colors.Black);

        // Act
        var css = sut.ToCssVars(input);

        // Assert
        css.Should().Contain($"{expectedPrefix}color");
        css.Should().Contain($"{expectedPrefix}background-color");
        css.Should().Contain($"{expectedPrefix}border-color");
    }

    [Fact]
    public void ToCssVars_Should_UseDefaultPrefix_When_PrefixBlank()
    {
        // Arrange
        var sut = new AllyariaPalette(Colors.White, Colors.Black, Colors.Black);

        // Act
        var css = sut.ToCssVars();

        // Assert
        css.Should().Contain("--aa-color");
        css.Should().Contain("--aa-background-color");
        css.Should().Contain("--aa-border-color");
        css.Trim().Should().EndWith(";");
    }

    [Fact]
    public void ToDisabledPalette_Should_DesaturateAndBlendTowardMid_And_RelaxContrast()
    {
        // Arrange
        var background = AllyariaColorValue.FromHsva(270, 70, 20);
        var border = AllyariaColorValue.FromHsva(270, 60, 25);
        var sut = new AllyariaPalette(background, null, border);

        // Act
        var disabled = sut.ToDisabledPalette();

        // Assert
        disabled.BackgroundColor.S.Should().BeGreaterThanOrEqualTo(0.0);
        disabled.BackgroundColor.S.Should().BeLessThanOrEqualTo(background.S);
        (disabled.BackgroundColor.V - 50.0).Should().BeLessThan(Math.Abs(background.V - 50.0));
        disabled.BackgroundColor.H.Should().BeApproximately(background.H, 0.001);
        disabled.BorderColor.S.Should().BeLessThanOrEqualTo(border.S);
        (disabled.BorderColor.V - 50.0).Should().BeLessThan(Math.Abs(border.V - 50.0));

        if (disabled.BorderColor.S > 0.0)
        {
            disabled.BorderColor.H.Should().BeApproximately(border.H, 0.001);
        }

        ColorHelper.ContrastRatio(disabled.ForegroundColor, disabled.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(3.0);
    }

    [Fact]
    public void ToHoverPalette_Should_DecreaseV_OnLightBackground_And_AdjustBorder_And_EnsureContrast()
    {
        // Arrange
        var lightBg = AllyariaColorValue.FromHsva(30, 30, 75);
        var border = AllyariaColorValue.FromHsva(30, 30, 65);
        var sut = new AllyariaPalette(lightBg, null, border);

        // Act
        var hover = sut.ToHoverPalette();

        // Assert
        hover.BackgroundColor.V.Should().BeApproximately(lightBg.V - 6.0, 0.2);
        hover.BorderColor.V.Should().BeApproximately(border.V - 8.0, 0.2);
        ColorHelper.ContrastRatio(hover.ForegroundColor, hover.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);

        hover.BackgroundColor.H.Should().BeApproximately(lightBg.H, 0.2);
        hover.BorderColor.H.Should().BeApproximately(border.H, 0.2);
    }

    [Fact]
    public void ToHoverPalette_Should_IncreaseV_OnDarkBackground()
    {
        // Arrange
        var darkBg = AllyariaColorValue.FromHsva(200, 20, 25);
        var border = AllyariaColorValue.FromHsva(200, 20, 30);
        var sut = new AllyariaPalette(darkBg, null, border);

        // Act
        var hover = sut.ToHoverPalette(5.0, 7.0);

        // Assert
        const double quantizationHalfStep = 100.0 / 255.0 / 2.0;
        hover.BackgroundColor.V.Should().BeApproximately(darkBg.V + 5.0, quantizationHalfStep);
        hover.BorderColor.V.Should().BeApproximately(border.V + 7.0, quantizationHalfStep);
        hover.BackgroundColor.H.Should().BeApproximately(darkBg.H, 1.5);
        hover.BorderColor.H.Should().BeApproximately(border.H, 1.5);
    }
}
