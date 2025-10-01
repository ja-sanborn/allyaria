using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaStyleTests
{
    [Fact]
    public void Ctor_Should_Use_Fallbacks_When_Optional_Args_Are_Null()
    {
        // Arrange
        var palette = new AllyariaPalette(); // real SUT dependency
        var typography = new AllyariaTypography(); // real SUT dependency
        var spacing = new AllyariaSpacing(); // real SUT dependency

        // Act
        var sut = new AllyariaStyle(palette, typography, spacing);

        // Assert
        sut.Palette.Should().Be(palette);
        sut.Typography.Should().Be(typography);
        sut.Spacing.Should().Be(spacing);

        // fallbacks derived from base inputs
        sut.PaletteHover.Should().Be(palette.ToHoverPalette());
        sut.PaletteDisabled.Should().Be(palette.ToDisabledPalette());
        sut.TypographyHover.Should().Be(typography);
        sut.TypographyDisabled.Should().Be(typography);
    }

    [Fact]
    public void Ctor_Should_Use_Provided_Overrides_When_Specified()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();

        var overrideHoverPalette = palette.ToHoverPalette(); // just reuse a valid value instance
        var overrideDisabledPalette = palette.ToDisabledPalette();
        var overrideHoverTypography = typography.Cascade(fontStyle: new AllyariaStringValue("italic"));
        var overrideDisabledTypography = typography.Cascade(fontWeight: new AllyariaStringValue("700"));

        // Act
        var sut = new AllyariaStyle(
            palette,
            typography,
            spacing,
            overrideHoverPalette,
            overrideHoverTypography,
            overrideDisabledPalette,
            overrideDisabledTypography
        );

        // Assert
        sut.PaletteHover.Should().Be(overrideHoverPalette);
        sut.PaletteDisabled.Should().Be(overrideDisabledPalette);
        sut.TypographyHover.Should().Be(overrideHoverTypography);
        sut.TypographyDisabled.Should().Be(overrideDisabledTypography);
    }

    [Fact]
    public void ToCss_Should_Concatenate_Palette_Typography_And_Spacing_In_Order()
    {
        // Arrange
        var palette = new AllyariaPalette(); // non-empty ToCss()
        var typography = new AllyariaTypography(); // empty ToCss() by default
        var spacing = new AllyariaSpacing(); // empty ToCss() by default
        var sut = new AllyariaStyle(palette, typography, spacing);

        // Act
        var actual = sut.ToCss();

        // Assert (compare to exact concatenation of underlying parts)
        var expected = string.Concat(palette.ToCss(), typography.ToCss(), spacing.ToCss());
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToCssDisabled_Should_Concatenate_DisabledPalette_DisabledTypography_And_Spacing()
    {
        // Arrange
        var basePalette = new AllyariaPalette();
        var baseTypography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();

        var disabledPalette = basePalette.ToDisabledPalette();
        var disabledTypography = baseTypography; // ctor default uses base typography when disabled arg is null

        var sut = new AllyariaStyle(basePalette, baseTypography, spacing);

        // Act
        var actual = sut.ToCssDisabled();

        // Assert
        var expected = string.Concat(disabledPalette.ToCss(), disabledTypography.ToCss(), spacing.ToCss());
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToCssHover_Should_Concatenate_HoverPalette_HoverTypography_And_Spacing()
    {
        // Arrange
        var basePalette = new AllyariaPalette();
        var baseTypography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();

        var hoverPalette = basePalette.ToHoverPalette();
        var hoverTypography = baseTypography; // ctor default uses base typography when hover arg is null

        var sut = new AllyariaStyle(basePalette, baseTypography, spacing);

        // Act
        var actual = sut.ToCssHover();

        // Assert
        var expected = string.Concat(hoverPalette.ToCss(), hoverTypography.ToCss(), spacing.ToCss());
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("Editor", "editor")]
    [InlineData("  My--Fancy  ", "my-fancy")]
    [InlineData("x---y   z", "x-y-z")]
    public void ToCssVars_Should_Normalize_CustomPrefix_And_Propagate_To_All_Groups(string given, string normalizedBase)
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();
        var sut = new AllyariaStyle(palette, typography, spacing);

        // Act
        var actual = sut.ToCssVars(given);

        // Assert
        var expected = string.Concat(
            spacing.ToCssVars(normalizedBase),
            palette.ToCssVars(normalizedBase),
            typography.ToCssVars(normalizedBase),
            palette.ToCssVars(normalizedBase + "-disabled"),
            typography.ToCssVars(normalizedBase + "-disabled"),
            palette.ToCssVars(normalizedBase + "-hover"),
            typography.ToCssVars(normalizedBase + "-hover")
        );

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("", "aa")] // empty -> default
    [InlineData("   ", "aa")] // whitespace -> default
    [InlineData("--", "aa")] // dashes -> default after normalization/trim
    public void ToCssVars_Should_Use_DefaultPrefix_AA_When_Input_Normalizes_To_Empty(string given,
        string expectedBasePrefix)
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();
        var sut = new AllyariaStyle(palette, typography, spacing);

        // Act
        var actual = sut.ToCssVars(given);

        // Assert
        // AllyariaStyle.ToCssVars builds three groups:
        //   base (expectedBasePrefix), disabled (expectedBasePrefix + "-disabled"), and hover (expectedBasePrefix + "-hover"),
        // in this exact concatenation order: Spacing, Palette, Typography (base), then Disabled(Palette, Typography),
        // then Hover(Palette, Typography).
        var expected = string.Concat(
            spacing.ToCssVars(expectedBasePrefix),
            palette.ToCssVars(expectedBasePrefix),
            typography.ToCssVars(expectedBasePrefix),
            palette.ToCssVars(expectedBasePrefix + "-disabled"),
            typography.ToCssVars(expectedBasePrefix + "-disabled"),
            palette.ToCssVars(expectedBasePrefix + "-hover"),
            typography.ToCssVars(expectedBasePrefix + "-hover")
        );

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToCssVars_Should_Work_With_Explicit_Overrides_For_All_Sections()
    {
        // Arrange
        var basePalette = new AllyariaPalette();
        var baseTypography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();

        var disabledPalette = basePalette.ToDisabledPalette();
        var hoverPalette = basePalette.ToHoverPalette();
        var disabledTypography = baseTypography.Cascade(fontWeight: new AllyariaStringValue("600"));
        var hoverTypography = baseTypography.Cascade(fontStyle: new AllyariaStringValue("italic"));

        var sut = new AllyariaStyle(
            basePalette,
            baseTypography,
            spacing,
            hoverPalette,
            hoverTypography,
            disabledPalette,
            disabledTypography
        );

        const string prefix = "theme-x";

        // Act
        var actual = sut.ToCssVars(prefix);

        // Assert
        var expected = string.Concat(
            spacing.ToCssVars("theme-x"),
            basePalette.ToCssVars("theme-x"),
            baseTypography.ToCssVars("theme-x"),
            disabledPalette.ToCssVars("theme-x-disabled"),
            disabledTypography.ToCssVars("theme-x-disabled"),
            hoverPalette.ToCssVars("theme-x-hover"),
            hoverTypography.ToCssVars("theme-x-hover")
        );

        actual.Should().Be(expected);
    }
}
