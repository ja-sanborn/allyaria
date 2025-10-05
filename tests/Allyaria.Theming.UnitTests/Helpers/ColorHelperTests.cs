using Allyaria.Theming.Helpers;
using Allyaria.Theming.Styles;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
public sealed class ColorHelperTests
{
    private const double Epsilon = 1e-6;

    [Fact]
    public void ContrastRatio_Should_Be_21_For_BlackOnWhite_And_Symmetric()
    {
        // Arrange
        var black = AllyariaColorValue.FromRgba(0, 0, 0);
        var white = AllyariaColorValue.FromRgba(255, 255, 255);

        // Act
        var r1 = ColorHelper.ContrastRatio(black, white);
        var r2 = ColorHelper.ContrastRatio(white, black);

        // Assert
        r1.Should().BeApproximately(21.0, 1e-9);
        r2.Should().BeApproximately(21.0, 1e-9);
    }

    [Fact]
    public void ContrastRatio_Should_Increase_When_Foreground_Darkens_On_LightBackground()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(210, 10, 95); // very light, slightly cool
        var lightFg = AllyariaColorValue.FromHsva(210, 10, 80);
        var darkerFg = AllyariaColorValue.FromHsva(210, 10, 30);

        // Act
        var rLight = ColorHelper.ContrastRatio(lightFg, bg);
        var rDark = ColorHelper.ContrastRatio(darkerFg, bg);

