using Allyaria.Theming.Typography;

namespace Allyaria.Theming.UnitTests.Typography;

public class AllyariaTypoItemTests
{
    [Fact]
    public void Ctor_FontFamily_QuotesNamesWithSpaces_Dedupes_AndPreservesOrder()
    {
        // Arrange / Act
        var sut = new AllyariaTypoItem(
            new[]
            {
                "Segoe UI", "Arial", "  ", "Arial", "sans-serif"
            }
        );

        // Assert
        sut.FontFamily.Should()
            .NotBeNull();

        sut.FontFamily!.Should()
            .ContainInOrder("\"Segoe UI\"", "Arial", "sans-serif");

        sut.FontFamily.Should()
            .HaveCount(3);
    }

    [Theory]
    [InlineData("350")]
    [InlineData("95")]
    [InlineData("1000")]
    public void Ctor_WithInvalidNumericFontWeight_Throws(string weight)
    {
        // Arrange
        Action act = () => _ = new AllyariaTypoItem(fontWeight: weight);

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*FontWeight must be normal|bold|lighter|bolder or a multiple of 100 between 100 and 900.*");
    }

    [Fact]
    public void Ctor_WithInvalidTextAlign_ThrowsArgumentException()
    {
        // Arrange
        var act = () => _ = new AllyariaTypoItem(textAlign: "middle");

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*TextAlign must be one of*");
    }

    [Fact]
    public void Ctor_WithInvalidVerticalAlign_Throws()
    {
        // Arrange
        Action act = () => _ = new AllyariaTypoItem(verticalAlign: "text-bottom");

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*VerticalAlign must be one of*");
    }

    [Fact]
    public void Ctor_WithLetterSpacingBareNumber_AssumesPx()
    {
        // Arrange / Act
        var sut = new AllyariaTypoItem(letterSpacing: "2");

        // Assert
        sut.LetterSpacing.Should()
            .Be("2px");
    }

    [Fact]
    public void Ctor_WithMixedCaseKeywords_NormalizesToLower()
    {
        // Arrange / Act
        var sut = new AllyariaTypoItem(
            new[]
            {
                "Segoe UI", "sans-serif"
            },
            "Medium",
            "ItAliC",
            "BoLd",
            "Normal",
            "NoRmAl",
            "CeNtEr",
            "UNDERLINE",
            "UpperCase",
            "Top",
            "normal"
        );

        // Assert
        sut.FontSize.Should()
            .Be("medium");

        sut.FontStyle.Should()
            .Be("italic");

        sut.FontWeight.Should()
            .Be("bold");

        sut.LetterSpacing.Should()
            .Be("normal");

        sut.LineHeight.Should()
            .Be("normal");

        sut.TextAlign.Should()
            .Be("center");

        sut.TextDecoration.Should()
            .Be("underline");

        sut.TextTransform.Should()
            .Be("uppercase");

        sut.WordSpacing.Should()
            .Be("normal");

        sut.VerticalAlign.Should()
            .Be("top");
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("-0.1")]
    public void Ctor_WithNegativeLineHeight_Throws(string value)
    {
        // Arrange
        Action act = () => _ = new AllyariaTypoItem(lineHeight: value);

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage(
                "*LineHeight must be 'normal', a positive unitless number, a length (px|em|rem|%), or var()/calc().*"
            );
    }

    [Fact]
    public void Ctor_WithTextDecorationNoneCombined_Throws()
    {
        // Arrange
        Action act = () => _ = new AllyariaTypoItem(textDecoration: "none underline");

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*TextDecoration 'none' cannot be combined*");
    }

    [Fact]
    public void Ctor_WithUnitlessPositiveLineHeight_Succeeds()
    {
        // Arrange / Act
        var sut = new AllyariaTypoItem(lineHeight: "1.4");

        // Assert
        sut.LineHeight.Should()
            .Be("1.4");
    }

    [Theory]
    [InlineData("normal")]
    [InlineData("bold")]
    [InlineData("100")]
    [InlineData("900")]
    public void Ctor_WithValidFontWeight_Succeeds(string weight)
    {
        // Arrange / Act
        var sut = new AllyariaTypoItem(fontWeight: weight);

        // Assert
        sut.FontWeight.Should()
            .Be(weight.ToLowerInvariant());
    }

