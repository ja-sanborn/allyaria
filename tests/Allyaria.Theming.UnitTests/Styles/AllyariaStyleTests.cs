using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaStyleTests
{
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
}
