namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTextDecorationLineTests
{
    [Theory]
    [InlineData(StyleTextDecorationLine.Kind.All, "overline line-through underline")]
    [InlineData(StyleTextDecorationLine.Kind.LineThrough, "line-through")]
    [InlineData(StyleTextDecorationLine.Kind.None, "none")]
    [InlineData(StyleTextDecorationLine.Kind.Overline, "overline")]
    [InlineData(StyleTextDecorationLine.Kind.OverlineLineThrough, "overline line-through")]
    [InlineData(StyleTextDecorationLine.Kind.OverlineUnderline, "overline underline")]
    [InlineData(StyleTextDecorationLine.Kind.Underline, "underline")]
    [InlineData(StyleTextDecorationLine.Kind.UnderlineLineThrough, "underline line-through")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleTextDecorationLine.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleTextDecorationLine(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "overline underline";

        // Act
        StyleTextDecorationLine sut = input;

        // Assert
        sut.Value.Should().Be(expected: "overline underline");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-decoration-style";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleTextDecorationLine sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-decoration-style");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleTextDecorationLine? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleTextDecorationLine(kind: StyleTextDecorationLine.Kind.OverlineUnderline);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "overline underline");
    }

    [Theory]
    [InlineData("overline line-through underline", StyleTextDecorationLine.Kind.All)]
    [InlineData("line-through", StyleTextDecorationLine.Kind.LineThrough)]
    [InlineData("none", StyleTextDecorationLine.Kind.None)]
    [InlineData("overline", StyleTextDecorationLine.Kind.Overline)]
    [InlineData("overline line-through", StyleTextDecorationLine.Kind.OverlineLineThrough)]
    [InlineData("overline underline", StyleTextDecorationLine.Kind.OverlineUnderline)]
    [InlineData("underline", StyleTextDecorationLine.Kind.Underline)]
    [InlineData("underline line-through", StyleTextDecorationLine.Kind.UnderlineLineThrough)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleTextDecorationLine.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleTextDecorationLine.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleTextDecorationLine(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-decoration";

        // Act
        var act = () => StyleTextDecorationLine.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: invalid-decoration");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleTextDecorationLine.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-decoration";

        // Act
        var success = StyleTextDecorationLine.TryParse(value: invalid, result: out var sut);

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
        var success = StyleTextDecorationLine.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "underline line-through";

        // Act
        var success = StyleTextDecorationLine.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "underline line-through");
    }
}
