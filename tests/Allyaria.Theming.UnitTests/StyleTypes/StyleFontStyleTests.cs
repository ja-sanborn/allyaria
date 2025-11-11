namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleFontStyleTests
{
    [Theory]
    [InlineData(StyleFontStyle.Kind.Italic, "italic")]
    [InlineData(StyleFontStyle.Kind.Normal, "normal")]
    [InlineData(StyleFontStyle.Kind.Oblique, "oblique")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleFontStyle.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleFontStyle(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "italic";

        // Act
        StyleFontStyle sut = input;

        // Assert
        sut.Value.Should().Be(expected: "italic");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-font-style";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleFontStyle sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-font-style");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleFontStyle? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleFontStyle(kind: StyleFontStyle.Kind.Oblique);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "oblique");
    }

    [Theory]
    [InlineData("italic", StyleFontStyle.Kind.Italic)]
    [InlineData("normal", StyleFontStyle.Kind.Normal)]
    [InlineData("oblique", StyleFontStyle.Kind.Oblique)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleFontStyle.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleFontStyle.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleFontStyle(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-style";

        // Act
        var act = () => StyleFontStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: not-a-style");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleFontStyle.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "wrong-style";

        // Act
        var success = StyleFontStyle.TryParse(value: invalid, result: out var sut);

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
        var success = StyleFontStyle.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "normal";

        // Act
        var success = StyleFontStyle.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "normal");
    }
}
