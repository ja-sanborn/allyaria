namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryStyleTests
{
    [Fact]
    public void Cascade_Should_Return_NewStyle_With_Overrides_When_PartialOverridesProvided()
    {
        // Arrange
        var original = new AryStyle();

        var overridePalette = new AryPalette(
            StyleDefaults.BackgroundColorHighContrast,
            foregroundColor: StyleDefaults.ForegroundColorHighContrast,
            backgroundColor: StyleDefaults.Transparent,
            borderColor: StyleDefaults.ForegroundColorHighContrast,
            isHighContrast: true
        );

        var overrideTypography = new AryTypography(new AryStringValue("Fira Code, monospace"));

        // Leave spacing as-is to exercise the "null keeps original" branch.
        var overrideBorder = new AryBorders(
            new AryNumberValue("0px"),
            new AryNumberValue("0px"),
            new AryNumberValue("0px"),
            new AryNumberValue("0px")
        );

        // Act
        var result = original.Cascade(
            overridePalette,
            overrideTypography,
            null,
            overrideBorder
        );

        // Assert
        result.Palette.Should().Be(overridePalette);
        result.Typography.Should().Be(overrideTypography);
        result.Border.Should().Be(overrideBorder);
        result.Spacing.Should().Be(original.Spacing); // preserved

        // Original is unchanged (record struct immutability semantics).
        original.Palette.Should().Be(new AryPalette());
        original.Typography.Should().Be(new AryTypography());
        original.Spacing.Should().Be(new ArySpacing());
        original.Border.Should().Be(new AryBorders());
    }

    [Fact]
    public void Cascade_Should_Return_SameStyle_When_NoOverridesProvided()
    {
        // Arrange
        var sut = new AryStyle(
            new AryPalette(isHighContrast: true),
            new AryTypography(fontWeight: new AryStringValue("600")),
            new ArySpacing(paddingTop: new AryNumberValue("20px")),
            new AryBorders(topStyle: new AryStringValue("double"))
        );

        // Act
        var result = sut.Cascade();

        // Assert
        result.Should().Be(sut);
    }

    [Fact]
    public void Ctor_Should_Use_ProvidedComponents_When_AllSpecified()
    {
        // Arrange
        // Use clearly non-default values where possible to ensure they flow through.
        var customPalette = new AryPalette(
            StyleDefaults.BackgroundColorDark,
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.ForegroundColorDark,
            StyleDefaults.ForegroundColorLight,
            true
        );

        var customTypography = new AryTypography(
            new AryStringValue("Monaspace Neon, monospace"),
            new AryNumberValue("17px"),
            new AryStringValue("italic"),
            new AryStringValue("700"),
            new AryNumberValue("0.12em"),
            new AryNumberValue("1.8"),
            new AryStringValue("center"),
            new AryStringValue("underline"),
            new AryStringValue("wavy"),
            new AryStringValue("uppercase"),
            new AryStringValue("middle")
        );

        var customSpacing = new ArySpacing(
            new AryNumberValue("10px"),
            new AryNumberValue("11px"),
            new AryNumberValue("12px"),
            new AryNumberValue("13px"),
            new AryNumberValue("14px"),
            new AryNumberValue("15px"),
            new AryNumberValue("16px"),
            new AryNumberValue("17px")
        );

        var customBorder = new AryBorders(
            new AryNumberValue("5px"),
            new AryNumberValue("6px"),
            new AryNumberValue("7px"),
            new AryNumberValue("8px"),
            new AryStringValue("dotted"),
            new AryStringValue("dashed"),
            new AryStringValue("double"),
            new AryStringValue("groove"),
            new AryNumberValue("2px"),
            new AryNumberValue("3px"),
            new AryNumberValue("4px"),
            new AryNumberValue("5px")
        );

        // Act
        var sut = new AryStyle(customPalette, customTypography, customSpacing, customBorder);

        // Assert
        sut.Palette.Should().Be(customPalette);
        sut.Typography.Should().Be(customTypography);
        sut.Spacing.Should().Be(customSpacing);
        sut.Border.Should().Be(customBorder);
    }

    [Fact]
    public void DefaultCtor_Should_Use_DefaultComponents_When_NoArgs()
    {
        // Arrange
        // (nothing)

        // Act
        var sut = new AryStyle();

        // Assert
        sut.Palette.Should().Be(new AryPalette());
        sut.Typography.Should().Be(new AryTypography());
        sut.Spacing.Should().Be(new ArySpacing());
        sut.Border.Should().Be(new AryBorders());
    }

    [Fact]
    public void ToCss_Should_Concatenate_ComponentCss_When_NotFocus()
    {
        // Arrange
        var palette = new AryPalette(
            StyleDefaults.BackgroundColorLight,
            StyleDefaults.Transparent,
            StyleDefaults.ForegroundColorLight,
            StyleDefaults.ForegroundColorLight
        );

        var typography = new AryTypography(
            fontSize: new AryNumberValue("19px"), textAlign: new AryStringValue("right")
        );

        var spacing = new ArySpacing(new AryNumberValue("1rem"), paddingBottom: new AryNumberValue("2rem"));
        var border = new AryBorders(new AryNumberValue("1px"), topStyle: new AryStringValue("solid"));

        var sut = new AryStyle(palette, typography, spacing, border);

        // Act
        var css = sut.ToCss("cmp");

        // Assert
        var expected = string.Concat(
            palette.ToCss("cmp"),
            typography.ToCss("cmp"),
            spacing.ToCss("cmp"),
            border.ToCss("cmp")
        );

        css.Should().Be(expected);
    }

    [Fact]
    public void ToCss_Should_Use_FocusBorder_When_IsFocusTrue()
    {
        // Arrange
        // Use zero widths to ensure FocusWidth branch picks "2px" (largest < 2) inside AryBorders.
        var focusBorder = new AryBorders(
            new AryNumberValue("0px"),
            new AryNumberValue("0px"),
            new AryNumberValue("0px"),
            new AryNumberValue("0px"),
            new AryStringValue("solid"),
            new AryStringValue("solid"),
            new AryStringValue("solid"),
            new AryStringValue("solid")
        );

        var palette = new AryPalette();
        var typography = new AryTypography();
        var spacing = new ArySpacing();

        var sut = new AryStyle(palette, typography, spacing, focusBorder);

        // Act
        var nonFocusCss = sut.ToCss(isFocus: false);
        var focusCss = sut.ToCss(isFocus: true);

        // Assert
        // The focus rendering should differ due to FocusWidth and dashed styles applied within AryBorders.
        focusCss.Should().NotBe(nonFocusCss);

        var expectedFocus = string.Concat(
            palette.ToCss(),
            typography.ToCss(),
            spacing.ToCss(),
            focusBorder.ToCss("", true)
        );

        focusCss.Should().Be(expectedFocus);
    }
}
