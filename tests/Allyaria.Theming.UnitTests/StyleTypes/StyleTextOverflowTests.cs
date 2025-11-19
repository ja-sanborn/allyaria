namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTextOverflowTests
{
    [Theory]
    [InlineData(StyleTextOverflow.Kind.Clip, "clip")]
    [InlineData(StyleTextOverflow.Kind.Ellipsis, "ellipsis")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleTextOverflow.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleTextOverflow(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "clip";

        // Act
        StyleTextOverflow sut = input;

        // Assert
        sut.Value.Should().Be(expected: "clip");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-overflow-value";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleTextOverflow sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-overflow-value");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleTextOverflow? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleTextOverflow(kind: StyleTextOverflow.Kind.Ellipsis);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "ellipsis");
    }

    [Theory]
    [InlineData("clip", StyleTextOverflow.Kind.Clip)]
    [InlineData("ellipsis", StyleTextOverflow.Kind.Ellipsis)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleTextOverflow.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleTextOverflow.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleTextOverflow(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-overflow";

        // Act
        var act = () => StyleTextOverflow.Parse(value: invalid);

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
        var act = () => StyleTextOverflow.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-overflow";

        // Act
        var success = StyleTextOverflow.TryParse(value: invalid, result: out var sut);

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
        var success = StyleTextOverflow.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "ellipsis";

        // Act
        var success = StyleTextOverflow.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "ellipsis");
    }
}
