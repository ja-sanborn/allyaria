using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaFontSizeCssTests
{
    [Theory]
    [InlineData("font-size: calc(100% - 2px);")]
    [InlineData("font-size: min(2rem, 5vw);")]
    public void Ctor_Should_Accept_FunctionValues_When_DeclarationIsValid(string css)
    {
        // Arrange
        // Act
        var sut = new AllyariaFontSizeCss(css);

        // Assert
        string declaration = sut;

        declaration.Should()
            .Contain("font-size");
    }

    [Theory]
    [InlineData("font-size: inherit;")]
    [InlineData("font-size: initial;")]
    [InlineData("font-size: unset;")]
    [InlineData("font-size: revert;")]
    public void Ctor_Should_Accept_GlobalKeywordValues_When_DeclarationIsValid(string css)
    {
        // Arrange
        // Act
        var sut = new AllyariaFontSizeCss(css);

        // Assert
        string declaration = sut;

        declaration.Should()
            .Contain("font-size");
    }

    [Theory]
    [InlineData("font-size: larger;")]
    [InlineData("font-size: x-large;")]
    [InlineData("font-size: XXX-LARGE;")] // case/whitespace normalization
    public void Ctor_Should_Accept_SizeKeywordValues_When_DeclarationIsValid(string css)
    {
        // Arrange
        // Act
        var sut = new AllyariaFontSizeCss(css);

        // Assert
        string declaration = sut;

        declaration.Should()
            .Contain("font-size")
            .And.Contain("large"); // just ensure normalization occurred to a known token
    }

    [Theory]
    [InlineData("font-size: 16px;", "font-size", "16px")]
    [InlineData(" FONT-SIZE :  1.25rem ", "font-size", "1.25rem")]
    [InlineData("\tfont-size:\t100%;", "font-size", "100%")]
    public void Ctor_Should_Parse_NumberValues_And_CanonicalizeName_When_DeclarationIsValid(string css,
        string expectedNameFragment,
        string expectedValueFragment)
    {
        // Arrange
        // Act
        var sut = new AllyariaFontSizeCss(css);

        // Assert
        // We don't rely on exact formatting from the base class; assert key fragments are present.
        string declaration = sut; // implicit to string returns CssProperty

        declaration.Should()
            .Contain(expectedNameFragment)
            .And.Contain(expectedValueFragment);
    }

    [Theory]
    [InlineData("font_size: 16px;")] // malformed name
    [InlineData("font-size 16px;")] // missing colon
    [InlineData("font-size: ;")] // missing value
    public void Ctor_Should_ThrowArgumentException_When_CssPropertyCannotBeParsed(string css)
    {
        // Arrange
        // Act
        var act = () => new AllyariaFontSizeCss(css);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unable to parse CSS property*")
            .WithParameterName("cssProperty");
    }

    [Fact]
    public void Ctor_Should_ThrowArgumentException_When_InputIsNull()
    {
        // Arrange
        string css = null!;

        // Act
        var act = () => new AllyariaFontSizeCss(css);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("cssProperty");
    }

    [Theory]
    [InlineData("font-size: gigantic;")] // not an allowed keyword
    [InlineData("font-size: abc;")] // not a number, not keyword, not global, not function
    public void Ctor_Should_ThrowArgumentException_WithParamNameValue_When_ValueIsInvalid(string css)
    {
        // Arrange
        // Act
        var act = () => new AllyariaFontSizeCss(css);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Invalid number value*")
            .WithParameterName("value");
    }

    [Fact]
    public void ImplicitConversion_FromString_Should_CreateInstance()
    {
        // Arrange
        var css = "font-size: 2rem;";

        // Act
        AllyariaFontSizeCss sut = css; // implicit from string

        // Assert
        string declaration = sut;

        declaration.Should()
            .Contain("font-size")
            .And.Contain("2rem");
    }

    [Fact]
    public void ImplicitConversion_ToString_Should_ThrowArgumentNullException_When_SourceIsNull()
    {
        // Arrange
        AllyariaFontSizeCss source = null!;

        // Act
        var act = () =>
        {
            string _ = source; // implicit to string invokes null-check
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("font-size: 18px;", "font-size", "18px")]
    [InlineData(" FONT-SIZE :   MEDIUM ;", "font-size", "medium")]
    public void Parse_Should_Return_Instance_WhoseDeclarationContainsExpectedFragments_When_Valid(string css,
        string expectedName,
        string expectedValueFragment)
    {
        // Arrange
        // Act
        var sut = AllyariaFontSizeCss.Parse(css);

        // Assert
        string declaration = sut;

        declaration.Should()
            .Contain(expectedName)
            .And.Contain(expectedValueFragment);
    }

    [Fact]
    public void Parse_Should_ThrowArgumentException_When_Invalid()
    {
        // Arrange
        var css = "font-size: bogus;";

        // Act
        var act = () => AllyariaFontSizeCss.Parse(css);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value"); // Create(...) throws with param name "value"
    }

    [Theory]
    [InlineData("font-size: ;")]
    [InlineData("font-size: nope;")]
    [InlineData("color: red;")]
    public void TryParse_Should_ReturnFalse_AndNull_When_DeclarationIsInvalid(string css)
    {
        // Arrange
        // Act
        var ok = AllyariaFontSizeCss.TryParse(css, out var result);

        // Assert
        ok.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void TryParse_Should_ReturnFalse_When_InputIsNullOrWhitespace(string css)
    {
        // Arrange
        // Act
        var ok = AllyariaFontSizeCss.TryParse(css, out var result);

        // Assert
        ok.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Theory]
    [InlineData("font-size: 14px;")]
    [InlineData("font-size: medium;")]
    [InlineData("font-size: inherit;")]
    [InlineData("font-size: calc(1rem + 2px);")]
    public void TryParse_Should_ReturnTrue_WithResult_When_DeclarationIsValid(string css)
    {
        // Arrange
        // Act
        var ok = AllyariaFontSizeCss.TryParse(css, out var result);

        // Assert
        ok.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        string declaration = result;

        declaration.Should()
            .Contain("font-size");
    }
}
