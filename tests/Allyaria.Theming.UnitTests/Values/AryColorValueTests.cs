using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.UnitTests.Values;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryColorValueTests
{
    [Fact]
    public void Alpha_Rounding_To_Byte_Is_AwayFromZero_When_Inspecting_HexRgba()
    {
        // Arrange
        var sut = AryColorValue.FromRgba(0, 0, 0, 0.5);

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
        var act = () => new AryColorValue("not-a-valid-rgb");

        // Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Theory]
    [InlineData("#1E90FF", 30, 144, 255)]
    [InlineData("#FF0000", 255, 0, 0)]
    [InlineData("#000000", 0, 0, 0)]
    public void Ctor_String_Hex6_Parses(string hex, int er, int eg, int eb)
    {
        // Arrange + Act
        var sut = new AryColorValue(hex);

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
        var sut = new AryColorValue("#1234567F"); // A=0x7F ≈ 127/255

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
        var hsv = new AryColorValue("hsv(210, 100%, 100%)");
        var hsva = new AryColorValue("hsva(0, 0%, 0%, 0.25)");

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
        var act = () => new AryColorValue("hsv(10, 200%, 30%)"); // S% out of [0..100]

        // Act + Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Theory]
    [InlineData("hsva(0, 0%, 0%, -0.01)")]
    [InlineData("hsva(0, 0%, 0%, 1.01)")]
    public void Ctor_String_Hsva_Alpha_OutOfRange_Throws(string input)
    {
        // Arrange + Act
        var sut = () => new AryColorValue(input);

        // Assert
        sut.Should()
            .Throw<AryArgumentException>()
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
        var act = () => new AryColorValue(input);

        // Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_rgb_rgba_Parses()
    {
        // Arrange + Act
        var rgb = new AryColorValue("rgb(255, 0, 128)");
        var rgba = new AryColorValue("rgba(255, 0, 128, 0.5)");

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
        var act = () => new AryColorValue("rgb(a,0,0)");

        // Act + Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_RgbPrefix_InvalidFormat_Throws()
    {
        // Arrange
        var act = () => new AryColorValue("rgb(1,2)");

        // Act + Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_ShortHex_Lowercase_Parses()
    {
        // Arrange + Act
        var rgb = new AryColorValue("#abc"); // expands to #aabbcc
        var rgba = new AryColorValue("#0f3c"); // #00ff33 with alpha nibble c → 12*17=204

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
        var rgb = new AryColorValue("#ABC"); // expands to #AABBCC
        var rgba = new AryColorValue("#0F3C"); // #00FF33 with alpha nibble C → 12*17=204

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
        var act = () => new AryColorValue("#abz"); // 'z' triggers ToHexNibble default branch

        // Act + Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*Unrecognized color*");
    }

    [Fact]
    public void Ctor_String_WebName_And_MaterialName_Parses()
    {
        // Arrange + Act
        var web = new AryColorValue("DodgerBlue");
        var mat = new AryColorValue("Deep Purple 200");

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
        var a = new AryColorValue("#11223344");
        var b = AryColorValue.FromRgba(0x11, 0x22, 0x33, 0x44 / 255.0);
        var c = new AryColorValue("#11223345");

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
    public void Equals_Should_ReturnFalse_When_Obj_Is_Not_ValueBase()
    {
        // Arrange
        var sut = new AryColorValue("#11223344");

        // Act
        // ReSharper disable once SuspiciousTypeConversion.Global
        var resultWithString = sut.Equals("not-a-valuebase");

        // ReSharper disable once SuspiciousTypeConversion.Global
        var resultWithInt = sut.Equals(123);

        // Assert
        resultWithString.Should()
            .BeFalse();

        resultWithInt.Should()
            .BeFalse();
    }

    [Fact]
    public void Equals_Should_ReturnTrue_When_Obj_Is_SameType_And_Equal()
    {
        // Arrange
        var left = new AryColorValue("#A1B2C3D4");
        var right = new AryColorValue("#A1B2C3D4");

        // Act
        var result = left.Equals((object)right);

        // Assert
        result.Should()
            .BeTrue();
    }

    [Fact]
    public void Equals_Should_ThrowArgumentException_When_Comparing_Color_To_StringValue()
    {
        // Arrange
        var sut = new AryColorValue("#000000FF");
        var other = new AryStringValue("black"); // different ValueBase subtype

        // Act
        // ReSharper disable once SuspiciousTypeConversion.Global
        var act = () => sut.Equals((object)other);

        // Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("Cannot compare values of different types.");
    }

    [Fact]
    public void Equals_Should_ThrowArgumentException_When_Obj_Is_Different_ValueBase_Subtype()
    {
        // Arrange
        var sut = new AryColorValue("#01020304");
        var other = Substitute.For<ValueBase>("any");

        // Act
        var act = () => sut.Equals((object)other);

        // Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("Cannot compare values of different types.");
    }

    [Theory]
    [InlineData(0, 100, 100, "#FF0000FF")] // sector 0
    [InlineData(60, 100, 100, "#FFFF00FF")] // sector 1
    [InlineData(120, 100, 100, "#00FF00FF")] // sector 2
    [InlineData(240, 100, 100, "#0000FFFF")] // sector 4
    [InlineData(300, 100, 100, "#FF00FFFF")] // sector 5 -> default branch
    public void FromHsva_Covers_All_HsvToRgb_Sectors(double h, double s, double v, string expectedHex)
    {
        // Act
        var sut = AryColorValue.FromHsva(h, s, v);

        // Assert
        sut.HexRgba.Should()
            .Be(expectedHex);
    }

    [Fact]
    public void FromHsva_Covers_HsvToRgb_Sector3()
    {
        // Arrange + Act
        var sut = AryColorValue.FromHsva(180, 100, 100); // sector 3

        // Assert
        sut.HexRgba.Should()
            .Be("#00FFFFFF");
    }

    [Fact]
    public void FromHsva_Grayscale_When_Saturation_Is_Zero()
    {
        // Arrange
        const double h = 123; // any hue
        const double s = 0; // forces grayscale branch (s <= 0)
        const double v = 50; // mid gray -> 127.5 -> rounds to 128 (0x80)

        // Act
        var sut = AryColorValue.FromHsva(h, s, v);

        // Assert
        sut.HexRgba.Should()
            .Be("#808080FF");
    }

    [Fact]
    public void FromHsva_Normalizes_Hue_At_360_To_Zero()
    {
        // Act
        var sut = AryColorValue.FromHsva(360, 100, 100);

        // Assert
        sut.HexRgba.Should()
            .Be("#FF0000FF");
    }

    [Theory]
    [InlineData(-720.0, 150.0, -50.0, 2.0, "#000000FF")] // hue normalizes to 0, s/v clamp, a clamp to 1.0
    [InlineData(-1.0, -10.0, 150.0, -0.5, "#FFFFFF00")] // hue normalizes, s->0 (white), v->100, a->0
    public void FromHsva_Should_Clamp_And_Normalize_When_Inputs_OutOfRange(double h,
        double s,
        double v,
        double a,
        string expectedHex)
    {
        // Arrange + Act
        var sut = AryColorValue.FromHsva(h, s, v, a);

        // Assert
        sut.HexRgba.Should()
            .Be(expectedHex);
    }

    [Theory]
    [InlineData(-0.0001)]
    [InlineData(1.0001)]
    public void FromRgba_Should_ThrowArgumentOutOfRange_When_Alpha_OutOfRange(double alpha)
    {
        // Arrange
        var act = () => AryColorValue.FromRgba(1, 2, 3, alpha);

        // Act + Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*must be*");
    }

    [Fact]
    public void Hsv_For_Black_Is_Zeroes_And_Hue_Is_Zero()
    {
        // Arrange
        var sut = new AryColorValue("#000000");

        // Act
        var hsv = sut.Hsv;

        // Assert
        hsv.Should()
            .Be("hsv(0, 0%, 0%)");
    }

    [Fact]
    public void Hsv_Get_Returns_Formatted_String()
    {
        // Arrange
        var sut = AryColorValue.FromRgba(255, 0, 0);

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
        var colored = new AryColorValue("#1E90FF");

        // Act
        (var h, var s, var v) = (colored.H, colored.S, colored.V);
        var recon = AryColorValue.FromHsva(h, s, v, colored.A);

        // Assert
        recon.HexRgba.Should()
            .Be(colored.HexRgba);
    }

    [Fact]
    public void Hsva_Get_Returns_Formatted_String()
    {
        // Arrange
        var sut = AryColorValue.FromRgba(255, 0, 0, 0.25);

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
        var sut = AryColorValue.FromRgba(0, 128, 255);

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
        var sut = AryColorValue.FromRgba(0, 255, 128);

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
        var sut = AryColorValue.FromRgba(255, 0, 128);

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
        AryColorValue sut = "#11223344";

        // Act
        string back = sut;

        // Assert
        back.Should()
            .Be("#11223344");

        var round = (AryColorValue)back;

        round.HexRgba.Should()
            .Be("#11223344");
    }

    [Fact]
    public void Named_Colors_Web_And_Material_Are_Resolved_Via_Public_Ctor()
    {
        // Arrange + Act
        var web = new AryColorValue("DodgerBlue");
        var material = new AryColorValue("deep purple a700");

        // Assert
        web.HexRgb.Should()
            .Be("#1E90FF");

        material.HexRgb.Should()
            .Be("#6200EA");
    }

    [Fact]
    public void Parse_Should_Create_Equivalent_Instance_To_Constructor()
    {
        // Arrange
        var input = "#CAFEBABE";
        var viaCtor = new AryColorValue(input);

        // Act
        var viaParse = AryColorValue.Parse(input);

        // Assert
        viaParse.HexRgba.Should()
            .Be(viaCtor.HexRgba);
    }

    [Fact]
    public void Rgb_Get_Returns_Formatted_String()
    {
        // Arrange
        var sut = AryColorValue.FromRgba(255, 0, 0);

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
        var sut = AryColorValue.FromRgba(255, 0, 0, 0.25);

        // Act
        var rgba = sut.Rgba;

        // Assert
        rgba.Should()
            .Be("rgba(255, 0, 0, 0.25)");
    }

    [Fact]
    public void ToCss_Should_Not_Include_Spaces_Around_Colon_And_Must_End_With_Semicolon()
    {
        // Arrange
        var sut = new AryColorValue("#00000080"); // ensure deterministic Value

        // Act
        var css = sut.ToCss(" Opacity ");

        // Assert
        css.Should()
            .Be("opacity:#00000080;");

        css.Should()
            .EndWith(";");

        css.Should()
            .NotContain(" :");

        css.Should()
            .NotContain(": ");
    }

    [Theory]
    [InlineData("Color", "color")]
    [InlineData(" background-Color ", "background-color")]
    [InlineData("BORDER-TOP", "border-top")]
    public void ToCss_Should_Return_Lowercased_Trimmed_Declaration_When_PropertyName_Provided(string propertyName,
        string expectedProp)
    {
        // Arrange
        var sut = new AryColorValue("#A1B2C3D4");

        // Act
        var css = sut.ToCss(propertyName);

        // Assert
        css.Should()
            .Be($"{expectedProp}:#A1B2C3D4;");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t \n")]
    public void ToCss_Should_Return_Value_Only_When_PropertyName_Is_NullOrWhitespace(string? propertyName)
    {
        // Arrange
        var sut = new AryColorValue("#11223344");

        // Act
        var css = sut.ToCss(propertyName!);

        // Assert
        css.Should()
            .Be("#11223344");
    }

    [Fact]
    public void Transparent_Keyword_Yields_ZeroAlpha()
    {
        // Arrange + Act
        var sut = new AryColorValue("transparent");

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
        var ok = AryColorValue.TryParse("definitely-not-a-colorValue", out var color);

        // Assert
        ok.Should()
            .BeFalse();

        color?.HexRgba.Should()
            .Be("#000000FF");
    }

    [Fact]
    public void TryParse_ReturnsTrue_And_Parses_Valid_Inputs()
    {
        // Arrange
        var okHex = AryColorValue.TryParse("#FF000080", out var hex);
        var okRgb = AryColorValue.TryParse("rgba(0, 128, 255, 0.25)", out var rgb);
        var okWeb = AryColorValue.TryParse("DodgerBlue", out var web);
        var okMat = AryColorValue.TryParse("Deep Purple 200", out var mat);

        // Assert
        okHex.Should()
            .BeTrue();

        hex?.HexRgba.Should()
            .Be("#FF000080");

        okRgb.Should()
            .BeTrue();

        rgb?.HexRgba.Should()
            .Be("#0080FF40");

        okWeb.Should()
            .BeTrue();

        web?.HexRgb.Should()
            .Be("#1E90FF");

        okMat.Should()
            .BeTrue();

        mat?.HexRgb.Should()
            .Be("#B39DDB");
    }

    [Fact]
    public void TryParse_ReturnsTrue_For_ShortHex_Forms_RGB_And_RGBA()
    {
        // Arrange + Act
        var okRgb = AryColorValue.TryParse("#abc", out var rgb);
        var okRgba = AryColorValue.TryParse("#0f3c", out var rgba);

        // Assert
        okRgb.Should()
            .BeTrue();

        rgb!.HexRgba.Should()
            .Be("#AABBCCFF");

        okRgba.Should()
            .BeTrue();

        rgba!.HexRgba.Should()
            .Be("#00FF33CC");
    }

    [Fact]
    public void Value_Returns_Rgba_String()
    {
        // Arrange + Act
        var sut = new AryColorValue("#11223344");

        // Assert
        sut.Value.Should()
            .Be("#11223344");
    }

    [Fact]
    public void ValueBase_Compare_Returns0_When_Both_Null()
    {
        // Arrange + Act
        var result = ValueBase.Compare(null, null);

        // Assert
        result.Should()
            .Be(0);
    }

    [Fact]
    public void ValueBase_Compare_Returns1_When_Right_Is_Null()
    {
        // Arrange
        var left = new AryColorValue("#000000");

        // Act
        var result = ValueBase.Compare(left, null);

        // Assert
        result.Should()
            .Be(1);
    }

    [Fact]
    public void ValueBase_Compare_ReturnsMinus1_When_Left_Is_Null()
    {
        // Arrange
        var right = new AryColorValue("#000000");

        // Act
        var result = ValueBase.Compare(null, right);

        // Assert
        result.Should()
            .Be(-1);
    }

    [Fact]
    public void ValueBase_Compare_ThrowsArgumentException_When_TypesDiffer()
    {
        // Arrange
        var left = new AryColorValue("#000000");
        var other = Substitute.For<ValueBase>("zzz");

        // Act
        var act = () => ValueBase.Compare(left, other);

        // Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("Cannot compare values of different types.");
    }

    [Fact]
    public void ValueBase_CompareTo_Returns0_When_Same_Reference()
    {
        // Arrange
        var sut = new AryColorValue("#010203");

        // Act
        var result = sut.CompareTo(sut);

        // Assert
        result.Should()
            .Be(0);
    }

    [Fact]
    public void ValueBase_CompareTo_Returns1_When_Other_Is_Null()
    {
        // Arrange
        var sut = new AryColorValue("#010203");

        // Act
        var result = sut.CompareTo(null);

        // Assert
        result.Should()
            .Be(1);
    }

    [Fact]
    public void ValueBase_CompareTo_ThrowsArgumentException_When_TypesDiffer()
    {
        // Arrange
        var sut = new AryColorValue("#010203");
        var other = Substitute.For<ValueBase>("zzz");

        // Act
        var act = () => sut.CompareTo(other);

        // Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("Cannot compare values of different types.");
    }

    [Fact]
    public void ValueBase_Relational_Operators_Work_By_Canonical_Value()
    {
        // Arrange
        var small = new AryColorValue("#00000000");
        var equalLeft = new AryColorValue("#11223344");
        var equalRight = new AryColorValue("#11223344");
        var big = new AryColorValue("#FFFFFFFF");

        // Act + Assert
        (small < big).Should()
            .BeTrue();

        (big > small).Should()
            .BeTrue();

        (equalLeft <= equalRight).Should()
            .BeTrue();

        (equalLeft >= equalRight).Should()
            .BeTrue();
    }

    [Fact]
    public void ValueBase_Static_Equals_Behavior_With_Nulls_And_SameType()
    {
        // Arrange
        var a = new AryColorValue("#11223344");
        var b = new AryColorValue("#11223344");
        ValueBase? n = null;

        // Act
        var bothNull = ValueBase.Equals(n, null);
        var leftNull = ValueBase.Equals(n, a);
        var rightNull = ValueBase.Equals(a, null);
        var equal = ValueBase.Equals(a, b);

        // Assert
        bothNull.Should()
            .BeFalse(); // static Equals(null, null) -> false per implementation

        leftNull.Should()
            .BeFalse();

        rightNull.Should()
            .BeFalse();

        equal.Should()
            .BeTrue();
    }

    [Fact]
    public void ValueBase_ToString_Returns_Value()
    {
        // Arrange
        var sut = new AryColorValue("#0A0B0C0D");

        // Act
        var text = sut.ToString();

        // Assert
        text.Should()
            .Be(sut.Value);
    }
}
