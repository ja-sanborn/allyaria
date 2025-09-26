using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaFunctionValueTests
{
    [Fact]
    public void Ctor_Should_AcceptLongestInnerUntilLastParen_When_ExtraInnerParensExist()
    {
        // Arrange
        var input = "min(max(1,(2)), (3 + (4)))";

        // Act
        var sut = new AllyariaFunctionValue(input);
        string actual = sut;

        // Assert
        actual.Should()
            .Be("min(max(1,(2)), (3 + (4)))");
    }

    [Fact]
    public void Ctor_Should_AllowNestedParentheses_When_InnerContainsFunctions()
    {
        // Arrange
        var input = "rgb(calc(1+2), 0, max(0, 1))";

        // Act
        var sut = new AllyariaFunctionValue(input);
        string actual = sut;

        // Assert
        actual.Should()
            .Be("rgb(calc(1+2), 0, max(0, 1))");
    }

    [Fact]
    public void Ctor_Should_HandleFunctionNamesWithDashes_When_CaseInsensitive()
    {
        // Arrange
        var input = "RePeAtInG-RaDiAl-GrAdIeNt(circle, red, blue)";

        // Act
        var sut = new AllyariaFunctionValue(input);
        string actual = sut;

        // Assert
        actual.Should()
            .Be("repeating-radial-gradient(circle, red, blue)");
    }

    [Fact]
    public void Ctor_Should_NormalizeCaseInsensitiveNameToLower_When_MixedCasingProvided()
    {
        // Arrange
        var input = "RGB(255 0 0)";

        // Act
        var sut = new AllyariaFunctionValue(input);
        string actual = sut; // implicit AllyariaFunctionValue -> string

        // Assert
        actual.Should()
            .Be("rgb(255 0 0)");
    }

    [Fact]
    public void Ctor_Should_PreserveCaseSensitiveName_When_ExactCasingProvided()
    {
        // Arrange
        var input = "rotateX(45deg)";

        // Act
        var sut = new AllyariaFunctionValue(input);
        string actual = sut;

        // Assert
        actual.Should()
            .Be("rotateX(45deg)");
    }

    [Fact]
    public void Ctor_Should_ThrowFormatException_When_CaseSensitiveNameCasingIsWrong()
    {
        // Arrange
        var input = "rotatex(45deg)";

        // Act
        var act = () => new AllyariaFunctionValue(input);

        // Assert
        act.Should()
            .Throw<FormatException>()
            .WithMessage("Unable to parse CSS function expression.");
    }

    [Theory]
    [InlineData("rgb(10 20 30")]
    [InlineData("rgb10 20 30)")]
    [InlineData("()(1)")]
    [InlineData("rgb()")]
    [InlineData("unknown(1 2 3)")]
    [InlineData("rgb)1 2 3)")]
    public void Ctor_Should_ThrowFormatException_When_InputIsMalformed(string input)
    {
        // Arrange & Act
        var act = () => new AllyariaFunctionValue(input);

        // Assert
        act.Should()
            .Throw<FormatException>()
            .WithMessage("Unable to parse CSS function expression.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_Should_ThrowFormatException_When_ValueIsEmptyOrWhitespace(string input)
    {
        // Arrange & Act
        var act = () => new AllyariaFunctionValue(input);

        // Assert
        act.Should()
            .Throw<FormatException>()
            .WithMessage("Value is null or whitespace.");
    }

    [Fact]
    public void Ctor_Should_ThrowFormatException_When_ValueIsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var act = () => new AllyariaFunctionValue(input!);

        // Assert
        act.Should()
            .Throw<FormatException>()
            .WithMessage("Value is null or whitespace.");
    }

    [Fact]
    public void Ctor_Should_TrimNameAndInner_When_SpacesAroundDelimiters()
    {
        // Arrange
        var input = "   calc   (   1rem + 2px   )   ";

        // Act
        var sut = new AllyariaFunctionValue(input);
        string actual = sut;

        // Assert
        actual.Should()
            .Be("calc(1rem + 2px)");
    }

    [Fact]
    public void Ctor_Should_TrimOuterWhitespace_And_InnerWhitespaceEdges_When_SurroundedBySpaces()
    {
        // Arrange
        var input = "   rgb(   10  20  30   )   ";

        // Act
        var sut = new AllyariaFunctionValue(input);
        string actual = sut;

        // Assert
        actual.Should()
            .Be("rgb(10  20  30)");
    }

    [Fact]
    public void Implicit_FromString_Should_ConstructAndNormalize_When_ValidInput()
    {
        // Arrange
        AllyariaFunctionValue sut = "Color-Mix(in srgb, red 50%, blue)";

        // Act
        string actual = sut;

        // Assert
        actual.Should()
            .Be("color-mix(in srgb, red 50%, blue)");
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnNormalizedValue_When_InstanceCreated()
    {
        // Arrange
        var sut = new AllyariaFunctionValue("rotateY(30deg)");

        // Act
        string actual = sut;

        // Assert
        actual.Should()
            .Be("rotateY(30deg)");
    }

    [Fact]
    public void Parse_Should_ReturnInstance_When_InputIsValid()
    {
        // Arrange
        var input = "HSL(200 30% 40%)";

        // Act
        var sut = AllyariaFunctionValue.Parse(input);
        string actual = sut;

        // Assert
        actual.Should()
            .Be("hsl(200 30% 40%)");
    }

    [Fact]
    public void Parse_Should_ThrowFormatException_When_InputIsInvalid()
    {
        // Arrange
        var input = "nope(1 2 3)";

        // Act
        var act = () => AllyariaFunctionValue.Parse(input);

        // Assert
        act.Should()
            .Throw<FormatException>()
            .WithMessage("Unable to parse CSS function expression.");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_InputIsInvalid()
    {
        // Arrange
        var input = "whoDis(123)";

        // Act
        var ok = AllyariaFunctionValue.TryParse(input, out var result);

        // Assert
        ok.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_InputIsValid()
    {
        // Arrange
        var input = "OkLch(50% 0.1 120)";

        // Act
        var ok = AllyariaFunctionValue.TryParse(input, out var result);

        // Assert
        ok.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        string normalized = result;

        normalized.Should()
            .Be("oklch(50% 0.1 120)");
    }
}
