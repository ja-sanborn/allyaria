namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleHyphensTests
{
    [Theory]
    [InlineData(StyleHyphens.Kind.Auto, "auto")]
    [InlineData(StyleHyphens.Kind.Manual, "manual")]
    [InlineData(StyleHyphens.Kind.None, "none")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleHyphens.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleHyphens(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "auto";

        // Act
        StyleHyphens sut = input;

        // Assert
        sut.Value.Should().Be(expected: "auto");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-style";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleHyphens sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-style");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleHyphens? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleHyphens(kind: StyleHyphens.Kind.None);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "none");
    }

    [Theory]
    [InlineData("auto", StyleHyphens.Kind.Auto)]
    [InlineData("manual", StyleHyphens.Kind.Manual)]
    [InlineData("none", StyleHyphens.Kind.None)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleHyphens.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleHyphens.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleHyphens(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-hyphen";

        // Act
        var act = () => StyleHyphens.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-hyphen");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleHyphens.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-hyphen";

        // Act
        var success = StyleHyphens.TryParse(value: invalid, result: out var sut);

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
        var success = StyleHyphens.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "manual";

        // Act
        var success = StyleHyphens.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "manual");
    }
}
