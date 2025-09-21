using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaColorTests
{
    [Fact]
    public void AlphaByte_Internal_Rounds_AwayFromZero()
    {
        var mid = AllyariaColor.FromRgba(0, 0, 0, 0.5);

        mid.AlphaByte.Should()
            .Be(128); // 0.5 * 255 = 127.5 → away-from-zero → 128
    }

    [Fact]
    public void Clamp_Clamp01_ClampByte_Work_As_Expected()
    {
        AllyariaColor.Clamp(-5.0, 0.0, 10.0)
            .Should()
            .Be(0.0);

        AllyariaColor.Clamp(15.0, 0.0, 10.0)
            .Should()
            .Be(10.0);

        AllyariaColor.Clamp01(1.2)
            .Should()
            .Be(1.0);

        AllyariaColor.ClampByte(300)
            .Should()
            .Be(255);
    }

    [Theory]
    [InlineData("#1E90FF", 30, 144, 255)]
    [InlineData("#FF0000", 255, 0, 0)]
    [InlineData("#000000", 0, 0, 0)]
    public void Ctor_String_Hex6_Parses(string hex, int er, int eg, int eb)
    {
        var c = new AllyariaColor(hex);

        c.R.Should()
            .Be((byte)er);

        c.G.Should()
            .Be((byte)eg);

        c.B.Should()
            .Be((byte)eb);

        c.A.Should()
            .Be(1.0);
    }

    [Fact]
    public void Ctor_String_Hex8_Parses_With_Alpha()
    {
        var c = new AllyariaColor("#1234567F"); // A=0x7F ≈ 127/255

        c.HexRgba.Should()
            .Be("#1234567F");

        c.A.Should()
            .BeApproximately(127.0 / 255.0, 1e-12);
    }

    [Fact]
    public void Ctor_String_hsv_hsva_Parses()
    {
        var c1 = new AllyariaColor("hsv(210, 100%, 100%)");

        c1.HexRgb.Should()
            .Be("#0080FF"); // sector math produces (0,128,255)

        var c2 = new AllyariaColor("hsva(0, 0%, 0%, 0.25)");

        c2.HexRgba.Should()
            .Be("#00000040"); // 0.25 -> 0x40
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-color")]
    [InlineData("#12")]
    [InlineData("rgb(300,0,0)")]
    [InlineData("hsv(1,2,3,4,5)")]
    public void Ctor_String_Invalid_Throws(string input)
    {
        var act = () => new AllyariaColor(input);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_rgb_rgba_Parses()
    {
        var c1 = new AllyariaColor("rgb(255, 0, 128)");

        c1.HexRgba.Should()
            .Be("#FF0080FF");

        var c2 = new AllyariaColor("rgba(255, 0, 128, 0.5)");

        c2.HexRgba.Should()
            .Be("#FF008080"); // 0.5 -> 128 (away-from-zero rounding)
    }

    [Fact]
    public void Ctor_String_ShortHex_Parses_RGB_And_RGBA()
    {
        var rgb = new AllyariaColor("#ABC"); // expands to #AABBCC

        rgb.HexRgba.Should()
            .Be("#AABBCCFF");

        var rgba = new AllyariaColor("#0F3C"); // #00FF33 with alpha C=12→ 12*17=204 => ~0.8

        rgba.HexRgb.Should()
            .Be("#00FF33");

        rgba.A.Should()
            .BeApproximately(204.0 / 255.0, 1e-12);
    }

    [Fact]
    public void Ctor_String_WebName_And_MaterialName_Parses()
    {
        var web = new AllyariaColor("DodgerBlue");

        web.HexRgb.Should()
            .Be("#1E90FF"); // from WebNameMap

        var mat = new AllyariaColor("Deep Purple 200");

        mat.HexRgb.Should()
            .Be("#B39DDB"); // from MaterialMap
    }

    [Fact]
    public void Equality_And_HashCode_Are_RGBA_Based()
    {
        var a = new AllyariaColor("#11223344");
        var b = AllyariaColor.FromRgba(0x11, 0x22, 0x33, 0x44 / 255.0);

        (a == b).Should()
            .BeTrue();

        a.Equals((object)b)
            .Should()
            .BeTrue();

        a.GetHashCode()
            .Should()
            .Be(b.GetHashCode());
    }

    [Fact]
    public void FromHexString_Parses_All_Supported_Forms()
    {
        AllyariaColor.FromHexString("#ABC", out var r1, out var g1, out var b1, out var a1);

        r1.Should()
            .Be(0xAA);

        g1.Should()
            .Be(0xBB);

        b1.Should()
            .Be(0xCC);

        a1.Should()
            .Be(1.0);

        AllyariaColor.FromHexString("#A1B2C3D4", out var r2, out var g2, out var b2, out var a2);

        r2.Should()
            .Be(0xA1);

        g2.Should()
            .Be(0xB2);

        b2.Should()
            .Be(0xC3);

        a2.Should()
            .BeApproximately(0xD4 / 255.0, 1e-12);

        var act = () => AllyariaColor.FromHexString("#12", out _, out _, out _, out _);

        act.Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void FromHexString_Throws_ArgumentException_When_Input_Does_Not_Start_With_Hash()
    {
        // Arrange
        const string input = "ABCDEF";

        // Act
        var act = () => AllyariaColor.FromHexString(input, out _, out _, out _, out _);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.ParamName == "s")
            .WithMessage("Invalid hex color: 'ABCDEF'*");
    }

    [Fact]
    public void FromHSVA_Converts_To_RGB_And_Clamps()
    {
        // HSV(0,0,100) => white
        var white = AllyariaColor.FromHsva(0, 0, 100, 1.1);

        white.HexRgb.Should()
            .Be("#FFFFFF");

        white.A.Should()
            .Be(1.0);

        // HSV(0,0,0) => black
        var black = AllyariaColor.FromHsva(0, 0, 0);

        black.HexRgb.Should()
            .Be("#000000");
    }

    [Fact]
    public void FromRgba_ClampsAlpha_And_SetsChannels()
    {
        var c = AllyariaColor.FromRgba(255, 128, 7, 1.5); // alpha should clamp to 1.0

        c.R.Should()
            .Be(255);

        c.G.Should()
            .Be(128);

        c.B.Should()
            .Be(7);

        c.A.Should()
            .Be(1.0);

        c.HexRgba.Should()
            .Be("#FF8007FF"); // AA from clamped alpha 1.0
    }

    [Fact]
    public void FromRgbString_And_FromHsvString_Parse()
    {
        AllyariaColor.FromRgbString("rgba(12, 34, 56, 0.25)", out var rr, out var gg, out var bb, out var aa);

        rr.Should()
            .Be(12);

        gg.Should()
            .Be(34);

        bb.Should()
            .Be(56);

        aa.Should()
            .BeApproximately(0.25, 1e-12);

        AllyariaColor.FromHsvString("hsva(210, 50%, 60%, 0.75)", out var r2, out var g2, out var b2, out var a2);

        a2.Should()
            .BeApproximately(0.75, 1e-12);

        var badRgb = () => AllyariaColor.FromRgbString("rgb(300,0,0)", out _, out _, out _, out _);

        badRgb.Should()
            .Throw<ArgumentException>();

        var badHsv = () => AllyariaColor.FromHsvString("hsv(10, 200%, 30%)", out _, out _, out _, out _);

        badHsv.Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void FromRgbString_InvalidFormat_ThrowsArgumentException()
    {
        var act = () => AllyariaColor.FromRgbString("not-a-valid-rgb", out _, out _, out _, out _);

        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.ParamName == "s")
            .WithMessage("Invalid rgb/rgba() format: 'not-a-valid-rgb'. Expected rgb(r,g,b) or rgba(r,g,b,a).*");
    }

    [Theory]
    [InlineData("#202020FF", "#535353FF")] // V≈12.5 < 50 => lighten by 20 → ≈32.5% => 0x53
    [InlineData("#C0C0C0FF", "#8D8D8DFF")] // V≈75.3 ≥ 50 => darken by 20 → ≈55.3% => 0x8C
    public void HoverColor_Lightens_Or_Darkens_By_20_Depending_On_V(string input, string expected)
    {
        var c = new AllyariaColor(input);
        var h = c.HoverColor();

        h.HexRgba.Should()
            .Be(expected);
    }

    [Fact]
    public void HsvToRgb_And_RgbToHsv_RoundTrip_Basics()
    {
        // Simple anchors
        AllyariaColor.HsvToRgb(
            0.0,
            0.0,
            100.0,
            out var wr,
            out var wg,
            out var wb
        );

        wr.Should()
            .Be(255);

        wg.Should()
            .Be(255);

        wb.Should()
            .Be(255);

        AllyariaColor.HsvToRgb(
            0.0,
            0.0,
            0.0,
            out var br,
            out var bg,
            out var bb
        );

        br.Should()
            .Be(0);

        bg.Should()
            .Be(0);

        bb.Should()
            .Be(0);

        // Round-trip via properties
        var colored = new AllyariaColor("#1E90FF");
        (var h, var s, var v) = (colored.H, colored.S, colored.V);
        var recon = AllyariaColor.FromHsva(h, s, v, colored.A);

        recon.HexRgba.Should()
            .Be(colored.HexRgba);
    }

    [Theory]
    [InlineData(
        0,
        0,
        100,
        255,
        255,
        255
    )] // s <= 0 → achromatic white
    [InlineData(
        0,
        0,
        25,
        64,
        64,
        64
    )] // s <= 0 → achromatic gray with rounding
    [InlineData(
        30,
        100,
        100,
        255,
        128,
        0
    )] // i = 0 branch
    [InlineData(
        90,
        100,
        100,
        128,
        255,
        0
    )] // i = 1 branch
    [InlineData(
        150,
        100,
        100,
        0,
        255,
        128
    )] // i = 2 branch
    [InlineData(
        210,
        100,
        100,
        0,
        128,
        255
    )] // i = 3 branch
    [InlineData(
        270,
        100,
        100,
        128,
        0,
        255
    )] // i = 4 branch
    [InlineData(
        330,
        100,
        100,
        255,
        0,
        128
    )] // default (i = 5) branch
    [InlineData(
        -30,
        100,
        100,
        255,
        0,
        128
    )] // h normalization: negative → wraps to 330
    [InlineData(
        720,
        100,
        100,
        255,
        0,
        0
    )] // h normalization: >360 → wraps to 0
    [InlineData(
        0,
        0,
        150,
        255,
        255,
        255
    )] // v clamp high (v>100)
    [InlineData(
        0,
        0,
        -10,
        0,
        0,
        0
    )] // v clamp low  (v<0)
    public void HsvToRgb_Produces_Expected_Bytes(double h,
        double s,
        double v,
        byte er,
        byte eg,
        byte eb)
    {
        AllyariaColor.HsvToRgb(
            h,
            s,
            v,
            out var r,
            out var g,
            out var b
        );

        r.Should()
            .Be(er);

        g.Should()
            .Be(eg);

        b.Should()
            .Be(eb);
    }

    [Fact]
    public void NormalizeMaterialKey_Strips_Separators_And_Lowers() => AllyariaColor
        .NormalizeMaterialKey(" Deep-Purple_200 ")
        .Should()
        .Be("deeppurple200");

    [Fact]
    public void Operator_GreaterThan_Works_ByHexOrder()
    {
        var lower = new AllyariaColor("#11223344");
        var higher = new AllyariaColor("#11223345");

        (higher > lower).Should()
            .BeTrue();

        (lower > higher).Should()
            .BeFalse();
    }

    [Fact]
    public void Operator_GreaterThanOrEqual_True_For_Greater_Or_Equal()
    {
        var a = new AllyariaColor("#00FFFFFF");
        var b = new AllyariaColor("#10FFFFFF");
        var aCopy = new AllyariaColor("#00FFFFFF");

        (b >= a).Should()
            .BeTrue(); // greater

        (a >= aCopy).Should()
            .BeTrue(); // equal

        (a >= b).Should()
            .BeFalse(); // not greater/equal
    }

    [Fact]
    public void Operator_LessThanOrEqual_True_For_Less_Or_Equal()
    {
        var a = new AllyariaColor("#00FFFFFF");
        var b = new AllyariaColor("#10FFFFFF");
        var aCopy = new AllyariaColor("#00FFFFFF");

        (a <= b).Should()
            .BeTrue(); // less

        (a <= aCopy).Should()
            .BeTrue(); // equal

        (b <= a).Should()
            .BeFalse(); // not less/equal
    }

    [Fact]
    public void Operator_NotEqual_True_When_Different_False_When_Same()
    {
        var a = new AllyariaColor("#AABBCCDD");
        var b = new AllyariaColor("#AABBCCDE");
        var aCopy = new AllyariaColor("#AABBCCDD");

        (a != b).Should()
            .BeTrue();

        (a != aCopy).Should()
            .BeFalse();
    }

    [Fact]
    public void Ordering_Is_By_Uppercase_HexRgba()
    {
        var a = new AllyariaColor("#00FFFFFF"); // ...FF
        var b = new AllyariaColor("#10FFFFFF"); // ...FF but higher R

        (a < b).Should()
            .BeTrue();

        a.CompareTo(b)
            .Should()
            .BeLessThan(0);

        string sa = a, sb = b; // implicit to string

        sa.Should()
            .Be(a.HexRgba);

        var back = (AllyariaColor)sa; // implicit from string

        back.Should()
            .Be(a);
    }

    [Fact]
    public void ParseDouble_InvalidString_ThrowsArgumentException()
    {
        Action act = () => AllyariaColor.ParseDouble("not-a-number", "x", 0, 10);

        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.ParamName == "x")
            .WithMessage("Could not parse number x='not-a-number'. (Parameter 'x')");
    }

    [Fact]
    public void ParseInt_InvalidString_ThrowsArgumentException()
    {
        Action act = () => AllyariaColor.ParseInt("not-a-number", "x", 0, 10);

        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.ParamName == "x")
            .WithMessage("Could not parse integer x='not-a-number'. (Parameter 'x')");
    }

    [Fact]
    public void Properties_Expose_Correct_Representations()
    {
        var c = new AllyariaColor("#663399"); // rebeccapurple RGB

        c.HexRgb.Should()
            .Be("#663399");

        c.HexRgba.Should()
            .Be("#663399FF");

        c.Rgb.Should()
            .Be("rgb(102, 51, 153)");

        c.Rgba.Should()
            .Be("rgba(102, 51, 153, 1)");

        c.Hsv.Should()
            .MatchRegex(@"^hsv\(\d+(\.\d{1,2})?, \d+(\.\d{1,2})?%, \d+(\.\d{1,2})?%\)$");

        c.Hsva.Should()
            .MatchRegex(@"^hsva\(\d+(\.\d{1,2})?, \d+(\.\d{1,2})?%, \d+(\.\d{1,2})?%, 1(\.0{1,3})?\)$");
    }

    [Theory]
    [InlineData(255, 0, 0, 0.0)] // max = rf → red case
    [InlineData(0, 255, 0, 120.0)] // max = gf → green case
    [InlineData(0, 0, 255, 240.0)] // max = bf → blue case (h < 0 adjustment not triggered)
    [InlineData(0, 255, 128, 150.0)] // case where h calculation yields negative, forces +360 normalization
    [InlineData(255, 0, 128, 330.0)] // max=R, (gf-bf)<0 → h<0 adjusted to +360
    public void RgbToHsv_Computes_Hue_Saturation_Value(byte r, byte g, byte b, double expectedHue)
    {
        // Act
        AllyariaColor.RgbToHsv(
            r,
            g,
            b,
            out var h,
            out var s,
            out var v
        );

        // Assert
        h.Should()
            .BeInRange(expectedHue - .25, expectedHue + .25);

        s.Should()
            .BeInRange(0, 100);

        v.Should()
            .BeInRange(0, 100);
    }

    [Theory]
    [InlineData("#000000FF", 50, "#808080FF")] // v=0 -> +50 => mid gray
    [InlineData("#FFFFFF80", -50, "#80808080")] // v=100 -> -50 => mid gray (alpha preserved)
    public void ShiftColor_Adjusts_V_Clamped_And_Preserves_Alpha(string input, double delta, string expectedHex)
    {
        var c = new AllyariaColor(input);
        var shifted = c.ShiftColor(delta);

        shifted.HexRgba.Should()
            .Be(expectedHex);

        shifted.A.Should()
            .Be(c.A);
    }

    [Fact]
    public void ToHexNibble_Parses_Valid_Digits_And_Throws_Otherwise()
    {
        AllyariaColor.ToHexNibble('0')
            .Should()
            .Be(0);

        AllyariaColor.ToHexNibble('f')
            .Should()
            .Be(15);

        AllyariaColor.ToHexNibble('F')
            .Should()
            .Be(15);

        Action act = () => AllyariaColor.ToHexNibble('z');

        act.Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void Transparent_Keyword_Yields_ZeroAlpha()
    {
        var t = new AllyariaColor("transparent");

        t.A.Should()
            .Be(0.0);

        t.HexRgba.Should()
            .Be("#00000000");
    }

    [Fact]
    public void TryFromWebName_And_TryFromMaterialName_Work()
    {
        var ok = AllyariaColor.TryFromWebName("DodgerBlue", out var col);

        ok.Should()
            .BeTrue();

        col.HexRgb.Should()
            .Be("#1E90FF");

        var okM = AllyariaColor.TryFromMaterialName("deep purple a700", out var colM);

        okM.Should()
            .BeTrue();

        colM.HexRgb.Should()
            .Be("#6200EA");
    }

    [Fact]
    public void Value_Returns_Rgba_String()
    {
        var c = new AllyariaColor("#11223344");

        c.Value.Should()
            .Be("#11223344"); // 0x44 = 68 / 255 ≈ 0.2667 → formatted with 3 decimals
    }
}
