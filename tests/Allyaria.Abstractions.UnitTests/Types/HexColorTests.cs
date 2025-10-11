using Allyaria.Abstractions.Types;

namespace Allyaria.Abstractions.UnitTests.Types;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class HexColorTests
{
    [Fact]
    public void CompareTo_Should_FollowLexicographicOrder_When_ComparingChannels()
    {
        // Arrange
        var lower = new HexColor("#00FFFF80"); // Alpha 128
        var higher = new HexColor("#00FFFFFF"); // Alpha 255 (same RGB)

        // Act
        var result = lower.CompareTo(higher);

        // Assert
        result.Should().BeLessThan(0);
        (lower < higher).Should().BeTrue();
        (higher > lower).Should().BeTrue();
        (higher >= lower).Should().BeTrue();
        (lower <= higher).Should().BeTrue();
    }

    [Fact]
    public void Constructor_Default_Should_Initialize_AllChannelsToZero_When_UsingParameterlessCtor()
    {
        // Arrange
        // (nothing)

        // Act
        var sut = new HexColor();

        // Assert
        sut.ToString().Should().Be("#00000000");
    }

    [Fact]
    public void Constructor_FromChannels_Should_DefaultAlphaToOpaque_When_AlphaIsNull()
    {
        // Arrange
        var r = new HexByte(255);
        var g = new HexByte(128);
        var b = new HexByte(0);

        // Act
        var sut = new HexColor(r, g, b);

        // Assert
        sut.ToString().Should().Be("#FF8000FF");
    }

    [Fact]
    public void Equals_And_HashCode_Should_TreatIdenticalColorsAsEqual()
    {
        // Arrange
        var a = new HexColor("#ABCDEF12");
        var b = new HexColor("rgba(171,205,239,0.07)"); // 0.07 * 255 = 17.85 -> 18 (0x12)

        // Act
        var equal = a.Equals(b);

        // Assert
        equal.Should().BeTrue();
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Fact]
    public void FromHsva_Should_Handle_HueWrapping_And_NegativeHue()
    {
        // Arrange
        // These two hues are equivalent: 420 == 60, and -300 == 60
        var a = HexColor.FromHsva(420, 1, 1);
        var b = HexColor.FromHsva(60, 1, 1);
        var c = HexColor.FromHsva(-300, 1, 1);

        // Act
        // (nothing; comparing)

        // Assert
        a.Should().Be(b);
        c.Should().Be(b);

        // Yellow expected (R=255,G=255,B=0, A=255)
        b.ToString().Should().Be("#FFFF00FF");
    }

    [Fact]
    public void FromHsva_Should_Map_To_CaseLt4_When_Hue_In_180_To_240()
    {
        // Arrange
        var hue = 210.0; // mid-segment
        var saturation = 1.0;
        var value = 1.0;

        // Act
        var sut = HexColor.FromHsva(hue, saturation, value);

        // Assert
        sut.ToString().Should().Be("#0080FFFF");
    }

    [Fact]
    public void FromHsva_Should_Map_To_CaseLt5_When_Hue_In_240_To_300()
    {
        // Arrange
        var hue = 270.0; // mid-segment
        var saturation = 1.0;
        var value = 1.0;

        // Act
        var sut = HexColor.FromHsva(hue, saturation, value);

        // Assert
        sut.ToString().Should().Be("#8000FFFF");
    }

    [Fact]
    public void FromHsva_Should_Map_To_DefaultCase_When_Hue_In_300_To_360()
    {
        // Arrange
        var hue = 330.0; // mid-segment
        var saturation = 1.0;
        var value = 1.0;

        // Act
        var sut = HexColor.FromHsva(hue, saturation, value);

        // Assert
        sut.ToString().Should().Be("#FF0080FF");
    }

    [Fact]
    public void FromHsva_Should_MapToExpectedRgb_When_SaturationZero_IsMidGray_And_AwayFromZeroRounding()
    {
        // Arrange
        // s = 0 => achromatic; v = 0.5 -> 128 by away-from-zero from 127.5
        // Act
        var sut = HexColor.FromHsva(123.45, 0, 0.5);

        // Assert
        sut.ToString().Should().Be("#808080FF");
    }

