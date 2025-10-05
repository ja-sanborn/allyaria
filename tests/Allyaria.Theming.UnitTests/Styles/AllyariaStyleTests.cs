using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaStyleTests
{
    [Fact]
    public void Cascade_Should_NotMutateOriginalStyle_When_Invoked()
    {
        // Arrange
        var sut = new AllyariaStyle(MakeVariant(10, 20, 30));
        var snapshot = sut; // record struct copy captures current state

        var newDefault = MakeVariant(128, 64, 32);

        // Act
        var cascaded = sut.Cascade(newDefault);

        // Assert
        // Original remains exactly as it was (value equality for record struct)
        sut.Should().Be(snapshot);

        // Ensure some concrete difference exists between original and cascaded to validate non-mutation
        cascaded.Default.Should().Be(newDefault);
        sut.Default.Should().NotBe(newDefault);

        // Variant palettes changed only in the cascaded copy
        sut.Disabled.Palette.Should().Be(snapshot.Disabled.Palette);
        cascaded.Disabled.Palette.Should().NotBe(snapshot.Disabled.Palette);
    }

    [Fact]
    public void Cascade_Should_ReplaceDefault_And_UpdateOnlyPalettes_When_NewDefaultProvided()
    {
        // Arrange
        var original = new AllyariaStyle(MakeVariant(5, 25, 55));

        var originalHoveredTypography = original.Hovered.Typography;
        var originalHoveredSpacing = original.Hovered.Spacing;
        var originalHoveredBorder = original.Hovered.Border;
        var originalHoveredPalette = original.Hovered.Palette;

        var originalLowTypography = original.Low.Typography;
        var originalLowSpacing = original.Low.Spacing;
        var originalLowBorder = original.Low.Border;
        var originalLowPalette = original.Low.Palette;

        var newDefault = MakeVariant(200, 150, 100);

        // Act
        var cascaded = original.Cascade(newDefault);

        // Assert
        // Default replaced
        cascaded.Default.Should().Be(newDefault);

        // Non-palette parts preserved for existing variants
        cascaded.Hovered.Typography.Should().Be(originalHoveredTypography);
        cascaded.Hovered.Spacing.Should().Be(originalHoveredSpacing);
        cascaded.Hovered.Border.Should().Be(originalHoveredBorder);

        cascaded.Low.Typography.Should().Be(originalLowTypography);
        cascaded.Low.Spacing.Should().Be(originalLowSpacing);
        cascaded.Low.Border.Should().Be(originalLowBorder);

        // Palettes updated (derived from the new default) — should differ from pre-cascade palettes
        cascaded.Hovered.Palette.Should().NotBe(originalHoveredPalette);
        cascaded.Low.Palette.Should().NotBe(originalLowPalette);

        // And should not equal the new default palette for non-default variants
        cascaded.Hovered.Palette.Should().NotBe(cascaded.Default.Palette);
        cascaded.Low.Palette.Should().NotBe(cascaded.Default.Palette);
    }

    [Fact]
    public void Ctor_Should_CreateVariants_FromDefault_When_Parameterless()
    {
        // Arrange & Act
        var sut = new AllyariaStyle();

        // Assert
        // Variants share non-palette configuration with Default
        sut.Disabled.Typography.Should().Be(sut.Default.Typography);
        sut.Hovered.Typography.Should().Be(sut.Default.Typography);
        sut.Focused.Typography.Should().Be(sut.Default.Typography);
        sut.Pressed.Typography.Should().Be(sut.Default.Typography);
        sut.Dragged.Typography.Should().Be(sut.Default.Typography);
        sut.Lowest.Typography.Should().Be(sut.Default.Typography);
        sut.Low.Typography.Should().Be(sut.Default.Typography);
        sut.High.Typography.Should().Be(sut.Default.Typography);
        sut.Highest.Typography.Should().Be(sut.Default.Typography);

        sut.Disabled.Spacing.Should().Be(sut.Default.Spacing);
        sut.Hovered.Spacing.Should().Be(sut.Default.Spacing);
        sut.Focused.Spacing.Should().Be(sut.Default.Spacing);
        sut.Pressed.Spacing.Should().Be(sut.Default.Spacing);
        sut.Dragged.Spacing.Should().Be(sut.Default.Spacing);
        sut.Lowest.Spacing.Should().Be(sut.Default.Spacing);
        sut.Low.Spacing.Should().Be(sut.Default.Spacing);
        sut.High.Spacing.Should().Be(sut.Default.Spacing);
        sut.Highest.Spacing.Should().Be(sut.Default.Spacing);

        sut.Disabled.Border.Should().Be(sut.Default.Border);
        sut.Hovered.Border.Should().Be(sut.Default.Border);
        sut.Focused.Border.Should().Be(sut.Default.Border);
        sut.Pressed.Border.Should().Be(sut.Default.Border);
        sut.Dragged.Border.Should().Be(sut.Default.Border);
        sut.Lowest.Border.Should().Be(sut.Default.Border);
        sut.Low.Border.Should().Be(sut.Default.Border);
        sut.High.Border.Should().Be(sut.Default.Border);
        sut.Highest.Border.Should().Be(sut.Default.Border);

        // Palettes for non-default variants should be derived (i.e., different from Default).
        sut.Disabled.Palette.Should().NotBe(sut.Default.Palette);
        sut.Hovered.Palette.Should().NotBe(sut.Default.Palette);
        sut.Focused.Palette.Should().NotBe(sut.Default.Palette);
        sut.Pressed.Palette.Should().NotBe(sut.Default.Palette);
        sut.Dragged.Palette.Should().NotBe(sut.Default.Palette);
        sut.Lowest.Palette.Should().NotBe(sut.Default.Palette);
        sut.Low.Palette.Should().NotBe(sut.Default.Palette);
        sut.High.Palette.Should().NotBe(sut.Default.Palette);
        sut.Highest.Palette.Should().NotBe(sut.Default.Palette);
    }

    [Fact]
    public void Ctor_Should_UsePassedDefault_And_DeriveOtherVariants_When_OthersAreNull()
    {
        // Arrange
        var baseVariant = MakeVariant(10, 20, 30);

        // Act
        var sut = new AllyariaStyle(baseVariant);

        // Assert
        // Base
        sut.Default.Should().Be(baseVariant);

        // The derived variants should preserve non-palette components (Typography/Spacing/Border config object)
        // because AllyariaStyleVariant.Cascade only overrides the palette when deriving.
        sut.Disabled.Typography.Should().Be(sut.Default.Typography);
        sut.Hovered.Typography.Should().Be(sut.Default.Typography);
        sut.Focused.Typography.Should().Be(sut.Default.Typography);
        sut.Pressed.Typography.Should().Be(sut.Default.Typography);
        sut.Dragged.Typography.Should().Be(sut.Default.Typography);
        sut.Lowest.Typography.Should().Be(sut.Default.Typography);
        sut.Low.Typography.Should().Be(sut.Default.Typography);
        sut.High.Typography.Should().Be(sut.Default.Typography);
        sut.Highest.Typography.Should().Be(sut.Default.Typography);

        sut.Disabled.Spacing.Should().Be(sut.Default.Spacing);
        sut.Hovered.Spacing.Should().Be(sut.Default.Spacing);
        sut.Focused.Spacing.Should().Be(sut.Default.Spacing);
        sut.Pressed.Spacing.Should().Be(sut.Default.Spacing);
        sut.Dragged.Spacing.Should().Be(sut.Default.Spacing);
        sut.Lowest.Spacing.Should().Be(sut.Default.Spacing);
        sut.Low.Spacing.Should().Be(sut.Default.Spacing);
        sut.High.Spacing.Should().Be(sut.Default.Spacing);
        sut.Highest.Spacing.Should().Be(sut.Default.Spacing);

        sut.Disabled.Border.Should().Be(sut.Default.Border);
        sut.Hovered.Border.Should().Be(sut.Default.Border);
        sut.Focused.Border.Should().Be(sut.Default.Border);
        sut.Pressed.Border.Should().Be(sut.Default.Border);
        sut.Dragged.Border.Should().Be(sut.Default.Border);
        sut.Lowest.Border.Should().Be(sut.Default.Border);
        sut.Low.Border.Should().Be(sut.Default.Border);
        sut.High.Border.Should().Be(sut.Default.Border);
        sut.Highest.Border.Should().Be(sut.Default.Border);

        // Palettes should generally differ from the base (they are derived).
        sut.Disabled.Palette.Should().NotBe(sut.Default.Palette);
        sut.Hovered.Palette.Should().NotBe(sut.Default.Palette);
        sut.Focused.Palette.Should().NotBe(sut.Default.Palette);
        sut.Pressed.Palette.Should().NotBe(sut.Default.Palette);
        sut.Dragged.Palette.Should().NotBe(sut.Default.Palette);
        sut.Lowest.Palette.Should().NotBe(sut.Default.Palette);
        sut.Low.Palette.Should().NotBe(sut.Default.Palette);
        sut.High.Palette.Should().NotBe(sut.Default.Palette);
        sut.Highest.Palette.Should().NotBe(sut.Default.Palette);
    }

    private static AllyariaStyleVariant MakeVariant(byte r, byte g, byte b)
    {
        var bg = AllyariaColorValue.FromRgba(r, g, b);
        var fg = AllyariaColorValue.FromRgba((byte)(255 - r), (byte)(255 - g), (byte)(255 - b));
        var border = AllyariaColorValue.FromRgba(r, g, b);

        var palette = new AllyariaPalette(
            bg,
            fg,
            border,
            null,
            false
        );

        return new AllyariaStyleVariant(palette);
    }
}