    [Theory]
    [InlineData("baseline")]
    [InlineData("top")]
    [InlineData("middle")]
    [InlineData("bottom")]
    [InlineData("sub")]
    [InlineData("text-top")]
    public void Ctor_WithValidVerticalAlign_Succeeds(string value)
    {
        // Arrange / Act
        var sut = new AllyariaTypoItem(verticalAlign: value);

        // Assert
        sut.VerticalAlign.Should()
            .Be(value);
    }

    [Fact]
    public void Ctor_WithWordSpacingPercent_CurrentImplementation_AllowsPercent()
    {
        // NOTE: Current NormalizeTrack() defers to IsLength(), which includes '%'.
        // Arrange / Act
        var sut = new AllyariaTypoItem(wordSpacing: "10%");

        // Assert
        sut.WordSpacing.Should()
            .Be("10%");
    }

    [Fact]
    public void NormalizeFontSize_WithInvalidInput_ThrowsArgumentException()
    {
        // Arrange
        var invalid = "foobar";

        // Act
        Action act = () => AllyariaTypoItem.NormalizeFontSize(invalid);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage(
                "*FontSize must be a length (px|em|rem|%) or CSS size keyword (xx-small..larger) or var()/calc().*"
            )
            .And.ParamName.Should()
            .Be("FontSize");
    }

    [Fact]
    public void NormalizeLineHeight_WithCalcExpression_ReturnsUnchanged()
    {
        // Arrange
        var input = "calc(100% - 2px)";

        // Act
        var result = AllyariaTypoItem.NormalizeLineHeight(input);

        // Assert
        result.Should()
            .Be("calc(100% - 2px)");
    }

    [Theory]
    [InlineData("-20px")]
    [InlineData("-1.5em")]
    [InlineData("-120%")]
    public void NormalizeLineHeight_WithNegativeLength_Throws(string input)
    {
        // Arrange
        Action act = () => AllyariaTypoItem.NormalizeLineHeight(input);

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*LineHeight must not be negative*")
            .And.ParamName.Should()
            .Be("LineHeight");
    }

    [Theory]
    [InlineData("20px")]
    [InlineData("1.5em")]
    [InlineData("120%")]
    public void NormalizeLineHeight_WithValidLengthLike_ReturnsSame(string input)
    {
        // Arrange / Act
        var result = AllyariaTypoItem.NormalizeLineHeight(input);

        // Assert
        result.Should()
            .Be(input);
    }

    [Fact]
    public void NormalizeLineHeight_WithVarExpression_ReturnsUnchanged()
    {
        // Arrange
        var input = "var(--lh)";

        // Act
        var result = AllyariaTypoItem.NormalizeLineHeight(input);

        // Assert
        result.Should()
            .Be("var(--lh)");
    }

    [Fact]
    public void NormalizeTextDecoration_WithInvalidToken_ThrowsArgumentException()
    {
        // Arrange
        var input = "underline squiggly";

        // Act
        Action act = () => AllyariaTypoItem.NormalizeTextDecoration(input);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*TextDecoration allows only: none, underline, overline, line-through.*")
            .And.ParamName.Should()
            .Be("TextDecoration");
    }

    [Fact]
    public void NormalizeTextDecoration_WithOnlyWhitespaceTokens_ReturnsNull()
    {
        // Arrange
        var input = "   \t  \r\n   ";

        // Act
        var result = AllyariaTypoItem.NormalizeTextDecoration(input);

        // Assert
        result.Should()
            .BeNull();
    }

    [Fact]
    public void NormalizeTrack_WithInvalidInput_ThrowsArgumentException()
    {
        // Arrange
        var invalid = "foobar";

        // Act
        Action act = () => AllyariaTypoItem.NormalizeTrack(invalid, "LetterSpacing");

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*LetterSpacing must be 'normal', a length (px|em|rem), or var()/calc().*")
            .And.ParamName.Should()
            .Be("LetterSpacing");
    }

