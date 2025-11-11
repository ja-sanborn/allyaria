namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleWordBreakTests
{
    [Theory]
    [InlineData(StyleWordBreak.Kind.BreakAll, "break-all")]
    [InlineData(StyleWordBreak.Kind.BreakWord, "break-word")]
    [InlineData(StyleWordBreak.Kind.KeepAll, "keep-all")]
    [InlineData(StyleWordBreak.Kind.Normal, "normal")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleWordBreak.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleWordBreak(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "keep-all";

        // Act
        StyleWordBreak sut = input;

        // Assert
        sut.Value.Should().Be(expected: "keep-all");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-break-style";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleWordBreak sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: invalid-break-style");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleWordBreak? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleWordBreak(kind: StyleWordBreak.Kind.BreakAll);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "break-all");
    }

    [Theory]
    [InlineData("break-all", StyleWordBreak.Kind.BreakAll)]
    [InlineData("break-word", StyleWordBreak.Kind.BreakWord)]
    [InlineData("keep-all", StyleWordBreak.Kind.KeepAll)]
    [InlineData("normal", StyleWordBreak.Kind.Normal)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleWordBreak.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleWordBreak.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleWordBreak(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-word-break";

        // Act
        var act = () => StyleWordBreak.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: invalid-word-break");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleWordBreak.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-word-break";

        // Act
        var success = StyleWordBreak.TryParse(value: invalid, result: out var sut);

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
        var success = StyleWordBreak.TryParse(value: invalid, result: out var sut);

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
        var success = StyleWordBreak.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "break-word");
    }
}
