using Allyaria.Theming.Constants;
using Allyaria.Theming.Types;

// ReSharper disable RedundantArgumentDefaultValue

namespace Allyaria.Theming.UnitTests.Types;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class HexColorTests
{
    [Fact]
    public void ContrastRatio_Should_Be_21_For_BlackOnWhite()
    {
        // Arrange
        var black = Colors.Black;
        var white = Colors.White;

        // Act
        var ratio = black.ContrastRatio(white);

        // Assert
        ratio.Should().BeApproximately(21.0, 1e-12);
    }

    [Fact]
    public void Ctor_Channels_Should_SetRGBA_And_ComputeHSV()
    {
        // Arrange
        var r = new HexByte(10);
        var g = new HexByte(20);
        var b = new HexByte(30);
        var a = new HexByte(40);

        // Act
        var sut = new HexColor(r, g, b, a);

        // Assert
        sut.R.Value.Should().Be(10);
        sut.G.Value.Should().Be(20);
        sut.B.Value.Should().Be(30);
        sut.A.Value.Should().Be(40);

        // HSV sanity: V=max/255, S>0, H in [0,360)
        sut.V.Should().BeApproximately(30 / 255.0, 1e-12);
        sut.S.Should().BeGreaterThan(0);
        sut.H.Should().BeGreaterThanOrEqualTo(0).And.BeLessThan(360);
    }

    [Fact]
    public void Ctor_String_Hsv_Should_Normalize_Negative_Hue_In_HsvaToRgba()
    {
        // Arrange
        const string input = "hsv(-60,100%,100%)"; // -60° should normalize to 300° inside HsvaToRgba

        // Act
        var sut = new HexColor(input);

        // Assert
        // HSV(300°,1,1) -> #FF00FF with default alpha 0xFF
        sut.ToString().Should().Be("#FF00FFFF");
    }

    [Fact]
    public void Ctor_String_Should_Parse_CSS4_SpaceSlash_Syntax()
    {
        // Arrange & Act
        var sut = new HexColor("rgb(255 0 0 / .5)");

        // Assert
        sut.ToString().Should().Be("#FF000080");
    }

    [Fact]
    public void Ctor_String_Should_Parse_NamedColor_From_Colors_Registry_CaseInsensitive()
    {
        // Arrange & Act
        var sut = new HexColor("red500"); // exists in Colors with exact hex

        // Assert (reference file Colors.Red500 => #F44336FF)
        sut.ToString().Should().Be(Colors.Red500.ToString());
    }

    [Theory]
    [InlineData("#ABC", "#AABBCCFF")]
    [InlineData("#ABCD", "#AABBCCDD")]
    [InlineData("#AABBCC", "#AABBCCFF")]
    [InlineData("#AABBCCDD", "#AABBCCDD")]
    public void Ctor_String_Should_ParseHex_Variants(string input, string expected)
    {
        // Arrange & Act
        var sut = new HexColor(input);

        // Assert
        sut.ToString().Should().Be(expected);
    }

    [Fact]
    public void Ctor_String_Should_ParseHsv_AndHsva()
    {
        // Arrange & Act
        var rgbFromHsv = new HexColor("hsv(0,100%,100%)"); // red
        var rgbaFromHsva = new HexColor("hsva(120,1,1,0.5)"); // green @ 50% alpha

        // Assert
        rgbFromHsv.ToString().Should().Be("#FF0000FF");
        rgbaFromHsva.ToString().Should().Be("#00FF0080");
    }

    [Fact]
    public void Ctor_String_Should_ParseHsv_WithFractional_SV()
    {
        // Arrange & Act
        var sut = new HexColor("hsv(0,0.75,0.5)"); // 75% sat, 50% value

        // Assert (rough shape: red is dominant, value ~128)
        sut.R.Value.Should().BeGreaterThan(100);
        sut.G.Value.Should().BeLessThan(128);
        sut.B.Value.Should().BeLessThan(128);
        sut.A.Value.Should().Be(255);
    }

    [Fact]
    public void Ctor_String_Should_ParseRgb_Ints_And_DefaultAlpha()
    {
        // Arrange & Act
        var sut = new HexColor("rgb(255,0,0)");

        // Assert
        sut.ToString().Should().Be("#FF0000FF");
    }

