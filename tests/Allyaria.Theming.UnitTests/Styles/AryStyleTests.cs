using Allyaria.Theming.Styles;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class AryStyleTests
{
    [Fact]
    public void Cascade_WithNoArgs_Returns_Identical_InstanceValues()
    {
        // Arrange
        var original = new AryStyle();

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
        var original = new AryStyle();
        var newPalette = new AryPalette();
        var newTypography = new AryTypography();
        var newSpacing = new ArySpacing();
        var newBorders = new AryBorders();

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
        var style = new AryStyle();

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
        AryPalette? palette = null;
        AryTypography? typography = null;
        ArySpacing? spacing = null;
        AryBorders? borders = null;

        // Act
        var style = new AryStyle(palette, typography, spacing, borders);

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
        var customPalette = new AryPalette();
        var customTypography = new AryTypography();
        var customSpacing = new ArySpacing();
        var customBorders = new AryBorders();

        // Act
        var style = new AryStyle(customPalette, customTypography, customSpacing, customBorders);

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
        var style = new AryStyle();

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
        var style = new AryStyle();

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

            css.Should().Contain(expectedPart + "-font-")
                .And.Contain(expectedPart + "-margin-")
                .And.Contain(expectedPart + "-padding-");
        }
    }
}
