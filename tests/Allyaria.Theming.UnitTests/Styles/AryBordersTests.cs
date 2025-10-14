using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryBordersTests
{
    [Fact]
    public void Cascade_Should_NotMutateOriginal_When_Called()
    {
        // Arrange
        var original = new AryBorders(
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px"),
            new AryStringValue("solid"),
            new AryStringValue("dashed"),
            new AryStringValue("double"),
            new AryStringValue("groove"),
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px")
        );

        // Act
        var _ = original.Cascade(new AryNumberValue("9px"));

        // Assert
        original.TopWidth.Value.Should().Be("1px"); // unchanged
    }

    [Fact]
    public void Cascade_Should_OverrideOnlySpecifiedMembers_When_PartialOverridesProvided()
    {
        // Arrange
        var original = new AryBorders(
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px"),
            new AryStringValue("solid"),
            new AryStringValue("dashed"),
            new AryStringValue("double"),
            new AryStringValue("groove"),
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px")
        );

        // Act
        var result = original.Cascade(
            endWidth: new AryNumberValue("10px"),
            bottomStyle: new AryStringValue("ridge"),
            topLeftRadius: new AryNumberValue("8px")
        );

        // Assert
        result.TopWidth.Value.Should().Be("1px"); // preserved
        result.EndWidth.Value.Should().Be("10px"); // overridden
        result.BottomWidth.Value.Should().Be("3px"); // preserved
        result.StartWidth.Value.Should().Be("4px"); // preserved

        ((string)result.TopStyle).Should().Be("solid"); // preserved
        ((string)result.EndStyle).Should().Be("dashed"); // preserved
        ((string)result.BottomStyle).Should().Be("ridge"); // overridden
        ((string)result.StartStyle).Should().Be("groove"); // preserved

        result.TopLeftRadius.Value.Should().Be("8px"); // overridden
        result.TopRightRadius.Value.Should().Be("2px"); // preserved
        result.BottomRightRadius.Value.Should().Be("3px"); // preserved
        result.BottomLeftRadius.Value.Should().Be("4px"); // preserved
    }

    [Fact]
    public void Cascade_Should_ReturnNewInstance_When_NoOverridesProvided()
    {
        // Arrange
        var sut = new AryBorders(
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px"),
            new AryStringValue("solid"),
            new AryStringValue("dashed"),
            new AryStringValue("double"),
            new AryStringValue("groove"),
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px")
        );

        // Act
        var result = sut.Cascade();

        // Assert (record struct equality compares all positional/state members)
        result.Should().Be(sut);
    }

    [Fact]
    public void Constructor_WithExplicitValues_Should_AssignPropertiesCorrectly_When_AllProvided()
    {
        // Arrange
        var topWidth = new AryNumberValue("1px");
        var endWidth = new AryNumberValue("2px");
        var bottomWidth = new AryNumberValue("3px");
        var startWidth = new AryNumberValue("4px");

        var topStyle = new AryStringValue("solid");
        var endStyle = new AryStringValue("dashed");
        var bottomStyle = new AryStringValue("double");
        var startStyle = new AryStringValue("groove");

        var tl = new AryNumberValue(".25rem");
        var tr = new AryNumberValue(".5rem");
        var br = new AryNumberValue("8px");
        var bl = new AryNumberValue("10px");

        // Act
        var sut = new AryBorders(
            topWidth, endWidth, bottomWidth, startWidth,
            topStyle, endStyle, bottomStyle, startStyle,
            tl, tr, br, bl
        );

        // Assert
        sut.TopWidth.Value.Should().Be("1px");
        sut.EndWidth.Value.Should().Be("2px");
        sut.BottomWidth.Value.Should().Be("3px");
        sut.StartWidth.Value.Should().Be("4px");

        ((string)sut.TopStyle).Should().Be("solid");
        ((string)sut.EndStyle).Should().Be("dashed");
        ((string)sut.BottomStyle).Should().Be("double");
        ((string)sut.StartStyle).Should().Be("groove");

        sut.TopLeftRadius.Value.Should().Be("0.25rem");
        sut.TopRightRadius.Value.Should().Be("0.5rem");
        sut.BottomRightRadius.Value.Should().Be("8px");
        sut.BottomLeftRadius.Value.Should().Be("10px");
    }

    [Fact]
    public void FocusWidth_Should_Be2px_When_LargestWidthLessThan2()
    {
        // Arrange
        var sut = new AryBorders(
            new AryNumberValue("1.25px"),
            new AryNumberValue("1.5px"),
            new AryNumberValue("0px"),
            new AryNumberValue("1.99px")
        );

        // Act
        var focus = sut.FocusWidth;

        // Assert
        focus.Value.Should().Be("2px");
        focus.Number.Should().Be(2.0);
    }

    [Theory]
    [InlineData("2px", "4px")]
    [InlineData("5px", "7px")]
    [InlineData("2", "4px")]
    public void FocusWidth_Should_BeLargestPlus2px_When_LargestWidthAtLeast2(string largestInput, string expected)
    {
        // Arrange: put the largest on StartWidth, mix others smaller
        var sut = new AryBorders(
            new AryNumberValue("1px"),
            new AryNumberValue("1.5px"),
            new AryNumberValue("0px"),
            new AryNumberValue(largestInput)
        );

        // Act
        var focus = sut.FocusWidth;

        // Assert
        focus.Value.Should().Be(expected);
    }

    [Fact]
    public void FocusWidth_Should_DisregardUnits_When_WidthsHaveDifferentUnits()
    {
        // Arrange: Numbers are 1 (rem), 50 (%), 3 (px), 2 (em) → largest=50 → focus=52px
        var sut = new AryBorders(
            new AryNumberValue("1rem"),
            new AryNumberValue("50%"),
            new AryNumberValue("3px"),
            new AryNumberValue("2em")
        );

        // Act
        var focus = sut.FocusWidth;

        // Assert
        focus.Value.Should().Be("52px");
        focus.Number.Should().Be(52.0);
    }

    [Fact]
    public void FocusWidth_Should_UseMaxAcrossAllSides_When_WidthsDiffer()
    {
        // Arrange
        var sut = new AryBorders(
            new AryNumberValue("3px"),
            new AryNumberValue("1px"),
            new AryNumberValue("4px"),
            new AryNumberValue("2px")
        );

        // Act
        var focus = sut.FocusWidth;

        // Assert (largest = 4 → 6px)
        focus.Value.Should().Be("6px");
        focus.Number.Should().Be(6.0);
    }

    [Fact]
    public void FromSingle_Should_CreateUniformBorders_When_ValidInputs()
    {
        // Arrange
        var width = new AryNumberValue("2px");
        var style = new AryStringValue("solid");
        var radius = new AryNumberValue(".25rem");

        // Act
        var sut = AryBorders.FromSingle(width, style, radius);

        // Assert
        sut.TopWidth.Value.Should().Be("2px");
        sut.EndWidth.Value.Should().Be("2px");
        sut.BottomWidth.Value.Should().Be("2px");
        sut.StartWidth.Value.Should().Be("2px");

        ((string)sut.TopStyle).Should().Be("solid");
        ((string)sut.EndStyle).Should().Be("solid");
        ((string)sut.BottomStyle).Should().Be("solid");
        ((string)sut.StartStyle).Should().Be("solid");

        sut.TopLeftRadius.Value.Should().Be("0.25rem");
        sut.TopRightRadius.Value.Should().Be("0.25rem");
        sut.BottomRightRadius.Value.Should().Be("0.25rem");
        sut.BottomLeftRadius.Value.Should().Be("0.25rem");
    }

    [Fact]
    public void FromSymmetric_Should_MapHorizontalAndVerticalCorrectly_When_ValidInputs()
    {
        // Arrange
        var widthH = new AryNumberValue("4px");
        var widthV = new AryNumberValue("1px");
        var styleH = new AryStringValue("dotted");
        var styleV = new AryStringValue("solid");
        var radiusTop = new AryNumberValue("3px");
        var radiusBottom = new AryNumberValue("6px");

        // Act
        var sut = AryBorders.FromSymmetric(widthH, widthV, styleH, styleV, radiusTop, radiusBottom);

        // Assert
        // Widths: vertical for top/bottom, horizontal for start/end
        sut.TopWidth.Value.Should().Be("1px");
        sut.BottomWidth.Value.Should().Be("1px");
        sut.StartWidth.Value.Should().Be("4px");
        sut.EndWidth.Value.Should().Be("4px");

        // Styles: vertical for top/bottom, horizontal for start/end
        ((string)sut.TopStyle).Should().Be("solid");
        ((string)sut.BottomStyle).Should().Be("solid");
        ((string)sut.StartStyle).Should().Be("dotted");
        ((string)sut.EndStyle).Should().Be("dotted");

        // Radii: top pair vs bottom pair
        sut.TopLeftRadius.Value.Should().Be("3px");
        sut.TopRightRadius.Value.Should().Be("3px");
        sut.BottomLeftRadius.Value.Should().Be("6px");
        sut.BottomRightRadius.Value.Should().Be("6px");
    }

    [Fact]
    public void NewDefaultConstructor_Should_SetMembersFromStyleDefaults_When_CalledWithoutArgs()
    {
        // Arrange & Act
        var sut = new AryBorders();

        // Assert (we don’t assert specific defaults, just that they are non-empty normalized values)
        sut.TopWidth.Value.Should().NotBeNullOrWhiteSpace();
        sut.EndWidth.Value.Should().NotBeNullOrWhiteSpace();
        sut.BottomWidth.Value.Should().NotBeNullOrWhiteSpace();
        sut.StartWidth.Value.Should().NotBeNullOrWhiteSpace();

        ((string)sut.TopStyle).Should().NotBeNullOrWhiteSpace();
        ((string)sut.EndStyle).Should().NotBeNullOrWhiteSpace();
        ((string)sut.BottomStyle).Should().NotBeNullOrWhiteSpace();
        ((string)sut.StartStyle).Should().NotBeNullOrWhiteSpace();

        sut.TopLeftRadius.Value.Should().NotBeNullOrWhiteSpace();
        sut.TopRightRadius.Value.Should().NotBeNullOrWhiteSpace();
        sut.BottomRightRadius.Value.Should().NotBeNullOrWhiteSpace();
        sut.BottomLeftRadius.Value.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ToCss_Should_EmitAllDeclarations_When_NoPrefixAndCustomValues()
    {
        // Arrange
        var width = new AryNumberValue("1px");
        var style = new AryStringValue("solid");
        var radius = new AryNumberValue("4px");
        var sut = AryBorders.FromSingle(width, style, radius);

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().Contain("border-top-width").And.Contain("1px");
        css.Should().Contain("border-inline-end-width").And.Contain("1px");
        css.Should().Contain("border-bottom-width").And.Contain("1px");
        css.Should().Contain("border-inline-start-width").And.Contain("1px");

        css.Should().Contain("border-top-style").And.Contain("solid");
        css.Should().Contain("border-inline-end-style").And.Contain("solid");
        css.Should().Contain("border-bottom-style").And.Contain("solid");
        css.Should().Contain("border-inline-start-style").And.Contain("solid");

        css.Should().Contain("border-top-left-radius").And.Contain("4px");
        css.Should().Contain("border-top-right-radius").And.Contain("4px");
        css.Should().Contain("border-bottom-left-radius").And.Contain("4px");
        css.Should().Contain("border-bottom-right-radius").And.Contain("4px");

        css.Should().NotContain("--"); // no CSS variables when no prefix is provided
    }

    [Theory]
    [InlineData(null)]
    [InlineData("   ")]
    public void ToCss_Should_NotUseCssVariables_When_PrefixIsNullOrWhitespace(string? prefix)
    {
        // Arrange
        var sut = AryBorders.FromSingle(
            new AryNumberValue("1px"), new AryStringValue("solid"), new AryNumberValue("4px")
        );

        // Act
        var css = sut.ToCss(prefix);

        // Assert
        css.Should().Contain("border-top-width").And.NotContain("--border-top-width");
        css.Should().Contain("border-inline-end-width").And.NotContain("--border-inline-end-width");
        css.Should().Contain("border-bottom-width").And.NotContain("--border-bottom-width");
        css.Should().Contain("border-inline-start-width").And.NotContain("--border-inline-start-width");
    }

    [Fact]
    public void ToCss_Should_UseFocusWidthAndDashedStyle_When_IsFocusTrue()
    {
        // Arrange
        // Largest width = 3px -> FocusWidth = 3 + 2 = 5px (see FocusWidth logic)
        var sut = new AryBorders(
            new AryNumberValue("1px"),
            new AryNumberValue("3px"),
            new AryNumberValue("2px"),
            new AryNumberValue("0px"),
            new AryStringValue("solid"),
            new AryStringValue("double"),
            new AryStringValue("groove"),
            new AryStringValue("ridge"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px"),
            new AryNumberValue("5px")
        );

        // Act
        var css = sut.ToCss(isFocus: true);

        // Assert (all widths should be the computed FocusWidth)
        css.Should().Contain("border-top-width").And.Contain("5px");
        css.Should().Contain("border-inline-end-width").And.Contain("5px");
        css.Should().Contain("border-bottom-width").And.Contain("5px");
        css.Should().Contain("border-inline-start-width").And.Contain("5px");

        // Assert (all styles should be dashed regardless of input)
        css.Should().Contain("border-top-style").And.Contain("dashed");
        css.Should().Contain("border-inline-end-style").And.Contain("dashed");
        css.Should().Contain("border-bottom-style").And.Contain("dashed");
        css.Should().Contain("border-inline-start-style").And.Contain("dashed");

        // Radii are not affected by focus flag
        css.Should().Contain("border-top-left-radius").And.Contain("2px");
        css.Should().Contain("border-top-right-radius").And.Contain("3px");
        css.Should().Contain("border-bottom-right-radius").And.Contain("4px");
        css.Should().Contain("border-bottom-left-radius").And.Contain("5px");
    }

    [Fact]
    public void ToCss_Should_UseNormalizedCssVarNames_When_PrefixProvided()
    {
        // Arrange
        var sut = AryBorders.FromSingle(
            new AryNumberValue("2px"), new AryStringValue("dotted"), new AryNumberValue("6px")
        );

        var prefix = " My__Prefix  v1 "; // underscores/spaces -> hyphens, lowercased, trimmed

        // Act
        var css = sut.ToCss(prefix);

        // Assert
        css.Should().Contain("--my-prefix-v1-border-top-width");
        css.Should().Contain("--my-prefix-v1-border-inline-end-width");
        css.Should().Contain("--my-prefix-v1-border-bottom-width");
        css.Should().Contain("--my-prefix-v1-border-inline-start-width");

        css.Should().Contain("--my-prefix-v1-border-top-style");
        css.Should().Contain("--my-prefix-v1-border-inline-end-style");
        css.Should().Contain("--my-prefix-v1-border-bottom-style");
        css.Should().Contain("--my-prefix-v1-border-inline-start-style");

        css.Should().Contain("--my-prefix-v1-border-top-left-radius");
        css.Should().Contain("--my-prefix-v1-border-top-right-radius");
        css.Should().Contain("--my-prefix-v1-border-bottom-left-radius");
        css.Should().Contain("--my-prefix-v1-border-bottom-right-radius");
    }
}
