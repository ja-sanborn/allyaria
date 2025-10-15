using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryTypographyTests
{
    [Fact]
    public void Cascade_Should_OverrideOnlySpecifiedValues_When_OverridesProvided()
    {
        // Arrange
        var original = new AryTypography(
            new AryStringValue("Inter"),
            new AryNumberValue("14px"),
            new AryStringValue("normal"),
            new AryStringValue("400"),
            new AryNumberValue("0.01em"),
            new AryNumberValue("1.2"),
            new AryStringValue("left"),
            new AryStringValue("none"),
            new AryStringValue("solid"),
            new AryStringValue("none"),
            new AryStringValue("baseline")
        );

        var newWeight = new AryStringValue("700");
        var newLineHeight = new AryNumberValue("1.6");

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
        var seed = new AryTypography(
            new AryStringValue("Inter"),
            new AryNumberValue("18px"),
            new AryStringValue("normal"),
            new AryStringValue("500"),
            new AryNumberValue("0.02em"),
            new AryNumberValue("1.4"),
            new AryStringValue("left"),
            new AryStringValue("none"),
            new AryStringValue("solid"),
            new AryStringValue("none"),
            new AryStringValue("baseline")
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
        var fontFamily = new AryStringValue("Inter, Segoe UI, sans-serif");
        var fontSize = new AryNumberValue("16px");
        var fontStyle = new AryStringValue("italic");
        var fontWeight = new AryStringValue("600");
        var letterSpacing = new AryNumberValue("0.15em");
        var lineHeight = new AryNumberValue("1.6");
        var textAlign = new AryStringValue("center");
        var textDecorationLine = new AryStringValue("underline");
        var textDecorationStyle = new AryStringValue("wavy");
        var textTransform = new AryStringValue("uppercase");
        var verticalAlign = new AryStringValue("middle");

        // Act
        var sut = new AryTypography(
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
        var sut = new AryTypography();

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
        var sut = new AryTypography();

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
        var sut = new AryTypography();

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
        var sut = new AryTypography();
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
