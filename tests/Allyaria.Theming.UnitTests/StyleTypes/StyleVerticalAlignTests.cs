namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleVerticalAlignTests
{
    [Theory]
    [InlineData(StyleVerticalAlign.Kind.Baseline, "baseline")]
    [InlineData(StyleVerticalAlign.Kind.Bottom, "bottom")]
    [InlineData(StyleVerticalAlign.Kind.Middle, "middle")]
    [InlineData(StyleVerticalAlign.Kind.Sub, "sub")]
    [InlineData(StyleVerticalAlign.Kind.Super, "super")]
    [InlineData(StyleVerticalAlign.Kind.TextBottom, "text-bottom")]
    [InlineData(StyleVerticalAlign.Kind.TextTop, "text-top")]
    [InlineData(StyleVerticalAlign.Kind.Top, "top")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleVerticalAlign.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleVerticalAlign(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "super";

        // Act
        StyleVerticalAlign sut = input;

        // Assert
        sut.Value.Should().Be(expected: "super");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-align";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleVerticalAlign sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-align");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleVerticalAlign? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleVerticalAlign(kind: StyleVerticalAlign.Kind.TextTop);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "text-top");
    }

    [Theory]
    [InlineData("baseline", StyleVerticalAlign.Kind.Baseline)]
    [InlineData("bottom", StyleVerticalAlign.Kind.Bottom)]
    [InlineData("middle", StyleVerticalAlign.Kind.Middle)]
    [InlineData("sub", StyleVerticalAlign.Kind.Sub)]
    [InlineData("super", StyleVerticalAlign.Kind.Super)]
    [InlineData("text-bottom", StyleVerticalAlign.Kind.TextBottom)]
    [InlineData("text-top", StyleVerticalAlign.Kind.TextTop)]
    [InlineData("top", StyleVerticalAlign.Kind.Top)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleVerticalAlign.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleVerticalAlign.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleVerticalAlign(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-vertical-align";

        // Act
        var act = () => StyleVerticalAlign.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-vertical-align");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleVerticalAlign.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-align";

        // Act
        var success = StyleVerticalAlign.TryParse(value: invalid, result: out var sut);

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
        var success = StyleVerticalAlign.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "middle";

        // Act
        var success = StyleVerticalAlign.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "middle");
    }
}
