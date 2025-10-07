namespace Allyaria.Theming.UnitTests.Values;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryStringValueTests
{
    [Theory]
    [InlineData("hello", "hello")]
    [InlineData("  hello  ", "hello")]
    [InlineData("   spaced   ", "spaced")]
    public void Constructor_Should_StoreNormalizedValue_When_InputIsValid(string input, string expected)
    {
        // Arrange & Act
        var sut = new AryStringValue(input);

        // Assert
        sut.Value.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_ThrowArgumentException_When_InputIsNullOrWhitespace(string? invalid)
    {
        // Arrange
        var act = () => new AryStringValue(invalid!);

        // Act & Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*cannot be null, empty or whitespace*");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateInstance_When_InputIsValid()
    {
        // Arrange
        AryStringValue sut = "  hello  ";

        // Act & Assert
        sut.Value.Should()
            .Be("hello");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ImplicitConversionFromString_Should_ThrowArgumentException_When_InputIsInvalid(string? invalid)
    {
        // Arrange
        var act = () =>
        {
            AryStringValue _ = invalid!;
        };

        // Act & Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*cannot be null, empty or whitespace*");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsValid()
    {
        // Arrange
        var sut = new AryStringValue("  hello  ");

        // Act
        string result = sut;

        // Assert
        result.Should()
            .Be("hello");
    }

    [Theory]
    [InlineData("abc", "abc")]
    [InlineData("   abc   ", "abc")]
    public void Parse_Should_ReturnNormalizedValue_When_InputIsValid(string input, string expected)
    {
        // Arrange & Act
        var result = AryStringValue.Parse(input);

        // Assert
        result.Value.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Parse_Should_ThrowArgumentException_When_InputIsInvalid(string? invalid)
    {
        // Arrange
        var act = () => AryStringValue.Parse(invalid!);

        // Act & Assert
        act.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*cannot be null, empty or whitespace*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void TryParse_Should_ReturnFalseAndNullResult_When_InputIsInvalid(string? invalid)
    {
        // Act
        var success = AryStringValue.TryParse(invalid!, out var result);

        // Assert
        success.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_InputIsValid()
    {
        // Arrange
        var input = "  something  ";

        // Act
        var success = AryStringValue.TryParse(input, out var result);

        // Assert
        success.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        result.Value.Should()
            .Be("something");
    }

    [Fact]
    public void ValidateInput_Should_Throw_When_Value_Has_Disallowed_Control_Characters_Invoked()
    {
        // Arrange + Act
        var sut = () => new AryStringValue($"ok{Environment.NewLine}bad");

        // Assert
        sut.Should()
            .Throw<AryArgumentException>()
            .WithMessage("*Value contains control characters.*");
    }
}
