namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleNumberTests
{
    [Fact]
    public void Constructor_Should_AllowEmptyString_AndSetNumberToZero()
    {
        // Arrange & Act
        var sut = new StyleNumber(value: string.Empty);

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
    }

    [Fact]
    public void Constructor_Should_AllowWhitespaceString_AndSetNumberToZero()
    {
        // Arrange & Act
        var sut = new StyleNumber(value: "   ");

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
    }

    [Theory]
    [InlineData("0", 0)]
    [InlineData("42", 42)]
    [InlineData("-99", -99)]
    public void Constructor_Should_SetNumber_When_ValidIntegerProvided(string input, int expected)
    {
        // Arrange & Act
        var sut = new StyleNumber(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Number.Should().Be(expected: expected);
    }

    [Fact]
    public void Constructor_Should_ThrowAryArgumentException_When_InvalidIntegerProvided()
    {
        // Arrange
        const string invalid = "abc123";

        // Act
        var act = () => new StyleNumber(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid number: abc123");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_IntegerStringProvided()
    {
        // Arrange
        const string input = "17";

        // Act
        StyleNumber sut = input;

        // Assert
        sut.Value.Should().Be(expected: "17");
        sut.Number.Should().Be(expected: 17);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_InvalidValueProvided()
    {
        // Arrange
        const string invalid = "bad123";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleNumber sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid number: bad123");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleNumber? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleNumber(value: "456");

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "456");
    }

    [Fact]
    public void Parse_Should_ReturnEmptyInstance_When_NullValueProvided()
    {
        // Arrange & Act
        var sut = StyleNumber.Parse(value: null);

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
    }

    [Fact]
    public void Parse_Should_ReturnExpectedInstance_When_ValidIntegerProvided()
    {
        // Arrange
        const string input = "123";

        // Act
        var sut = StyleNumber.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: "123");
        sut.Number.Should().Be(expected: 123);
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_InvalidIntegerProvided()
    {
        // Arrange
        const string invalid = "xyz";

        // Act
        var act = () => StyleNumber.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid number: xyz");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-number";

        // Act
        var success = StyleNumber.TryParse(value: invalid, result: out var sut);

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
        var success = StyleNumber.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: string.Empty);
        sut.Number.Should().Be(expected: 0);
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "88";

        // Act
        var success = StyleNumber.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Number.Should().Be(expected: 88);
    }
}
