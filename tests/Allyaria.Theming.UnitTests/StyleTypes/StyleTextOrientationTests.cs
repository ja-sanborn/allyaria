namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTextOrientationTests
{
    [Theory]
    [InlineData(StyleTextOrientation.Kind.Mixed, "mixed")]
    [InlineData(StyleTextOrientation.Kind.Sideways, "sideways")]
    [InlineData(StyleTextOrientation.Kind.Upright, "upright")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleTextOrientation.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleTextOrientation(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "sideways";

        // Act
        StyleTextOrientation sut = input;

        // Assert
        sut.Value.Should().Be(expected: "sideways");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-text-orientation";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleTextOrientation sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-text-orientation");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleTextOrientation? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleTextOrientation(kind: StyleTextOrientation.Kind.Mixed);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "mixed");
    }

    [Theory]
    [InlineData("mixed", StyleTextOrientation.Kind.Mixed)]
    [InlineData("sideways", StyleTextOrientation.Kind.Sideways)]
    [InlineData("upright", StyleTextOrientation.Kind.Upright)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleTextOrientation.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleTextOrientation.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleTextOrientation(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-orientation";

        // Act
        var act = () => StyleTextOrientation.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-orientation");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleTextOrientation.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "unknown-orientation";

        // Act
        var success = StyleTextOrientation.TryParse(value: invalid, result: out var sut);

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
        var success = StyleTextOrientation.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "upright";

        // Act
        var success = StyleTextOrientation.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "upright");
    }
}
