using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryBordersTests
{
    [Fact]
    public void Cascade_Should_ApplyOnlyProvidedOverrides_When_PartialOverridesProvided()
    {
        // Arrange
        var original = new AryBorders(
            new AryNumberValue("1px"),
            new AryNumberValue("1px"),
            new AryNumberValue("1px"),
            new AryNumberValue("1px"),
            BorderStyle.Solid,
            BorderStyle.Solid,
            BorderStyle.Solid,
            BorderStyle.Solid,
            new AryNumberValue("2px"),
            new AryNumberValue("2px"),
            new AryNumberValue("2px"),
            new AryNumberValue("2px")
        );

        // Act
        var sut = original.Cascade(
            new AryNumberValue("10px"),
            leftStyle: BorderStyle.Dashed,
            bottomLeftRadius: new AryNumberValue("12px")
        );

        // Assert
        sut.TopWidth.Should().Be(new AryNumberValue("10px"));
        sut.RightWidth.Should().Be(original.RightWidth);
        sut.BottomWidth.Should().Be(original.BottomWidth);
        sut.LeftWidth.Should().Be(original.LeftWidth);

        sut.TopStyle.Should().Be(original.TopStyle);
        sut.RightStyle.Should().Be(original.RightStyle);
        sut.BottomStyle.Should().Be(original.BottomStyle);
        sut.LeftStyle.Should().Be(BorderStyle.Dashed);

        sut.TopLeftRadius.Should().Be(original.TopLeftRadius);
        sut.TopRightRadius.Should().Be(original.TopRightRadius);
        sut.BottomRightRadius.Should().Be(original.BottomRightRadius);
        sut.BottomLeftRadius.Should().Be(new AryNumberValue("12px"));
    }

    [Fact]
    public void Cascade_Should_PreserveExistingValues_When_NoOverridesProvided()
    {
        // Arrange
        var original = new AryBorders(
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px"),
            BorderStyle.Solid,
            BorderStyle.Dashed,
            BorderStyle.Dotted,
            BorderStyle.None,
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px")
        );

        // Act
        var sut = original.Cascade();

        // Assert
        sut.Should().Be(original);
    }

    [Fact]
    public void Cascade_Should_ReturnNewInstance_When_OverridesApplied()
    {
        // Arrange
        var original = AryBorders.FromSingle(Sizing.Thin, BorderStyle.Solid, Sizing.Size2);
        var overrideTopStyle = BorderStyle.Dashed;

        // Act
        var result = original.Cascade(topStyle: overrideTopStyle);

        // Assert
        result.Should().NotBe(original);
        result.TopStyle.Should().Be(overrideTopStyle);

        // unchanged samples
        result.RightStyle.Should().Be(original.RightStyle);
        result.BottomLeftRadius.Should().Be(original.BottomLeftRadius);
    }

    [Fact]
    public void Ctor_Should_ApplyOverrides_When_ValuesProvided()
    {
        // Arrange
        var wTop = new AryNumberValue("3px");
        var wRight = new AryNumberValue("2px");
        var wBottom = new AryNumberValue("1px");
        var wLeft = new AryNumberValue("0.5rem");

        var sTop = new AryStringValue("dashed");
        var sRight = BorderStyle.Dotted;
        var sBottom = BorderStyle.Double;
        var sLeft = BorderStyle.None;

        var rTl = new AryNumberValue("4px");
        var rTr = new AryNumberValue("6px");
        var rBr = new AryNumberValue("8px");
        var rBl = new AryNumberValue("10px");

        // Act
        var sut = new AryBorders(
            wTop, wRight, wBottom, wLeft,
            sTop, sRight, sBottom, sLeft,
            rTl, rTr, rBr, rBl
        );

        // Assert
        sut.TopWidth.Should().Be(wTop);
        sut.RightWidth.Should().Be(wRight);
        sut.BottomWidth.Should().Be(wBottom);
        sut.LeftWidth.Should().Be(wLeft);

        sut.TopStyle.Should().Be(sTop);
        sut.RightStyle.Should().Be(sRight);
        sut.BottomStyle.Should().Be(sBottom);
        sut.LeftStyle.Should().Be(sLeft);

        sut.TopLeftRadius.Should().Be(rTl);
        sut.TopRightRadius.Should().Be(rTr);
        sut.BottomRightRadius.Should().Be(rBr);
        sut.BottomLeftRadius.Should().Be(rBl);
    }

    [Fact]
    public void Ctor_Should_UseDefaults_When_AllParametersAreNull()
    {
        // Arrange & Act
        var sut = new AryBorders();

        // Assert
        sut.TopWidth.Should().Be(StyleDefaults.BorderWidth);
        sut.RightWidth.Should().Be(StyleDefaults.BorderWidth);
        sut.BottomWidth.Should().Be(StyleDefaults.BorderWidth);
        sut.LeftWidth.Should().Be(StyleDefaults.BorderWidth);

        sut.TopStyle.Should().Be(StyleDefaults.BorderStyle);
        sut.RightStyle.Should().Be(StyleDefaults.BorderStyle);
        sut.BottomStyle.Should().Be(StyleDefaults.BorderStyle);
        sut.LeftStyle.Should().Be(StyleDefaults.BorderStyle);

        sut.TopLeftRadius.Should().Be(StyleDefaults.BorderRadius);
        sut.TopRightRadius.Should().Be(StyleDefaults.BorderRadius);
        sut.BottomRightRadius.Should().Be(StyleDefaults.BorderRadius);
        sut.BottomLeftRadius.Should().Be(StyleDefaults.BorderRadius);
    }

    [Fact]
    public void FromSingle_Should_CreateUniformBorders_When_SameInputsProvided()
    {
        // Arrange
        var width = Sizing.Thin;
        var style = BorderStyle.Dotted;
        var radius = Sizing.Size3;

        // Act
        var sut = AryBorders.FromSingle(width, style, radius);

        // Assert
        sut.TopWidth.Should().Be(width);
        sut.RightWidth.Should().Be(width);
        sut.BottomWidth.Should().Be(width);
        sut.LeftWidth.Should().Be(width);

        sut.TopStyle.Should().Be(style);
        sut.RightStyle.Should().Be(style);
        sut.BottomStyle.Should().Be(style);
        sut.LeftStyle.Should().Be(style);

        sut.TopLeftRadius.Should().Be(radius);
        sut.TopRightRadius.Should().Be(radius);
        sut.BottomRightRadius.Should().Be(radius);
        sut.BottomLeftRadius.Should().Be(radius);
    }

    [Fact]
    public void FromSymmetric_Should_AssignHorizontalVerticalAndCornerPairs_Correctly()
    {
        // Arrange
        var wH = new AryNumberValue("5px");
        var wV = new AryNumberValue("7px");
        var sH = BorderStyle.Double;
        var sV = BorderStyle.Groove;
        var rTop = new AryNumberValue("3px");
        var rBottom = new AryNumberValue("9px");

        // Act
        var sut = AryBorders.FromSymmetric(wH, wV, sH, sV, rTop, rBottom);

        // Assert
        sut.LeftWidth.Should().Be(wH);
        sut.RightWidth.Should().Be(wH);
        sut.TopWidth.Should().Be(wV);
        sut.BottomWidth.Should().Be(wV);
        sut.LeftStyle.Should().Be(sH);
        sut.RightStyle.Should().Be(sH);
        sut.TopStyle.Should().Be(sV);
        sut.BottomStyle.Should().Be(sV);
        sut.TopLeftRadius.Should().Be(rTop);
        sut.TopRightRadius.Should().Be(rTop);
        sut.BottomLeftRadius.Should().Be(rBottom);
        sut.BottomRightRadius.Should().Be(rBottom);
    }

    [Fact]
    public void ToCss_Should_ApplyNormalizedVariablePrefix_When_PrefixProvided()
    {
        // Arrange
        var sut = AryBorders.FromSingle(Sizing.Thin, BorderStyle.Solid, Sizing.Size2);
        var rawPrefix = "  My  --Cool__Prefix  ";

        // Act
        var css = sut.ToCss(rawPrefix);

        // Assert
        var prefix = "--my-cool__prefix-";

        var expected = $"{prefix}border-top-width:1px;" +
            $"{prefix}border-right-width:1px;" +
            $"{prefix}border-bottom-width:1px;" +
            $"{prefix}border-left-width:1px;" +
            $"{prefix}border-top-style:solid;" +
            $"{prefix}border-right-style:solid;" +
            $"{prefix}border-bottom-style:solid;" +
            $"{prefix}border-left-style:solid;" +
            $"{prefix}border-top-left-radius:8px;" +
            $"{prefix}border-top-right-radius:8px;" +
            $"{prefix}border-bottom-left-radius:8px;" +
            $"{prefix}border-bottom-right-radius:8px;";

        css.Should().Be(expected);
    }

    [Fact]
    public void ToCss_Should_EmitAllDeclarations_InCorrectOrder_When_NoPrefix()
    {
        // Arrange
        var sut = AryBorders.FromSingle(Sizing.Thin, BorderStyle.Solid, Sizing.Size2);

        // Act
        var css = sut.ToCss();

        // Assert
        var expected = "border-top-width:1px;" +
            "border-right-width:1px;" +
            "border-bottom-width:1px;" +
            "border-left-width:1px;" +
            "border-top-style:solid;" +
            "border-right-style:solid;" +
            "border-bottom-style:solid;" +
            "border-left-style:solid;" +
            "border-top-left-radius:8px;" +
            "border-top-right-radius:8px;" +
            "border-bottom-left-radius:8px;" +
            "border-bottom-right-radius:8px;";

        css.Should().Be(expected);
    }
}
