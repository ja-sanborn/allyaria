using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaBordersTests
{
    [Fact]
    public void Cascade_Should_Apply_Only_Specified_Overrides_Leaving_Others_Intact()
    {
        // Arrange
        AllyariaNumberValue width = "1px";
        AllyariaStringValue style = "solid";
        AllyariaNumberValue radius = ".25rem";

        var baseBorders = AllyariaBorders.FromSingle(width, style, radius);

        // Explicit overrides
        AllyariaNumberValue newTopWidth = "4px";
        AllyariaStringValue newLeftStyle = "dashed";
        AllyariaNumberValue newBottomRightRadius = "1rem";

        // Act
        var sut = baseBorders.Cascade(
            newTopWidth,
            leftStyle: newLeftStyle,
            bottomRightRadius: newBottomRightRadius
        );

        // Assert
        sut.TopWidth.Should().Be(newTopWidth);
        sut.LeftStyle.Should().Be(newLeftStyle);
        sut.BottomRightRadius.Should().Be(newBottomRightRadius);

        sut.RightWidth.Should().Be(width);
        sut.BottomWidth.Should().Be(width);
        sut.LeftWidth.Should().Be(width);

        sut.TopStyle.Should().Be(style);
        sut.RightStyle.Should().Be(style);
        sut.BottomStyle.Should().Be(style);

        sut.TopLeftRadius.Should().Be(radius);
        sut.TopRightRadius.Should().Be(radius);
        sut.BottomLeftRadius.Should().Be(radius);
    }

    [Fact]
    public void Cascade_Should_Preserve_Existing_Values_When_Overrides_Are_Null()
    {
        // Arrange
        AllyariaNumberValue width = "1px";
        AllyariaStringValue style = "solid";
        AllyariaNumberValue radius = ".25rem";

        var baseBorders = AllyariaBorders.FromSingle(width, style, radius);

        // Act
        var sut = baseBorders.Cascade(
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );

        // Assert
        sut.Should().Be(baseBorders);
    }

    [Fact]
    public void Ctor_Should_Leave_All_Members_Null_When_No_Args_Are_Passed()
    {
        // Arrange & Act
        var sut = new AllyariaBorders();

        // Assert
        sut.TopWidth.Should().BeNull();
        sut.RightWidth.Should().BeNull();
        sut.BottomWidth.Should().BeNull();
        sut.LeftWidth.Should().BeNull();

        sut.TopStyle.Should().BeNull();
        sut.RightStyle.Should().BeNull();
        sut.BottomStyle.Should().BeNull();
        sut.LeftStyle.Should().BeNull();

        sut.TopLeftRadius.Should().BeNull();
        sut.TopRightRadius.Should().BeNull();
        sut.BottomRightRadius.Should().BeNull();
        sut.BottomLeftRadius.Should().BeNull();
    }

    [Fact]
    public void Ctor_Should_Set_All_Members_When_All_Are_Provided()
    {
        // Arrange
        AllyariaNumberValue topW = "1px";
        AllyariaNumberValue rightW = "2px";
        AllyariaNumberValue bottomW = "3px";
        AllyariaNumberValue leftW = "4px";

        AllyariaStringValue topS = "solid";
        AllyariaStringValue rightS = "dashed";
        AllyariaStringValue bottomS = "dotted";
        AllyariaStringValue leftS = "double";

        AllyariaNumberValue tl = ".125rem";
        AllyariaNumberValue tr = ".25rem";
        AllyariaNumberValue br = ".5rem";
        AllyariaNumberValue bl = "1rem";

        // Act
        var sut = new AllyariaBorders(
            topW, rightW, bottomW, leftW,
            topS, rightS, bottomS, leftS,
            tl, tr, br, bl
        );

        // Assert
        sut.TopWidth.Should().Be(topW);
        sut.RightWidth.Should().Be(rightW);
        sut.BottomWidth.Should().Be(bottomW);
        sut.LeftWidth.Should().Be(leftW);

        sut.TopStyle.Should().Be(topS);
        sut.RightStyle.Should().Be(rightS);
        sut.BottomStyle.Should().Be(bottomS);
        sut.LeftStyle.Should().Be(leftS);

        sut.TopLeftRadius.Should().Be(tl);
        sut.TopRightRadius.Should().Be(tr);
        sut.BottomRightRadius.Should().Be(br);
        sut.BottomLeftRadius.Should().Be(bl);
    }

    [Fact]
    public void FromSingle_Should_Apply_Same_Width_Style_And_Radius_To_All_Sides_And_Corners()
    {
        // Arrange
        AllyariaNumberValue width = "1px";
        AllyariaStringValue style = "solid";
        AllyariaNumberValue radius = ".25rem";

        // Act
        var sut = AllyariaBorders.FromSingle(width, style, radius);

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

        var css = sut.ToCss();
        css.Should().Contain("border-top-width");
        css.Should().Contain("border-right-width");
        css.Should().Contain("border-bottom-width");
        css.Should().Contain("border-left-width");
        css.Should().Contain("border-top-style");
        css.Should().Contain("border-right-style");
        css.Should().Contain("border-bottom-style");
        css.Should().Contain("border-left-style");
        css.Should().Contain("border-top-left-radius");
        css.Should().Contain("border-top-right-radius");
        css.Should().Contain("border-bottom-right-radius");
        css.Should().Contain("border-bottom-left-radius");
    }

    [Fact]
    public void FromSymmetric_Should_Map_Vertical_And_Horizontal_Sides_Correctly_And_Set_Radii_Pairs()
    {
        // Arrange
        AllyariaNumberValue widthH = "2px";
        AllyariaNumberValue widthV = "1px";
        AllyariaStringValue styleH = "dashed";
        AllyariaStringValue styleV = "solid";
        AllyariaNumberValue radiusTop = ".5rem";
        AllyariaNumberValue radiusBottom = "1rem";

        // Act
        var sut = AllyariaBorders.FromSymmetric(widthH, widthV, styleH, styleV, radiusTop, radiusBottom);

        // Assert
        sut.TopWidth.Should().Be(widthV);
        sut.BottomWidth.Should().Be(widthV);
        sut.RightWidth.Should().Be(widthH);
        sut.LeftWidth.Should().Be(widthH);

        sut.TopStyle.Should().Be(styleV);
        sut.BottomStyle.Should().Be(styleV);
        sut.RightStyle.Should().Be(styleH);
        sut.LeftStyle.Should().Be(styleH);

        sut.TopLeftRadius.Should().Be(radiusTop);
        sut.TopRightRadius.Should().Be(radiusTop);
        sut.BottomRightRadius.Should().Be(radiusBottom);
        sut.BottomLeftRadius.Should().Be(radiusBottom);
    }

    [Fact]
    public void Immutability_Should_Be_Preserved_When_Using_WithExpression()
    {
        // Arrange
        var original = AllyariaBorders.FromSingle("1px", "solid", ".25rem");

        // Act
        var modified = original with
        {
            TopWidth = "4px"
        };

        // Assert
        original.TopWidth!.Value.Should().Be("1px");
        modified.TopWidth!.Value.Should().Be("4px");
    }

    [Fact]
    public void Record_Value_Equality_Should_Consider_All_Properties()
    {
        // Arrange
        var a = new AllyariaBorders("1px", leftStyle: "solid", topLeftRadius: ".25rem");
        var b = new AllyariaBorders("1px", leftStyle: "solid", topLeftRadius: ".25rem");

        var c = b with
        {
            TopRightRadius = ".5rem"
        };

        // Act & Assert
        a.Should().Be(b);
        a.Should().NotBe(c);
    }

    [Fact]
    public void ToCss_Should_Emit_Declarations_For_Only_NonNull_Members_In_Defined_Order()
    {
        // Arrange
        AllyariaNumberValue topW = "1px";
        AllyariaStringValue bottomS = "dotted";
        AllyariaNumberValue tr = ".5rem";

        var sut = new AllyariaBorders(
            topW,
            bottomStyle: bottomS,
            topRightRadius: tr
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().Contain("border-top-width");
        css.Should().Contain("border-bottom-style");
        css.Should().Contain("border-top-right-radius");

        css.Should().NotContain("border-right-width");
        css.Should().NotContain("border-left-width");
        css.Should().NotContain("border-top-style");
        css.Should().NotContain("border-right-style");
        css.Should().NotContain("border-left-style");
        css.Should().NotContain("border-top-left-radius");
        css.Should().NotContain("border-bottom-right-radius");
        css.Should().NotContain("border-bottom-left-radius");

        var idxTopW = css.IndexOf("border-top-width", StringComparison.Ordinal);
        var idxBottomS = css.IndexOf("border-bottom-style", StringComparison.Ordinal);
        var idxTopRightR = css.IndexOf("border-top-right-radius", StringComparison.Ordinal);

        (idxTopW < idxBottomS && idxBottomS < idxTopRightR).Should().BeTrue();
    }

    [Fact]
    public void ToCss_Should_Return_Empty_String_When_All_Members_Are_Null()
    {
        // Arrange
        var sut = new AllyariaBorders();

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().BeEmpty();
    }

    [Theory]
    [InlineData("", "--aa-")]
    [InlineData("   ", "--aa-")]
    [InlineData("Editor", "--editor-")]
    [InlineData("Editor  Theme", "--editor-theme-")]
    [InlineData("--Editor--Theme--", "--editor-theme-")]
    [InlineData("  editor---THEME  ", "--editor-theme-")]
    public void ToCssVars_Should_Normalize_Prefix_And_Emit_Variables(string inputPrefix, string expectedPrefix)
    {
        // Arrange
        AllyariaNumberValue topW = "2px";
        AllyariaStringValue leftS = "dashed";
        AllyariaNumberValue bl = "1rem";

        var sut = new AllyariaBorders(
            topW,
            leftStyle: leftS,
            bottomLeftRadius: bl
        );

        // Act
        var vars = sut.ToCssVars(inputPrefix);

        // Assert
        vars.Should().Contain($"{expectedPrefix}border-top-width");
        vars.Should().Contain($"{expectedPrefix}border-left-style");
        vars.Should().Contain($"{expectedPrefix}border-bottom-left-radius");
        vars.Should().NotContain("  ");
        vars.Should().NotContain("--Editor");
        vars.Should().NotContain("THEME");
    }

    [Fact]
    public void ToCssVars_Should_Return_Empty_String_When_All_Members_Are_Null()
    {
        // Arrange
        var sut = new AllyariaBorders();

        // Act
        var vars = sut.ToCssVars("any");

        // Assert
        vars.Should().BeEmpty();
    }
}
