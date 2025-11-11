namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleWhiteSpaceTests
{
    [Theory]
    [InlineData(StyleWhiteSpace.Kind.BreakSpaces, "break-spaces")]
    [InlineData(StyleWhiteSpace.Kind.Collapse, "collapse")]
    [InlineData(StyleWhiteSpace.Kind.Normal, "normal")]
    [InlineData(StyleWhiteSpace.Kind.Nowrap, "nowrap")]
    [InlineData(StyleWhiteSpace.Kind.Pre, "pre")]
    [InlineData(StyleWhiteSpace.Kind.PreLine, "pre-line")]
    [InlineData(StyleWhiteSpace.Kind.PreserveNowrap, "preserve nowrap")]
    [InlineData(StyleWhiteSpace.Kind.PreWrap, "pre-wrap")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleWhiteSpace.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleWhiteSpace(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "nowrap";

        // Act
        StyleWhiteSpace sut = input;

        // Assert
        sut.Value.Should().Be(expected: "nowrap");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-whitespace";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleWhiteSpace sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-whitespace");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleWhiteSpace? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleWhiteSpace(kind: StyleWhiteSpace.Kind.PreWrap);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "pre-wrap");
    }

    [Theory]
    [InlineData("break-spaces", StyleWhiteSpace.Kind.BreakSpaces)]
    [InlineData("collapse", StyleWhiteSpace.Kind.Collapse)]
    [InlineData("normal", StyleWhiteSpace.Kind.Normal)]
    [InlineData("nowrap", StyleWhiteSpace.Kind.Nowrap)]
    [InlineData("pre", StyleWhiteSpace.Kind.Pre)]
    [InlineData("pre-line", StyleWhiteSpace.Kind.PreLine)]
    [InlineData("preserve nowrap", StyleWhiteSpace.Kind.PreserveNowrap)]
    [InlineData("pre-wrap", StyleWhiteSpace.Kind.PreWrap)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleWhiteSpace.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleWhiteSpace.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleWhiteSpace(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-white-space";

        // Act
        var act = () => StyleWhiteSpace.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-white-space");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleWhiteSpace.Parse(value: invalid);

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
        var success = StyleWhiteSpace.TryParse(value: invalid, result: out var sut);

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
        var success = StyleWhiteSpace.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "pre-line";

        // Act
        var success = StyleWhiteSpace.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "pre-line");
    }
}
