namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleStringTests
{
    [Fact]
    public void Constructor_Should_SetValueToEmptyString_When_NoValueProvided()
    {
        // Arrange & Act
        var sut = new StyleString();

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void Constructor_Should_StoreEmptyString_When_ValueIsWhitespaceOnly()
    {
        // Arrange
        const string raw = "   ";

        // Act
        var sut = new StyleString(value: raw);

        // Assert
        sut.Value.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void Constructor_Should_TrimAndStoreValue_When_ValueHasSurroundingWhitespace()
    {
        // Arrange
        const string raw = "  some-value  ";

        // Act
        var sut = new StyleString(value: raw);

        // Assert
        sut.Value.Should().Be(expected: "some-value");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateStyleString_When_InputIsValid()
    {
        // Arrange
        const string raw = "implicit-value";

        // Act
        StyleString sut = raw;

        // Assert
        sut.Value.Should().Be(expected: "implicit-value");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_TrimValue_When_InputHasSurroundingWhitespace()
    {
        // Arrange
        const string raw = "  implicit-trim  ";

        // Act
        StyleString sut = raw;

        // Assert
        sut.Value.Should().Be(expected: "implicit-trim");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InputIsWhitespaceOnly()
    {
        // Arrange
        var sut = new StyleString(value: "   ");

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_StyleStringIsNull()
    {
        // Arrange
        StyleString? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_UnderlyingValueIsEmpty()
    {
        // Arrange
        var sut = new StyleString(value: string.Empty);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_ValueIsNonEmpty()
    {
        // Arrange
        var sut = new StyleString(value: "string-value");

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "string-value");
    }

    [Fact]
    public void Parse_Should_ReturnStyleStringWithTrimmedValue_When_InputIsValid()
    {
        // Arrange
        const string raw = "  parsed-value  ";

        // Act
        var sut = StyleString.Parse(value: raw);

        // Assert
        sut.Value.Should().Be(expected: "parsed-value");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueContainsControlCharacters()
    {
        // Arrange
        var invalid = "a\0b";

        // Act
        var act = () => StyleString.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNullResult_When_InputContainsControlCharacters()
    {
        // Arrange
        var invalid = "x\0y";

        // Act
        var success = StyleString.TryParse(value: invalid, result: out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_InputIsValid()
    {
        // Arrange
        const string raw = "try-parse-value";

        // Act
        var success = StyleString.TryParse(value: raw, result: out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().NotBeNull();
        result.Value.Should().Be(expected: "try-parse-value");
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndTrimmedResult_When_InputHasWhitespace()
    {
        // Arrange
        const string raw = "   spaced-value   ";

        // Act
        var success = StyleString.TryParse(value: raw, result: out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().NotBeNull();
        result.Value.Should().Be(expected: "spaced-value");
    }
}
