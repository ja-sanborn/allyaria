using Allyaria.Theming.Constants;
using Allyaria.Theming.Helpers;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Helpers;

public sealed class ColorHelperTests
{
    [Fact]
    public void Blend_Should_InterpolateLinearly_When_T_InRange()
    {
        // Arrange
        const double start = 10.0;
        const double target = 30.0;
        const double t = 0.25;

        // Act
        var actual = ColorHelper.Blend(start, target, t);

        // Assert
        actual.Should()
            .Be(15.0);
    }

    [Fact]
    public void Blend_Should_ReturnStart_When_T_Is_LessOrEqualZero()
    {
        // Arrange
        const double start = 10.0;
        const double target = 30.0;
        const double t = -1.0;

        // Act
        var actual = ColorHelper.Blend(start, target, t);

        // Assert
        actual.Should()
            .Be(start);
    }

    [Fact]
    public void Blend_Should_ReturnTarget_When_T_Is_GreaterOrEqualOne()
    {
        // Arrange
        const double start = 10.0;
        const double target = 30.0;
        const double t = 2.0;

        // Act
        var actual = ColorHelper.Blend(start, target, t);

        // Assert
        actual.Should()
            .Be(target);
    }

    [Fact]
    public void ContrastRatio_Should_Be_21_For_Black_On_White_And_Symmetric()
    {
        // Arrange
        var blackOnWhite = ColorHelper.ContrastRatio(Colors.Black, Colors.White);
        var whiteOnBlack = ColorHelper.ContrastRatio(Colors.White, Colors.Black);

        // Act + Assert
        blackOnWhite.Should()
            .BeApproximately(21.0, 1e-12);

        whiteOnBlack.Should()
            .BeApproximately(21.0, 1e-12);

        blackOnWhite.Should()
            .Be(whiteOnBlack);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Darken_Along_ValueRail_When_Background_Is_White()
    {
        // Arrange
        var fg = AllyariaColorValue.FromRgba(238, 238, 238);
        var bg = Colors.White;
        const double minimum = 4.5;

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);

        // Assert
        result.MeetsMinimum.Should()
            .BeTrue();

        result.ContrastRatio.Should()
            .BeGreaterThanOrEqualTo(minimum);

        result.ContrastRatio.Should()
            .BeLessThanOrEqualTo(21.0);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Mix_Toward_White_When_ValueRails_Cannot_Reach_Target()
    {
        // Arrange
        var bg = Colors.Black;
        var fg = AllyariaColorValue.FromHsva(0, 100, 10);
        const double minimum = 7.0;

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);

        // Assert
        result.MeetsMinimum.Should()
            .BeTrue();

        result.ContrastRatio.Should()
            .BeGreaterThanOrEqualTo(minimum);

        result.ContrastRatio.Should()
            .BeLessThanOrEqualTo(21.0);
    }

    [Fact]
    public void
        EnsureMinimumContrast_Should_Prefer_HigherRatio_When_Both_Mix_Directions_Are_Better_Than_Rail_Best_And_None_Meet_BlackDominates()
    {
        // Arrange
        var bg = Colors.Red;
        var fg = AllyariaColorValue.FromHsva(0, 100, 40);
        const double minimum = 10.0;

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);

        // Assert
        var whiteVsBg = ColorHelper.ContrastRatio(Colors.White, bg);
        var blackVsBg = ColorHelper.ContrastRatio(Colors.Black, bg);

        result.MeetsMinimum.Should()
            .BeFalse();

        result.ContrastRatio.Should()
            .BeApproximately(blackVsBg, 1e-9);

