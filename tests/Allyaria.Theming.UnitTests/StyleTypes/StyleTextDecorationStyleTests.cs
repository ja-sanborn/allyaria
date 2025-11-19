namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTextDecorationStyleTests
{
    [Theory]
    [InlineData(StyleTextDecorationStyle.Kind.Dashed, "dashed")]
    [InlineData(StyleTextDecorationStyle.Kind.Dotted, "dotted")]
    [InlineData(StyleTextDecorationStyle.Kind.Double, "double")]
    [InlineData(StyleTextDecorationStyle.Kind.Solid, "solid")]
    [InlineData(StyleTextDecorationStyle.Kind.Wavy, "wavy")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleTextDecorationStyle.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleTextDecorationStyle(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "wavy";

        // Act
        StyleTextDecorationStyle sut = input;

        // Assert
        sut.Value.Should().Be(expected: "wavy");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-style-value";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleTextDecorationStyle sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-style-value");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleTextDecorationStyle? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleTextDecorationStyle(kind: StyleTextDecorationStyle.Kind.Dotted);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "dotted");
    }

    [Theory]
    [InlineData("dashed", StyleTextDecorationStyle.Kind.Dashed)]
    [InlineData("dotted", StyleTextDecorationStyle.Kind.Dotted)]
    [InlineData("double", StyleTextDecorationStyle.Kind.Double)]
    [InlineData("solid", StyleTextDecorationStyle.Kind.Solid)]
    [InlineData("wavy", StyleTextDecorationStyle.Kind.Wavy)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleTextDecorationStyle.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleTextDecorationStyle.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleTextDecorationStyle(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-style";

        // Act
        var act = () => StyleTextDecorationStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-style");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleTextDecorationStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-decoration-style";

        // Act
        var success = StyleTextDecorationStyle.TryParse(value: invalid, result: out var sut);

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
        var success = StyleTextDecorationStyle.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "solid";

        // Act
        var success = StyleTextDecorationStyle.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "solid");
    }
}
