namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleOverflowWrapTests
{
    [Theory]
    [InlineData(StyleOverflowWrap.Kind.Anywhere, "anywhere")]
    [InlineData(StyleOverflowWrap.Kind.BreakWord, "break-word")]
    [InlineData(StyleOverflowWrap.Kind.Normal, "normal")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleOverflowWrap.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleOverflowWrap(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "anywhere";

        // Act
        StyleOverflowWrap sut = input;

        // Assert
        sut.Value.Should().Be(expected: "anywhere");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-wrap";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleOverflowWrap sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-wrap");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleOverflowWrap? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleOverflowWrap(kind: StyleOverflowWrap.Kind.Normal);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "normal");
    }

    [Theory]
    [InlineData("anywhere", StyleOverflowWrap.Kind.Anywhere)]
    [InlineData("break-word", StyleOverflowWrap.Kind.BreakWord)]
    [InlineData("normal", StyleOverflowWrap.Kind.Normal)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleOverflowWrap.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleOverflowWrap.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleOverflowWrap(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-wrap";

        // Act
        var act = () => StyleOverflowWrap.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-wrap");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleOverflowWrap.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-overflow-wrap";

        // Act
        var success = StyleOverflowWrap.TryParse(value: invalid, result: out var sut);

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
        var success = StyleOverflowWrap.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "break-word";

        // Act
        var success = StyleOverflowWrap.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "break-word");
    }
}
