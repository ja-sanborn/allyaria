namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleLengthTests
{
    [Fact]
    public void Constructor_Should_AllowEmptyString_AndSetNumberToZero()
    {
        // Arrange & Act
        var sut = new StyleLength(value: string.Empty);

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
        sut.LengthUnit.Should().BeNull();
    }

    [Fact]
    public void Constructor_Should_AllowWhitespaceString_AndSetNumberToZero()
    {
        // Arrange & Act
        var sut = new StyleLength(value: "   ");

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
        sut.LengthUnit.Should().BeNull();
    }

    [Theory]
    [InlineData("10", 10.0)]
    [InlineData("-3.14", -3.14)]
    [InlineData(".5", 0.5)]
    [InlineData("  42  ", 42.0)]
    public void Constructor_Should_ParseNumericValues_When_NoUnitProvided(string input, double expectedNumber)
    {
        // Arrange & Act
        var sut = new StyleLength(value: input);

        // Assert
        sut.Number.Should().BeApproximately(expectedValue: expectedNumber, precision: 0.0001);
        sut.LengthUnit.Should().BeNull();
    }

    [Theory]
    [InlineData("10px", 10.0, LengthUnits.Pixel)]
    [InlineData("1.5em", 1.5, LengthUnits.Em)]
    [InlineData("0.25rem", 0.25, LengthUnits.RootEm)]
    [InlineData("-42%", -42.0, LengthUnits.Percent)]
    [InlineData("100vw", 100.0, LengthUnits.ViewportWidth)]
    public void Constructor_Should_ParseValidLengthStrings_WithUnits(string input,
        double expectedNumber,
        LengthUnits expectedUnit)
    {
        // Arrange & Act
        var sut = new StyleLength(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Number.Should().BeApproximately(expectedValue: expectedNumber, precision: 0.0001);
        sut.LengthUnit.Should().Be(expected: expectedUnit);
    }

    [Fact]
    public void Constructor_Should_ThrowAryArgumentException_When_InvalidNumberProvided()
    {
        // Arrange
        const string invalid = "not-a-length";

        // Act
        var act = () => new StyleLength(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid length: not-a-length");
    }

    [Fact]
    public void Constructor_Should_ThrowAryArgumentException_When_UnknownUnitProvided()
    {
        // Arrange
        const string invalid = "10qq";

        // Act
        var act = () => new StyleLength(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid length: 10qq");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_Recognized()
    {
        // Arrange
        const string input = "10rem";

        // Act
        StyleLength sut = input;

        // Assert
        sut.Value.Should().Be(expected: "10rem");
        sut.LengthUnit.Should().Be(expected: LengthUnits.RootEm);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_Invalid()
    {
        // Arrange
        const string invalid = "1bad";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleLength sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid length: 1bad");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_Null()
    {
        // Arrange
        StyleLength? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_NotNull()
    {
        // Arrange
        var sut = new StyleLength(value: "15px");

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "15px");
    }

    [Fact]
    public void Parse_Should_ReturnEmptyInstance_When_NullValueProvided()
    {
        // Arrange & Act
        var sut = StyleLength.Parse(value: null);

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
        sut.LengthUnit.Should().BeNull();
    }

    [Fact]
    public void Parse_Should_ReturnExpectedInstance_When_ValidLengthProvided()
    {
        // Arrange
        const string input = "5cm";

        // Act
        var sut = StyleLength.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: "5cm");
        sut.Number.Should().Be(expected: 5.0);
        sut.LengthUnit.Should().Be(expected: LengthUnits.Centimeter);
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_InvalidValueProvided()
    {
        // Arrange
        const string invalid = "bad-value";

        // Act
        var act = () => StyleLength.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid length: bad-value");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_NumberIsTooLargeToParse()
    {
        // Arrange
        var veryLargeNumber = "1" + new string(c: '0', count: 400);

        // Act
        var success = StyleLength.TryParse(value: veryLargeNumber, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "??";

        // Act
        var success = StyleLength.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndEmptyInstance_When_ValueIsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var success = StyleLength.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
        sut.LengthUnit.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "100%";

        // Act
        var success = StyleLength.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "100%");
        sut.Number.Should().Be(expected: 100);
        sut.LengthUnit.Should().Be(expected: LengthUnits.Percent);
    }
}
