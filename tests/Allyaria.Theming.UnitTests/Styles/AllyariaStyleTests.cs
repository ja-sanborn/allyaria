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
        sut.Disabled.PaletteVariant.Should().Be(snapshot.Disabled.PaletteVariant);
        cascaded.Disabled.PaletteVariant.Should().NotBe(snapshot.Disabled.PaletteVariant);
    }

    [Fact]
    public void Cascade_Should_PreserveNonPaletteConfig_AcrossMultipleCascades_When_Chained()
    {
        // Arrange
        var sut = new AllyariaStyle(MakeVariant(30, 60, 90));

        var originalPressedTypography = sut.Pressed.Typography;
        var originalPressedSpacing = sut.Pressed.Spacing;
        var originalPressedBorder = sut.Pressed.Border;

        var firstDefault = MakeVariant(180, 100, 40);
        var secondDefault = MakeVariant(10, 220, 140);

        // Act
        var afterFirst = sut.Cascade(firstDefault);
        var afterSecond = afterFirst.Cascade(secondDefault);

        // Assert
        // Default should reflect the last cascade
        afterSecond.Default.Should().Be(secondDefault);

        // Non-paletteVariant members preserved across cascades
        afterSecond.Pressed.Typography.Should().Be(originalPressedTypography);
        afterSecond.Pressed.Spacing.Should().Be(originalPressedSpacing);
        afterSecond.Pressed.Border.Should().Be(originalPressedBorder);

        // Palettes should have changed between each cascade (Pressed as representative)
        afterFirst.Pressed.PaletteVariant.Should().NotBe(sut.Pressed.PaletteVariant);
        afterSecond.Pressed.PaletteVariant.Should().NotBe(afterFirst.Pressed.PaletteVariant);
    }

    [Fact]
    public void Cascade_Should_ReplaceDefault_And_UpdateOnlyPalettes_When_NewDefaultProvided()
    {
        // Arrange
        var original = new AllyariaStyle(MakeVariant(5, 25, 55));

        var originalHoveredTypography = original.Hovered.Typography;
        var originalHoveredSpacing = original.Hovered.Spacing;
        var originalHoveredBorder = original.Hovered.Border;
        var originalHoveredPalette = original.Hovered.PaletteVariant;

        var originalLowTypography = original.Low.Typography;
        var originalLowSpacing = original.Low.Spacing;
        var originalLowBorder = original.Low.Border;
        var originalLowPalette = original.Low.PaletteVariant;

        var newDefault = MakeVariant(200, 150, 100);

        // Act
        var cascaded = original.Cascade(newDefault);

        // Assert
        // Default replaced
        cascaded.Default.Should().Be(newDefault);

        // Non-paletteVariant parts preserved for existing variants
        cascaded.Hovered.Typography.Should().Be(originalHoveredTypography);
        cascaded.Hovered.Spacing.Should().Be(originalHoveredSpacing);
        cascaded.Hovered.Border.Should().Be(originalHoveredBorder);

        cascaded.Low.Typography.Should().Be(originalLowTypography);
        cascaded.Low.Spacing.Should().Be(originalLowSpacing);
        cascaded.Low.Border.Should().Be(originalLowBorder);

        // Palettes updated (derived from the new default) — should differ from pre-cascade palettes
        cascaded.Hovered.PaletteVariant.Should().NotBe(originalHoveredPalette);
        cascaded.Low.PaletteVariant.Should().NotBe(originalLowPalette);

        // And should not equal the new default paletteVariant for non-default variants
        cascaded.Hovered.PaletteVariant.Should().NotBe(cascaded.Default.PaletteVariant);
        cascaded.Low.PaletteVariant.Should().NotBe(cascaded.Default.PaletteVariant);
    }

    [Fact]
    public void Cascade_Should_UpdatePalettes_For_AllVariants_When_NewDefaultProvided()
    {
        // Arrange
        var sut = new AllyariaStyle(MakeVariant(12, 34, 56));

        var originalPalettes = new[]
        {
            sut.Disabled.PaletteVariant,
            sut.Hovered.PaletteVariant,
            sut.Focused.PaletteVariant,
            sut.Pressed.PaletteVariant,
            sut.Dragged.PaletteVariant,
            sut.Lowest.PaletteVariant,
            sut.Low.PaletteVariant,
            sut.High.PaletteVariant,
            sut.Highest.PaletteVariant
        };

        var newDefault = MakeVariant(200, 180, 160);

        // Act
        var cascaded = sut.Cascade(newDefault);

        // Assert
        var updatedPalettes = new[]
        {
            cascaded.Disabled.PaletteVariant,
            cascaded.Hovered.PaletteVariant,
            cascaded.Focused.PaletteVariant,
            cascaded.Pressed.PaletteVariant,
            cascaded.Dragged.PaletteVariant,
            cascaded.Lowest.PaletteVariant,
            cascaded.Low.PaletteVariant,
            cascaded.High.PaletteVariant,
            cascaded.Highest.PaletteVariant
        };

        // all non-default variant palettes should change after cascading
        updatedPalettes.Should().HaveCount(originalPalettes.Length)
            .And.SatisfyRespectively(
                p0 => p0.Should().NotBe(originalPalettes[0]),
                p1 => p1.Should().NotBe(originalPalettes[1]),
                p2 => p2.Should().NotBe(originalPalettes[2]),
                p3 => p3.Should().NotBe(originalPalettes[3]),
                p4 => p4.Should().NotBe(originalPalettes[4]),
                p5 => p5.Should().NotBe(originalPalettes[5]),
                p6 => p6.Should().NotBe(originalPalettes[6]),
                p7 => p7.Should().NotBe(originalPalettes[7]),
                p8 => p8.Should().NotBe(originalPalettes[8])
            );

        // default paletteVariant specifically updates to the new one
        cascaded.Default.PaletteVariant.Should().Be(newDefault.PaletteVariant);
    }

    [Fact]
    public void Ctor_Should_CreateVariants_FromDefault_When_Parameterless()
    {
        // Arrange & Act
        var sut = new AllyariaStyle();

        // Assert
        // Variants share non-paletteVariant configuration with Default
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
        sut.Disabled.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Hovered.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Focused.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Pressed.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Dragged.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Lowest.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Low.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.High.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Highest.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
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

        // The derived variants should preserve non-paletteVariant components (Typography/Spacing/Border config object)
        // because AllyariaStyleVariant.Cascade only overrides the paletteVariant when deriving.
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
        sut.Disabled.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Hovered.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Focused.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Pressed.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Dragged.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Lowest.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Low.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.High.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
        sut.Highest.PaletteVariant.Should().NotBe(sut.Default.PaletteVariant);
    }

    private static AllyariaStyleVariant MakeVariant(byte r, byte g, byte b)
    {
        var background = AllyariaColorValue.FromRgba(r, g, b);
        var foreground = AllyariaColorValue.FromRgba((byte)(255 - r), (byte)(255 - g), (byte)(255 - b));
        var border = AllyariaColorValue.FromRgba(r, g, b);
        var palette = new AllyariaPaletteVariant(background, foreground, border);

        return new AllyariaStyleVariant(palette);
    }
}
