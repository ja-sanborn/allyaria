namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class BordersTests
{
    [Fact]
    public void Cascade_Should_NotMutateOriginal_When_Called()
    {
        // Arrange
        var original = new Borders(
            new ThemeNumber("1px"),
            new ThemeNumber("2px"),
            new ThemeNumber("3px"),
            new ThemeNumber("4px"),
            new ThemeString("solid"),
            new ThemeString("dashed"),
            new ThemeString("double"),
            new ThemeString("groove"),
            new ThemeNumber("1px"),
            new ThemeNumber("2px"),
            new ThemeNumber("3px"),
            new ThemeNumber("4px")
        );

        // Act
        var _ = original.Cascade(new ThemeNumber("9px"));

        // Assert
        original.TopWidth.Value.Should().Be("1px"); // unchanged
    }

    [Fact]
    public void Cascade_Should_OverrideOnlySpecifiedMembers_When_PartialOverridesProvided()
    {
        // Arrange
        var original = new Borders(
            new ThemeNumber("1px"),
            new ThemeNumber("2px"),
            new ThemeNumber("3px"),
            new ThemeNumber("4px"),
            new ThemeString("solid"),
            new ThemeString("dashed"),
            new ThemeString("double"),
            new ThemeString("groove"),
            new ThemeNumber("1px"),
            new ThemeNumber("2px"),
            new ThemeNumber("3px"),
            new ThemeNumber("4px")
        );

        // Act
        var result = original.Cascade(
            endWidth: new ThemeNumber("10px"),
            bottomStyle: new ThemeString("ridge"),
            topLeftRadius: new ThemeNumber("8px")
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
        var sut = new Borders(
            new ThemeNumber("1px"),
            new ThemeNumber("2px"),
            new ThemeNumber("3px"),
            new ThemeNumber("4px"),
            new ThemeString("solid"),
            new ThemeString("dashed"),
            new ThemeString("double"),
            new ThemeString("groove"),
            new ThemeNumber("1px"),
            new ThemeNumber("2px"),
            new ThemeNumber("3px"),
            new ThemeNumber("4px")
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
        var topWidth = new ThemeNumber("1px");
        var endWidth = new ThemeNumber("2px");
        var bottomWidth = new ThemeNumber("3px");
        var startWidth = new ThemeNumber("4px");

        var topStyle = new ThemeString("solid");
        var endStyle = new ThemeString("dashed");
        var bottomStyle = new ThemeString("double");
        var startStyle = new ThemeString("groove");

        var tl = new ThemeNumber(".25rem");
        var tr = new ThemeNumber(".5rem");
        var br = new ThemeNumber("8px");
        var bl = new ThemeNumber("10px");

        // Act
        var sut = new Borders(
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
    public void FromSingle_Should_CreateUniformBorders_When_ValidInputs()
    {
        // Arrange
        var width = new ThemeNumber("2px");
        var style = new ThemeString("solid");
        var radius = new ThemeNumber(".25rem");

        // Act
        var sut = Borders.FromSingle(width, style, radius);

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
        var widthH = new ThemeNumber("4px");
        var widthV = new ThemeNumber("1px");
        var styleH = new ThemeString("dotted");
        var styleV = new ThemeString("solid");
        var radiusTop = new ThemeNumber("3px");
        var radiusBottom = new ThemeNumber("6px");

        // Act
        var sut = Borders.FromSymmetric(widthH, widthV, styleH, styleV, radiusTop, radiusBottom);

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
        var sut = new Borders();

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
        var width = new ThemeNumber("1px");
        var style = new ThemeString("solid");
        var radius = new ThemeNumber("4px");
        var sut = Borders.FromSingle(width, style, radius);

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
        var sut = Borders.FromSingle(new ThemeNumber("1px"), new ThemeString("solid"), new ThemeNumber("4px"));

        // Act
        var css = sut.ToCss(prefix);

        // Assert
        css.Should().Contain("border-top-width").And.NotContain("--border-top-width");
        css.Should().Contain("border-inline-end-width").And.NotContain("--border-inline-end-width");
        css.Should().Contain("border-bottom-width").And.NotContain("--border-bottom-width");
        css.Should().Contain("border-inline-start-width").And.NotContain("--border-inline-start-width");
    }

    [Fact]
    public void ToCss_Should_UseNormalizedCssVarNames_When_PrefixProvided()
    {
        // Arrange
        var sut = Borders.FromSingle(new ThemeNumber("2px"), new ThemeString("dotted"), new ThemeNumber("6px"));

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
