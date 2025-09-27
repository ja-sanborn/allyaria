using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaTypographyTests
{
    [Fact]
    public void ToCss_Should_Append_All_KnownProperties_InFixedOrder_When_AllProvided()
    {
        // Arrange
        var fontFamily = new AllyariaStringValue("Inter, Segoe UI, sans-serif");
        var fontSize = new AllyariaStringValue("16px");
        var fontStyle = new AllyariaStringValue("normal");
        var fontWeight = new AllyariaStringValue("400");
        var letterSpacing = new AllyariaStringValue("0.01em");
        var lineHeight = new AllyariaStringValue("1.6");
        var textAlign = new AllyariaStringValue("left");
        var textDecoration = new AllyariaStringValue("none");
        var textTransform = new AllyariaStringValue("none");
        var verticalAlign = new AllyariaStringValue("baseline");
        var wordSpacing = new AllyariaStringValue("0");

        var sut = new AllyariaTypography(
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecoration,
            textTransform,
            verticalAlign,
            wordSpacing
        );

        // Act
        var css = sut.ToCss();

        // Assert (explicit order = the order in the implementation)
        var expected = fontFamily.ToCss("font-family") +
            fontSize.ToCss("font-size") +
            fontStyle.ToCss("font-style") +
            fontWeight.ToCss("font-weight") +
            letterSpacing.ToCss("letter-spacing") +
            lineHeight.ToCss("line-height") +
            textAlign.ToCss("text-align") +
            textDecoration.ToCss("text-decoration") +
            textTransform.ToCss("text-transform") +
            verticalAlign.ToCss("vertical-align") +
            wordSpacing.ToCss("word-spacing");

        css.Should()
            .Be(expected);
    }

    [Fact]
    public void ToCss_Should_Concatenate_InDeclarationOrder_When_MultipleValuesProvided()
    {
        // Arrange
        var fontFamily = new AllyariaStringValue("Inter, Segoe UI, sans-serif");
        var fontSize = new AllyariaStringValue("14px");
        var fontStyle = new AllyariaStringValue("italic");

        var sut = new AllyariaTypography(
            fontFamily,
            fontSize,
            fontStyle
        );

        // Act
        var css = sut.ToCss();

        // Assert (build expected using AllyariaStringValue's own formatting)
        var expected = fontFamily.ToCss("font-family") +
            fontSize.ToCss("font-size") +
            fontStyle.ToCss("font-style");

        css.Should()
            .Be(expected);
    }

    [Fact]
    public void ToCss_Should_ReturnEmptyString_When_AllPropertiesNull()
    {
        // Arrange
        var sut = new AllyariaTypography();

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Be(string.Empty);
    }

    [Fact]
    public void ToCss_Should_SkipNulls_AndIncludeProvided_When_MixedValues()
    {
        // Arrange
        var textAlign = new AllyariaStringValue("center");
        var textTransform = new AllyariaStringValue("uppercase");
        var sut = new AllyariaTypography(textAlign: textAlign, textTransform: textTransform);

        // Act
        var css = sut.ToCss();

        // Assert
        var expected = textAlign.ToCss("text-align") +
            textTransform.ToCss("text-transform");

        css.Should()
            .Be(expected);
    }

    [Fact]
    public void ToCssVars_Should_Append_All_KnownProperties_InFixedOrder_When_AllProvided()
    {
        // Arrange
        var fontFamily = new AllyariaStringValue("Inter, Segoe UI, sans-serif");
        var fontSize = new AllyariaStringValue("16px");
        var fontStyle = new AllyariaStringValue("normal");
        var fontWeight = new AllyariaStringValue("400");
        var letterSpacing = new AllyariaStringValue("0.01em");
        var lineHeight = new AllyariaStringValue("1.6");
        var textAlign = new AllyariaStringValue("left");
        var textDecoration = new AllyariaStringValue("none");
        var textTransform = new AllyariaStringValue("none");
        var verticalAlign = new AllyariaStringValue("baseline");
        var wordSpacing = new AllyariaStringValue("0");

        var sut = new AllyariaTypography(
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecoration,
            textTransform,
            verticalAlign,
            wordSpacing
        );

        // Act
        var cssVars = sut.ToCssVars();

        // Assert (explicit order = the order in the implementation)
        var expected = fontFamily.ToCss("--aa-font-family") +
            fontSize.ToCss("--aa-font-size") +
            fontStyle.ToCss("--aa-font-style") +
            fontWeight.ToCss("--aa-font-weight") +
            letterSpacing.ToCss("--aa-letter-spacing") +
            lineHeight.ToCss("--aa-line-height") +
            textAlign.ToCss("--aa-text-align") +
            textDecoration.ToCss("--aa-text-decoration") +
            textTransform.ToCss("--aa-text-transform") +
            verticalAlign.ToCss("--aa-vertical-align") +
            wordSpacing.ToCss("--aa-word-spacing");

        cssVars.Should()
            .Be(expected);
    }

    [Fact]
    public void ToCssVars_Should_Apply_CustomPrefix_To_All_Properties()
    {
        // Arrange
        var fontSize = new AllyariaStringValue("14px");
        var fontWeight = new AllyariaStringValue("700");
        var sut = new AllyariaTypography(fontSize: fontSize, fontWeight: fontWeight);

        // Act
        var cssVars = sut.ToCssVars("brand");

        // Assert
        cssVars.Should()
            .Contain("--brand-font-size:14px;")
            .And.Contain("--brand-font-weight:700;")
            .And.NotContain("--aa-");
    }

    [Fact]
    public void ToCssVars_Should_Concatenate_InDeclarationOrder_When_MultipleValuesProvided()
    {
        // Arrange
        var weight = new AllyariaStringValue("600");
        var letterSpacing = new AllyariaStringValue("0.02em");
        var lineHeight = new AllyariaStringValue("1.5");

        var sut = new AllyariaTypography(
            fontWeight: weight,
            letterSpacing: letterSpacing,
            lineHeight: lineHeight
        );

        // Act
        var cssVars = sut.ToCssVars();

        // Assert
        var expected = weight.ToCss("--aa-font-weight") +
            letterSpacing.ToCss("--aa-letter-spacing") +
            lineHeight.ToCss("--aa-line-height");

        cssVars.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("---")]
    public void ToCssVars_Should_FallBack_To_DefaultPrefix_When_Prefix_IsEmptyOrWhitespace(string prefix)
    {
        // Arrange
        var sut = new AllyariaTypography(new AllyariaStringValue("Arial"));

        // Act
        var cssVars = sut.ToCssVars(prefix);

        // Assert
        cssVars.Should()
            .Contain("--aa-font-family");
    }

    [Fact]
    public void ToCssVars_Should_Handle_Prefix_With_LeadingAndTrailingDashes()
    {
        // Arrange
        var fontStyle = new AllyariaStringValue("italic");
        var sut = new AllyariaTypography(fontStyle: fontStyle);

        // Act
        var cssVars = sut.ToCssVars("---Custom---");

        // Assert
        cssVars.Should()
            .Contain("--custom-font-style:italic;");
    }

    [Fact]
    public void ToCssVars_Should_Normalize_CustomPrefix_To_Lowercase_TrimDashes_And_SpacesToHyphens()
    {
        // Arrange
        var sut = new AllyariaTypography(new AllyariaStringValue("Arial"));

        // Act
        var cssVars = sut.ToCssVars("  --My THEME  ");

        // Assert
        cssVars.Should()
            .Contain("--my-theme-font-family:Arial;");
    }

    [Fact]
    public void ToCssVars_Should_ReturnEmptyString_When_AllPropertiesNull()
    {
        // Arrange
        var sut = new AllyariaTypography();

        // Act
        var css = sut.ToCssVars();

        // Assert
        css.Should()
            .Be(string.Empty);
    }

    [Fact]
    public void ToCssVars_Should_SkipNulls_AndIncludeProvided_When_MixedValues()
    {
        // Arrange
        var verticalAlign = new AllyariaStringValue("middle");
        var wordSpacing = new AllyariaStringValue("0.1em");

        var sut = new AllyariaTypography(
            verticalAlign: verticalAlign,
            wordSpacing: wordSpacing
        );

        // Act
        var cssVars = sut.ToCssVars();

        // Assert
        var expected = verticalAlign.ToCss("--aa-vertical-align") +
            wordSpacing.ToCss("--aa-word-spacing");

        cssVars.Should()
            .Be(expected);
    }
}
