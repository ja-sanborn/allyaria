using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaStyleTests
{
    [Fact]
    public void Ctor_Should_Use_Fallbacks_When_Optional_Args_Are_Null()
    {
        // Arrange
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();
        var borders = new AllyariaBorders();

        // Act
        var sut = new AllyariaStyle(palette, typography, spacing, borders);

        // Assert
        sut.Palette.Should().Be(palette);
        sut.Typography.Should().Be(typography);
        sut.Spacing.Should().Be(spacing);
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
        var borders = new AllyariaBorders();

        var overrideHoverPalette = palette.ToHoverPalette();
        var overrideDisabledPalette = palette.ToDisabledPalette();
        var overrideHoverTypography = typography.Cascade(fontStyle: new AllyariaStringValue("italic"));
        var overrideDisabledTypography = typography.Cascade(fontWeight: new AllyariaStringValue("700"));

        // Act
        var sut = new AllyariaStyle(
            palette,
            typography,
            spacing,
            borders,
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
        var palette = new AllyariaPalette();
        var typography = new AllyariaTypography();
        var spacing = new AllyariaSpacing();
        var borders = new AllyariaBorders();
        var sut = new AllyariaStyle(palette, typography, spacing, borders);

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
        var borders = new AllyariaBorders();

        var disabledPalette = basePalette.ToDisabledPalette();
        var disabledTypography = baseTypography;

        var sut = new AllyariaStyle(basePalette, baseTypography, spacing, borders);

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
        var borders = new AllyariaBorders();
        var hoverPalette = basePalette.ToHoverPalette();
        var hoverTypography = baseTypography;
        var sut = new AllyariaStyle(basePalette, baseTypography, spacing, borders);

        // Act
        var actual = sut.ToCssHover();

        // Assert
        var expected = string.Concat(hoverPalette.ToCss(), hoverTypography.ToCss(), spacing.ToCss());
        actual.Should().Be(expected);
    }
}
