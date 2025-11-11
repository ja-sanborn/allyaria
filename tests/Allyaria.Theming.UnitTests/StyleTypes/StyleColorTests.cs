namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleColorTests
{
    [Fact]
    public void Constructor_Should_CreateInstance_When_HexColorObjectProvided()
    {
        // Arrange
        var hex = new HexColor(value: "#123456");

        // Act
        var sut = new StyleColor(color: hex);

        // Assert
        sut.Color.Should().BeEquivalentTo(expectation: hex);
        sut.Value.Should().Be(expected: hex.ToString());
    }

    [Fact]
    public void Constructor_Should_SetValue_When_ValidNamedColorProvided()
    {
        // Arrange
        const string input = "red";

        // Act
        var sut = new StyleColor(value: input);

        // Assert
        sut.Value.Should().Be(expected: "#FF0000FF"); // normalized ARGB hex
        sut.Color.ToString().Should().Be(expected: "#FF0000FF");
    }

    [Fact]
    public void Constructor_Should_SetValueAndColor_When_ValidHexColorProvided()
    {
        // Arrange
        const string input = "#FFAA33";

        // Act
        var sut = new StyleColor(value: input);

        // Assert
        sut.Value.Should().Be(expected: "#FFAA33FF");
        sut.Color.ToString().Should().Be(expected: "#FFAA33FF"); // HexColor normalizes to 8-digit ARGB
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentException_When_InvalidColorProvided()
    {
        // Arrange
        const string invalid = "notacolor";

        // Act
        var act = () => new StyleColor(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid color string: notacolor.");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_Recognized()
    {
        // Arrange
        const string input = "#ABCDEF";

        // Act
        StyleColor sut = input;

        // Assert
        sut.Value.Should().Be(expected: "#ABCDEFFF");
        sut.Color.ToString().Should().Be(expected: "#ABCDEFFF");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowArgumentException_When_Invalid()
    {
        // Arrange
        const string invalid = "bad-color";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleColor sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid color string: bad-color*");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleColor? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleColor(value: "#123456");

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "#123456FF");
    }

    [Fact]
    public void Parse_Should_ReturnExpectedInstance_When_ValidColorProvided()
    {
        // Arrange
        const string input = "#FFFFFF";

        // Act
        var sut = StyleColor.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: "#FFFFFFFF");
        sut.Color.ToString().Should().Be(expected: "#FFFFFFFF");
    }

    [Fact]
    public void Parse_Should_ThrowArgumentException_When_InvalidColorProvided()
    {
        // Arrange
        const string invalid = "??";

        // Act
        var act = () => StyleColor.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid color string: ??.");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_NullProvided()
    {
        // Arrange
        string? input = null;

        // Act
        var act = () => StyleColor.Parse(value: input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "value cannot be null, empty, or whitespace.");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_InvalidColorProvided()
    {
        // Arrange
        const string invalid = "not-a-color";

        // Act
        var success = StyleColor.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValidColorProvided()
    {
        // Arrange
        const string input = "#00FF00";

        // Act
        var success = StyleColor.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "#00FF00FF");
        sut.Color.ToString().Should().Be(expected: "#00FF00FF");
    }
}
