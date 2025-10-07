using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ArySpacingTests
{
    [Fact]
    public void Cascade_Should_NotMutateOriginal_When_CreatingNewInstance()
    {
        // Arrange
        var original = new ArySpacing(
            Sizing.Size1,
            Sizing.Size2,
            Sizing.Size3,
            Sizing.Size4,
            Sizing.Size5,
            Sizing.Size6,
            Sizing.Size7,
            Sizing.Size8
        );

        // Act
        _ = original.Cascade(Sizing.Size9);

        // Assert
        original.MarginTop.Should().Be(Sizing.Size1);
        original.MarginRight.Should().Be(Sizing.Size2);
        original.MarginBottom.Should().Be(Sizing.Size3);
        original.MarginLeft.Should().Be(Sizing.Size4);
        original.PaddingTop.Should().Be(Sizing.Size5);
        original.PaddingRight.Should().Be(Sizing.Size6);
        original.PaddingBottom.Should().Be(Sizing.Size7);
        original.PaddingLeft.Should().Be(Sizing.Size8);
    }

    [Fact]
    public void Cascade_Should_OverrideSpecified_When_ArgsProvided()
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
        var result = sut.Cascade(
            marginLeft: "40px",
            paddingRight: "60px"
        );

        // Assert
        result.MarginTop.Should().Be(new AryNumberValue("1px"));
        result.MarginRight.Should().Be(new AryNumberValue("2px"));
        result.MarginBottom.Should().Be(new AryNumberValue("3px"));
        result.MarginLeft.Should().Be(new AryNumberValue("40px"));
        result.PaddingTop.Should().Be(new AryNumberValue("5px"));
        result.PaddingRight.Should().Be(new AryNumberValue("60px"));
        result.PaddingBottom.Should().Be(new AryNumberValue("7px"));
        result.PaddingLeft.Should().Be(new AryNumberValue("8px"));
    }

    [Fact]
    public void Cascade_Should_PreserveExisting_When_ArgsNull()
    {
        // Arrange
        var original = new ArySpacing(
            "11px",
            "12px",
            "13px",
            "14px",
            "15px",
            "16px",
            "17px",
            "18px"
        );

        var sut = original;

        // Act
        var result = sut.Cascade();

        // Assert
        result.Should().BeEquivalentTo(original);
    }

    [Fact]
    public void Constructor_Should_ApplyProvidedValues_When_ArgumentsGiven()
    {
        // Arrange
        AryNumberValue mt = "1px";
        AryNumberValue mr = "2px";
        AryNumberValue mb = "3px";
        AryNumberValue ml = "4px";
        AryNumberValue pt = "5px";
        AryNumberValue pr = "6px";
        AryNumberValue pb = "7px";
        AryNumberValue pl = "8px";

        // Act
        var sut = new ArySpacing(mt, mr, mb, ml, pt, pr, pb, pl);

        // Assert
        sut.MarginTop.Should().Be(mt);
        sut.MarginRight.Should().Be(mr);
        sut.MarginBottom.Should().Be(mb);
        sut.MarginLeft.Should().Be(ml);
        sut.PaddingTop.Should().Be(pt);
        sut.PaddingRight.Should().Be(pr);
        sut.PaddingBottom.Should().Be(pb);
        sut.PaddingLeft.Should().Be(pl);
    }

    [Fact]
    public void Constructor_Should_UseDefaults_When_ArgumentsOmitted()
    {
        // Arrange & Act
        var sut = new ArySpacing();

        // Assert
        sut.MarginTop.Should().Be(StyleDefaults.Margin);
        sut.MarginRight.Should().Be(StyleDefaults.Margin);
        sut.MarginBottom.Should().Be(StyleDefaults.Margin);
        sut.MarginLeft.Should().Be(StyleDefaults.Margin);
        sut.PaddingTop.Should().Be(StyleDefaults.Padding);
        sut.PaddingRight.Should().Be(StyleDefaults.Padding);
        sut.PaddingBottom.Should().Be(StyleDefaults.Padding);
        sut.PaddingLeft.Should().Be(StyleDefaults.Padding);
    }

    [Fact]
    public void FromSingle_Should_SetAllSidesUniformly()
    {
        // Arrange
        AryNumberValue margin = "5px";
        AryNumberValue padding = "7px";

        // Act
        var sut = ArySpacing.FromSingle(margin, padding);

        // Assert
        sut.MarginTop.Should().Be(margin);
        sut.MarginRight.Should().Be(margin);
        sut.MarginBottom.Should().Be(margin);
        sut.MarginLeft.Should().Be(margin);
        sut.PaddingTop.Should().Be(padding);
        sut.PaddingRight.Should().Be(padding);
        sut.PaddingBottom.Should().Be(padding);
        sut.PaddingLeft.Should().Be(padding);
    }

    [Fact]
    public void FromSymmetric_Should_SetHorizontalAndVerticalCorrectly()
    {
        // Arrange
        AryNumberValue mh = "1px";
        AryNumberValue mv = "2px";
        AryNumberValue ph = "3px";
        AryNumberValue pv = "4px";

        // Act
        var sut = ArySpacing.FromSymmetric(mh, mv, ph, pv);

        // Assert
        sut.MarginTop.Should().Be(mv);
        sut.MarginBottom.Should().Be(mv);
        sut.MarginLeft.Should().Be(mh);
        sut.MarginRight.Should().Be(mh);
        sut.PaddingTop.Should().Be(pv);
        sut.PaddingBottom.Should().Be(pv);
        sut.PaddingLeft.Should().Be(ph);
        sut.PaddingRight.Should().Be(ph);
    }

    [Fact]
    public void ToCss_Should_BuildCssDeclarations_When_NoPrefix()
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

        // Assert
        var expected = "margin-top:1px;" +
            "margin-right:2px;" +
            "margin-bottom:3px;" +
            "margin-left:4px;" +
            "padding-top:5px;" +
            "padding-right:6px;" +
            "padding-bottom:7px;" +
            "padding-left:8px;";

        css.Should().Be(expected);
    }

    [Fact]
    public void ToCss_Should_UseCssVariables_When_PrefixProvided()
    {
        // Arrange
        var sut = new ArySpacing(
            "10px",
            "20px",
            "30px",
            "40px",
            "50px",
            "60px",
            "70px",
            "80px"
        );

        // Act
        var css = sut.ToCss("  Theme  --Primary  ");

        // Assert
        var expected = "--theme-primary-margin-top:10px;" +
            "--theme-primary-margin-right:20px;" +
            "--theme-primary-margin-bottom:30px;" +
            "--theme-primary-margin-left:40px;" +
            "--theme-primary-padding-top:50px;" +
            "--theme-primary-padding-right:60px;" +
            "--theme-primary-padding-bottom:70px;" +
            "--theme-primary-padding-left:80px;";

        css.Should().Be(expected);
    }
}
