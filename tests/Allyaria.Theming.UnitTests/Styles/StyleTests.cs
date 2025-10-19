namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleTests
{
    [Fact]
    public void Cascade_Should_Return_NewStyle_With_Overrides_When_PartialOverridesProvided()
    {
        // Arrange
        var original = new Style();

        var overridePalette = new Palette(
            StyleDefaults.BackgroundThemeColorHighContrast,
            foregroundColor: StyleDefaults.ForegroundThemeColorHighContrast,
            backgroundColor: StyleDefaults.Transparent,
            borderColor: StyleDefaults.ForegroundThemeColorHighContrast,
            isHighContrast: true
        );

        var overrideTypography = new Typography(new ThemeString("Fira Code, monospace"));

        // Leave spacing as-is to exercise the "null keeps original" branch.
        var overrideBorder = new Borders(
            new ThemeNumber("0px"),
            new ThemeNumber("0px"),
            new ThemeNumber("0px"),
            new ThemeNumber("0px")
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
        original.Palette.Should().Be(new Palette());
        original.Typography.Should().Be(new Typography());
        original.Spacing.Should().Be(new Spacing());
        original.Border.Should().Be(new Borders());
    }

    [Fact]
    public void Cascade_Should_Return_SameStyle_When_NoOverridesProvided()
    {
        // Arrange
        var sut = new Style(
            new Palette(isHighContrast: true),
            new Typography(fontWeight: new ThemeString("600")),
            new Spacing(paddingTop: new ThemeNumber("20px")),
            new Borders(topStyle: new ThemeString("double"))
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
        var customPalette = new Palette(
            StyleDefaults.BackgroundThemeColorDark,
            StyleDefaults.BackgroundThemeColorLight,
            StyleDefaults.ForegroundThemeColorDark,
            StyleDefaults.ForegroundThemeColorLight,
            true
        );

        var customTypography = new Typography(
            new ThemeString("Monaspace Neon, monospace"),
            new ThemeNumber("17px"),
            new ThemeString("italic"),
            new ThemeString("700"),
            new ThemeNumber("0.12em"),
            new ThemeNumber("1.8"),
            new ThemeString("center"),
            new ThemeString("underline"),
            new ThemeString("wavy"),
            new ThemeString("uppercase"),
            new ThemeString("middle")
        );

        var customSpacing = new Spacing(
            new ThemeNumber("10px"),
            new ThemeNumber("11px"),
            new ThemeNumber("12px"),
            new ThemeNumber("13px"),
            new ThemeNumber("14px"),
            new ThemeNumber("15px"),
            new ThemeNumber("16px"),
            new ThemeNumber("17px")
        );

        var customBorder = new Borders(
            new ThemeNumber("5px"),
            new ThemeNumber("6px"),
            new ThemeNumber("7px"),
            new ThemeNumber("8px"),
            new ThemeString("dotted"),
            new ThemeString("dashed"),
            new ThemeString("double"),
            new ThemeString("groove"),
            new ThemeNumber("2px"),
            new ThemeNumber("3px"),
            new ThemeNumber("4px"),
            new ThemeNumber("5px")
        );

        // Act
        var sut = new Style(customPalette, customTypography, customSpacing, customBorder);

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
        var sut = new Style();

        // Assert
        sut.Palette.Should().Be(new Palette());
        sut.Typography.Should().Be(new Typography());
        sut.Spacing.Should().Be(new Spacing());
        sut.Border.Should().Be(new Borders());
    }
}
