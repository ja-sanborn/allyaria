namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class TypographyTests
{
    [Fact]
    public void Cascade_Should_OverrideOnlySpecifiedValues_When_OverridesProvided()
    {
        // Arrange
        var original = new Typography(
            new ThemeString("Inter"),
            new ThemeNumber("14px"),
            new ThemeString("normal"),
            new ThemeString("400"),
            new ThemeNumber("0.01em"),
            new ThemeNumber("1.2"),
            new ThemeString("left"),
            new ThemeString("none"),
            new ThemeString("solid"),
            new ThemeString("none"),
            new ThemeString("baseline")
        );

        var newWeight = new ThemeString("700");
        var newLineHeight = new ThemeNumber("1.6");

        // Act
        var sut = original.Cascade(fontWeight: newWeight, lineHeight: newLineHeight);

        // Assert (only the two specified values change)
        sut.FontWeight.Should().Be(newWeight);
        sut.LineHeight.Should().Be(newLineHeight);

        sut.FontFamily.Should().Be(original.FontFamily);
        sut.FontSize.Should().Be(original.FontSize);
        sut.FontStyle.Should().Be(original.FontStyle);
        sut.LetterSpacing.Should().Be(original.LetterSpacing);
        sut.TextAlign.Should().Be(original.TextAlign);
        sut.TextDecorationLine.Should().Be(original.TextDecorationLine);
        sut.TextDecorationStyle.Should().Be(original.TextDecorationStyle);
        sut.TextTransform.Should().Be(original.TextTransform);
        sut.VerticalAlign.Should().Be(original.VerticalAlign);
    }

    [Fact]
    public void Cascade_Should_RetainExistingValues_When_OverridesAreNull()
    {
        // Arrange
        var seed = new Typography(
            new ThemeString("Inter"),
            new ThemeNumber("18px"),
            new ThemeString("normal"),
            new ThemeString("500"),
            new ThemeNumber("0.02em"),
            new ThemeNumber("1.4"),
            new ThemeString("left"),
            new ThemeString("none"),
            new ThemeString("solid"),
            new ThemeString("none"),
            new ThemeString("baseline")
        );

        // Act
        var sut = seed.Cascade();

        // Assert
        sut.Should().BeEquivalentTo(seed);
    }

    [Fact]
    public void Ctor_Custom_Should_Use_ProvidedValues_When_Specified()
    {
        // Arrange
        var fontFamily = new ThemeString("Inter, Segoe UI, sans-serif");
        var fontSize = new ThemeNumber("16px");
        var fontStyle = new ThemeString("italic");
        var fontWeight = new ThemeString("600");
        var letterSpacing = new ThemeNumber("0.15em");
        var lineHeight = new ThemeNumber("1.6");
        var textAlign = new ThemeString("center");
        var textDecorationLine = new ThemeString("underline");
        var textDecorationStyle = new ThemeString("wavy");
        var textTransform = new ThemeString("uppercase");
        var verticalAlign = new ThemeString("middle");

        // Act
        var sut = new Typography(
            fontFamily,
            fontSize,
            fontStyle,
            fontWeight,
            letterSpacing,
            lineHeight,
            textAlign,
            textDecorationLine,
            textDecorationStyle,
            textTransform,
            verticalAlign
        );

        // Assert
        sut.FontFamily.Should().Be(fontFamily);
        sut.FontSize.Should().Be(fontSize);
        sut.FontStyle.Should().Be(fontStyle);
        sut.FontWeight.Should().Be(fontWeight);
        sut.LetterSpacing.Should().Be(letterSpacing);
        sut.LineHeight.Should().Be(lineHeight);
        sut.TextAlign.Should().Be(textAlign);
        sut.TextDecorationLine.Should().Be(textDecorationLine);
        sut.TextDecorationStyle.Should().Be(textDecorationStyle);
        sut.TextTransform.Should().Be(textTransform);
        sut.VerticalAlign.Should().Be(verticalAlign);
    }

    [Fact]
    public void Ctor_Defaults_Should_Use_StyleDefaults_When_NotProvided()
    {
        // Arrange & Act
        var sut = new Typography();

        // Assert
        sut.FontFamily.Value.Should().Be(StyleDefaults.FontFamily.Value);
        sut.FontSize.Value.Should().Be(StyleDefaults.FontSize.Value);
        sut.FontStyle.Value.Should().Be(StyleDefaults.FontStyle.Value);
        sut.FontWeight.Value.Should().Be(StyleDefaults.FontWeight.Value);
        sut.LetterSpacing.Value.Should().Be(StyleDefaults.LetterSpacing.Value);
        sut.LineHeight.Value.Should().Be(StyleDefaults.LineHeight.Value);
        sut.TextAlign.Value.Should().Be(StyleDefaults.TextAlign.Value);
        sut.TextDecorationLine.Value.Should().Be(StyleDefaults.TextDecorationLine.Value);
        sut.TextDecorationStyle.Value.Should().Be(StyleDefaults.TextDecorationStyle.Value);
        sut.TextTransform.Value.Should().Be(StyleDefaults.TextTransform.Value);
        sut.VerticalAlign.Value.Should().Be(StyleDefaults.VerticalAlign.Value);
    }

    [Fact]
    public void ToCss_Should_EmitAllDeclarations_When_PrefixIsEmptyString()
    {
        // Arrange
        var sut = new Typography();

        // Act
        var css = sut.ToCss(string.Empty);

        // Assert
        css.Should().NotBeNullOrWhiteSpace();
        css.Count(c => c == ';').Should().Be(11);

        css.Should().Contain("font-family:")
            .And.Contain("vertical-align:");
    }

    [Fact]
    public void ToCss_Should_EmitAllDeclarations_When_PrefixIsNull()
    {
        // Arrange
        var sut = new Typography();

        // Act
        var css = sut.ToCss(null);

        // Assert
        css.Should().NotBeNullOrWhiteSpace();

        // 11 typography declarations expected
        css.Count(c => c == ';').Should().Be(11);

        // Ensure standard property names are present when no prefix is provided
        css.Should().Contain("font-family:")
            .And.Contain("font-size:")
            .And.Contain("font-style:")
            .And.Contain("font-weight:")
            .And.Contain("letter-spacing:")
            .And.Contain("line-height:")
            .And.Contain("text-align:")
            .And.Contain("text-decoration-line:")
            .And.Contain("text-decoration-style:")
            .And.Contain("text-transform:")
            .And.Contain("vertical-align:");
    }

    [Fact]
    public void ToCss_Should_EmitCustomProperties_When_PrefixProvided()
    {
        // Arrange
        var sut = new Typography();
        var prefix = "TyPo Graphy"; // mixed case + whitespace to exercise normalization

        // Act
        var css = sut.ToCss(prefix);

        // Assert
        css.Should().NotBeNullOrWhiteSpace();
        css.Count(c => c == ';').Should().Be(11);

        // Expect CSS custom properties using the normalized, lower-cased prefix with spaces -> hyphens
        css.Should().Contain("--typo-graphy-font-family:")
            .And.Contain("--typo-graphy-font-size:")
            .And.Contain("--typo-graphy-font-style:")
            .And.Contain("--typo-graphy-font-weight:")
            .And.Contain("--typo-graphy-letter-spacing:")
            .And.Contain("--typo-graphy-line-height:")
            .And.Contain("--typo-graphy-text-align:")
            .And.Contain("--typo-graphy-text-decoration-line:")
            .And.Contain("--typo-graphy-text-decoration-style:")
            .And.Contain("--typo-graphy-text-transform:")
            .And.Contain("--typo-graphy-vertical-align:");
    }
}
