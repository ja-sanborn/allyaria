using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaCssColorTests
{
    [Fact]
    public void Alpha_Rounding_To_Byte_Is_AwayFromZero_When_Inspecting_HexRgba()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(0, 0, 0, 0.5);

        // Act
        var hex = sut.HexRgba;

        // Assert
        hex.Should()
            .Be("#00000080"); // 0.5 * 255 = 127.5 → rounds away-from-zero -> 128 (0x80)
    }

    [Fact]
    public void Constructor_InvalidRgb_Function_Format_Throws()
    {
        // Arrange
        var act = () => new AllyariaCssColor("not-a-valid-rgb");

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Theory]
    [InlineData("#1E90FF", 30, 144, 255)]
    [InlineData("#FF0000", 255, 0, 0)]
    [InlineData("#000000", 0, 0, 0)]
    public void Ctor_String_Hex6_Parses(string hex, int er, int eg, int eb)
    {
        // Arrange + Act
        var sut = new AllyariaCssColor(hex);

        // Assert
        sut.R.Should()
            .Be((byte)er);

        sut.G.Should()
            .Be((byte)eg);

        sut.B.Should()
            .Be((byte)eb);

        sut.A.Should()
            .Be(1.0);
    }

    [Fact]
    public void Ctor_String_Hex8_Parses_With_Alpha()
    {
        // Arrange + Act
        var sut = new AllyariaCssColor("#1234567F"); // A=0x7F ≈ 127/255

        // Assert
        sut.HexRgba.Should()
            .Be("#1234567F");

        sut.A.Should()
            .BeApproximately(127.0 / 255.0, 1e-12);
    }

    [Fact]
    public void Ctor_String_hsv_hsva_Parses()
    {
        // Arrange + Act
        var hsv = new AllyariaCssColor("hsv(210, 100%, 100%)");
        var hsva = new AllyariaCssColor("hsva(0, 0%, 0%, 0.25)");

        // Assert
        hsv.HexRgb.Should()
            .Be("#0080FF"); // (0,128,255)

        hsva.HexRgba.Should()
            .Be("#00000040"); // 0.25 -> 0x40
    }

    [Fact]
    public void Ctor_String_Hsv_OutOfRange_Throws()
    {
        // Arrange
        var act = () => new AllyariaCssColor("hsv(10, 200%, 30%)"); // S% out of [0..100]

        // Act + Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-color")]
    [InlineData("#12")]
    [InlineData("rgb(300,0,0)")]
    [InlineData("hsv(1,2,3,4,5)")]
    public void Ctor_String_Invalid_Throws(string input)
    {
        // Arrange
        var act = () => new AllyariaCssColor(input);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_rgb_rgba_Parses()
    {
        // Arrange + Act
        var rgb = new AllyariaCssColor("rgb(255, 0, 128)");
        var rgba = new AllyariaCssColor("rgba(255, 0, 128, 0.5)");

        // Assert
        rgb.HexRgba.Should()
            .Be("#FF0080FF");

        rgba.HexRgba.Should()
            .Be("#FF008080"); // 0.5 -> 128 (away-from-zero rounding)
    }

    [Fact]
    public void Ctor_String_Rgb_With_NonInteger_Component_Throws()
    {
        // Arrange
        var act = () => new AllyariaCssColor("rgb(a,0,0)");

        // Act + Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_RgbPrefix_InvalidFormat_Throws()
    {
        // Arrange
        var act = () => new AllyariaCssColor("rgb(1,2)");

        // Act + Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_ShortHex_Lowercase_Parses()
    {
        // Arrange + Act
        var rgb = new AllyariaCssColor("#abc"); // expands to #aabbcc
        var rgba = new AllyariaCssColor("#0f3c"); // #00ff33 with alpha nibble c → 12*17=204

        // Assert
        rgb.HexRgba.Should()
            .Be("#AABBCCFF");

        rgba.HexRgb.Should()
            .Be("#00FF33");

        rgba.A.Should()
            .BeApproximately(204.0 / 255.0, 1e-12);
    }

    [Fact]
    public void Ctor_String_ShortHex_Parses_RGB_And_RGBA()
    {
        // Arrange + Act
        var rgb = new AllyariaCssColor("#ABC"); // expands to #AABBCC
        var rgba = new AllyariaCssColor("#0F3C"); // #00FF33 with alpha nibble C → 12*17=204

        // Assert
        rgb.HexRgba.Should()
            .Be("#AABBCCFF");

        rgba.HexRgb.Should()
            .Be("#00FF33");

        rgba.A.Should()
            .BeApproximately(204.0 / 255.0, 1e-12);
    }

    [Fact]
    public void Ctor_String_ShortHex_With_Invalid_Hex_Digit_Throws()
    {
        // Arrange
        var act = () => new AllyariaCssColor("#abz"); // 'z' triggers ToHexNibble default branch

        // Act + Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_WebName_And_MaterialName_Parses()
    {
        // Arrange + Act
        var web = new AllyariaCssColor("DodgerBlue");
        var mat = new AllyariaCssColor("Deep Purple 200");

        // Assert
        web.HexRgb.Should()
            .Be("#1E90FF");

        mat.HexRgb.Should()
            .Be("#B39DDB");
    }

    [Fact]
    public void Equality_And_HashCode_Are_ValueBased()
    {
        // Arrange
        var a = new AllyariaCssColor("#11223344");
        var b = AllyariaCssColor.FromRgba(0x11, 0x22, 0x33, 0x44 / 255.0);
        var c = new AllyariaCssColor("#11223345");

        // Act
        var equal = a == b;
        var notEqual = a != c;

        // Assert
        equal.Should()
            .BeTrue();

        notEqual.Should()
            .BeTrue();

        a.Equals((object)b)
            .Should()
            .BeTrue();

        a.GetHashCode()
            .Should()
            .Be(b.GetHashCode());
    }

    [Fact]
    public void FromHsva_Converts_To_RGB_And_Clamps()
    {
        // Arrange + Act
        var white = AllyariaCssColor.FromHsva(0, 0, 100, 1.1); // alpha should clamp to 1.0
        var black = AllyariaCssColor.FromHsva(0, 0, 0); // black

        // Assert
        white.HexRgb.Should()
            .Be("#FFFFFF");

        white.A.Should()
            .Be(1.0);

        black.HexRgb.Should()
            .Be("#000000");
    }

    [Theory]
    [InlineData(
        0,
        0,
        100,
        255,
        255,
        255
    )] // achromatic white
    [InlineData(
        0,
        0,
        25,
        64,
        64,
        64
    )] // achromatic gray (rounding)
    [InlineData(
        30,
        100,
        100,
        255,
        128,
        0
    )] // sector i = 0
    [InlineData(
        90,
        100,
        100,
        128,
        255,
        0
    )] // sector i = 1
    [InlineData(
        150,
        100,
        100,
        0,
        255,
        128
    )] // sector i = 2
    [InlineData(
        210,
        100,
        100,
        0,
        128,
        255
    )] // sector i = 3
    [InlineData(
        270,
        100,
        100,
        128,
        0,
        255
    )] // sector i = 4
    [InlineData(
        330,
        100,
        100,
        255,
        0,
        128
    )] // sector i = 5
    [InlineData(
        -30,
        100,
        100,
        255,
        0,
        0
    )] // h normalization wraps to 330
    [InlineData(
        720,
        100,
        100,
        255,
        0,
        0
    )] // h normalization wraps to 0
    [InlineData(
        0,
        0,
        150,
        255,
        255,
        255
    )] // v clamp high
    [InlineData(
        0,
        0,
        -10,
        0,
        0,
        0
    )] // v clamp low
    public void FromHsva_Produces_Expected_Bytes(double h,
        double s,
        double v,
        byte er,
        byte eg,
        byte eb)
    {
        // Arrange + Act
        var sut = AllyariaCssColor.FromHsva(h, s, v);

        // Assert
        sut.R.Should()
            .Be(er);

        sut.G.Should()
            .Be(eg);

        sut.B.Should()
            .Be(eb);
    }

    [Fact]
    public void FromRgba_ClampsAlpha_And_SetsChannels()
    {
        // Arrange + Act
        var sut = AllyariaCssColor.FromRgba(255, 128, 7, 1.5); // alpha clamps to 1.0

        // Assert
        sut.R.Should()
            .Be(255);

        sut.G.Should()
            .Be(128);

        sut.B.Should()
            .Be(7);

        sut.A.Should()
            .Be(1.0);

        sut.HexRgba.Should()
            .Be("#FF8007FF"); // AA from clamped alpha 1.0
    }

    [Theory]
    [InlineData("#202020FF", "#535353FF")] // V≈12.5 < 50 => lighten by 20 → ≈32.5% => 0x53
    [InlineData("#C0C0C0FF", "#8D8D8DFF")] // V≈75.3 ≥ 50 => darken by 20 → ≈55.3% => 0x8D/0x8C rounded
    public void HoverColor_Lightens_Or_Darkens_By_20_Depending_On_V(string input, string expected)
    {
        // Arrange
        var sut = new AllyariaCssColor(input);

        // Act
        var hover = sut.HoverColor();

        // Assert
        hover.HexRgba.Should()
            .Be(expected);

        hover.A.Should()
            .Be(sut.A);
    }

    [Fact]
    public void Hsv_Get_Returns_Formatted_String()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(255, 0, 0);

        // Act
        var hsv = sut.Hsv;

        // Assert
        hsv.Should()
            .Be("hsv(0, 100%, 100%)");
    }

    [Fact]
    public void Hsv_RoundTrip_Via_Public_Api_Basics()
    {
        // Arrange
        var colored = new AllyariaCssColor("#1E90FF");

        // Act
        (var h, var s, var v) = (colored.H, colored.S, colored.V);
        var recon = AllyariaCssColor.FromHsva(h, s, v, colored.A);

        // Assert
        recon.HexRgba.Should()
            .Be(colored.HexRgba);
    }

    [Fact]
    public void Hsva_Get_Returns_Formatted_String()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(255, 0, 0, 0.25);

        // Act
        var hsva = sut.Hsva;

        // Assert
        hsva.Should()
            .Be("hsva(0, 100%, 100%, 0.25)");
    }

    [Fact]
    public void Hue_Computation_MaxBf_Is_Approximately_210()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(0, 128, 255);

        // Act
        var hue = sut.H;

        // Assert
        hue.Should()
            .BeInRange(209.75, 210.25); // covers max==bf branch
    }

    [Fact]
    public void Hue_Computation_MaxGf_Is_Approximately_150()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(0, 255, 128);

        // Act
        var hue = sut.H;

        // Assert
        hue.Should()
            .BeInRange(149.75, 150.25); // covers max==gf branch
    }

    [Fact]
    public void Hue_Computation_MaxRf_Adjusts_Negative_To_330()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(255, 0, 128);

        // Act
        var hue = sut.H;

        // Assert
        hue.Should()
            .BeInRange(329.75, 330.25); // covers max==rf branch and (h<0) correction
    }

    [Fact]
    public void Implicit_String_Conversions_Work()
    {
        // Arrange
        AllyariaCssColor sut = "#11223344";

        // Act
        string back = sut;

        // Assert
        back.Should()
            .Be("#11223344");

        var round = (AllyariaCssColor)back;

        round.HexRgba.Should()
            .Be("#11223344");
    }

    [Fact]
    public void Named_Colors_Web_And_Material_Are_Resolved_Via_Public_Ctor()
    {
        // Arrange + Act
        var web = new AllyariaCssColor("DodgerBlue");
        var material = new AllyariaCssColor("deep purple a700");

        // Assert
        web.HexRgb.Should()
            .Be("#1E90FF");

        material.HexRgb.Should()
            .Be("#6200EA");
    }

    [Fact]
    public void Rgb_Get_Returns_Formatted_String()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(255, 0, 0);

        // Act
        var rgb = sut.Rgb;

        // Assert
        rgb.Should()
            .Be("rgb(255, 0, 0)");
    }

    [Fact]
    public void Rgba_Get_Returns_Formatted_String()
    {
        // Arrange
        var sut = AllyariaCssColor.FromRgba(255, 0, 0, 0.25);

        // Act
        var rgba = sut.Rgba;

        // Assert
        rgba.Should()
            .Be("rgba(255, 0, 0, 0.25)");
    }

    [Theory]
    [InlineData("#000000FF", 50, "#808080FF")] // v=0 -> +50 => mid gray
    [InlineData("#FFFFFF80", -50, "#80808080")] // v=100 -> -50 => mid gray (alpha preserved)
    public void ShiftColor_Adjusts_V_Clamped_And_Preserves_Alpha(string input, double delta, string expectedHex)
    {
        // Arrange
        var sut = new AllyariaCssColor(input);

        // Act
        var shifted = sut.ShiftColor(delta);

        // Assert
        shifted.HexRgba.Should()
            .Be(expectedHex);

        shifted.A.Should()
            .Be(sut.A);
    }

    [Fact]
    public void Transparent_Keyword_Yields_ZeroAlpha()
    {
        // Arrange + Act
        var sut = new AllyariaCssColor("transparent");

        // Assert
        sut.A.Should()
            .Be(0.0);

        sut.HexRgba.Should()
            .Be("#00000000");
    }

    [Fact]
    public void TryParse_ReturnsFalse_And_Black_On_Invalid_Input()
    {
        // Arrange + Act
        var ok = AllyariaCssColor.TryParse("definitely-not-a-color", out var color);

        // Assert
        ok.Should()
            .BeFalse();

        color.HexRgba.Should()
            .Be("#000000FF");
    }

    [Fact]
    public void TryParse_ReturnsTrue_And_Parses_Valid_Inputs()
    {
        // Arrange
        var okHex = AllyariaCssColor.TryParse("#FF000080", out var hex);
        var okRgb = AllyariaCssColor.TryParse("rgba(0, 128, 255, 0.25)", out var rgb);
        var okWeb = AllyariaCssColor.TryParse("DodgerBlue", out var web);
        var okMat = AllyariaCssColor.TryParse("Deep Purple 200", out var mat);

        // Assert
        okHex.Should()
            .BeTrue();

        hex.HexRgba.Should()
            .Be("#FF000080");

        okRgb.Should()
            .BeTrue();

        rgb.HexRgba.Should()
            .Be("#0080FF40");

        okWeb.Should()
            .BeTrue();

        web.HexRgb.Should()
            .Be("#1E90FF");

        okMat.Should()
            .BeTrue();

        mat.HexRgb.Should()
            .Be("#B39DDB");
    }

    [Fact]
    public void Value_Returns_Rgba_String()
    {
        // Arrange + Act
        var sut = new AllyariaCssColor("#11223344");

        // Assert
        sut.Value.Should()
            .Be("#11223344");
    }
}
