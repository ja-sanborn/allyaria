namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleScrollBehaviorTests
{
    [Theory]
    [InlineData(StyleScrollBehavior.Kind.Auto, "auto")]
    [InlineData(StyleScrollBehavior.Kind.Smooth, "smooth")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleScrollBehavior.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleScrollBehavior(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "auto";

        // Act
        StyleScrollBehavior sut = input;

        // Assert
        sut.Value.Should().Be(expected: "auto");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-scroll-value";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleScrollBehavior sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-scroll-value");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleScrollBehavior? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleScrollBehavior(kind: StyleScrollBehavior.Kind.Smooth);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "smooth");
    }

    [Theory]
    [InlineData("auto", StyleScrollBehavior.Kind.Auto)]
    [InlineData("smooth", StyleScrollBehavior.Kind.Smooth)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleScrollBehavior.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleScrollBehavior.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleScrollBehavior(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-scroll";

        // Act
        var act = () => StyleScrollBehavior.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-scroll");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleScrollBehavior.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-scroll-behavior";

        // Act
        var success = StyleScrollBehavior.TryParse(value: invalid, result: out var sut);

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
        var success = StyleScrollBehavior.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "smooth";

        // Act
        var success = StyleScrollBehavior.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "smooth");
    }
}
