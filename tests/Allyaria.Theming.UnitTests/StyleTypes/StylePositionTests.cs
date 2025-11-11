namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StylePositionTests
{
    [Theory]
    [InlineData(StylePosition.Kind.Absolute, "absolute")]
    [InlineData(StylePosition.Kind.Fixed, "fixed")]
    [InlineData(StylePosition.Kind.Relative, "relative")]
    [InlineData(StylePosition.Kind.Static, "static")]
    [InlineData(StylePosition.Kind.Sticky, "sticky")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StylePosition.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StylePosition(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "fixed";

        // Act
        StylePosition sut = input;

        // Assert
        sut.Value.Should().Be(expected: "fixed");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-position-value";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StylePosition sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-position-value");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StylePosition? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StylePosition(kind: StylePosition.Kind.Relative);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "relative");
    }

    [Theory]
    [InlineData("absolute", StylePosition.Kind.Absolute)]
    [InlineData("fixed", StylePosition.Kind.Fixed)]
    [InlineData("relative", StylePosition.Kind.Relative)]
    [InlineData("static", StylePosition.Kind.Static)]
    [InlineData("sticky", StylePosition.Kind.Sticky)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StylePosition.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StylePosition.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StylePosition(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-position";

        // Act
        var act = () => StylePosition.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-position");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StylePosition.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "wrong-position";

        // Act
        var success = StylePosition.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var success = StylePosition.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "sticky";

        // Act
        var success = StylePosition.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "sticky");
    }
}
