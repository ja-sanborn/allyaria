using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaPaletteVariantTests
{
    [Fact]
    public void Cascade_Should_OverrideOnlyProvidedMembers_And_RecontrastForeground_When_BackgroundChanges()
    {
        // Arrange
        var initialBackground = Colors.White;
        var initialForeground = Colors.Black;
        var initialBorder = Colors.Grey700;

        var sut = new AllyariaPaletteVariant(initialBackground, initialForeground, initialBorder);

        var newBackground = Colors.Lightblue100; // still light, but different value

        // Act
        var cascaded = sut.Cascade(newBackground);

        // Assert
        cascaded.BackgroundColor.Should().Be(newBackground);
        cascaded.BorderColor.Should().Be(initialBorder); // unchanged

        // Foreground should still meet contrast for the new background
        ColorHelper.ContrastRatio(cascaded.ForegroundColor, cascaded.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void Cascade_Should_RespectExplicitOverrides_For_AllMembers()
    {
        // Arrange
        var sut = new AllyariaPaletteVariant(Colors.Black, Colors.White, Colors.Red500);

        var bg = Colors.Blue200;
        var fg = Colors.Yellow800; // may be adjusted later, but start with explicit request
        var border = Colors.Blue800;

        // Act
        var cascaded = sut.Cascade(bg, fg, border);

        // Assert
        cascaded.BackgroundColor.Should().Be(bg);
        cascaded.BorderColor.Should().Be(border);

        // Foreground may be re-adjusted for contrast; assert contrast rather than identity
        ColorHelper.ContrastRatio(cascaded.ForegroundColor, cascaded.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void Ctor_Should_AdjustForeground_ToMeetContrast_When_Insufficient()
    {
        // Arrange
        var background = Colors.White; // very light
        var lowContrastForeground = Colors.Grey400; // light gray; insufficient on white

        // Sanity: confirm initial pair is below 4.5:1
        ColorHelper.ContrastRatio(lowContrastForeground, background).Should().BeLessThan(4.5);

        // Act
        var sut = new AllyariaPaletteVariant(background, lowContrastForeground, Colors.White);

        // Assert
        sut.BackgroundColor.Should().Be(background);
        sut.ForegroundColor.Should().NotBe(lowContrastForeground); // must be adjusted
        ColorHelper.ContrastRatio(sut.ForegroundColor, sut.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void Ctor_Should_DefaultBorderToBackground_When_BorderNotProvided()
    {
        // Arrange
        var background = Colors.Black; // #000000FF
        var foreground = Colors.White; // #FFFFFFFF

        // Act
        var sut = new AllyariaPaletteVariant(background, foreground);

        // Assert
        sut.BackgroundColor.Should().Be(background);
        sut.BorderColor.Should().Be(background);
        sut.ForegroundColor.Should().Be(foreground); // already meets contrast; should remain unchanged
        ColorHelper.ContrastRatio(sut.ForegroundColor, sut.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Theory]
    [InlineData("#FFFFFF", "#BDBDBD")] // white on light grey -> adjust darker foreground
    [InlineData("#000000", "#424242")] // black background with grey foreground -> adjust lighter foreground
    public void Palette_Should_Always_Ensure_MinimumContrast_Of_4_5(string bgHex, string fgHex)
    {
        // Arrange
        var background = new AllyariaColorValue(bgHex);
        var requestedForeground = new AllyariaColorValue(fgHex);

        // Guard: chosen inputs start below 4.5 where applicable
        ColorHelper.ContrastRatio(requestedForeground, background).Should().BeLessThan(4.5);

        // Act
        var sut = new AllyariaPaletteVariant(background, requestedForeground);

        // Assert
        ColorHelper.ContrastRatio(sut.ForegroundColor, sut.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void ToCss_Should_ContainDeclarations_For_Foreground_Background_And_Border()
    {
        // Arrange
        var bg = Colors.Black; // #000000FF
        var fg = Colors.White; // #FFFFFFFF
        var bd = Colors.Red500; // #F44336FF

        var sut = new AllyariaPaletteVariant(bg, fg, bd);

        // Act
        var css = sut.ToCss(); // default varPrefix = ""

        // Assert
        css.Should().Contain("color", "foreground should emit a 'color' CSS declaration");
        css.Should().Contain(fg.HexRgba);
        css.Should().Contain("background-color");
        css.Should().Contain(bg.HexRgba);
        css.Should().Contain("border-color");
        css.Should().Contain(bd.HexRgba);
        css.Should().EndWith(";"); // last declaration also ends with semicolon per implementation notes
    }
}