        blackVsBg.Should()
            .BeGreaterThan(whiteVsBg);
    }

    [Fact]
    public void
        EnsureMinimumContrast_Should_Prefer_HigherRatio_When_Both_Mix_Directions_Are_Better_Than_Rail_Best_And_None_Meet_WhiteDominates()
    {
        // Arrange
        // Background: saturated blue -> white has much higher contrast than black.
        var bg = AllyariaColorValue.FromRgba(0, 0, 255);

        // Foreground: some mid yellow-ish (rails along V won’t reach 9:1 against blue),
        // forcing the algorithm into the pole-mix “best-approaching” logic.
        var fg = AllyariaColorValue.FromHsva(60, 100, 60);
        const double minimum = 9.0; // Unreachable by rails; white vs blue ~8.6 < 9, black vs blue ~2.4 < 9

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);

        // Assert: best-approaching should prefer toward WHITE over all others for blue backgrounds.
        var whiteVsBg = ColorHelper.ContrastRatio(Colors.White, bg);
        var blackVsBg = ColorHelper.ContrastRatio(Colors.Black, bg);

        result.MeetsMinimum.Should()
            .BeFalse();

        result.ContrastRatio.Should()
            .BeApproximately(whiteVsBg, 1e-9);

        whiteVsBg.Should()
            .BeGreaterThan(blackVsBg);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Return_BestApproaching_When_No_Path_Meets_Target()
    {
        // Arrange
        var bg = Colors.Red;
        var fg = AllyariaColorValue.FromHsva(0, 100, 30);
        const double minimum = 10.0;

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);
        var maxTheoretical = ColorHelper.ContrastRatio(Colors.Black, bg);

        // Assert
        result.MeetsMinimum.Should()
            .BeFalse();

        result.ContrastRatio.Should()
            .BeLessThan(maxTheoretical + 1e-9);

        result.ContrastRatio.Should()
            .BeGreaterThan(5.0);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Return_Unchanged_When_Already_Meets_Minimum()
    {
        // Arrange
        var fg = Colors.White;
        var bg = Colors.Black;
        var startRatio = ColorHelper.ContrastRatio(fg, bg);

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg);

        // Assert
        result.MeetsMinimum.Should()
            .BeTrue();

        result.ContrastRatio.Should()
            .BeApproximately(startRatio, 1e-12);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Try_Opposite_Rail_When_Initial_Direction_Cannot_Meet()
    {
        // Arrange
        var bg = AllyariaColorValue.FromRgba(128, 128, 128);
        var fg = AllyariaColorValue.FromHsva(0, 0, 55);
        const double minimum = 5.0;

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);

        // Assert
        result.MeetsMinimum.Should()
            .BeTrue();

        result.ContrastRatio.Should()
            .BeGreaterThanOrEqualTo(minimum);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Update_Best_When_Second_Has_Higher_Ratio_But_None_Meet()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(0, 0, 20);
        var fg = AllyariaColorValue.FromHsva(0, 20, 10);

        var maxExt = Math.Max(ColorHelper.ContrastRatio(Colors.White, bg), ColorHelper.ContrastRatio(Colors.Black, bg));
        var minimum = maxExt + 0.1;

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);

        // Assert
        var whiteVsBg = ColorHelper.ContrastRatio(Colors.White, bg);
        var blackVsBg = ColorHelper.ContrastRatio(Colors.Black, bg);

        result.MeetsMinimum.Should()
            .BeFalse();

        result.ContrastRatio.Should()
            .BeApproximately(whiteVsBg, 1e-9);

        whiteVsBg.Should()
            .BeGreaterThan(blackVsBg);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Update_Best_With_TowardBlack_When_Black_Is_Best_But_None_Meet()
    {
        // Arrange
        var bg = AllyariaColorValue.FromHsva(0, 20, 60);
        var fg = AllyariaColorValue.FromHsva(0, 20, 10);

        var blackVsBg = ColorHelper.ContrastRatio(Colors.Black, bg);
        var minimum = blackVsBg + 0.1;

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, minimum);

        // Assert
        var whiteVsBg = ColorHelper.ContrastRatio(Colors.White, bg);

        result.MeetsMinimum.Should()
            .BeFalse();

        result.ContrastRatio.Should()
            .BeApproximately(blackVsBg, 1e-9);

        blackVsBg.Should()
            .BeGreaterThan(whiteVsBg);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Use_TieBreak_Brighten_When_V_LessThan_50_On_NearTie()
    {
        // Arrange
        var fg = AllyariaColorValue.FromHsva(155, 60, 33);
        var bg = AllyariaColorValue.FromHsva(155, 60, 33);

        var up = AllyariaColorValue.FromHsva(155, 60, 35);
        var dn = AllyariaColorValue.FromHsva(155, 60, 31);

        var rUp = ColorHelper.ContrastRatio(up, bg);
        var rDn = ColorHelper.ContrastRatio(dn, bg);

        Math.Abs(rUp - rDn)
            .Should()
            .BeLessThan(1e-6);

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, rUp);

        // Assert
        result.MeetsMinimum.Should()
            .BeTrue();

        result.ContrastRatio.Should()
            .BeApproximately(rUp, 1e-12);

        result.ContrastRatio.Should()
            .BeLessThan(rDn);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Use_TieBreak_Darken_When_V_AtLeast_50_On_NearTie()
    {
        // Arrange
        var fg = AllyariaColorValue.FromHsva(286, 100, 64);
        var bg = AllyariaColorValue.FromHsva(286, 100, 64);

        var up = AllyariaColorValue.FromHsva(286, 100, 66);
        var dn = AllyariaColorValue.FromHsva(286, 100, 62);

        var rUp = ColorHelper.ContrastRatio(up, bg);
        var rDn = ColorHelper.ContrastRatio(dn, bg);

        Math.Abs(rUp - rDn)
            .Should()
            .BeLessThan(1e-6);

        // Act
        var result = ColorHelper.EnsureMinimumContrast(fg, bg, rDn);

        // Assert
        result.MeetsMinimum.Should()
            .BeTrue();

        result.ContrastRatio.Should()
            .BeApproximately(rDn, 1e-12);

        result.ContrastRatio.Should()
            .BeLessThan(rUp);
    }

    [Fact]
    public void MixSrgb_Should_ClampToStart_And_End_When_T_OutOfRange()
    {
        // Arrange
        var a = AllyariaColorValue.FromRgba(10, 20, 30);
        var b = AllyariaColorValue.FromRgba(200, 210, 220);

        // Act
        var below = ColorHelper.MixSrgb(a, b, -0.5);
        var above = ColorHelper.MixSrgb(a, b, 1.5);

        // Assert
        below.HexRgba.Should()
            .Be(a.HexRgba);

        above.HexRgba.Should()
            .Be(b.HexRgba);
    }

    [Fact]
    public void MixSrgb_Should_Round_Half_AwayFromZero_Per_Channel()
    {
        // Arrange
        var start = AllyariaColorValue.FromRgba(0, 254, 0);
        var end = AllyariaColorValue.FromRgba(1, 255, 1);

        // Act
        var mid = ColorHelper.MixSrgb(start, end, 0.5);

        // Assert
        mid.R.Should()
            .Be(1);

        mid.G.Should()
            .Be(255);

        mid.B.Should()
            .Be(1);
    }

    [Fact]
    public void RelativeLuminance_Should_Use_Gamma_Branch_For_Larger_Channel()
    {
        // Arrange
        var color = AllyariaColorValue.FromRgba(11, 0, 0);

        // Act
        var actual = ColorHelper.RelativeLuminance(color);

        // Assert (0.2126 * ((c + 0.055)/1.055)^2.4)
        var c = 11.0 / 255.0;
        var expected = 0.2126 * Math.Pow((c + 0.055) / 1.055, 2.4);

        actual.Should()
            .BeApproximately(expected, 1e-12);
    }

    [Fact]
    public void RelativeLuminance_Should_Use_Linear_Branch_For_Small_Channel()
    {
        // Arrange
        var color = AllyariaColorValue.FromRgba(10, 0, 0);

        // Act
        var actual = ColorHelper.RelativeLuminance(color);

        // Assert (0.2126 * (10/255)/12.92)
        var expected = 0.2126 * (10.0 / 255.0 / 12.92);

        actual.Should()
            .BeApproximately(expected, 1e-12);
    }
}
