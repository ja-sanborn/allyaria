using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaSpacingTests
{
    [Fact]
    public void Cascade_Should_Keep_Existing_Values_When_Override_Is_Null()
    {
        // Arrange
        var sut = new AllyariaSpacing(
            new AllyariaNumberValue("1px"),
            new AllyariaNumberValue("2px"),
            new AllyariaNumberValue("3px"),
            new AllyariaNumberValue("4px"),
            new AllyariaNumberValue("5px"),
            new AllyariaNumberValue("6px"),
            new AllyariaNumberValue("7px"),
            new AllyariaNumberValue("8px")
        );

        // Act
        // Provide no overrides (all null)
        var cascaded = sut.Cascade();

        // Assert
        cascaded.Should().BeEquivalentTo(sut);
    }

    [Fact]
    public void Cascade_Should_Override_Only_Specified_Sides()
    {
        // Arrange
        var sut = new AllyariaSpacing(
            new AllyariaNumberValue("1px"),
            new AllyariaNumberValue("2px"),
            new AllyariaNumberValue("3px"),
            new AllyariaNumberValue("4px"),
            new AllyariaNumberValue("5px"),
            new AllyariaNumberValue("6px"),
            new AllyariaNumberValue("7px"),
            new AllyariaNumberValue("8px")
        );

        // Act
        var cascaded = sut.Cascade(
            marginRight: new AllyariaNumberValue("22px"),
            paddingBottom: new AllyariaNumberValue("77px")
        );

        // Assert
        cascaded.MarginTop!.ToCss("margin-top").Should().Be("margin-top:1px;");
        cascaded.MarginRight!.ToCss("margin-right").Should().Be("margin-right:22px;");
        cascaded.MarginBottom!.ToCss("margin-bottom").Should().Be("margin-bottom:3px;");
        cascaded.MarginLeft!.ToCss("margin-left").Should().Be("margin-left:4px;");
        cascaded.PaddingTop!.ToCss("padding-top").Should().Be("padding-top:5px;");
        cascaded.PaddingRight!.ToCss("padding-right").Should().Be("padding-right:6px;");
        cascaded.PaddingBottom!.ToCss("padding-bottom").Should().Be("padding-bottom:77px;");
        cascaded.PaddingLeft!.ToCss("padding-left").Should().Be("padding-left:8px;");
    }

    [Fact]
    public void Ctor_Should_Set_All_Properties_When_Provided()
    {
        // Arrange
        var mt = new AllyariaNumberValue("1px");
        var mr = new AllyariaNumberValue("2px");
        var mb = new AllyariaNumberValue("3px");
        var ml = new AllyariaNumberValue("4px");
        var pt = new AllyariaNumberValue("5px");
        var pr = new AllyariaNumberValue("6px");
        var pb = new AllyariaNumberValue("7px");
        var pl = new AllyariaNumberValue("8px");

        // Act
        var sut = new AllyariaSpacing(mt, mr, mb, ml, pt, pr, pb, pl);

        // Assert
        sut.MarginTop.Should().NotBeNull();
        sut.MarginRight.Should().NotBeNull();
        sut.MarginBottom.Should().NotBeNull();
        sut.MarginLeft.Should().NotBeNull();
        sut.PaddingTop.Should().NotBeNull();
        sut.PaddingRight.Should().NotBeNull();
        sut.PaddingBottom.Should().NotBeNull();
        sut.PaddingLeft.Should().NotBeNull();
    }

    [Fact]
    public void FromSingle_Should_Set_All_Sides_When_Given_Margin_And_Padding()
    {
        // Arrange
        var margin = new AllyariaNumberValue("10px");
        var padding = new AllyariaNumberValue("1rem");

        // Act
        var sut = AllyariaSpacing.FromSingle(margin, padding);

        // Assert
        sut.MarginTop!.ToCss("margin-top").Should().Be("margin-top:10px;");
        sut.MarginRight!.ToCss("margin-right").Should().Be("margin-right:10px;");
        sut.MarginBottom!.ToCss("margin-bottom").Should().Be("margin-bottom:10px;");
        sut.MarginLeft!.ToCss("margin-left").Should().Be("margin-left:10px;");
        sut.PaddingTop!.ToCss("padding-top").Should().Be("padding-top:1rem;");
        sut.PaddingRight!.ToCss("padding-right").Should().Be("padding-right:1rem;");
        sut.PaddingBottom!.ToCss("padding-bottom").Should().Be("padding-bottom:1rem;");
        sut.PaddingLeft!.ToCss("padding-left").Should().Be("padding-left:1rem;");
    }

    [Fact]
    public void FromSymmetric_Should_Assign_Vertical_And_Horizontal_Correctly()
    {
        // Arrange
        var marginHorizontal = new AllyariaNumberValue("4px");
        var marginVertical = new AllyariaNumberValue("8px");
        var paddingHorizontal = new AllyariaNumberValue("2%");
        var paddingVertical = new AllyariaNumberValue("3%");

        // Act
        var sut = AllyariaSpacing.FromSymmetric(marginHorizontal, marginVertical, paddingHorizontal, paddingVertical);

        // Assert
        sut.MarginTop!.ToCss("margin-top").Should().Be("margin-top:8px;");
        sut.MarginRight!.ToCss("margin-right").Should().Be("margin-right:4px;");
        sut.MarginBottom!.ToCss("margin-bottom").Should().Be("margin-bottom:8px;");
        sut.MarginLeft!.ToCss("margin-left").Should().Be("margin-left:4px;");
        sut.PaddingTop!.ToCss("padding-top").Should().Be("padding-top:3%;");
        sut.PaddingRight!.ToCss("padding-right").Should().Be("padding-right:2%;");
        sut.PaddingBottom!.ToCss("padding-bottom").Should().Be("padding-bottom:3%;");
        sut.PaddingLeft!.ToCss("padding-left").Should().Be("padding-left:2%;");
    }

    [Fact]
    public void ToCss_Should_Concatenate_In_Defined_Order_When_Some_Values_Set()
    {
        // Arrange
        var mt = new AllyariaNumberValue("1px");
        var mb = new AllyariaNumberValue("3px");
        var pr = new AllyariaNumberValue("6px");
        var pl = new AllyariaNumberValue("8px");

        // only set a subset to ensure AppendIfNotNull covers both branches
        var sut = new AllyariaSpacing(mt, marginBottom: mb, paddingRight: pr, paddingLeft: pl);

        // Act
        var css = sut.ToCss();

        // Assert
        // Order should be: margin-top, margin-right, margin-bottom, margin-left, padding-top, padding-right,
        //                  padding-bottom, padding-left — but only those set are emitted.
        css.Should().Be("margin-top:1px;margin-bottom:3px;padding-right:6px;padding-left:8px;");
    }

    [Fact]
    public void ToCss_Should_Handle_All_Sides_Set()
    {
        // Arrange
        var sut = new AllyariaSpacing(
            new AllyariaNumberValue("1px"),
            new AllyariaNumberValue("2px"),
            new AllyariaNumberValue("3px"),
            new AllyariaNumberValue("4px"),
            new AllyariaNumberValue("5px"),
            new AllyariaNumberValue("6px"),
            new AllyariaNumberValue("7px"),
            new AllyariaNumberValue("8px")
        );

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().Be(
            "margin-top:1px;" +
            "margin-right:2px;" +
            "margin-bottom:3px;" +
            "margin-left:4px;" +
            "padding-top:5px;" +
            "padding-right:6px;" +
            "padding-bottom:7px;" +
            "padding-left:8px;"
        );
    }

    [Fact]
    public void ToCss_Should_Return_EmptyString_When_All_Values_Are_Null()
    {
        // Arrange
        var sut = new AllyariaSpacing();

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should().BeEmpty();
    }

    [Fact]
    public void ToCssVars_Should_Handle_All_Sides_Set_With_DefaultPrefix()
    {
        // Arrange
        var sut = new AllyariaSpacing(
            new AllyariaNumberValue("1px"),
            new AllyariaNumberValue("2px"),
            new AllyariaNumberValue("3px"),
            new AllyariaNumberValue("4px"),
            new AllyariaNumberValue("5px"),
            new AllyariaNumberValue("6px"),
            new AllyariaNumberValue("7px"),
            new AllyariaNumberValue("8px")
        );

        // Act
        var cssVars = sut.ToCssVars(); // default/empty -> "--aa-"

        // Assert
        cssVars.Should().Be(
            "--aa-margin-top:1px;" +
            "--aa-margin-right:2px;" +
            "--aa-margin-bottom:3px;" +
            "--aa-margin-left:4px;" +
            "--aa-padding-top:5px;" +
            "--aa-padding-right:6px;" +
            "--aa-padding-bottom:7px;" +
            "--aa-padding-left:8px;"
        );
    }

    [Theory]
    [InlineData("", "--aa-")] // empty -> default
    [InlineData("   ", "--aa-")] // whitespace -> default
    [InlineData("editor", "--editor-")] // simple custom
    [InlineData("  My--Fancy  Prefix  ", "--my-fancy-prefix-")] // collapse dashes/whitespace, trim, lowercase
    [InlineData("---X---", "--x-")] // leading/trailing dashes trimmed
    public void ToCssVars_Should_Normalize_Prefix_Correctly(string given, string expectedBase)
    {
        // Arrange
        var mt = new AllyariaNumberValue("10px");
        var pl = new AllyariaNumberValue("2rem");
        var sut = new AllyariaSpacing(mt, paddingLeft: pl);

        // Act
        var cssVars = sut.ToCssVars(given);

        // Assert
        cssVars.Should().Be($"{expectedBase}margin-top:10px;{expectedBase}padding-left:2rem;");
    }

    [Fact]
    public void ToCssVars_Should_Return_EmptyString_When_All_Values_Are_Null()
    {
        // Arrange
        var sut = new AllyariaSpacing();

        // Act
        var cssVars = sut.ToCssVars();

        // Assert
        cssVars.Should().BeEmpty();
    }

    [Fact]
    public void ToCssVars_Should_Throw_ArgumentNullException_When_Prefix_Is_Null()
    {
        // Arrange
        var sut = new AllyariaSpacing(new AllyariaNumberValue("1px"));

        // Act
        var act = () => sut.ToCssVars(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .Where(ex => ex.ParamName == "input" || ex.ParamName == "input" || ex.Message.Contains("input") ||
                ex.ParamName == "pattern" || ex.ParamName == "input"
            )

            // .NET's Regex.Replace throws for null "input" parameter; across frameworks ParamName can be "input".
            ;
    }
}
