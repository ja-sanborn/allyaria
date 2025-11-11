namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTextAlignTests
{
    [Theory]
    [InlineData(StyleTextAlign.Kind.Center, "center")]
    [InlineData(StyleTextAlign.Kind.End, "end")]
    [InlineData(StyleTextAlign.Kind.Justify, "justify")]
    [InlineData(StyleTextAlign.Kind.MatchParent, "match-parent")]
    [InlineData(StyleTextAlign.Kind.Start, "start")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleTextAlign.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleTextAlign(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "center";

        // Act
        StyleTextAlign sut = input;

        // Assert
        sut.Value.Should().Be(expected: "center");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-align-value";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleTextAlign sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-align-value");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleTextAlign? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleTextAlign(kind: StyleTextAlign.Kind.End);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "end");
    }

    [Theory]
    [InlineData("center", StyleTextAlign.Kind.Center)]
    [InlineData("end", StyleTextAlign.Kind.End)]
    [InlineData("justify", StyleTextAlign.Kind.Justify)]
    [InlineData("match-parent", StyleTextAlign.Kind.MatchParent)]
    [InlineData("start", StyleTextAlign.Kind.Start)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleTextAlign.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleTextAlign.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleTextAlign(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-align";

        // Act
        var act = () => StyleTextAlign.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-align");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleTextAlign.Parse(value: invalid);

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
        var success = StyleTextAlign.TryParse(value: invalid, result: out var sut);

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
        var success = StyleTextAlign.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "justify";

        // Act
        var success = StyleTextAlign.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "justify");
    }
}
