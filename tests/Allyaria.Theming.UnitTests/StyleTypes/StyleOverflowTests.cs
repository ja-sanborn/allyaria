namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleOverflowTests
{
    [Theory]
    [InlineData(StyleOverflow.Kind.Auto, "auto")]
    [InlineData(StyleOverflow.Kind.Clip, "clip")]
    [InlineData(StyleOverflow.Kind.Hidden, "hidden")]
    [InlineData(StyleOverflow.Kind.Scroll, "scroll")]
    [InlineData(StyleOverflow.Kind.Visible, "visible")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleOverflow.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleOverflow(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "scroll";

        // Act
        StyleOverflow sut = input;

        // Assert
        sut.Value.Should().Be(expected: "scroll");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "unknown-overflow";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleOverflow sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: unknown-overflow");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleOverflow? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleOverflow(kind: StyleOverflow.Kind.Visible);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "visible");
    }

    [Theory]
    [InlineData("auto", StyleOverflow.Kind.Auto)]
    [InlineData("clip", StyleOverflow.Kind.Clip)]
    [InlineData("hidden", StyleOverflow.Kind.Hidden)]
    [InlineData("scroll", StyleOverflow.Kind.Scroll)]
    [InlineData("visible", StyleOverflow.Kind.Visible)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleOverflow.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleOverflow.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleOverflow(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-overflow";

        // Act
        var act = () => StyleOverflow.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-overflow");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleOverflow.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-style";

        // Act
        var success = StyleOverflow.TryParse(value: invalid, result: out var sut);

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
        var success = StyleOverflow.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "hidden";

        // Act
        var success = StyleOverflow.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "hidden");
    }
}
