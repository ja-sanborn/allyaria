using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ArySpacingTests
{
    [Fact]
    public void Cascade_Should_OverrideSpecified_And_PreserveUnspecified()
    {
        // Arrange
        var original = new ArySpacing(
            new AryNumberValue("1px"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px"),
            new AryNumberValue("5px"),
            new AryNumberValue("6px"),
            new AryNumberValue("7px"),
            new AryNumberValue("8px")
        );

        // Act
        var sut = original.Cascade(
            new AryNumberValue("10px"),
            null, // preserve
            new AryNumberValue("30px"),
            null, // preserve
            null, // preserve
            new AryNumberValue("60px"),
            null, // preserve
            new AryNumberValue("80px")
        );

        // Assert (overridden)
        sut.MarginTop.Value.Should().Be("10px");
        sut.MarginBottom.Value.Should().Be("30px");
        sut.PaddingEnd.Value.Should().Be("60px");
        sut.PaddingStart.Value.Should().Be("80px");

        // Assert (preserved)
        sut.MarginEnd.Value.Should().Be("2px");
        sut.MarginStart.Value.Should().Be("4px");
        sut.PaddingTop.Value.Should().Be("5px");
        sut.PaddingBottom.Value.Should().Be("7px");
    }

    [Fact]
    public void Ctor_Should_AssignProvidedValues_When_AllEightValuesAreSupplied()
    {
        // Arrange
        var mt = new AryNumberValue("1px");
        var me = new AryNumberValue("2px");
        var mb = new AryNumberValue("3px");
        var ms = new AryNumberValue("4px");
        var pt = new AryNumberValue("5px");
        var pe = new AryNumberValue("6px");
        var pb = new AryNumberValue("7px");
        var ps = new AryNumberValue("8px");

        // Act
        var sut = new ArySpacing(mt, me, mb, ms, pt, pe, pb, ps);

        // Assert
        sut.MarginTop.Value.Should().Be("1px");
        sut.MarginEnd.Value.Should().Be("2px");
        sut.MarginBottom.Value.Should().Be("3px");
        sut.MarginStart.Value.Should().Be("4px");
        sut.PaddingTop.Value.Should().Be("5px");
        sut.PaddingEnd.Value.Should().Be("6px");
        sut.PaddingBottom.Value.Should().Be("7px");
        sut.PaddingStart.Value.Should().Be("8px");
    }

    [Fact]
    public void Ctor_Should_NotThrow_When_UsingDefaults()
    {
        // Arrange & Act
        var act = () => new ArySpacing();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void FromSingle_Should_ApplySameMarginAndPaddingToAllSides()
    {
        // Arrange
        var margin = new AryNumberValue("12px");
        var padding = new AryNumberValue("0.5rem");

        // Act
        var sut = ArySpacing.FromSingle(margin, padding);

        // Assert
        sut.MarginTop.Value.Should().Be("12px");
        sut.MarginEnd.Value.Should().Be("12px");
        sut.MarginBottom.Value.Should().Be("12px");
        sut.MarginStart.Value.Should().Be("12px");

        sut.PaddingTop.Value.Should().Be("0.5rem");
        sut.PaddingEnd.Value.Should().Be("0.5rem");
        sut.PaddingBottom.Value.Should().Be("0.5rem");
        sut.PaddingStart.Value.Should().Be("0.5rem");
    }

    [Fact]
    public void FromSymmetric_Should_MapHorizontalAndVerticalCorrectly()
    {
        // Arrange
        var mh = new AryNumberValue("2em"); // left/right
        var mv = new AryNumberValue("1em"); // top/bottom
        var ph = new AryNumberValue("6px"); // left/right
        var pv = new AryNumberValue("3px"); // top/bottom

        // Act
        var sut = ArySpacing.FromSymmetric(mh, mv, ph, pv);

        // Assert
        // Margins
        sut.MarginTop.Value.Should().Be("1em");
        sut.MarginBottom.Value.Should().Be("1em");
        sut.MarginEnd.Value.Should().Be("2em");
        sut.MarginStart.Value.Should().Be("2em");

        // Paddings
        sut.PaddingTop.Value.Should().Be("3px");
        sut.PaddingBottom.Value.Should().Be("3px");
        sut.PaddingEnd.Value.Should().Be("6px");
        sut.PaddingStart.Value.Should().Be("6px");
    }

    [Fact]
    public void Record_WithExpression_Should_ProduceNewInstance_When_Cascading()
    {
        // Arrange
        var original = ArySpacing.FromSingle("1px", "2px");

        // Act
        var mutated = original with
        {
            MarginTop = new AryNumberValue("10px")
        };

        // Assert
        mutated.MarginTop.Value.Should().Be("10px");
        original.MarginTop.Value.Should().Be("1px"); // immutability/value semantics check
    }

    [Fact]
    public void ToCss_Should_EmitAllEightLogicalDeclarations_InSpecifiedOrder()
    {
        // Arrange
        var sut = new ArySpacing(
            "1px",
            "2px",
            "3px",
            "4px",
            "5px",
            "6px",
            "7px",
            "8px"
        );

        // Act
        var css = sut.ToCss();

        // Assert - contains each property once
        css.Should().Contain("margin-top:");
        css.Should().Contain("margin-inline-end:");
        css.Should().Contain("margin-bottom:");
        css.Should().Contain("margin-inline-start:");
        css.Should().Contain("padding-top:");
        css.Should().Contain("padding-inline-end:");
        css.Should().Contain("padding-bottom:");
        css.Should().Contain("padding-inline-start:");

        // Assert - contains our values
        css.Should().Contain("1px");
        css.Should().Contain("2px");
        css.Should().Contain("3px");
        css.Should().Contain("4px");
        css.Should().Contain("5px");
        css.Should().Contain("6px");
        css.Should().Contain("7px");
        css.Should().Contain("8px");

        // Assert - order is correct
        var order = new[]
        {
            "margin-top:",
            "margin-inline-end:",
            "margin-bottom:",
            "margin-inline-start:",
            "padding-top:",
            "padding-inline-end:",
            "padding-bottom:",
            "padding-inline-start:"
        };

        var positions = order.Select(token => css.IndexOf(token, StringComparison.Ordinal)).ToArray();
        positions.Should().OnlyContain(p => p >= 0);
        positions.Should().BeInAscendingOrder();

        // Assert - terminators present for each declaration (defensive, not assuming exact formatting)
        var semicolons = css.Count(c => c == ';');
        semicolons.Should().BeGreaterThanOrEqualTo(8);
    }
}