    [Fact]
    public void ToCss_EmitsFontFamilyWhenPresent_AndOmitsWhenEmpty()
    {
        // Arrange
        var withFamilies = new AllyariaTypoItem(
            new[]
            {
                "Inter", "sans-serif"
            },
            "16px"
        );

        var noFamilies = new AllyariaTypoItem(fontSize: "16px", fontFamily: Array.Empty<string>());

        // Act
        var cssWith = withFamilies.ToCss();
        var cssNone = noFamilies.ToCss();

        // Assert
        cssWith.Should()
            .StartWith("font-family:Inter,sans-serif;")
            .And.Contain("font-size:16px;");

        cssNone.Should()
            .Be("font-size:16px;");
    }

    [Fact]
    public void ToCss_OrderIsFixed_AndIncludesVerticalAlignAtEnd()
    {
        // Arrange
        var sut = new AllyariaTypoItem(
            new[]
            {
                "Inter", "sans-serif"
            },
            "16px",
            fontWeight: "400",
            lineHeight: "1.5",
            fontStyle: "italic",
            textAlign: "center",
            letterSpacing: "0.5px",
            wordSpacing: "1px",
            textTransform: "uppercase",
            textDecoration: "underline",
            verticalAlign: "top"
        );

        // Act
        var css = sut.ToCss();

        // Assert (verify ordering by index positions)
        var iFamily = css.IndexOf("font-family:", StringComparison.Ordinal);
        var iSize = css.IndexOf("font-size:", StringComparison.Ordinal);
        var iWeight = css.IndexOf("font-weight:", StringComparison.Ordinal);
        var iLh = css.IndexOf("line-height:", StringComparison.Ordinal);
        var iStyle = css.IndexOf("font-style:", StringComparison.Ordinal);
        var iAlign = css.IndexOf("text-align:", StringComparison.Ordinal);
        var iLetter = css.IndexOf("letter-spacing:", StringComparison.Ordinal);
        var iWord = css.IndexOf("word-spacing:", StringComparison.Ordinal);
        var iTrans = css.IndexOf("text-transform:", StringComparison.Ordinal);
        var iDecor = css.IndexOf("text-decoration:", StringComparison.Ordinal);
        var iVAlign = css.IndexOf("vertical-align:", StringComparison.Ordinal);

        iFamily.Should()
            .BeGreaterThanOrEqualTo(0);

        iFamily.Should()
            .BeLessThan(iSize);

        iSize.Should()
            .BeLessThan(iWeight);

        iWeight.Should()
            .BeLessThan(iLh);

        iLh.Should()
            .BeLessThan(iStyle);

        iStyle.Should()
            .BeLessThan(iAlign);

        iAlign.Should()
            .BeLessThan(iLetter);

        iLetter.Should()
            .BeLessThan(iWord);

        iWord.Should()
            .BeLessThan(iTrans);

        iTrans.Should()
            .BeLessThan(iDecor);

        iDecor.Should()
            .BeLessThan(iVAlign);

        // Also assert no spaces around separators globally
        css.Should()
            .NotContain(": ")
            .And.NotContain("; ");
    }

    [Fact]
    public void ToCss_VarAndCalc_ArePassedThrough()
    {
        // Arrange
        var sut = new AllyariaTypoItem(
            fontSize: "calc(1rem + 1px)",
            letterSpacing: "var(--letter)",
            wordSpacing: "var(--word)"
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Contain("font-size:calc(1rem + 1px);");

        css.Should()
            .Contain("letter-spacing:var(--letter);");

        css.Should()
            .Contain("word-spacing:var(--word);");
    }

    [Fact]
    public void ToCss_WhenOnlyFontSizeProvided_EmitsSingleDeclaration()
    {
        // Arrange
        var sut = new AllyariaTypoItem(fontSize: "16");

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Be("font-size:16px;");
    }

    [Fact]
    public void ToString_BareNumberFontSize_AppendsPx()
    {
        // Arrange
        var sut = new AllyariaTypoItem(fontSize: "20");

        // Act
        var css = sut.ToString();

        // Assert
        css.Should()
            .Contain("font-size:20px;");
    }
}
