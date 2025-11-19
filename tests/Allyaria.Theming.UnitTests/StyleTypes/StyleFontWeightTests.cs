namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleFontWeightTests
{
    [Theory]
    [InlineData(StyleFontWeight.Kind.Bold, "bold")]
    [InlineData(StyleFontWeight.Kind.Bolder, "bolder")]
    [InlineData(StyleFontWeight.Kind.Lighter, "lighter")]
    [InlineData(StyleFontWeight.Kind.Normal, "normal")]
    [InlineData(StyleFontWeight.Kind.Weight100, "100")]
    [InlineData(StyleFontWeight.Kind.Weight200, "200")]
    [InlineData(StyleFontWeight.Kind.Weight300, "300")]
    [InlineData(StyleFontWeight.Kind.Weight400, "400")]
    [InlineData(StyleFontWeight.Kind.Weight500, "500")]
    [InlineData(StyleFontWeight.Kind.Weight600, "600")]
    [InlineData(StyleFontWeight.Kind.Weight700, "700")]
    [InlineData(StyleFontWeight.Kind.Weight800, "800")]
    [InlineData(StyleFontWeight.Kind.Weight900, "900")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleFontWeight.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleFontWeight(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "700";

        // Act
        StyleFontWeight sut = input;

        // Assert
        sut.Value.Should().Be(expected: "700");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-weight";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleFontWeight sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-weight");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleFontWeight? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleFontWeight(kind: StyleFontWeight.Kind.Weight900);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "900");
    }

    [Theory]
    [InlineData("bold", StyleFontWeight.Kind.Bold)]
    [InlineData("bolder", StyleFontWeight.Kind.Bolder)]
    [InlineData("lighter", StyleFontWeight.Kind.Lighter)]
    [InlineData("normal", StyleFontWeight.Kind.Normal)]
    [InlineData("100", StyleFontWeight.Kind.Weight100)]
    [InlineData("200", StyleFontWeight.Kind.Weight200)]
    [InlineData("300", StyleFontWeight.Kind.Weight300)]
    [InlineData("400", StyleFontWeight.Kind.Weight400)]
    [InlineData("500", StyleFontWeight.Kind.Weight500)]
    [InlineData("600", StyleFontWeight.Kind.Weight600)]
    [InlineData("700", StyleFontWeight.Kind.Weight700)]
    [InlineData("800", StyleFontWeight.Kind.Weight800)]
    [InlineData("900", StyleFontWeight.Kind.Weight900)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleFontWeight.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleFontWeight.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleFontWeight(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-weight";

        // Act
        var act = () => StyleFontWeight.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: not-a-weight");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleFontWeight.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-weight";

        // Act
        var success = StyleFontWeight.TryParse(value: invalid, result: out var sut);

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
        var success = StyleFontWeight.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "bold";

        // Act
        var success = StyleFontWeight.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "bold");
    }
}
