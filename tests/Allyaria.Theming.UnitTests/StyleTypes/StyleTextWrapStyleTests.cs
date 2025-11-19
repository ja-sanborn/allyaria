namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTextWrapStyleTests
{
    [Theory]
    [InlineData(StyleTextWrapStyle.Kind.Auto, "auto")]
    [InlineData(StyleTextWrapStyle.Kind.Balance, "balance")]
    [InlineData(StyleTextWrapStyle.Kind.Pretty, "pretty")]
    [InlineData(StyleTextWrapStyle.Kind.Stable, "stable")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleTextWrapStyle.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleTextWrapStyle(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "pretty";

        // Act
        StyleTextWrapStyle sut = input;

        // Assert
        sut.Value.Should().Be(expected: "pretty");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-text-wrap";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleTextWrapStyle sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-text-wrap");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleTextWrapStyle? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleTextWrapStyle(kind: StyleTextWrapStyle.Kind.Stable);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "stable");
    }

    [Theory]
    [InlineData("auto", StyleTextWrapStyle.Kind.Auto)]
    [InlineData("balance", StyleTextWrapStyle.Kind.Balance)]
    [InlineData("pretty", StyleTextWrapStyle.Kind.Pretty)]
    [InlineData("stable", StyleTextWrapStyle.Kind.Stable)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleTextWrapStyle.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleTextWrapStyle.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleTextWrapStyle(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-wrap-style";

        // Act
        var act = () => StyleTextWrapStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-wrap-style");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleTextWrapStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-wrap-style";

        // Act
        var success = StyleTextWrapStyle.TryParse(value: invalid, result: out var sut);

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
        var success = StyleTextWrapStyle.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "balance";

        // Act
        var success = StyleTextWrapStyle.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "balance");
    }
}
