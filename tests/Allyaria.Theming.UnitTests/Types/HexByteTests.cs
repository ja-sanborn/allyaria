namespace Allyaria.Theming.UnitTests.Types;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class HexByteTests
{
    [Fact]
    public void ByteCtor_Should_SetValue_When_ValidByteProvided()
    {
        // Arrange
        const byte input = 173;

        // Act
        var sut = new HexByte(input);

        // Assert
        sut.Value.Should().Be(173);
        sut.ToString().Should().Be("AD");
    }

    [Theory]
    [InlineData(-1.0, 0)]
    [InlineData(1.7, 255)]
    public void ClampAlpha_Should_ClampAndConvert_OutOfRange_To_Ends(double input, int expectedByte)
    {
        // Arrange & Act
        var sut = HexByte.ClampAlpha(input);

        // Assert
        sut.Value.Should().Be((byte)expectedByte);
    }

    [Fact]
    public void ClampAlpha_Should_Throw_When_InputIsNaN()
    {
        // Arrange
        var act = () => HexByte.ClampAlpha(double.NaN);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void ComparisonOperators_Should_BeConsistentWithValue()
    {
        // Arrange
        var low = new HexByte(10);
        var high = new HexByte(20);

        // Act & Assert
        (high > low).Should().BeTrue();
        (high >= low).Should().BeTrue();
        (low < high).Should().BeTrue();
        (low <= high).Should().BeTrue();
        (low <= new HexByte(10)).Should().BeTrue();
        (high >= new HexByte(20)).Should().BeTrue();
        low.CompareTo(high).Should().BeLessThan(0);
        high.CompareTo(low).Should().BeGreaterThan(0);
    }

    [Fact]
    public void DefaultCtor_Should_InitializeToZero_When_Called()
    {
        // Arrange & Act
        var sut = new HexByte();

        // Assert
        ((byte)sut).Should().Be(0);
        sut.ToString().Should().Be("00");
    }

    [Fact]
    public void Equality_Should_Fail_For_DifferentValues_AndDifferentTypes()
    {
        // Arrange
        var a = new HexByte(1);
        var b = new HexByte(2);
        object otherType = 1;

        // Act & Assert
        a.Equals(b).Should().BeFalse();
        a.Equals(otherType).Should().BeFalse();
        (a == b).Should().BeFalse();
        (a != b).Should().BeTrue();
    }

    [Fact]
    public void Equality_Should_Work_For_SameValue()
    {
        // Arrange
        var a = new HexByte(123);
        var b = new HexByte(123);

        // Act & Assert
        a.Equals(b).Should().BeTrue();
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
        a.GetHashCode().Should().Be(b.GetHashCode());
        a.Equals((object)b).Should().BeTrue();
    }

    [Theory]
    [InlineData(0.0, 0)]
    [InlineData(1.0, 255)]
    [InlineData(0.5, 128)] // 0.5 * 255 = 127.5 -> ToEven => 128
    public void FromNormalized_Should_MapToByte_With_BankersRounding(double normalized, int expectedByte)
    {
        // Arrange & Act
        var sut = HexByte.FromNormalized(normalized);

        // Assert
        sut.Value.Should().Be((byte)expectedByte);
    }

    [Fact]
    public void FromNormalized_Should_Throw_When_NotFinite()
    {
        // Arrange
        var actNaN = () => HexByte.FromNormalized(double.NaN);
        var actPosInf = () => HexByte.FromNormalized(double.PositiveInfinity);
        var actNegInf = () => HexByte.FromNormalized(double.NegativeInfinity);

        // Assert
        actNaN.Should().Throw<AryArgumentException>()
            .WithMessage("Normalized value must be a finite number.");

        actPosInf.Should().Throw<AryArgumentException>();
        actNegInf.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData(-0.0000001)]
    [InlineData(1.0000001)]
    public void FromNormalized_Should_Throw_When_OutOfRange(double value)
    {
        // Arrange
        var act = () => HexByte.FromNormalized(value);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void ImplicitConversions_Should_Work_Between_Types()
    {
        // Arrange
        HexByte fromByte = 200;
        HexByte fromString = "0f";

        // Act
        byte backToByte = fromString;
        string toStringFromHex = fromByte;

        // Assert
        fromByte.Value.Should().Be(200);
        fromString.Value.Should().Be(15);
        backToByte.Should().Be(15);
        toStringFromHex.Should().Be("C8");
    }

    [Fact]
    public void Parse_Should_ReturnHexByte_When_Valid()
    {
        // Arrange & Act
        var result = HexByte.Parse("7f");

        // Assert
        result.Value.Should().Be(0x7F);
    }

    [Theory]
    [InlineData("F", 0x0F, "0F")]
    [InlineData("0a", 0x0A, "0A")]
    [InlineData(" ff ", 0xFF, "FF")]
    [InlineData("00", 0x00, "00")]
    public void StringCtor_Should_ParseHex_When_InputIs1or2Digits_WithWhitespaceAllowed(string input,
        int expectedByte,
        string expectedUpper)
    {
        // Arrange & Act
        var sut = new HexByte(input);

        // Assert
        sut.Value.Should().Be((byte)expectedByte);
        sut.ToString().Should().Be(expectedUpper);
    }

    [Theory]
    [InlineData("123")] // > 2 chars
    [InlineData("G")] // non-hex
    [InlineData("--")]
    public void StringCtor_Should_ThrowAryArgumentException_When_NotValidHex(string input)
    {
        // Arrange
        var act = () => _ = new HexByte(input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(ex => ex.Message.Contains("Invalid hexadecimal string", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Length > 0
            );
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void StringCtor_Should_ThrowAryArgumentException_When_NullOrWhitespace(string? input)
    {
        // Arrange
        var act = () => _ = new HexByte(input!);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData(0, 255, -1.0, 0)]
    [InlineData(0, 255, 2.0, 255)]
    public void ToLerpByte_Should_ClampFactor_When_OutOfRange(double start, double end, double t, int expected)
    {
        // Arrange
        var sut = new HexByte((byte)start);

        // Act
        var actual = sut.ToLerpByte((byte)end, t);

        // Assert
        actual.Should().Be((byte)expected);
    }

    [Theory]
    [InlineData(0, 255, 0.0, 0)]
    [InlineData(0, 255, 1.0, 255)]
    [InlineData(0, 255, 0.5, 128)] // 127.5 -> ToEven => 128
    [InlineData(10, 20, 0.25, 12)] // 12.5 -> ToEven => 12
    [InlineData(200, 100, 0.5, 150)] // descending
    public void ToLerpByte_Should_InterpolateLinearly_With_ClampingAndBankersRounding(int start,
        int end,
        double t,
        int expected)
    {
        // Arrange
        var sut = new HexByte((byte)start);

        // Act
        var actual = sut.ToLerpByte((byte)end, t);

        // Assert
        actual.Should().Be((byte)expected);
    }

    [Fact]
    public void ToLerpByte_Should_TreatNonFiniteFactor_AsZero()
    {
        // Arrange
        var sut = new HexByte(77);

        // Act
        var actual = sut.ToLerpByte(200, double.NaN);

        // Assert
        actual.Should().Be(77);
    }

    [Fact]
    public void ToLerpHexByte_Should_WrapToLerpByte_Result()
    {
        // Arrange
        var sut = new HexByte(10);

        // Act
        var hex = sut.ToLerpHexByte(20, 0.25);

        // Assert
        ((byte)hex).Should().Be(12);
        hex.ToString().Should().Be("0C");
    }

    [Theory]
    [InlineData(0, 255, 0.0, 0)]
    [InlineData(0, 255, 1.0, 255)]
    [InlineData(0, 255, 0.5, 188)] // gamma-correct midpoint
    public void ToLerpLinearByte_Should_InterpolateInLinearLight(int start, int end, double t, int expected)
    {
        // Arrange
        var sut = new HexByte((byte)start);

        // Act
        var actual = sut.ToLerpLinearByte((byte)end, t);

        // Assert
        actual.Should().Be((byte)expected);
    }

    [Fact]
    public void ToLerpLinearByte_Should_TreatNonFiniteFactor_AsZero()
    {
        // Arrange
        var sut = new HexByte(100);

        // Act
        var actual = sut.ToLerpLinearByte(200, double.PositiveInfinity);

        // Assert
        actual.Should().Be(100);
    }

    [Fact]
    public void ToLerpLinearHexByte_Should_WrapToLerpLinearByte_Result()
    {
        // Arrange
        var sut = new HexByte(0);

        // Act
        var hex = sut.ToLerpLinearHexByte(new HexByte(255), 0.5);

        // Assert
        ((byte)hex).Should().Be(188);
        hex.ToString().Should().Be("BC");
    }

    [Fact]
    public void ToNormalized_Should_ReturnValueDividedBy255()
    {
        // Arrange
        var sut = new HexByte(128);

        // Act
        var actual = sut.ToNormalized();

        // Assert
        actual.Should().BeApproximately(128.0 / 255.0, 1e-12);
    }

    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(255, 1.0)]
    public void ToSrgbLinearValue_Should_MapExtremes(int input, double expected)
    {
        // Arrange
        var sut = new HexByte((byte)input);

        // Act
        var linear = sut.ToSrgbLinearValue();

        // Assert
        linear.Should().BeApproximately(expected, 1e-12);
    }

    [Fact]
    public void ToSrgbLinearValue_Should_UseSrgbEotf_For_MiddleValues()
    {
        // Arrange
        var sut = new HexByte(12); // below 0.04045*255 threshold (≈10.33) is false; 12 -> pow branch

        // Act
        var linear = sut.ToSrgbLinearValue();

        // Assert
        // Verified expected via the sRGB EOTF: ((c+0.055)/1.055)^2.4 where c=12/255
        var c = 12 / 255.0;
        var expected = Math.Pow((c + 0.055) / 1.055, 2.4);
        linear.Should().BeApproximately(expected, 1e-12);
    }

    [Fact]
    public void ToString_Should_ReturnTwoUppercaseHexDigits()
    {
        // Arrange
        var sut = new HexByte(15);

        // Act
        var s = sut.ToString();

        // Assert
        s.Should().Be("0F");
    }

    [Theory]
    [InlineData("ZZ")]
    [InlineData("123")] // >2 chars
    public void TryParse_Should_ReturnFalse_When_Invalid(string input)
    {
        // Arrange & Act
        var ok = HexByte.TryParse(input, out var result);

        // Assert
        ok.Should().BeFalse();
        result.Value.Should().Be(0);
    }

    [Fact]
    public void TryParse_Should_ReturnFalse_When_NullOrWhitespace()
    {
        // Arrange & Act
        var okNull = HexByte.TryParse(null, out var r1);
        var okEmpty = HexByte.TryParse("", out var r2);
        var okWs = HexByte.TryParse("   ", out var r3);

        // Assert
        okNull.Should().BeFalse();
        okEmpty.Should().BeFalse();
        okWs.Should().BeFalse();

        r1.Value.Should().Be(0);
        r2.Value.Should().Be(0);
        r3.Value.Should().Be(0);
    }

    [Theory]
    [InlineData("0", 0)]
    [InlineData("ff", 255)]
    public void TryParse_Should_SetResult_When_Valid(string input, int expected)
    {
        // Arrange & Act
        var ok = HexByte.TryParse(input, out var result);

        // Assert
        ok.Should().BeTrue();
        result.Value.Should().Be((byte)expected);
    }
}
