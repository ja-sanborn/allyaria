namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleOverscrollBehaviorTests
{
    [Theory]
    [InlineData(StyleOverscrollBehavior.Kind.Auto, "auto")]
    [InlineData(StyleOverscrollBehavior.Kind.Contain, "contain")]
    [InlineData(StyleOverscrollBehavior.Kind.None, "none")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleOverscrollBehavior.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleOverscrollBehavior(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "none";

        // Act
        StyleOverscrollBehavior sut = input;

        // Assert
        sut.Value.Should().Be(expected: "none");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-valid-behavior";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleOverscrollBehavior sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: not-a-valid-behavior");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleOverscrollBehavior? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleOverscrollBehavior(kind: StyleOverscrollBehavior.Kind.Contain);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "contain");
    }

    [Theory]
    [InlineData("auto", StyleOverscrollBehavior.Kind.Auto)]
    [InlineData("contain", StyleOverscrollBehavior.Kind.Contain)]
    [InlineData("none", StyleOverscrollBehavior.Kind.None)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleOverscrollBehavior.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleOverscrollBehavior.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleOverscrollBehavior(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-overscroll";

        // Act
        var act = () => StyleOverscrollBehavior.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-overscroll");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleOverscrollBehavior.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-overscroll";

        // Act
        var success = StyleOverscrollBehavior.TryParse(value: invalid, result: out var sut);

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
        var success = StyleOverscrollBehavior.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "contain";

        // Act
        var success = StyleOverscrollBehavior.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "contain");
    }
}
