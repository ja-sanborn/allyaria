namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleLineBreakTests
{
    [Theory]
    [InlineData(StyleLineBreak.Kind.Anywhere, "anywhere")]
    [InlineData(StyleLineBreak.Kind.Auto, "auto")]
    [InlineData(StyleLineBreak.Kind.Loose, "loose")]
    [InlineData(StyleLineBreak.Kind.Normal, "normal")]
    [InlineData(StyleLineBreak.Kind.Strict, "strict")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleLineBreak.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleLineBreak(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "loose";

        // Act
        StyleLineBreak sut = input;

        // Assert
        sut.Value.Should().Be(expected: "loose");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-line-break";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleLineBreak sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-line-break");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleLineBreak? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleLineBreak(kind: StyleLineBreak.Kind.Strict);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "strict");
    }

    [Theory]
    [InlineData("anywhere", StyleLineBreak.Kind.Anywhere)]
    [InlineData("auto", StyleLineBreak.Kind.Auto)]
    [InlineData("loose", StyleLineBreak.Kind.Loose)]
    [InlineData("normal", StyleLineBreak.Kind.Normal)]
    [InlineData("strict", StyleLineBreak.Kind.Strict)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleLineBreak.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleLineBreak.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleLineBreak(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-linebreak";

        // Act
        var act = () => StyleLineBreak.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: invalid-linebreak");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleLineBreak.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-value";

        // Act
        var success = StyleLineBreak.TryParse(value: invalid, result: out var sut);

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
        var success = StyleLineBreak.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "auto";

        // Act
        var success = StyleLineBreak.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "auto");
    }
}