    [Fact]
    public void ImplicitConversions_Should_Work_BothWays()
    {
        // Arrange
        HexColor fromString = "#FFA50080"; // orange @ 50%

        // Act
        string roundTripped = fromString; // implicit to string

        // Assert
        roundTripped.Should().Be("#FFA50080");
        ((HexColor)"#FFA50080").Should().Be(fromString);
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_Hsv_Function_PrimaryRed()
    {
        // Arrange
        var input = "hsv(0,100%,100%)";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        sut.ToString().Should().Be("#FF0000FF");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_Hsva_Function_GrayWithAlpha()
    {
        // Arrange
        var input = "hsva(0, 0%, 50%, 0.5)";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        // v = 50% => RGB all 128; alpha 0.5 => 128
        sut.ToString().Should().Be("#80808080");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_LongHex6_DefaultsToOpaque()
    {
        // Arrange
        var input = "#A1B2C3";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        sut.ToString().Should().Be("#A1B2C3FF");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_LongHex8_WithAlpha()
    {
        // Arrange
        var input = "#A1B2C344";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        sut.ToString().Should().Be("#A1B2C344");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_Rgb_Function_NoAlpha()
    {
        // Arrange
        var input = "rgb(1, 2, 3)";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        sut.ToString().Should().Be("#010203FF");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_Rgba_Function_WithAlpha()
    {
        // Arrange
        var input = "rgba(255,0,128,0.5)";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        // 0.5 * 255 = 127.5 -> away-from-zero => 128 (0x80)
        sut.ToString().Should().Be("#FF008080");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_ShortHex3_WithoutAlpha()
    {
        // Arrange
        var input = "#0F8";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        sut.ToString().Should().Be("#00FF88FF");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedHex_When_ShortHex4_WithAlpha()
    {
        // Arrange
        var input = "#0F8C";

        // Act
        var sut = HexColor.Parse(input);

        // Assert
        sut.ToString().Should().Be("#00FF88CC");
    }

    [Theory]
    [InlineData("#12")] // unsupported length
    [InlineData("#GGG")] // invalid hex digit
    [InlineData("##123456")] // invalid format
    public void Parse_Should_ThrowAryArgumentException_When_HexIsInvalid(string input)
    {
        // Arrange
        // (nothing)

        // Act
        var act = () => HexColor.Parse(input);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData("hsv(0,-1%,50%)")] // negative percent
    [InlineData("hsv(0,101%,50%)")] // >100%
    [InlineData("hsv(0,50%,101%)")] // >100%
    [InlineData("hsva(0,50%,50%,1.1)")] // alpha > 1
    [InlineData("hsvx(0,50%,50%)")] // malformed
    public void Parse_Should_ThrowAryArgumentException_When_Hsv_IsInvalid(string input)
    {
        // Arrange
        // (nothing)

        // Act
        var act = () => HexColor.Parse(input);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData("rgba(256,0,0,1)")]
    [InlineData("rgba(0,256,0,1)")]
    [InlineData("rgba(0,0,256,1)")]
    [InlineData("rgba(0,0,0,1.5)")] // alpha out of range
    [InlineData("rgba(0,0)")] // malformed
    public void Parse_Should_ThrowAryArgumentException_When_Rgba_IsInvalid(string input)
    {
        // Arrange
        // (nothing)

        // Act
        var act = () => HexColor.Parse(input);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void ParseHex_Should_ThrowAryArgumentException_When_HexLengthUnsupported()
    {
        // Arrange
        var input = "#12345"; // length = 5, not 3/4/6/8

        // Act
        var act = () => HexColor.Parse(input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*Unsupported hex color length*");
    }

    [Fact]
    public void ToString_Should_ReturnUppercaseHex_WithHashPrefix_AndEightDigits()
    {
        // Arrange
        var sut = new HexColor(new HexByte(0x0a), new HexByte(0x1b), new HexByte(0x2c), new HexByte(0x3d));

        // Act
        var s = sut.ToString();

        // Assert
        s.Should().Be("#0A1B2C3D");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not-a-color")]
    public void TryParse_Should_ReturnFalse_And_DefaultResult_When_InputIsInvalid(string? input)
    {
        // Arrange
        // (nothing)

        // Act
        var ok = HexColor.TryParse(input!, out var result);

        // Assert
        ok.Should().BeFalse();
        result.ToString().Should().Be("#00000000"); // default(HexColor)
    }

    [Fact]
    public void TryParse_Should_ReturnTrue_And_SetResult_When_InputIsValid()
    {
        // Arrange
        var input = "#11223344";

        // Act
        var ok = HexColor.TryParse(input, out var result);

        // Assert
        ok.Should().BeTrue();
        result.ToString().Should().Be("#11223344");
    }
}