        // Assert
        rDark.Should().BeGreaterThan(rLight);
    }

    [Fact]
    public void DeriveDisabled_Should_Desaturate_And_Blend_Value_Toward_Mid_And_Keep_Readability()
    {
        // Arrange
        var bg0 = AllyariaColorValue.FromHsva(210, 40, 90);
        var fg0 = AllyariaColorValue.FromHsva(210, 20, 20);
        var border0 = AllyariaColorValue.FromHsva(210, 40, 85);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);

        const double desat = 60.0;
        const double towardMidT = 0.15;

        var expectedBg = AllyariaColorValue.FromHsva(
            bg0.H,
            bg0.S - desat,
            bg0.V + (50.0 - bg0.V) * towardMidT
        );

        var expectedBorder = AllyariaColorValue.FromHsva(
            border0.H,
            border0.S - desat,
            border0.V + (50.0 - border0.V) * towardMidT
        );

        // Act
        var disabled = ColorHelper.DeriveDisabled(basePalette, desat, towardMidT, 3.0);

        // Assert
        disabled.BackgroundColor.Value.Should().Be(expectedBg.Value);
        disabled.BorderColor.Value.Should().Be(expectedBorder.Value);

        // Foreground should have relaxed readability (>= 3.0)
        ColorHelper.ContrastRatio(disabled.ForegroundColor, disabled.BackgroundColor)
            .Should().BeGreaterThanOrEqualTo(3.0 - 1e-6);
    }

    [Fact]
    public void DeriveHigh_Should_Darken_On_DarkTheme_And_ReEnsure_Contrast()
    {
        // Arrange (dark theme)
        var bg0 = AllyariaColorValue.FromHsva(20, 15, 30);
        var fg0 = AllyariaColorValue.FromHsva(20, 15, 85);
        var border0 = AllyariaColorValue.FromHsva(20, 15, 32);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 8.0;

        // Expected via same quantization path
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V - delta); // darker on dark
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V - (delta + 2.0));

        // Act
        var high = ColorHelper.DeriveHigh(basePalette, delta);

        // Assert
        high.BackgroundColor.Value.Should().Be(expectedBg.Value);
        high.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(high.ForegroundColor, high.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void DeriveHigh_Should_Lighten_On_LightTheme_And_ReEnsure_Contrast()
    {
        // Arrange (light theme)
        var bg0 = AllyariaColorValue.FromHsva(20, 15, 85);
        var fg0 = AllyariaColorValue.FromHsva(20, 15, 15);
        var border0 = AllyariaColorValue.FromHsva(20, 15, 83);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 8.0;

        // Compute expected via same conversion logic as helper to avoid rounding mismatches
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V + delta);
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V + (delta + 2.0));

        // Act
        var high = ColorHelper.DeriveHigh(basePalette, delta);

        // Assert
        high.BackgroundColor.Value.Should().Be(expectedBg.Value);
        high.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(high.ForegroundColor, high.BackgroundColor)
            .Should().BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void DeriveHighest_Should_DarkenMore_On_DarkTheme()
    {
        // Arrange (dark theme)
        var bg0 = AllyariaColorValue.FromHsva(20, 15, 30);
        var fg0 = AllyariaColorValue.FromHsva(20, 15, 85);
        var border0 = AllyariaColorValue.FromHsva(20, 15, 32);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 12.0;

        // Expected via same quantization path
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V - delta); // darker on dark
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V - (delta + 2.0));

        // Act
        var highest = ColorHelper.DeriveHighest(basePalette, delta);

        // Assert
        highest.BackgroundColor.Value.Should().Be(expectedBg.Value);
        highest.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(highest.ForegroundColor, highest.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void DeriveHighest_Should_LightenMore_On_LightTheme()
    {
        // Arrange
        var bg0 = AllyariaColorValue.FromHsva(20, 15, 85);
        var fg0 = AllyariaColorValue.FromHsva(20, 15, 15);
        var border0 = AllyariaColorValue.FromHsva(20, 15, 83);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 12.0;

        // Compute expected via the same HSVA->RGBA quantization path to avoid rounding drift.
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V + delta);
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V + (delta + 2.0));

        // Act
        var highest = ColorHelper.DeriveHighest(basePalette, delta);

        // Assert
        highest.BackgroundColor.Value.Should().Be(expectedBg.Value);
        highest.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(highest.ForegroundColor, highest.BackgroundColor)
            .Should().BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void DeriveLow_Should_Darken_On_LightTheme_And_ReEnsure_Contrast()
    {
        // Arrange (light theme)
        var bg0 = AllyariaColorValue.FromHsva(200, 10, 85);
        var fg0 = AllyariaColorValue.FromHsva(200, 10, 15);
        var border0 = AllyariaColorValue.FromHsva(200, 10, 83);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 8.0;

        // Compute expected via the same HSVA->RGBA quantization path to avoid rounding drift.
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V - delta);
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V - (delta + 2.0));

        // Act
        var low = ColorHelper.DeriveLow(basePalette, delta);

        // Assert
        low.BackgroundColor.Value.Should().Be(expectedBg.Value);
        low.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(low.ForegroundColor, low.BackgroundColor)
            .Should().BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void DeriveLow_Should_Lighten_On_DarkTheme_And_ReEnsure_Contrast()
    {
        // Arrange (dark theme)
        var bg0 = AllyariaColorValue.FromHsva(200, 10, 30);
        var fg0 = AllyariaColorValue.FromHsva(200, 10, 85);
        var border0 = AllyariaColorValue.FromHsva(200, 10, 28);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 8.0;

        // Expected via same quantization path
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V + delta); // lighter on dark
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V + (delta + 2.0));

        // Act
        var low = ColorHelper.DeriveLow(basePalette, delta);

        // Assert
        low.BackgroundColor.Value.Should().Be(expectedBg.Value);
        low.BorderColor.Value.Should().Be(expectedBorder.Value);
        ColorHelper.ContrastRatio(low.ForegroundColor, low.BackgroundColor).Should().BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void DeriveLowest_Should_DarkenMore_On_LightTheme()
    {
        // Arrange
        var bg0 = AllyariaColorValue.FromHsva(200, 10, 85);
        var fg0 = AllyariaColorValue.FromHsva(200, 10, 15);
        var border0 = AllyariaColorValue.FromHsva(200, 10, 83);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 12.0;

        // Compute expected via the same HSVA->RGBA quantization path to avoid rounding drift.
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V - delta);
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V - (delta + 2.0));

        // Act
        var lowest = ColorHelper.DeriveLowest(basePalette, delta);

        // Assert
        lowest.BackgroundColor.Value.Should().Be(expectedBg.Value);
        lowest.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(lowest.ForegroundColor, lowest.BackgroundColor)
            .Should().BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void DeriveLowest_Should_LightenMore_On_DarkTheme()
    {
        // Arrange (dark theme)
        var bg0 = AllyariaColorValue.FromHsva(200, 10, 30);
        var fg0 = AllyariaColorValue.FromHsva(200, 10, 85);
        var border0 = AllyariaColorValue.FromHsva(200, 10, 28);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);
        const double delta = 12.0;

        // Expected via same quantization path
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V + delta); // lighter on dark
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V + (delta + 2.0));

        // Act
        var lowest = ColorHelper.DeriveLowest(basePalette, delta);

        // Assert
        lowest.BackgroundColor.Value.Should().Be(expectedBg.Value);
        lowest.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(lowest.ForegroundColor, lowest.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Adjust_Along_ValueRail_When_Possible()
    {
        // Arrange
        // Start with a light gray foreground on white background -> low contrast.
        var bg = AllyariaColorValue.FromRgba(255, 255, 255);
        var start = AllyariaColorValue.FromHsva(0, 0, 80); // light gray (S=0 keeps hue rail behavior clear)

        // Act
        var result = ColorHelper.EnsureMinimumContrast(start, bg, 4.5);

        // Assert
        result.MeetsMinimum.Should().BeTrue();

        // On S=0, hue is 0 and S stays 0; only V should change.
        result.ForegroundColor.S.Should().BeApproximately(start.S, 1e-9);
        result.ForegroundColor.H.Should().BeApproximately(start.H, 1e-9);
        result.ForegroundColor.V.Should().BeLessThan(start.V);
        result.ContrastRatio.Should().BeGreaterThanOrEqualTo(4.5 - 1e-6);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Return_BestApproach_When_Target_Unreachable()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(120, 100, 50); // saturated mid green
        var start = AllyariaColorValue.FromHsva(120, 100, 50); // identical → initial ratio is 1
        var absurdTarget = 100.0; // impossible

        // Act
        var result = ColorHelper.EnsureMinimumContrast(start, bg, absurdTarget);

        // Assert
        result.MeetsMinimum.Should().BeFalse();
        result.ContrastRatio.Should().BeGreaterThan(1.0); // must have improved over the starting 1:1

        // It should have shifted either toward dark (V↓) or light (mix rail) to maximize contrast.
        result.ForegroundColor.V.Should().NotBeApproximately(start.V, 1e-9);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Return_SameColor_When_Already_Meets_Min()
    {
        // Arrange
        var fg = AllyariaColorValue.FromRgba(0, 0, 0);
        var bg = AllyariaColorValue.FromRgba(255, 255, 255);

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, 4.5);

        // Assert
        result.MeetsMinimum.Should().BeTrue();
        result.ForegroundColor.Value.Should().Be(fg.Value);
        result.ContrastRatio.Should().BeGreaterThanOrEqualTo(4.5 - 1e-9);
    }

    [Theory]
    [InlineData(6.0, 8.0, 4.5)] // Hovered defaults
    [InlineData(8.0, 10.0, 4.5)] // Focused defaults
    [InlineData(12.0, 14.0, 4.5)] // Pressed defaults
    [InlineData(16.0, 18.0, 4.5)] // Dragged defaults
    public void NudgeState_Derivatives_Should_Adjust_V_Down_On_LightTheme(double bgDelta,
        double borderDelta,
        double minC)
    {
        // Arrange (light theme: V >= 50 → direction = -1)
        var bg0 = AllyariaColorValue.FromHsva(30, 20, 80);
        var fg0 = AllyariaColorValue.FromHsva(30, 10, 15);
        var border0 = AllyariaColorValue.FromHsva(30, 20, 78);
        var basePalette = new AllyariaPaletteVariant(bg0, fg0, border0);

        // Expected values computed via the same HSVA→RGBA quantization path to avoid rounding drift.
        var expectedBg = AllyariaColorValue.FromHsva(bg0.H, bg0.S, bg0.V - bgDelta);
        var expectedBorder = AllyariaColorValue.FromHsva(border0.H, border0.S, border0.V - borderDelta);

        // Act
        var hovered = ColorHelper.DeriveHovered(basePalette, bgDelta, borderDelta, minC);
        var focused = ColorHelper.DeriveFocused(basePalette, bgDelta, borderDelta, minC);
        var pressed = ColorHelper.DerivePressed(basePalette, bgDelta, borderDelta, minC);
        var dragged = ColorHelper.DeriveDragged(basePalette, bgDelta, borderDelta, minC);

        // Assert
        hovered.BackgroundColor.Value.Should().Be(expectedBg.Value);
        hovered.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(hovered.ForegroundColor, hovered.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(minC - 1e-6);

        focused.BackgroundColor.Value.Should().Be(expectedBg.Value);
        focused.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(focused.ForegroundColor, focused.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(minC - 1e-6);

        pressed.BackgroundColor.Value.Should().Be(expectedBg.Value);
        pressed.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(pressed.ForegroundColor, pressed.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(minC - 1e-6);

        dragged.BackgroundColor.Value.Should().Be(expectedBg.Value);
        dragged.BorderColor.Value.Should().Be(expectedBorder.Value);

        ColorHelper.ContrastRatio(dragged.ForegroundColor, dragged.BackgroundColor).Should()
            .BeGreaterThanOrEqualTo(minC - 1e-6);
    }

    [Fact]
    public void RelativeLuminance_Should_BeOne_For_White()
    {
        // Arrange
        var white = AllyariaColorValue.FromRgba(255, 255, 255);

        // Act
        var luminance = ColorHelper.RelativeLuminance(white);

        // Assert
        luminance.Should().BeApproximately(1.0, Epsilon);
    }

    [Fact]
    public void RelativeLuminance_Should_BeZero_For_Black()
    {
        // Arrange
        var black = AllyariaColorValue.FromRgba(0, 0, 0);

        // Act
        var luminance = ColorHelper.RelativeLuminance(black);

        // Assert
        luminance.Should().BeApproximately(0.0, Epsilon);
    }

    [Theory]
    [InlineData(255, 0, 0, 0.2126)] // pure red
    [InlineData(0, 255, 0, 0.7152)] // pure green
    [InlineData(0, 0, 255, 0.0722)] // pure blue
    public void RelativeLuminance_Should_Match_WCAG_Primaries(byte r, byte g, byte b, double expected)
    {
        // Arrange
        var color = AllyariaColorValue.FromRgba(r, g, b);

        // Act
        var luminance = ColorHelper.RelativeLuminance(color);

        // Assert
        luminance.Should().BeApproximately(expected, 1e-4);
    }

    [Fact]
    public void SearchTowardPole_Should_Find_MinimumContrast_Mix_When_ValueRail_Fails()
    {
        // Arrange
        var background = AllyariaColorValue.FromHsva(240, 100, 10); // very dark blue
        var start = AllyariaColorValue.FromHsva(240, 100, 50); // saturated blue mid value

        const double minimum = 10.0; // deliberately high to force the hue-rail attempts to fail

        // Act
        var result = ColorHelper.EnsureMinimumContrast(start, background, minimum);

        // Assert
        result.MeetsMinimum.Should().BeTrue();
        result.ContrastRatio.Should().BeGreaterThanOrEqualTo(minimum - 1e-6);
        result.ForegroundColor.S.Should().BeLessThan(start.S);
    }
}
