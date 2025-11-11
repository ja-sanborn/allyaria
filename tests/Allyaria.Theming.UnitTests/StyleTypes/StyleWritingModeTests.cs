namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleWritingModeTests
{
    [Theory]
    [InlineData(StyleWritingMode.Kind.HorizontalTb, "horizontal-tb")]
    [InlineData(StyleWritingMode.Kind.SidewaysLr, "sideways-lr")]
    [InlineData(StyleWritingMode.Kind.SidewaysRl, "sideways-rl")]
    [InlineData(StyleWritingMode.Kind.VerticalLr, "vertical-lr")]
    [InlineData(StyleWritingMode.Kind.VerticalRl, "vertical-rl")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleWritingMode.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleWritingMode(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "sideways-rl";

        // Act
        StyleWritingMode sut = input;

        // Assert
        sut.Value.Should().Be(expected: "sideways-rl");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-mode";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleWritingMode sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-mode");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleWritingMode? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleWritingMode(kind: StyleWritingMode.Kind.VerticalRl);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "vertical-rl");
    }

    [Theory]
    [InlineData("horizontal-tb", StyleWritingMode.Kind.HorizontalTb)]
    [InlineData("sideways-lr", StyleWritingMode.Kind.SidewaysLr)]
    [InlineData("sideways-rl", StyleWritingMode.Kind.SidewaysRl)]
    [InlineData("vertical-lr", StyleWritingMode.Kind.VerticalLr)]
    [InlineData("vertical-rl", StyleWritingMode.Kind.VerticalRl)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleWritingMode.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleWritingMode.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleWritingMode(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-writing-mode";

        // Act
        var act = () => StyleWritingMode.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-writing-mode");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleWritingMode.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-writing-mode";

        // Act
        var success = StyleWritingMode.TryParse(value: invalid, result: out var sut);

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
        var success = StyleWritingMode.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "vertical-lr";

        // Act
        var success = StyleWritingMode.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "vertical-lr");
    }
}
