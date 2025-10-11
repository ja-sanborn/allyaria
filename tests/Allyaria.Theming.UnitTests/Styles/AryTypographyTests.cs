using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryTypographyTests
{
    [Fact]
    public void Cascade_Should_ReturnNewInstanceWithOverrides_When_PartialOverridesProvided()
    {
        // Arrange
        var baseTypography = new AryTypography(
            S("Inter, 'Segoe UI', sans-serif"),
            N("16px"),
            S("normal"),
            S("400"),
            N("0.02em"),
            N("24px"),
            S("start"),
            S("none"),
            S("solid"),
            S("none"),
            S("baseline")
        );

        var newFamily = S("Georgia, serif");
        var newWeight = S("600");
        var newAlign = S("center");

        // Act
        var sut = baseTypography.Cascade(newFamily, fontWeight: newWeight, textAlign: newAlign);

        // Assert
        sut.FontFamily.Should().BeEquivalentTo(newFamily);
        sut.FontWeight.Should().BeEquivalentTo(newWeight);
        sut.TextAlign.Should().BeEquivalentTo(newAlign);
        sut.FontSize.Should().BeEquivalentTo(baseTypography.FontSize);
        sut.FontStyle.Should().BeEquivalentTo(baseTypography.FontStyle);
        sut.LetterSpacing.Should().BeEquivalentTo(baseTypography.LetterSpacing);
        sut.LineHeight.Should().BeEquivalentTo(baseTypography.LineHeight);
        sut.TextDecorationLine.Should().BeEquivalentTo(baseTypography.TextDecorationLine);
        sut.TextDecorationStyle.Should().BeEquivalentTo(baseTypography.TextDecorationStyle);
        sut.TextTransform.Should().BeEquivalentTo(baseTypography.TextTransform);
        sut.VerticalAlign.Should().BeEquivalentTo(baseTypography.VerticalAlign);
        baseTypography.FontFamily.Should().BeEquivalentTo(S("Inter, 'Segoe UI', sans-serif"));
        baseTypography.TextAlign.Should().BeEquivalentTo(S("start"));
    }

    [Fact]
    public void Constructor_Should_UseDefaultValues_When_UsingParameterlessCtor()
    {
        // Arrange + Act
        var sut = new AryTypography();

        // Assert
        sut.FontFamily.Should().Be(StyleDefaults.FontFamily);
        sut.FontSize.Should().Be(StyleDefaults.FontSize);
        sut.FontStyle.Should().Be(StyleDefaults.FontStyle);
        sut.FontWeight.Should().Be(StyleDefaults.FontWeight);
        sut.LetterSpacing.Should().Be(StyleDefaults.LetterSpacing);
        sut.LineHeight.Should().Be(StyleDefaults.LineHeight);
        sut.TextAlign.Should().Be(StyleDefaults.TextAlign);
        sut.TextDecorationLine.Should().Be(StyleDefaults.TextDecorationLine);
        sut.TextDecorationStyle.Should().Be(StyleDefaults.TextDecorationStyle);
        sut.TextTransform.Should().Be(StyleDefaults.TextTransform);
        sut.VerticalAlign.Should().Be(StyleDefaults.VerticalAlign);
    }

    private static AryNumberValue N(string v) => new(v);

    [Fact]
    public void RecordEquality_Should_TreatInstancesAsEqual_When_AllMembersMatch()
    {
        // Arrange
        var a = new AryTypography(
            S("Inter"),
            N("16px"),
            S("normal"),
            S("400"),
            N("0.02em"),
            N("24px"),
            S("start"),
            S("none"),
            S("solid"),
            S("none"),
            S("baseline")
        );

        var b = new AryTypography(
            S("Inter"),
            N("16px"),
            S("normal"),
            S("400"),
            N("0.02em"),
            N("24px"),
            S("start"),
            S("none"),
            S("solid"),
            S("none"),
            S("baseline")
        );

        // Act
        var areEqual = a.Equals(b);

        // Assert
        areEqual.Should().BeTrue();
        (a == b).Should().BeTrue();
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    private static AryStringValue S(string v) => new(v);

    [Fact]
    public void ToCss_Should_EmitAllProperties_When_AllValuesProvided()
    {
        // Arrange
        var sut = new AryTypography(
            S("Inter,'Segoe UI',sans-serif"),
            N("16px"),
            S("italic"),
            S("700"),
            N("0.02em"),
            N("1.5"),
            S("center"),
            S("underline"),
            S("wavy"),
            S("uppercase"),
            S("middle")
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().Be(
            "font-family:Inter,'Segoe UI',sans-serif;" +
            "font-size:16px;" +
            "font-style:italic;" +
            "font-weight:700;" +
            "letter-spacing:0.02em;" +
            "line-height:1.5;" +
            "text-align:center;" +
            "text-decoration-line:underline;" +
            "text-decoration-style:wavy;" +
            "text-transform:uppercase;" +
            "vertical-align:middle;"
        );
    }

    [Fact]
    public void ToCss_Should_PrefixVariablesAndNormalizePrefix_When_VarPrefixProvided()
    {
        // Arrange
        var sut = new AryTypography(
            S("Inter"),
            N("14px"),
            S("normal"),
            S("500"),
            N("0.01em"),
            N("20px"),
            S("left"),
            S("none"),
            S("solid"),
            S("none"),
            S("baseline")
        );

        var varPrefix = "  My--Theme   Token  ";

        // Act
        var css = sut.ToCss(varPrefix);

        // Assert
        var expectedPrefix = "--my-theme-token-";

        css.Should().Be(
            $"{expectedPrefix}font-family:Inter;" +
            $"{expectedPrefix}font-size:14px;" +
            $"{expectedPrefix}font-style:normal;" +
            $"{expectedPrefix}font-weight:500;" +
            $"{expectedPrefix}letter-spacing:0.01em;" +
            $"{expectedPrefix}line-height:20px;" +
            $"{expectedPrefix}text-align:left;" +
            $"{expectedPrefix}text-decoration-line:none;" +
            $"{expectedPrefix}text-decoration-style:solid;" +
            $"{expectedPrefix}text-transform:none;" +
            $"{expectedPrefix}vertical-align:baseline;"
        );
    }

    [Fact]
    public void WithExpression_Should_ProduceNewInstance_When_ChangingSingleProperty()
    {
        // Arrange
        var original = new AryTypography(
            S("Inter"),
            N("16px"),
            S("normal"),
            S("400"),
            N("0.02em"),
            N("24px"),
            S("start"),
            S("none"),
            S("solid"),
            S("none"),
            S("baseline")
        );

        // Act
        var sut = original with
        {
            FontWeight = S("600")
        };

        // Assert
        sut.FontWeight.Should().BeEquivalentTo(S("600"));

        (sut with
            {
                FontWeight = original.FontWeight
            })
            .Should().BeEquivalentTo(original);
    }
}
