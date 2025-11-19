namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleBoxSizingTests
{
    [Theory]
    [InlineData(StyleBoxSizing.Kind.BorderBox, "border-box")]
    [InlineData(StyleBoxSizing.Kind.ContentBox, "content-box")]
    [InlineData(StyleBoxSizing.Kind.Inherit, "inherit")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleBoxSizing.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleBoxSizing(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "content-box";

        // Act
        StyleBoxSizing sut = input;

        // Assert
        sut.Value.Should().Be(expected: "content-box");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "weird-sizing";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleBoxSizing sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: weird-sizing");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleBoxSizing? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleBoxSizing(kind: StyleBoxSizing.Kind.ContentBox);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "content-box");
    }

    [Theory]
    [InlineData("border-box", StyleBoxSizing.Kind.BorderBox)]
    [InlineData("content-box", StyleBoxSizing.Kind.ContentBox)]
    [InlineData("inherit", StyleBoxSizing.Kind.Inherit)]
    public void Parse_Should_ReturnExpectedStyleBoxSizing_When_ValueIsValid(string input,
        StyleBoxSizing.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleBoxSizing.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleBoxSizing(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-sizing";

        // Act
        var act = () => StyleBoxSizing.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: invalid-sizing");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleBoxSizing.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-box";

        // Act
        var success = StyleBoxSizing.TryParse(value: invalid, result: out var sut);

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
        var success = StyleBoxSizing.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "border-box";

        // Act
        var success = StyleBoxSizing.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "border-box");
    }
}
