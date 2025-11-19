namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTextTransformTests
{
    [Theory]
    [InlineData(StyleTextTransform.Kind.Capitalize, "capitalize")]
    [InlineData(StyleTextTransform.Kind.Lowercase, "lowercase")]
    [InlineData(StyleTextTransform.Kind.None, "none")]
    [InlineData(StyleTextTransform.Kind.Uppercase, "uppercase")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleTextTransform.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleTextTransform(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "capitalize";

        // Act
        StyleTextTransform sut = input;

        // Assert
        sut.Value.Should().Be(expected: "capitalize");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-transform-style";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleTextTransform sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-transform-style");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleTextTransform? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleTextTransform(kind: StyleTextTransform.Kind.Lowercase);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "lowercase");
    }

    [Theory]
    [InlineData("capitalize", StyleTextTransform.Kind.Capitalize)]
    [InlineData("lowercase", StyleTextTransform.Kind.Lowercase)]
    [InlineData("none", StyleTextTransform.Kind.None)]
    [InlineData("uppercase", StyleTextTransform.Kind.Uppercase)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleTextTransform.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleTextTransform.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleTextTransform(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-transform";

        // Act
        var act = () => StyleTextTransform.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-transform");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleTextTransform.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-transform";

        // Act
        var success = StyleTextTransform.TryParse(value: invalid, result: out var sut);

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
        var success = StyleTextTransform.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "uppercase";

        // Act
        var success = StyleTextTransform.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "uppercase");
    }
}
