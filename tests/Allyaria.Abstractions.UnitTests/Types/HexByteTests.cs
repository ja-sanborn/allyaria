using Allyaria.Abstractions.Types;

namespace Allyaria.Abstractions.UnitTests.Types;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class HexByteTests
{
    [Theory]
    [InlineData(-1.0, 0x00)]
    [InlineData(0.0, 0x00)]
    [InlineData(0.5, 0x80)]
    [InlineData(1.0, 0xFF)]
    [InlineData(2.0, 0xFF)]
    public void ClampAlpha_Should_Clamp_AndConvert(double input, byte expected)
    {
        // Arrange & Act
        var sut = HexByte.ClampAlpha(input);

        // Assert
        sut.Value.Should().Be(expected);
    }

    [Fact]
    public void CompareTo_Should_Order_ByByteValue()
    {
        // Arrange
        var low = new HexByte(0x01);
        var high = new HexByte(0xFE);

        // Act
        var comparison = low.CompareTo(high);

        // Assert
        comparison.Should().BeLessThan(0);
    }

    [Theory]
    [InlineData((byte)0x00, "00")]
    [InlineData((byte)0x0A, "0A")]
    [InlineData((byte)0x7F, "7F")]
    [InlineData((byte)0xFF, "FF")]
    public void Ctor_Byte_Should_Format_AsUppercaseTwoChars(byte value, string expected)
    {
        // Arrange & Act
        var sut = new HexByte(value);

        // Assert
        sut.Value.Should().Be(value);
        sut.ToString().Should().Be(expected);
    }

    [Fact]
    public void Ctor_Default_Should_SetZero_When_NoArguments()
    {
        // Arrange & Act
        var sut = new HexByte();

        // Assert
        sut.Value.Should().Be(0);
        sut.ToString().Should().Be("00");
    }

    [Theory]
    [InlineData("FF", 0xFF)]
    [InlineData("0A", 0x0A)]
    [InlineData("fF", 0xFF)]
    [InlineData("7f", 0x7F)]
    public void Ctor_String_Should_Parse_And_Uppercase(string input, byte expectedByte)
    {
        // Arrange & Act
        var sut = new HexByte(input);

        // Assert
        sut.Value.Should().Be(expectedByte);
        sut.ToString().Should().Be(input.Trim().ToUpperInvariant());
    }

    [Theory]
    [InlineData("GG")]
    [InlineData("0x0A")]
    [InlineData("ZZ")]
    public void Ctor_String_Should_ThrowAryArgumentException_When_InvalidHex(string input)
    {
        // Arrange
        var act = () => _ = new HexByte(input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*Invalid hex nibble*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_String_Should_ThrowAryArgumentException_When_NullOrWhitespace(string? input)
    {
        // Arrange
        var act = () => _ = new HexByte(input!);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void Equals_Should_BeTrue_When_ByteValuesMatch()
    {
        // Arrange
        var a = new HexByte(0xAB);
        var b = new HexByte(0xAB);

        // Act
        var result = a.Equals(b);

        // Assert
        result.Should().BeTrue();
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Fact]
    public void EqualsObject_Should_ReturnFalse_When_DifferentType()
    {
        // Arrange
        var sut = new HexByte(0x10);

        // Act
        var result = sut.Equals(new object());

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(0.0, 0x00)]
    [InlineData(1.0, 0xFF)]
    [InlineData(0.5, 0x80)] // 127.5 rounds away from zero to 128
    [InlineData(0.00196078431372549, 0x01)] // ~1/510 * 255 ≈ 0.5 → rounds to 1
    public void FromAlpha_Should_Map_NormalizedToByte_UsingAwayFromZero(double alpha, byte expectedByte)
    {
        // Arrange & Act
        var sut = HexByte.FromNormalized(alpha);

        // Assert
        sut.Value.Should().Be(expectedByte);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    public void FromAlpha_Should_ThrowAryArgumentException_When_OutOfRange(double alpha)
    {
        // Arrange
        var act = () => _ = HexByte.FromNormalized(alpha);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void GetHashCode_Should_BeSame_ForEqualValues()
    {
        // Arrange
        var a = new HexByte(0x42);
        var b = new HexByte("42");

        // Act
        var hashA = a.GetHashCode();
        var hashB = b.GetHashCode();

        // Assert
        hashA.Should().Be(hashB);
    }

    [Fact]
    public void Implicit_ByteToHex_Should_InvokeByteCtor()
    {
        // Arrange
        HexByte sut = 0xAB;

        // Act & Assert
        sut.ToString().Should().Be("AB");
        sut.Value.Should().Be(0xAB);
    }

    [Fact]
    public void Implicit_HexToByte_Should_ReturnUnderlyingByte()
    {
        // Arrange
        var sut = new HexByte("FF");

        // Act
        byte value = sut;

        // Assert
        value.Should().Be(0xFF);
    }

    [Fact]
    public void Implicit_HexToString_Should_ReturnNibble()
    {
        // Arrange
        var sut = new HexByte(0x0A);

        // Act
        string asString = sut;

        // Assert
        asString.Should().Be("0A");
    }

    [Fact]
    public void Implicit_StringToHex_Should_InvokeStringCtor()
    {
        // Arrange
        HexByte sut = "0a";

        // Act & Assert
        sut.Value.Should().Be(0x0A);
        sut.ToString().Should().Be("0A");
    }

    [Theory]
    [InlineData("FF", 0xFF, "FF")]
    [InlineData("0a", 0x0A, "0A")]
    [InlineData("F", 0x0F, "FF")]
    public void Parse_String_Should_CreateExpectedHex(string input, byte expectedByte, string expectedValue)
    {
        // Arrange & Act
        var sut = HexByte.Parse(input);

        // Assert
        sut.Value.Should().Be(expectedByte);
        sut.ToString().Should().Be(expectedValue);
    }

    [Fact]
    public void RelationalOperators_Should_Work_AsExpected()
    {
        // Arrange
        var a = new HexByte(0x10);
        var b = new HexByte(0x20);
        var c = new HexByte(0x20);

        // Act & Assert
        (b > a).Should().BeTrue();
        (a < b).Should().BeTrue();
        (b >= c).Should().BeTrue();
        (a <= c).Should().BeTrue();
    }

    [Fact]
    public void ToString_Should_ReturnNibble()
    {
        // Arrange
        var sut = new HexByte(0x3E);

        // Act
        var text = sut.ToString();

        // Assert
        text.Should().Be("3E");
    }

    [Theory]
    [InlineData(null, false, null)]
    [InlineData("", false, null)]
    [InlineData("   ", false, null)]
    [InlineData("GG", false, null)]
    [InlineData("ff", true, "FF")]
    [InlineData("0F", true, "0F")]
    [InlineData("F", true, "0F")]
    public void TryParse_String_Should_ReturnExpected(string? input, bool expectedSuccess, string? expectedValue)
    {
        // Arrange & Act
        var success = HexByte.TryParse(input!, out var result);

        // Assert
        success.Should().Be(expectedSuccess);

        if (expectedSuccess)
        {
            result.Should().NotBeNull();
            result.ToString().Should().Be(expectedValue);
        }
        else
        {
            result.Should().BeNull();
        }
    }
}
