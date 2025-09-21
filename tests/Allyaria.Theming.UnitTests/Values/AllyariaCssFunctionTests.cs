using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaCssFunctionTests
{
    [Theory]
    [InlineData("--x", "var(--x)")]
    [InlineData("--LONG_token", "var(--LONG_token)")]
    public void DoubleDashShortcut_ShouldOnlyApply_WhenNoExplicitName(string input, string expected)
    {
        // Arrange

        // Act
        var noName = new AllyariaCssFunction(input);
        var calcName = new AllyariaCssFunction("calc", input);
        var varName = new AllyariaCssFunction("var", input);

        // Assert
        ((string)noName).Should()
            .Be(expected);

        ((string)calcName).Should()
            .BeEmpty();

        ((string)varName).Should()
            .BeEmpty();
    }

    [Theory]
    [InlineData("calc", " calc(1+2) ")]
    [InlineData("min", "   min( 1 , 2 )   ")]
    public void ExplicitName_InputMayHaveOuterWhitespace_ButShapeMustStillBeStrict(string name, string padded)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(name, padded);
        var result = (string)sut;

        // Assert
        result.Should()
            .NotBeEmpty();
    }

    [Fact]
    public void ExplicitName_MustMatchFunction_AndBeStrictlyShaped()
    {
        // Arrange
        var matchInput = "calc(1+2)";
        var mismatchInput = "min(1,2)";
        var spacedInput = "calc (1+2)";

        // Act
        var ok = new AllyariaCssFunction("calc", matchInput);
        var mismatch = new AllyariaCssFunction("calc", mismatchInput);
        var spaced = new AllyariaCssFunction("calc", spacedInput);

        // Assert
        ((string)ok).Should()
            .Be("calc(1+2)");

        ((string)mismatch).Should()
            .BeEmpty();

        ((string)spaced).Should()
            .Be("calc(1+2)");
    }

    [Fact]
    public void ExplicitName_Var_ShouldAcceptVarFunctionShapeOnly()
    {
        // Arrange
        var input1 = "var(--primary)";
        var input2 = "VAR(  --x  )";

        // Act
        var ok = new AllyariaCssFunction("var", input1);
        var bad = new AllyariaCssFunction("var", input2);

        // Assert
        ((string)ok).Should()
            .Be("var(--primary)");

        ((string)bad).Should()
            .Be("var(--x)");
    }

    [Theory]
    [InlineData("calc", "min(1,2)")]
    [InlineData("min", "max(1,2)")]
    [InlineData("max", "calc(1+2)")]
    [InlineData("var", "calc(--x)")]
    public void ExplicitName_WhenFuncNameMismatch_ShouldBeEmpty(string name, string value)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(name, value);
        var result = (string)sut;

        // Assert
        result.Should()
            .BeEmpty();
    }

    [Theory]
    [InlineData("CALC", "calc(1+2)", "calc(1+2)")]
    [InlineData("mIn", "MIN( 1rem , 2rem )", "min(1rem , 2rem)")]
    [InlineData("MaX", "MaX(--x , 10px)", "max(--x , 10px)")]
    public void ExplicitName_WhenNameMatches_IgnoresCase_AndNormalizes(string name, string value, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(name, value);
        var result = (string)sut;

        // Assert
        result.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData("max", "max(1,2")]
    [InlineData("var", "var()")]
    [InlineData("calc", "background")]
    public void ExplicitName_WhenNotStrictFuncShape_ShouldBeEmpty(string name, string value)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(name, value);
        var result = (string)sut;

        // Assert
        result.Should()
            .BeEmpty();
    }

    [Theory]
    [InlineData("min", "minmax(1)")]
    [InlineData("max", "maximum(2,3)")]
    [InlineData("calc", "calcx(100% - 2rem)")]
    [InlineData("var", "variant(--x)")]
    public void ExplicitName_WhenStartsWithNameButDifferentFunction_ShouldBeEmpty(string name, string value)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(name, value);
        var result = (string)sut;

        // Assert
        result.Should()
            .BeEmpty();
    }

    [Theory]
    [InlineData("calc", "calc( 100% - var(--Gap) )", "calc(100% - var(--Gap))")]
    [InlineData("min", "min( 10px ,  20px )", "min(10px ,  20px)")]
    public void ExplicitName_WhenStrictShape_ShouldPreserveInnerSpacing(string name, string value, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssFunction(name, value);
        var result = (string)sut;

        // Assert
        result.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData("Min(1rem,2rem)", "min(1rem,2rem)")]
    [InlineData("MAX(10px, 20px)", "max(10px, 20px)")]
    [InlineData("calc ( 100% - var(--Gap) )", "calc(100% - var(--Gap))")]
    public void GeneralFunction_ShouldLowercaseName_AndPreserveInner(string input, string expected)
    {
        // Arrange

        // Act
        var f = new AllyariaCssFunction(input);
        var result = (string)f;

        // Assert
        result.Should()
            .Be(expected);
    }

    [Fact]
    public void GreaterThan_WhenLeftValid_AndRightExplicitNameMismatch_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("calc(100% - 2rem)");
        var right = new AllyariaCssFunction("calc", "calculus(100% - 2rem)");

        // Act
        var result = left > right;

        // Assert
        result.Should()
            .BeTrue();
    }

    [Fact]
    public void GreaterThanOrEqual_WhenLeftValid_AndRightInvalid_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("var(--ok)");
        var right = new AllyariaCssFunction("var", "variant(--ok)");

        // Act
        var result = left >= right;

        // Assert
        result.Should()
            .BeTrue();
    }

    [Fact]
    public void ImplicitOperators_ShouldRoundTrip()
    {
        // Arrange
        AllyariaCssFunction f = "--gap";

        // Act
        string result = f;

        // Assert
        result.Should()
            .Be("var(--gap)");
    }

    [Theory]
    [InlineData("background")]
    [InlineData("var()")]
    [InlineData("mi n(1)")]
    public void InvalidInputs_ShouldNormalizeToEmpty(string input)
    {
        // Arrange

        // Act
        var f = new AllyariaCssFunction(input);
        var result = (string)f;

        // Assert
        result.Should()
            .BeEmpty();
    }

    [Fact]
    public void LessThan_WhenLeftExplicitNameMismatch_AndRightValid_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("min", "minmax(1)");
        var right = new AllyariaCssFunction("min(1)");

        // Act
        var result = left < right;

        // Assert
        result.Should()
            .BeTrue();
    }

    [Fact]
    public void LessThanOrEqual_WhenNormalizedValuesEqual_ShouldBeTrue()
    {
        // Arrange
        var left = new AllyariaCssFunction("MAX", "MaX( 2 )");
        var right = new AllyariaCssFunction("max(2)");

        // Act
        var result = left <= right;

        // Assert
        result.Should()
            .BeTrue();
    }

    [Fact]
    public void NullValue_ShouldNormalizeToEmpty()
    {
        // Arrange
        string? input = null;

        // Act
        var f = new AllyariaCssFunction(input!);
        var result = (string)f;

        // Assert
        result.Should()
            .BeEmpty();
    }

    [Fact]
    public void Trimming_ShouldBeApplied_ButInnerSpacingPreserved()
    {
        // Arrange
        var input = "  max( 10px ,  20px )  ";

        // Act
        var f = new AllyariaCssFunction(input);
        var result = (string)f;

        // Assert
        result.Should()
            .Be("max(10px ,  20px)");
    }

    [Theory]
    [InlineData("var(--X)", "var(--X)")]
    [InlineData("VAR(--x)", "var(--x)")]
    [InlineData(" Var(--Gap) ", "var(--Gap)")]
    public void VarFunction_WithParens_ShouldLowercaseNameAndPreserveInner(string input, string expected)
    {
        // Arrange

        // Act
        var f = new AllyariaCssFunction(input);
        var result = (string)f;

        // Assert
        result.Should()
            .Be(expected);
    }

    [Fact]
    public void VarShorthand_WhenNoName_ShouldNormalizeToVar()
    {
        // Arrange
        var input = "--main-color";

        // Act
        var f = new AllyariaCssFunction(input);
        var result = (string)f;

        // Assert
        result.Should()
            .Be("var(--main-color)");
    }
}
