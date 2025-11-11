namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleBorderOutlineStyleTests
{
    [Theory]
    [InlineData(StyleBorderOutlineStyle.Kind.Dashed, "dashed")]
    [InlineData(StyleBorderOutlineStyle.Kind.Dotted, "dotted")]
    [InlineData(StyleBorderOutlineStyle.Kind.Double, "double")]
    [InlineData(StyleBorderOutlineStyle.Kind.Groove, "groove")]
    [InlineData(StyleBorderOutlineStyle.Kind.Inset, "inset")]
    [InlineData(StyleBorderOutlineStyle.Kind.None, "none")]
    [InlineData(StyleBorderOutlineStyle.Kind.Outset, "outset")]
    [InlineData(StyleBorderOutlineStyle.Kind.Ridge, "ridge")]
    [InlineData(StyleBorderOutlineStyle.Kind.Solid, "solid")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleBorderOutlineStyle.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleBorderOutlineStyle(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ReturnValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "dotted";

        // Act
        StyleBorderOutlineStyle sut = input;

        // Assert
        sut.Value.Should().Be(expected: "dotted");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-border-style";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleBorderOutlineStyle sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-border-style");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleBorderOutlineStyle? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleBorderOutlineStyle(kind: StyleBorderOutlineStyle.Kind.Groove);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "groove");
    }

    [Theory]
    [InlineData("dashed", StyleBorderOutlineStyle.Kind.Dashed)]
    [InlineData("dotted", StyleBorderOutlineStyle.Kind.Dotted)]
    [InlineData("double", StyleBorderOutlineStyle.Kind.Double)]
    [InlineData("groove", StyleBorderOutlineStyle.Kind.Groove)]
    [InlineData("inset", StyleBorderOutlineStyle.Kind.Inset)]
    [InlineData("none", StyleBorderOutlineStyle.Kind.None)]
    [InlineData("outset", StyleBorderOutlineStyle.Kind.Outset)]
    [InlineData("ridge", StyleBorderOutlineStyle.Kind.Ridge)]
    [InlineData("solid", StyleBorderOutlineStyle.Kind.Solid)]
    public void Parse_Should_ReturnStyleBorderOutlineStyle_When_ValidStringProvided(string input,
        StyleBorderOutlineStyle.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleBorderOutlineStyle.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleBorderOutlineStyle(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "fake-style";

        // Act
        var act = () => StyleBorderOutlineStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: fake-style");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleBorderOutlineStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-border-style";

        // Act
        var success = StyleBorderOutlineStyle.TryParse(value: invalid, result: out var sut);

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
        var success = StyleBorderOutlineStyle.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string valid = "solid";

        // Act
        var success = StyleBorderOutlineStyle.TryParse(value: valid, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: valid);
    }
}
