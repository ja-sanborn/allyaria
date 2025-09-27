using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaTypographyTests
{
    [Fact]
    public void Cascade_Should_Copy_Values_From_Overrides_When_OriginalHasNulls()
    {
        // Arrange
        var oFamily = new AllyariaStringValue("Arial, sans-serif");
        var oLineHeight = new AllyariaStringValue("1.8");
        var oWordSpacing = new AllyariaStringValue("0.2em");

        var sut = new AllyariaTypography(); // all nulls

        // Act
        var cascaded = sut.Cascade(oFamily, lineHeight: oLineHeight, wordSpacing: oWordSpacing);

        // Assert
        cascaded.FontFamily.Should()
            .BeSameAs(oFamily);

        cascaded.LineHeight.Should()
            .BeSameAs(oLineHeight);

        cascaded.WordSpacing.Should()
            .BeSameAs(oWordSpacing);

        cascaded.FontSize.Should()
            .BeNull();

        cascaded.FontStyle.Should()
            .BeNull();

        cascaded.FontWeight.Should()
            .BeNull();

        cascaded.LetterSpacing.Should()
            .BeNull();

        cascaded.TextAlign.Should()
            .BeNull();

        cascaded.TextDecoration.Should()
            .BeNull();

        cascaded.TextTransform.Should()
            .BeNull();

        cascaded.VerticalAlign.Should()
            .BeNull();
    }

    [Fact]
    public void Cascade_Should_NotMutate_OriginalInstance_When_Called()
    {
        // Arrange
        var originalFontSize = new AllyariaStringValue("12px");
        var sut = new AllyariaTypography(FontSize: originalFontSize);

        var overrideFontSize = new AllyariaStringValue("18px");

        // Act
        var cascaded = sut.Cascade(fontSize: overrideFontSize);

        // Assert
        sut.FontSize.Should()
            .BeSameAs(originalFontSize); // original unchanged

        cascaded.FontSize.Should()
            .BeSameAs(overrideFontSize); // new has override
    }

    [Fact]
    public void Cascade_Should_Override_Only_SpecifiedProperties_When_PartialOverridesProvided()
    {
        // Arrange
        var existingFamily = new AllyariaStringValue("Inter");
        var existingWeight = new AllyariaStringValue("400");
        var sut = new AllyariaTypography(existingFamily, FontWeight: existingWeight);

        var newWeight = new AllyariaStringValue("600"); // only override weight
        var newTextAlign = new AllyariaStringValue("right"); // and add a previously null field

        // Act
        var cascaded = sut.Cascade(fontWeight: newWeight, textAlign: newTextAlign);

        // Assert
        cascaded.FontFamily.Should()
            .BeSameAs(existingFamily); // kept

        cascaded.FontWeight.Should()
            .BeSameAs(newWeight); // overridden

        cascaded.TextAlign.Should()
            .BeSameAs(newTextAlign); // newly set

        cascaded.FontSize.Should()
            .BeNull(); // untouched remains null
    }

    [Fact]
    public void Cascade_Should_Retain_ExistingValues_When_AllOverridesAreNull()
    {
        // Arrange
        var fontFamily = new AllyariaStringValue("Inter");
        var fontSize = new AllyariaStringValue("14px");
        var fontStyle = new AllyariaStringValue("normal");
        var fontWeight = new AllyariaStringValue("500");
        var letterSpacing = new AllyariaStringValue("0.01em");
        var lineHeight = new AllyariaStringValue("1.5");
        var textAlign = new AllyariaStringValue("justify");
        var textDecoration = new AllyariaStringValue("line-through");
        var textTransform = new AllyariaStringValue("capitalize");
        var verticalAlign = new AllyariaStringValue("top");
        var wordSpacing = new AllyariaStringValue("0.05em");

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
        var cascaded = sut.Cascade();

        // Assert (same instances are kept since overrides are null)
        cascaded.FontFamily.Should()
            .BeSameAs(fontFamily);

        cascaded.FontSize.Should()
            .BeSameAs(fontSize);

        cascaded.FontStyle.Should()
            .BeSameAs(fontStyle);

        cascaded.FontWeight.Should()
            .BeSameAs(fontWeight);

        cascaded.LetterSpacing.Should()
            .BeSameAs(letterSpacing);

        cascaded.LineHeight.Should()
            .BeSameAs(lineHeight);

        cascaded.TextAlign.Should()
            .BeSameAs(textAlign);

        cascaded.TextDecoration.Should()
            .BeSameAs(textDecoration);

        cascaded.TextTransform.Should()
            .BeSameAs(textTransform);

        cascaded.VerticalAlign.Should()
            .BeSameAs(verticalAlign);

        cascaded.WordSpacing.Should()
            .BeSameAs(wordSpacing);
    }

    [Fact]
    public void Cascade_Should_Return_NewInstance_With_OverridesApplied_When_AllOverridesProvided()
    {
        // Arrange
        var original = new AllyariaTypography(
            new AllyariaStringValue("Inter"),
            new AllyariaStringValue("14px"),
            new AllyariaStringValue("normal"),
            new AllyariaStringValue("400"),
            new AllyariaStringValue("0.01em"),
            new AllyariaStringValue("1.4"),
            new AllyariaStringValue("left"),
            new AllyariaStringValue("none"),
            new AllyariaStringValue("none"),
            new AllyariaStringValue("baseline"),
            new AllyariaStringValue("0")
        );

        var oFontFamily = new AllyariaStringValue("Segoe UI");
        var oFontSize = new AllyariaStringValue("16px");
        var oFontStyle = new AllyariaStringValue("italic");
        var oFontWeight = new AllyariaStringValue("700");
        var oLetterSpacing = new AllyariaStringValue("0.02em");
        var oLineHeight = new AllyariaStringValue("1.6");
        var oTextAlign = new AllyariaStringValue("center");
        var oTextDecoration = new AllyariaStringValue("underline");
        var oTextTransform = new AllyariaStringValue("uppercase");
        var oVerticalAlign = new AllyariaStringValue("middle");
        var oWordSpacing = new AllyariaStringValue("0.1em");

        var sut = original;

        // Act
        var cascaded = sut.Cascade(
            oFontFamily,
            oFontSize,
            oFontStyle,
            oFontWeight,
            oLetterSpacing,
            oLineHeight,
            oTextAlign,
            oTextDecoration,
            oTextTransform,
            oVerticalAlign,
            oWordSpacing
        );

        // Assert
        cascaded.FontFamily.Should()
            .BeSameAs(oFontFamily);

        cascaded.FontSize.Should()
            .BeSameAs(oFontSize);

        cascaded.FontStyle.Should()
            .BeSameAs(oFontStyle);

        cascaded.FontWeight.Should()
            .BeSameAs(oFontWeight);

        cascaded.LetterSpacing.Should()
            .BeSameAs(oLetterSpacing);

        cascaded.LineHeight.Should()
            .BeSameAs(oLineHeight);

        cascaded.TextAlign.Should()
            .BeSameAs(oTextAlign);

        cascaded.TextDecoration.Should()
            .BeSameAs(oTextDecoration);

        cascaded.TextTransform.Should()
            .BeSameAs(oTextTransform);

        cascaded.VerticalAlign.Should()
            .BeSameAs(oVerticalAlign);

        cascaded.WordSpacing.Should()
            .BeSameAs(oWordSpacing);
    }

    [Fact]
    public void Cascade_Should_Support_Chaining_To_Accumulate_Overrides_When_CalledMultipleTimes()
    {
        // Arrange
        var sut = new AllyariaTypography(new AllyariaStringValue("Inter"));

        var firstSize = new AllyariaStringValue("14px");
        var secondWeight = new AllyariaStringValue("700");
        var thirdTransform = new AllyariaStringValue("uppercase");

        // Act
        var afterFirst = sut.Cascade(fontSize: firstSize);
        var afterSecond = afterFirst.Cascade(fontWeight: secondWeight);
        var afterThird = afterSecond.Cascade(textTransform: thirdTransform);

        // Assert
        afterThird.FontFamily.Should()
            .NotBeNull();

        afterThird.FontSize.Should()
            .BeSameAs(firstSize);

        afterThird.FontWeight.Should()
            .BeSameAs(secondWeight);

        afterThird.TextTransform.Should()
            .BeSameAs(thirdTransform);
    }

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
        var sut = new AllyariaTypography(TextAlign: textAlign, TextTransform: textTransform);

        // Act
        var css = sut.ToCss();

        // Assert
        var expected = textAlign.ToCss("text-align") +
            textTransform.ToCss("text-transform");

        css.Should()
            .Be(expected);
    }

    [Fact]
    public void ToCssVars_Should_Collapse_Whitespace_And_Dashes_To_SingleDash()
    {
        // Arrange
        var sut = new AllyariaTypography(new AllyariaStringValue("Arial"));
        var prefix = "test  - - -  test";

        // Act
        var css = sut.ToCssVars(prefix);

        // Assert
        css.Should()
            .Contain("--test-test-font-family:Arial");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ToCssVars_Should_Default_To_AA_Prefix_When_Prefix_Is_Empty_Or_Whitespace(string input)
    {
        // Arrange
        var sut = new AllyariaTypography(new AllyariaStringValue("Arial"));

        // Act
        var css = sut.ToCssVars(input);

        // Assert
        css.Should()
            .Contain("--aa-font-family:Arial");
    }

    [Fact]
    public void ToCssVars_Should_Include_All_NonNullProperties_In_ExpectedOrder()
    {
        // Arrange
        var sut = new AllyariaTypography(
            new AllyariaStringValue("Inter"),
            new AllyariaStringValue("16px"),
            new AllyariaStringValue("italic"),
            new AllyariaStringValue("600"),
            new AllyariaStringValue("0.02em"),
            new AllyariaStringValue("1.4"),
            new AllyariaStringValue("center"),
            new AllyariaStringValue("underline"),
            new AllyariaStringValue("uppercase"),
            new AllyariaStringValue("baseline"),
            new AllyariaStringValue("0.1em")
        );

        // Act
        var css = sut.ToCssVars("Theme");

        // Assert
        css.Should()
            .Contain("--theme-font-family:Inter")
            .And.Contain("--theme-font-size:16px")
            .And.Contain("--theme-font-style:italic")
            .And.Contain("--theme-font-weight:600")
            .And.Contain("--theme-letter-spacing:0.02em")
            .And.Contain("--theme-line-height:1.4")
            .And.Contain("--theme-text-align:center")
            .And.Contain("--theme-text-decoration:underline")
            .And.Contain("--theme-text-transform:uppercase")
            .And.Contain("--theme-vertical-align:baseline")
            .And.Contain("--theme-word-spacing:0.1em");
    }

    [Fact]
    public void ToCssVars_Should_Normalize_Prefix_By_Trimming_And_Lowercasing_And_Replacing_Spaces_And_Dashes()
    {
        // Arrange
        var sut = new AllyariaTypography(new AllyariaStringValue("Arial"));
        var prefix = "  --My THEME  - - Name--";

        // Act
        var css = sut.ToCssVars(prefix);

        // Assert
        css.Should()
            .Contain("--my-theme-name-font-family:Arial")
            .And.NotContain("  ")
            .And.NotContain("--My THEME");
    }

    [Fact]
    public void ToCssVars_Should_ReturnEmptyString_When_AllPropertiesAreNull()
    {
        // Arrange
        var sut = new AllyariaTypography();

        // Act
        var css = sut.ToCssVars("theme");

        // Assert
        css.Should()
            .BeEmpty();
    }
}
