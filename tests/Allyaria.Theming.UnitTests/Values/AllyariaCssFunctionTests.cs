using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaCssFunctionTests
{
    [Theory]
    [InlineData("scalc(1)", "scalc(1)")] // arbitrary identifier allowed
    [InlineData("my_func(42)", "my_func(42)")] // underscore
    [InlineData("my-func(42)", "my-func(42)")] // hyphen
    public void ArbitraryIdentifier_Names_AreAccepted_AndLowercased(string input, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("--x")]
    [InlineData("--LONG_token")]
    public void DoubleDash_Shorthand_IsNotSupported_ShouldNormalizeToEmpty(string input)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().BeEmpty();
    }

    // --- Name normalization and acceptance ---

    [Theory]
    [InlineData("Min(1rem,2rem)", "min(1rem,2rem)")]
    [InlineData("MAX(10px, 20px)", "max(10px, 20px)")]
    [InlineData("calc( 100% - var(--Gap) )", "calc(100% - var(--Gap))")]
    public void GeneralFunction_ShouldLowercaseName_AndPreserveInner(string input, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void GeneralFunction_WithNestedParentheses_ShouldUseFirstOpenAndLastClose()
    {
        // Arrange
        var input = "calc( min(10px, 20px) + max(1rem, 2rem) )";

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().Be("calc(min(10px, 20px) + max(1rem, 2rem))");
    }

    [Theory]
    [InlineData("calc( 1 + 2 )", "calc(1 + 2)")]
    [InlineData("min( 1rem , 2rem )", "min(1rem , 2rem)")]
    public void GeneralFunction_WithOuterWhitespace_ShouldNormalize(string input, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction($"   {input}   ");
        var result = (string)sut;

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void GreaterThan_WhenLeftValid_AndRightInvalid_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("calc(100% - 2rem)");
        var right = new AllyariaCssFunction("calc ulus(100% - 2rem)"); // invalid

        // Act
        var result = left > right;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GreaterThanOrEqual_WhenLeftValid_AndRightInvalid_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("var(--ok)");
        var right = new AllyariaCssFunction("var iant(--ok)"); // invalid

        // Act
        var result = left >= right;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ImplicitOperators_ShouldRoundTrip_ForValidFunction()
    {
        // Arrange
        AllyariaCssFunction sut = "calc(1+2)";

        // Act
        string result = sut;

        // Assert
        result.Should().Be("calc(1+2)");
    }

    [Theory]
    [InlineData("background")]
    [InlineData("var()")]
    [InlineData("mi n(1)")]
    public void InvalidInputs_ShouldNormalizeToEmpty(string input)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void LessThan_WhenLeftInvalid_AndRightValid_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("min max(1)"); // invalid
        var right = new AllyariaCssFunction("min(1)");

        // Act
        var result = left < right;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void LessThanOrEqual_WhenNormalizedValuesEqual_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("MaX( 2 )");
        var right = new AllyariaCssFunction("max(2)");

        // Act
        var result = left <= right;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void MissingFinalParen_ShouldBeInvalid()
    {
        // Arrange
        var input = "max(10px, 20px";

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void NullValue_ShouldNormalizeToEmpty()
    {
        // Arrange
        string? input = null;

        // Act
        var sut = new AllyariaCssFunction(input!);
        var result = (string)sut;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SpaceBetweenNameAndParen_ShouldBeInvalid()
    {
        // Arrange
        var input = "calc (1+2)";

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Trimming_ShouldBeApplied_ButInnerSpacingPreserved()
    {
        // Arrange
        var input = "  max( 10px ,  20px )  ";

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().Be("max(10px ,  20px)");
    }

    [Theory]
    [InlineData("var(--X)", "var(--X)")]
    [InlineData("VAR(--x)", "var(--x)")]
    [InlineData(" Var(--Gap) ", "var(--Gap)")]
    public void VarFunction_ShouldLowercaseName_AndPreserveInner(string input, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void TryParse_ValidInput_ReturnsTrue_And_Normalizes()
    {
        // Arrange
        var input = "MAX( 1px , 2px )";

        // Act
        var ok = AllyariaCssFunction.TryParse(input, out var func);

        // Assert
        ok.Should().BeTrue();                    // public API returns true for valid (normalizable) input
        ((string)func).Should().Be("max(1px , 2px)");
    }

    [Fact]
    public void TryParse_InvalidInput_ReturnsFalse_And_EmptyValue()
    {
        // Arrange
        var input = "background";

        // Act
        var ok = AllyariaCssFunction.TryParse(input, out var func);

        // Assert
        ok.Should().BeFalse();
        ((string)func).Should().BeEmpty();
    }

    [Fact]
    public void NameStartingWithDigit_IsInvalid()
    {
        // Arrange
        var input = "1calc(1+2)";

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("calc(1+2))well")] // extra trailing ')'
    [InlineData("calc(1+2)more")]  // trailing tokens after last ')'
    public void TrailingCharacters_After_FinalParen_ShouldBeInvalid(string input)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(input);
        var result = (string)sut;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Implicit_ToString_On_Null_Instance_ReturnsEmpty()
    {
        // Arrange
        AllyariaCssFunction? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().BeEmpty();
    }
}
