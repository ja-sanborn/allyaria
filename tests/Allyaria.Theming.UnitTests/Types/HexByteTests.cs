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
        var sut = new HexByte(value: input);

        // Assert
        sut.Value.Should().Be(expected: 173);
        sut.ToString().Should().Be(expected: "AD");
    }

    [Theory]
    [InlineData(-1.0, 0)]
    [InlineData(1.7, 255)]
    public void ClampAlpha_Should_ClampAndConvert_OutOfRange_To_Ends(double input, int expectedByte)
    {
        // Arrange & Act
        var sut = HexByte.ClampAlpha(value: input);

        // Assert
        sut.Value.Should().Be(expected: (byte)expectedByte);
    }

    [Fact]
    public void ClampAlpha_Should_Throw_When_InputIsNaN()
    {
        // Arrange
        var act = () => HexByte.ClampAlpha(value: double.NaN);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void ComparisonOperators_Should_BeConsistentWithValue()
    {
        // Arrange
        var low = new HexByte(value: 10);
        var high = new HexByte(value: 20);

        // Act & Assert
        (high > low).Should().BeTrue();
        (high >= low).Should().BeTrue();
        (low < high).Should().BeTrue();
        (low <= high).Should().BeTrue();
        (low <= new HexByte(value: 10)).Should().BeTrue();
        (high >= new HexByte(value: 20)).Should().BeTrue();
        low.CompareTo(other: high).Should().BeLessThan(expected: 0);
        high.CompareTo(other: low).Should().BeGreaterThan(expected: 0);
    }

    [Fact]
    public void DefaultCtor_Should_InitializeToZero_When_Called()
    {
        // Arrange & Act
        var sut = new HexByte();

        // Assert
        ((byte)sut).Should().Be(expected: 0);
        sut.ToString().Should().Be(expected: "00");
    }

    [Fact]
    public void Equality_Should_Fail_For_DifferentValues_AndDifferentTypes()
    {
        // Arrange
        var a = new HexByte(value: 1);
        var b = new HexByte(value: 2);
        object otherType = 1;

        // Act & Assert
        a.Equals(other: b).Should().BeFalse();
        a.Equals(obj: otherType).Should().BeFalse();
        (a == b).Should().BeFalse();
        (a != b).Should().BeTrue();
    }

    [Fact]
    public void Equality_Should_Work_For_SameValue()
    {
        // Arrange
        var a = new HexByte(value: 123);
        var b = new HexByte(value: 123);

        // Act & Assert
        a.Equals(other: b).Should().BeTrue();
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
        a.GetHashCode().Should().Be(expected: b.GetHashCode());
        a.Equals(obj: b).Should().BeTrue();
    }

    [Theory]
    [InlineData(0.0, 0)]
    [InlineData(1.0, 255)]
    [InlineData(0.5, 128)] // 0.5 * 255 = 127.5 -> ToEven => 128
    public void FromNormalized_Should_MapToByte_With_BankersRounding(double normalized, int expectedByte)
    {
        // Arrange & Act
        var sut = HexByte.FromNormalized(value: normalized);

        // Assert
        sut.Value.Should().Be(expected: (byte)expectedByte);
    }

    [Fact]
    public void FromNormalized_Should_Throw_When_NotFinite()
    {
        // Arrange
        var actNaN = () => HexByte.FromNormalized(value: double.NaN);
        var actPosInf = () => HexByte.FromNormalized(value: double.PositiveInfinity);
        var actNegInf = () => HexByte.FromNormalized(value: double.NegativeInfinity);

        // Assert
        actNaN.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Normalized theme must be a finite number.");

        actPosInf.Should().Throw<AryArgumentException>();
        actNegInf.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData(-0.0000001)]
    [InlineData(1.0000001)]
    public void FromNormalized_Should_Throw_When_OutOfRange(double value)
    {
        // Arrange
        var act = () => HexByte.FromNormalized(value: value);

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
        fromByte.Value.Should().Be(expected: 200);
        fromString.Value.Should().Be(expected: 15);
        backToByte.Should().Be(expected: 15);
        toStringFromHex.Should().Be(expected: "C8");
    }

    [Fact]
    public void Parse_Should_ReturnHexByte_When_Valid()
    {
        // Arrange & Act
        var result = HexByte.Parse(value: "7f");

        // Assert
        result.Value.Should().Be(expected: 0x7F);
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
        var sut = new HexByte(value: input);

        // Assert
        sut.Value.Should().Be(expected: (byte)expectedByte);
        sut.ToString().Should().Be(expected: expectedUpper);
    }

    [Theory]
    [InlineData("123")] // > 2 chars
    [InlineData("G")] // non-hex
    [InlineData("--")]
    public void StringCtor_Should_ThrowAryArgumentException_When_NotValidHex(string input)
    {
        // Arrange
        var act = () => _ = new HexByte(value: input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                        "Invalid hexadecimal string", StringComparison.OrdinalIgnoreCase
                    ) ||
                    ex.Message.Length > 0
            );
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void StringCtor_Should_ThrowAryArgumentException_When_NullOrWhitespace(string? input)
    {
        // Arrange
        var act = () => _ = new HexByte(value: input!);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData(0, 255, -1.0, 0)]
    [InlineData(0, 255, 2.0, 255)]
    public void ToLerpByte_Should_ClampFactor_When_OutOfRange(double start, double end, double t, int expected)
    {
        // Arrange
        var sut = new HexByte(value: (byte)start);

        // Act
        var actual = sut.ToLerpByte(end: (byte)end, factor: t);

        // Assert
        actual.Should().Be(expected: (byte)expected);
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
        var sut = new HexByte(value: (byte)start);

        // Act
        var actual = sut.ToLerpByte(end: (byte)end, factor: t);

        // Assert
        actual.Should().Be(expected: (byte)expected);
    }

    [Fact]
    public void ToLerpByte_Should_TreatNonFiniteFactor_AsZero()
    {
        // Arrange
        var sut = new HexByte(value: 77);

        // Act
        var actual = sut.ToLerpByte(end: 200, factor: double.NaN);

        // Assert
        actual.Should().Be(expected: 77);
    }

    [Fact]
    public void ToLerpHexByte_Should_WrapToLerpByte_Result()
    {
        // Arrange
        var sut = new HexByte(value: 10);

        // Act
        var hex = sut.ToLerpHexByte(end: 20, factor: 0.25);

        // Assert
        ((byte)hex).Should().Be(expected: 12);
        hex.ToString().Should().Be(expected: "0C");
    }

    [Theory]
    [InlineData(0, 255, 0.0, 0)]
    [InlineData(0, 255, 1.0, 255)]
    [InlineData(0, 255, 0.5, 188)] // gamma-correct midpoint
    public void ToLerpLinearByte_Should_InterpolateInLinearLight(int start, int end, double t, int expected)
    {
        // Arrange
        var sut = new HexByte(value: (byte)start);

        // Act
        var actual = sut.ToLerpLinearByte(end: (byte)end, factor: t);

        // Assert
        actual.Should().Be(expected: (byte)expected);
    }

    [Fact]
    public void ToLerpLinearByte_Should_TreatNonFiniteFactor_AsZero()
    {
        // Arrange
        var sut = new HexByte(value: 100);

        // Act
        var actual = sut.ToLerpLinearByte(end: 200, factor: double.PositiveInfinity);

        // Assert
        actual.Should().Be(expected: 100);
    }

    [Fact]
    public void ToLerpLinearHexByte_Should_WrapToLerpLinearByte_Result()
    {
        // Arrange
        var sut = new HexByte(value: 0);

        // Act
        var hex = sut.ToLerpLinearHexByte(end: new HexByte(value: 255), factor: 0.5);

        // Assert
        ((byte)hex).Should().Be(expected: 188);
        hex.ToString().Should().Be(expected: "BC");
    }

    [Fact]
    public void ToNormalized_Should_ReturnValueDividedBy255()
    {
        // Arrange
        var sut = new HexByte(value: 128);

        // Act
        var actual = sut.ToNormalized();

        // Assert
        actual.Should().BeApproximately(expectedValue: 128.0 / 255.0, precision: 1e-12);
    }

    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(255, 1.0)]
    public void ToSrgbLinearValue_Should_MapExtremes(int input, double expected)
    {
        // Arrange
        var sut = new HexByte(value: (byte)input);

        // Act
        var linear = sut.ToSrgbLinearValue();

        // Assert
        linear.Should().BeApproximately(expectedValue: expected, precision: 1e-12);
    }

    [Fact]
    public void ToSrgbLinearValue_Should_UseSrgbEotf_For_MiddleValues()
    {
        // Arrange
        var sut = new HexByte(value: 12); // below 0.04045*255 threshold (≈10.33) is false; 12 -> pow branch

        // Act
        var linear = sut.ToSrgbLinearValue();

        // Assert
        // Verified expected via the sRGB EOTF: ((c+0.055)/1.055)^2.4 where c=12/255
        var c = 12 / 255.0;
        var expected = Math.Pow(x: (c + 0.055) / 1.055, y: 2.4);
        linear.Should().BeApproximately(expectedValue: expected, precision: 1e-12);
    }

    [Fact]
    public void ToString_Should_ReturnTwoUppercaseHexDigits()
    {
        // Arrange
        var sut = new HexByte(value: 15);

        // Act
        var s = sut.ToString();

        // Assert
        s.Should().Be(expected: "0F");
    }

    [Theory]
    [InlineData("ZZ")]
    [InlineData("123")] // >2 chars
    public void TryParse_Should_ReturnFalse_When_Invalid(string input)
    {
        // Arrange & Act
        var ok = HexByte.TryParse(value: input, result: out var result);

        // Assert
        ok.Should().BeFalse();
        result.Value.Should().Be(expected: 0);
    }

    [Fact]
    public void TryParse_Should_ReturnFalse_When_NullOrWhitespace()
    {
        // Arrange & Act
        var okNull = HexByte.TryParse(value: null, result: out var r1);
        var okEmpty = HexByte.TryParse(value: "", result: out var r2);
        var okWs = HexByte.TryParse(value: "   ", result: out var r3);

        // Assert
        okNull.Should().BeFalse();
        okEmpty.Should().BeFalse();
        okWs.Should().BeFalse();

        r1.Value.Should().Be(expected: 0);
        r2.Value.Should().Be(expected: 0);
        r3.Value.Should().Be(expected: 0);
    }

    [Theory]
    [InlineData("0", 0)]
    [InlineData("ff", 255)]
    public void TryParse_Should_SetResult_When_Valid(string input, int expected)
    {
        // Arrange & Act
        var ok = HexByte.TryParse(value: input, result: out var result);

        // Assert
        ok.Should().BeTrue();
        result.Value.Should().Be(expected: (byte)expected);
    }
}
