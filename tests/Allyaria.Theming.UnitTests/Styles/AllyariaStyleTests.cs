using Allyaria.Theming.Styles;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class AllyariaStyleTests
{
    [Fact]
    public void Cascade_WithNoArgs_Returns_Identical_InstanceValues()
    {
        // Arrange
        var original = new AllyariaStyle();

        // Act
        var cascaded = original.Cascade();

        // Assert
        cascaded.Palette.Should().Be(original.Palette);
        cascaded.Typography.Should().Be(original.Typography);
        cascaded.Spacing.Should().Be(original.Spacing);
        cascaded.Border.Should().Be(original.Border);
    }

    [Fact]
    public void Cascade_WithOverrides_Replaces_Only_Provided_Subcomponents()
    {
        // Arrange
        var original = new AllyariaStyle();
        var newPalette = new AllyariaPalette();
        var newTypography = new AllyariaTypography();
        var newSpacing = new AllyariaSpacing();
        var newBorders = new AllyariaBorders();

        // Act
        var cascaded = original.Cascade(
            newPalette,
            newTypography,
            newSpacing,
            newBorders
        );

        // Assert
        cascaded.Palette.Should().Be(newPalette);
        cascaded.Typography.Should().Be(newTypography);
        cascaded.Spacing.Should().Be(newSpacing);
        cascaded.Border.Should().Be(newBorders);
    }

    [Fact]
    public void Ctor_Default_Initializes_All_Default_Subcomponents()
    {
        // Arrange

        // Act
        var style = new AllyariaStyle();

        // Assert
        style.Palette.Should().NotBeNull();
        style.Typography.Should().NotBeNull();
        style.Spacing.Should().NotBeNull();
        style.Border.Should().NotBeNull();
    }

    [Fact]
    public void Ctor_WithNulls_Initializes_All_Default_Subcomponents()
    {
        // Arrange
        AllyariaPalette? palette = null;
        AllyariaTypography? typography = null;
        AllyariaSpacing? spacing = null;
        AllyariaBorders? borders = null;

        // Act
        var style = new AllyariaStyle(palette, typography, spacing, borders);

        // Assert
        style.Palette.Should().NotBeNull();
        style.Typography.Should().NotBeNull();
        style.Spacing.Should().NotBeNull();
        style.Border.Should().NotBeNull();
    }

    [Fact]
    public void Ctor_WithOverrides_Uses_Provided_Subcomponents()
    {
        // Arrange
        var customPalette = new AllyariaPalette();
        var customTypography = new AllyariaTypography();
        var customSpacing = new AllyariaSpacing();
        var customBorders = new AllyariaBorders();

        // Act
        var style = new AllyariaStyle(customPalette, customTypography, customSpacing, customBorders);

        // Assert
        style.Palette.Should().Be(customPalette);
        style.Typography.Should().Be(customTypography);
        style.Spacing.Should().Be(customSpacing);
        style.Border.Should().Be(customBorders);
    }

    [Fact]
    public void ToCss_WithEmptyPrefix_Concatenates_All_Subcomponents_Css()
    {
        // Arrange
        var style = new AllyariaStyle();

        // Act
        var css = style.ToCss();

        // Assert
        css.Should().NotBeNull();
        css.Should().NotBeEmpty();

        css.Should().Contain("font-")
            .And.Contain("margin-")
            .And.Contain("padding-");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Ui-Theme")]
    [InlineData("ui theme")]
    public void ToCss_WithPrefix_Normalizes_And_Propagates_To_Subcomponents(string? prefix)
    {
        // Arrange
        var style = new AllyariaStyle();

        // Act
        var css = style.ToCss(prefix);

        // Assert
        css.Should().NotBeNullOrEmpty();

        var normalized = (prefix ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(normalized))
        {
            css.Should().NotContain("--");
        }
        else
        {
            var expectedPart = "--" + Regex
                .Replace(normalized, @"[\s-]+", "-")
                .Trim('-')
                .ToLowerInvariant();

            css.Should().Contain(expectedPart + "-var-font-")
                .And.Contain(expectedPart + "-var-margin-")
                .And.Contain(expectedPart + "-var-padding-");
        }
    }
}