    [Fact]
    public void Ctor_String_Should_ParseRgb_Percentages()
    {
        // Arrange & Act
        var sut = new HexColor("rgb(100%,0%,0%)");

        // Assert
        sut.ToString().Should().Be("#FF0000FF");
    }

    [Fact]
    public void Ctor_String_Should_ParseRgba_WithFractionAlpha()
    {
        // Arrange & Act
        var sut = new HexColor("rgba(10,20,30,0.5)");

        // Assert
        sut.R.Value.Should().Be(10);
        sut.G.Value.Should().Be(20);
        sut.B.Value.Should().Be(30);
        sut.A.Value.Should().Be(128);
    }

    [Theory]
    [InlineData("#ZZZ")]
    [InlineData("#12")]
    [InlineData("##123456")]
    public void Ctor_String_Should_Throw_On_InvalidHex(string input)
    {
        // Arrange
        var act = () => new HexColor(input);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData("hsv(bad,10,10)")]
    [InlineData("hsv(0,150%,10%)")] // percent out of range
    [InlineData("hsva(0,0,0,NaN)")] // invalid alpha
    public void Ctor_String_Should_Throw_On_InvalidHsvInputs(string input)
    {
        // Arrange
        var act = () => new HexColor(input);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void Ctor_String_Should_Throw_On_InvalidRgbText()
    {
        // Arrange
        var act = () => new HexColor("rgb( , , )");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*Invalid RGB(A) color*");
    }

    [Fact]
    public void Ctor_String_Should_Throw_On_UnknownName()
    {
        // Arrange
        var act = () => new HexColor("not-a-real-color-name");

        // Assert
        act.Should().Throw<AryArgumentException>().WithMessage("*Invalid color string*");
    }

    [Fact]
    public void DefaultCtor_Should_ZeroRGBA_AndHSV()
    {
        // Arrange & Act
        var sut = new HexColor();

        // Assert
        sut.R.Value.Should().Be(0);
        sut.G.Value.Should().Be(0);
        sut.B.Value.Should().Be(0);
        sut.A.Value.Should().Be(0);
        sut.S.Should().Be(0);
        sut.V.Should().Be(0);
        sut.H.Should().Be(0);
        sut.ToString().Should().Be("#00000000");
    }

    [Fact]
    public void Desaturate_Should_Reduce_S_And_Optionally_Blend_Value_Toward_Mid_With_ByteQuantization()
    {
        // Arrange
        var saturated = HexColor.FromHsva(0, 1, 1); // bright red

        // Act
        var desat = saturated.Desaturate(0.6, 0.2);

        // Assert
        // S is reduced by amount: S' = 1 - 0.6 = 0.4
        desat.S.Should().BeApproximately(0.4, 1e-12);

        // V is blended toward 0.5: V_target = 1 + (0.5 - 1) * 0.2 = 0.9
        // BUT after converting back to 8-bit, 0.9 * 255 = 229.5 -> bankers round => 230 => 230/255
        var expectedQuantizedV = 230.0 / 255.0;
        desat.V.Should().BeApproximately(expectedQuantizedV, 1e-12);

        // (Optional extra sanity: resulting RGB should reflect the same quantization)
        // For H=0, S=0.4, V≈0.9 -> RGB ≈ (0.9, 0.54, 0.54) -> bytes (230, 138, 138) with banker’s rounding.
        desat.R.Value.Should().Be(230);
        desat.G.Value.Should().Be(138);
        desat.B.Value.Should().Be(138);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Adjust_ValueRail_To_Reach_Minimum_When_ForegroundEqualsBackground()
    {
        // Arrange
        var fg = Colors.Grey500; // #9E9E9EFF (S = 0, V ≈ 0.6196)
        var bg = Colors.Grey500;

        // Sanity: starting contrast is 1
        fg.ContrastRatio(bg).Should().BeApproximately(1.0, 1e-12);

        // Act
        var resolved = fg.EnsureMinimumContrast(bg, 3.0);

        // Assert
        // 1) It must change (can't meet 3:1 while identical to background)
        resolved.Should().NotBe(fg);

        // 2) It must actually meet the requested minimum
        resolved.ContrastRatio(bg).Should().BeGreaterThanOrEqualTo(3.0);

        // 3) Preserve hue/saturation (value rail only) and alpha
        resolved.S.Should().BeApproximately(fg.S, 1e-12);
        resolved.H.Should().BeApproximately(fg.H, 1e-12);
        resolved.A.Should().BeEquivalentTo(fg.A);

        // 4) Implementation may choose either pole (black or white). Validate it moved away
        //    and snapped to an extreme (common strategy).
        var extremes = new[]
        {
            0.0,
            1.0
        };

        extremes.Should().Contain(resolved.V);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Enter_SearchTowardPole_When_ValueRail_Cannot_Reach_Minimum()
    {
        // Arrange
        var bg = Colors.Black;
        var sut = HexColor.FromHsva(240, 1.0, 0.5);

        sut.ContrastRatio(bg).Should().BeLessThan(3.0);
        HexColor.FromHsva(240, 1.0, 1.0).ContrastRatio(bg).Should().BeLessThan(3.0);

        // Act
        var resolved = sut.EnsureMinimumContrast(bg, 3.0);

        // Assert
        resolved.Should().NotBe(sut);
        resolved.ContrastRatio(bg).Should().BeGreaterThanOrEqualTo(3.0);
        resolved.S.Should().BeLessThan(sut.S);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Keep_Best_Candidate_When_Minimum_Is_Unattainable()
    {
        // Arrange
        var bg = Colors.Grey500;
        var fg = Colors.Grey500;
        const double minRatio = 21.0; // maximum allowed, unreachable vs mid-gray

        // Act
        var resolved = fg.EnsureMinimumContrast(bg, minRatio);

        // Assert
        // We can't meet 21:1 vs Grey500, so result must be a "best effort" (contrast < 21)
        resolved.ContrastRatio(bg).Should().BeLessThan(minRatio);

        // And it should move in the direction that yields the larger contrast vs Grey500 — i.e., darker.
        resolved.V.Should().BeLessThan(fg.V);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_Return_TowardBlack_When_White_Direction_Cannot_Meet_Required_Minimum()
    {
        // Arrange
        var bg = Colors.White;
        var fg = Colors.Grey400; // relatively light; moving toward white cannot raise contrast enough for 7:1
        const double minRatio = 7.0;

        // Sanity: starting contrast is below target
        fg.ContrastRatio(bg).Should().BeLessThan(minRatio);

        // Act
        var resolved = fg.EnsureMinimumContrast(bg, minRatio);

        // Assert
        resolved.Should().NotBe(fg);
        resolved.ContrastRatio(bg).Should().BeGreaterThanOrEqualTo(minRatio);

        // Must have gone darker (toward black direction)
        resolved.V.Should().BeLessThan(fg.V);
    }

    [Fact]
    public void EnsureMinimumContrast_Should_ReturnSelf_When_Already_Passing()
    {
        // Arrange
        var fg = Colors.Black;
        var bg = Colors.White;

        // Act
        var resolved = fg.EnsureMinimumContrast(bg, 3.0);

        // Assert
        resolved.Should().BeEquivalentTo(fg);
    }

    [Fact]
    public void Equality_Comparison_And_Operators_Should_Work()
    {
        // Arrange
        var a = new HexColor("#11223344");
        var b = new HexColor("#11223344");
        var c = new HexColor("#11223345");

        // Act & Assert
        a.Equals(b).Should().BeTrue();
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
        a.CompareTo(b).Should().Be(0);

        (c > a).Should().BeTrue();
        (a < c).Should().BeTrue();
        (c >= a).Should().BeTrue();
        (a <= c).Should().BeTrue();

        a.GetHashCode().Should().Be(b.GetHashCode());
        a.Equals((object)b).Should().BeTrue();
        a.Equals((object)c).Should().BeFalse();
    }

    [Fact]
    public void FromHsva_Should_Wrap_And_Clamp_All_Inputs()
    {
        // Arrange
        var overHue = 720.0; // wraps to 0
        var negHue = -60.0; // wraps to 300
        var overS = 2.0; // clamped
        var negV = -1.0; // clamped
        var overA = 7.0; // clamped

        // Act
        var wrap0 = HexColor.FromHsva(overHue, 1, 1, 1);
        var wrap300 = HexColor.FromHsva(negHue, 1, 1, 1);
        var clamped = HexColor.FromHsva(120, overS, negV, overA);

        // Assert
        wrap0.ToString().Should().Be("#FF0000FF");
        wrap300.ToString().Should().Be("#FF00FFFF");
        clamped.ToString().Should().Be("#000000FF"); // v clamped to 0 -> black, alpha clamped to 1
    }

    [Theory]
    [InlineData(0, "#FF0000FF")] // sector <1
    [InlineData(60, "#FFFF00FF")] // sector <2
    [InlineData(120, "#00FF00FF")] // sector <3
    [InlineData(180, "#00FFFFFF")] // sector <4
    [InlineData(240, "#0000FFFF")] // sector <5
    [InlineData(300, "#FF00FFFF")] // default
    public void HsvaToRgba_Should_Produce_Expected_Primaries_And_Secondaries(double hue, string expected)
    {
        // Arrange & Act
        var sut = HexColor.FromHsva(hue, 1, 1, 1);

        // Assert
        sut.ToString().Should().Be(expected);
    }

    [Fact]
    public void Implicit_Conversions_String_To_Color_And_Back_Should_Work()
    {
        // Arrange
        HexColor c1 = "#01020304";
        HexColor c2 = "RedA400"; // from registry

        // Act
        string s1 = c1;
        string s2 = c2;

        // Assert
        s1.Should().Be("#01020304");
        s2.Should().Be(Colors.RedA400.ToString());
    }

    [Fact]
    public void Invert_Should_Produce_Photographic_Negative()
    {
        // Arrange
        var sut = Colors.Black.SetAlpha(0x7F);

        // Act
        var negative = sut.Invert();

        // Assert
        negative.ToString().Should().Be("#FFFFFFFF".Replace("FF", "FF").Insert(7, "7F").Remove(9)); // #FFFFFF7F
        negative.R.Value.Should().Be(255);
        negative.A.Value.Should().Be(0x7F); // alpha preserved
        negative.ToString().Should().Be("#FFFFFF7F");
    }

    [Fact]
    public void IsDark_IsLight_IsOpaque_IsTransparent_Should_Behave_AsDocumented()
    {
        // Arrange
        var black = Colors.Black;
        var white = Colors.White;
        var transparent = Colors.Black.SetAlpha(0);

        // Act & Assert
        black.IsDark().Should().BeTrue();
        black.IsLight().Should().BeFalse();

        white.IsLight().Should().BeTrue();
        white.IsDark().Should().BeFalse();

        white.IsOpaque().Should().BeTrue();
        transparent.IsTransparent().Should().BeTrue();
    }

    [Fact]
    public void Parse_And_TryParse_Should_Work_For_Valid_And_Invalid()
    {
        // Arrange & Act
        var parsed = HexColor.Parse("#11223344");

        var ok1 = HexColor.TryParse("red", out var fromName);
        var ok2 = HexColor.TryParse("#BADHEX", out var bad1);
        var ok3 = HexColor.TryParse(null, out var bad2);

        // Assert
        parsed.ToString().Should().Be("#11223344");

        ok1.Should().BeTrue();
        fromName.ToString().Should().Be(Colors.Red.ToString());

        ok2.Should().BeFalse();
        bad1.ToString().Should().Be("#00000000");

        ok3.Should().BeFalse();
        bad2.ToString().Should().Be("#00000000");
    }

    [Fact]
    public void RgbToHsv_Should_Handle_Gray_And_Sector_Picks_With_Ties()
    {
        // gray => H=0,S=0,V=~0.5
        var gray = new HexColor(new HexByte(128), new HexByte(128), new HexByte(128));
        gray.S.Should().Be(0);
        gray.H.Should().Be(0);
        gray.V.Should().BeApproximately(128 / 255.0, 1e-12);

        // red >= green & red >= blue sector
        var redish = new HexColor(new HexByte(200), new HexByte(100), new HexByte(50));
        redish.H.Should().BeGreaterThanOrEqualTo(0).And.BeLessThan(120);

        // green >= red & green >= blue sector
        var greenish = new HexColor(new HexByte(50), new HexByte(220), new HexByte(60));
        greenish.H.Should().BeGreaterThanOrEqualTo(120).And.BeLessThan(240);

        // blue sector by elimination
        var blueish = new HexColor(new HexByte(40), new HexByte(30), new HexByte(200));
        blueish.H.Should().BeGreaterThanOrEqualTo(240).And.BeLessThan(360);

        // ties favor first true branch (R then G then B)
        var tieRg = new HexColor(new HexByte(200), new HexByte(200), new HexByte(0)); // R>=G and R>=B
        tieRg.H.Should().BeGreaterThanOrEqualTo(0).And.BeLessThan(120);
    }

    [Fact]
    public void SetAlpha_Should_Create_New_With_Same_RGB_And_New_A()
    {
        // Arrange
        var baseColor = Colors.Red.SetAlpha(0); // ensure starting point predictable

        // Act
        var withAlpha = baseColor.SetAlpha(200);

        // Assert
        withAlpha.R.Should().BeEquivalentTo(baseColor.R);
        withAlpha.G.Should().BeEquivalentTo(baseColor.G);
        withAlpha.B.Should().BeEquivalentTo(baseColor.B);
        withAlpha.A.Value.Should().Be(200);
    }

    [Theory]
    [InlineData(0.25)]
    [InlineData(0.75)]
    public void ShiftLightness_Should_Move_Toward_Mid_With_Quantization_Tolerances(double startV)
    {
        // Arrange
        var h = 210.0;
        var s = 0.5;
        var start = HexColor.FromHsva(h, s, startV);

        // Act
        var shifted = start.ShiftLightness(0.25);

        // Assert
        // Moves toward mid (0.5):
        Math.Abs(shifted.V - 0.5).Should().BeLessThan(Math.Abs(startV - 0.5));

        // V is quantized to 8-bit steps after HSVA->RGBA conversion:
        var oneStep = 1.0 / 255.0; // ≈ 0.00392157
        Math.Abs(shifted.V - 0.5).Should().BeLessThanOrEqualTo(oneStep);

        // Hue & Saturation can drift slightly due to quantization and sector math.
        shifted.H.Should().BeApproximately(start.H, 0.5); // allow ~±0.5° drift
        shifted.S.Should().BeApproximately(start.S, oneStep); // allow ±1/255 drift
    }

    [Fact]
    public void ToComponentBorderColor_Should_Apply_10pct_EdgePole_Nudge_When_FGvsBorder_IsLow()
    {
        // Arrange
        var fg = Colors.Grey500;
        var fill = Colors.Grey500;
        var outer = Colors.Grey500;
        const double minContrast = 1.0;

        // Expected result: a 10% linear-light nudge toward the edge pole (White for a dark fill).
        var expected = fg.ToLerpLinearPreserveAlpha(Colors.White, 0.10);

        // Act
        var actual = fg.ToComponentBorderColor(outer, fill, minContrast, false);

        // Assert
        actual.Should().NotBe(fg);

        var vsFg = actual.ContrastRatio(fg);
        vsFg.Should().BeGreaterThan(1.0);
        vsFg.Should().BeLessThan(1.5);

        Math.Abs(actual.R.Value - expected.R.Value).Should().BeLessThanOrEqualTo(2);
        Math.Abs(actual.G.Value - expected.G.Value).Should().BeLessThanOrEqualTo(2);
        Math.Abs(actual.B.Value - expected.B.Value).Should().BeLessThanOrEqualTo(2);
        actual.A.Value.Should().Be(expected.A.Value);
    }

    [Fact]
    public void ToComponentBorderColor_Should_DelegateTo_ToDividerBorderColor_When_ComponentFill_IsNull()
    {
        // Arrange
        var fg = Colors.Grey700;
        var outer = Colors.White;
        const double minContrast = 3.0;

        // Act
        var actual = fg.ToComponentBorderColor(outer, null, minContrast, false);

        // Assert
        var expected = fg.ToDividerBorderColor(outer, minContrast, false);
        actual.ToString().Should().Be(expected.ToString()); // compare stable representation
    }

    [Fact]
    public void ToComponentBorderColor_Should_Handle_Fill_And_Outer_After_Adjustments()
    {
        // Arrange
        var fg = Colors.Grey800;
        var fill = Colors.Grey300;
        var outer = Colors.White;

        // Act
        var border = fg.ToComponentBorderColor(outer, fill, 3.0);

        // Assert
        // It should pass vs at least one side and keep hierarchy with fill (not stronger than FG over fill)
        var passVsFill = border.ContrastRatio(fill) >= 3.0;
        var passVsOuter = border.ContrastRatio(outer) >= 3.0;
        (passVsFill || passVsOuter).Should().BeTrue();

        var fgVsFill = fg.ContrastRatio(fill);
        border.ContrastRatio(fill).Should().BeLessThanOrEqualTo(fgVsFill);
    }

    [Fact]
    public void ToComponentBorderColor_Should_Return_FG_When_HighContrast()
    {
        // Arrange
        var fg = Colors.Blue500;
        var outer = Colors.White;

        // Act
        var border = fg.ToComponentBorderColor(outer, Colors.Blue100, 3.0, true);

        // Assert
        border.Should().BeEquivalentTo(fg);
    }

    [Fact]
    public void ToComponentBorderColor_Should_Use_PassingScore_InfiniteBranch_When_MinContrast_Is_Unattainable()
    {
        // Arrange
        var fg = Colors.Grey700; // arbitrary foreground (instance this)
        var fill = Colors.Grey500; // mid gray
        var outer = Colors.Grey500; // same as fill
        const double minContrast = 21.0; // maximum allowed (guarded by EnsureMinimumContrast)

        // Act
        var border = fg.ToComponentBorderColor(outer, fill, minContrast);

        // Assert
        // The chosen border should be different from the fill (we attempted to move away from it).
        border.Should().NotBe(fill);

        // And it cannot possibly meet 21:1 against either adjacency; this verifies we were in the "unattainable" branch.
        border.ContrastRatio(fill).Should().BeLessThan(minContrast);
        border.ContrastRatio(outer).Should().BeLessThan(minContrast);

        // Also sanity check: we moved toward the darker pole given Grey500 (common outcome for best-approach),
        // so V should be <= the fill's V after hierarchy tweaks (tolerate quantization jitter).
        border.V.Should().BeLessThanOrEqualTo(fill.V + 1.0 / 255.0);
    }

    [Fact]
    public void ToDividerBorderColor_Should_Pass_Contrast_And_Not_Exceed_FG_Contrast()
    {
        // Arrange
        var surface = Colors.Grey100;
        var fg = Colors.Grey900; // strong FG

        // Act
        var border = fg.ToDividerBorderColor(surface, 3.0);

        // Assert
        var borderVsSurface = border.ContrastRatio(surface);
        var fgVsSurface = fg.ContrastRatio(surface);

        borderVsSurface.Should().BeGreaterThanOrEqualTo(3.0);
        borderVsSurface.Should().BeLessThanOrEqualTo(fgVsSurface);
    }

    [Fact]
    public void ToDividerBorderColor_Should_Return_HighContrastStroke_When_Flagged()
    {
        // Arrange
        var surface = Colors.White;
        var fg = Colors.Red500;

        // Act
        var hc = fg.ToDividerBorderColor(surface, highContrast: true);

        // Assert (light surface -> black stroke)
        hc.Should().BeEquivalentTo(Colors.Black);
    }

    [Fact]
    public void ToLerpLinear_Should_GammaCorrect_And_Include_Alpha()
    {
        // Arrange
        var start = Colors.Black.SetAlpha(0);
        var end = Colors.White.SetAlpha(255);

        // Act
        var mid = start.ToLerpLinear(end, 0.5);

        // Assert
        // In linear light, midpoint is ~#BCBCBC and alpha half
        mid.A.Value.Should().Be(128);
        mid.R.Value.Should().Be(188);
        mid.G.Value.Should().Be(188);
        mid.B.Value.Should().Be(188);
        mid.ToString().Should().Be("#BCBCBC80");
    }

    [Fact]
    public void ToLerpLinearPreserveAlpha_Should_GammaCorrect_And_Keep_Alpha()
    {
        // Arrange
        var start = Colors.Black.SetAlpha(0x7F);
        var end = Colors.White.SetAlpha(0x20);

        // Act
        var mid = start.ToLerpLinearPreserveAlpha(end, 0.5);

        // Assert
        mid.ToString().Should().Be("#BCBCBC7F"); // same RGB as above, preserved alpha
    }

    [Fact]
    public void ToRelativeLuminance_Should_Match_Known_Values_For_Black_And_White()
    {
        // Arrange
        var black = Colors.Black;
        var white = Colors.White;

        // Act
        var lb = black.ToRelativeLuminance();
        var lw = white.ToRelativeLuminance();

        // Assert
        lb.Should().BeApproximately(0.0, 1e-12);
        lw.Should().BeApproximately(1.0, 1e-12);
    }

    [Fact]
    public void ToString_Should_Format_As_RRGGBBAA()
    {
        // Arrange
        var sut = new HexColor(new HexByte(0x0A), new HexByte(0x1B), new HexByte(0x2C), new HexByte(0x3D));

        // Act
        var s = sut.ToString();

        // Assert
        s.Should().Be("#0A1B2C3D");
    }
}
