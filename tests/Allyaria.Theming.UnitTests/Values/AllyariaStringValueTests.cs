using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaStringValueTests
{
    [Theory]
    [InlineData("hello", "hello")]
    [InlineData("  hello  ", "hello")]
    [InlineData("   spaced   ", "spaced")]
    public void Constructor_Should_StoreNormalizedValue_When_InputIsValid(string input, string expected)
    {
        // Arrange & Act
        var sut = new AllyariaStringValue(input);

        // Assert
        sut.Value.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_ThrowArgumentException_When_InputIsNullOrWhitespace(string invalid)
    {
        // Arrange
        var act = () => new AllyariaStringValue(invalid);

        // Act & Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateInstance_When_InputIsValid()
    {
        // Arrange
        AllyariaStringValue sut = "  hello  ";

        // Act & Assert
        sut.Value.Should()
            .Be("hello");
    }

    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData("   ")]
    public void ImplicitConversionFromString_Should_ThrowArgumentException_When_InputIsInvalid(string invalid)
    {
        // Arrange
        var act = () =>
        {
            AllyariaStringValue _ = invalid;
        };

        // Act & Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsValid()
    {
        // Arrange
        var sut = new AllyariaStringValue("  hello  ");

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
        var result = AllyariaStringValue.Parse(input);

        // Assert
        result.Value.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData("   ")]
    public void Parse_Should_ThrowArgumentException_When_InputIsInvalid(string invalid)
    {
        // Arrange
        var act = () => AllyariaStringValue.Parse(invalid);

        // Act & Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData("   ")]
    public void TryParse_Should_ReturnFalseAndNullResult_When_InputIsInvalid(string invalid)
    {
        // Act
        var success = AllyariaStringValue.TryParse(invalid, out var result);

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
        var success = AllyariaStringValue.TryParse(input, out var result);

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
        var sut = () => new AllyariaStringValue($"ok{Environment.NewLine}bad");

        // Assert
        sut.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Value contains control characters.*");
    }
}
