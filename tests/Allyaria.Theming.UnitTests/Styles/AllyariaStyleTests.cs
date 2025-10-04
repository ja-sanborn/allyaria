using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaStyleTests
{
    [Fact]
    public void Cascade_Should_OverrideOnlySpecifiedVariants_When_SomeProvided()
    {
        // Arrange
        var initial = new AllyariaStyle(MakeVariant(100, 110, 120));
        var newDefault = MakeVariant(1, 2, 3);
        var newPressed = MakeVariant(7, 8, 9);

        // Act
        var result = initial.Cascade(newDefault, pressedStyle: newPressed);

        // Assert
        result.Default.Should().Be(newDefault);
        result.Pressed.Should().Be(newPressed);

        // Unchanged variants should remain equal to the original
        result.Disabled.Should().Be(initial.Disabled);
        result.Hovered.Should().Be(initial.Hovered);
        result.Focused.Should().Be(initial.Focused);
        result.Dragged.Should().Be(initial.Dragged);
        result.Lowest.Should().Be(initial.Lowest);
        result.Low.Should().Be(initial.Low);
        result.High.Should().Be(initial.High);
        result.Highest.Should().Be(initial.Highest);
    }

    [Fact]
    public void Cascade_Should_ReturnIdenticalInstance_When_NoOverridesSpecified()
    {
        // Arrange
        var initial = new AllyariaStyle(MakeVariant(15, 25, 35));

        // Act
        var result = initial.Cascade();

        // Assert
        result.Should().Be(initial);
    }

    [Fact]
    public void Ctor_Should_RespectExplicitOverrides_When_SomeVariantsProvided()
    {
        // Arrange
        var baseVariant = MakeVariant(40, 50, 60);
        var explicitPressed = MakeVariant(200, 10, 10);
        var explicitHighest = MakeVariant(5, 200, 240);
        var explicitHovered = MakeVariant(80, 90, 100);

        // Act
        var sut = new AllyariaStyle(
            baseVariant,
            pressedStyle: explicitPressed,
            highestStyle: explicitHighest,
            hoveredStyle: explicitHovered
        );

        // Assert
        sut.Default.Should().Be(baseVariant);
        sut.Pressed.Should().Be(explicitPressed);
        sut.Highest.Should().Be(explicitHighest);
        sut.Hovered.Should().Be(explicitHovered);

        // Unspecified variants should still be populated (derived internally), i.e., not equal to default palette typically
        sut.Disabled.Palette.Should().NotBe(baseVariant.Palette);
        sut.Focused.Palette.Should().NotBe(baseVariant.Palette);
        sut.Dragged.Palette.Should().NotBe(baseVariant.Palette);
        sut.Lowest.Palette.Should().NotBe(baseVariant.Palette);
        sut.Low.Palette.Should().NotBe(baseVariant.Palette);
        sut.High.Palette.Should().NotBe(baseVariant.Palette);
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

    [Fact]
    public void Immutability_Should_NotMutateOriginal_When_Cascading()
    {
        // Arrange
        var original = new AllyariaStyle(MakeVariant(60, 70, 80));
        var replacementHighest = MakeVariant(120, 130, 140);

        // Act
        var modified = original.Cascade(highestStyle: replacementHighest);

        // Assert
        modified.Should().NotBe(original);

        // Original remains unchanged
        original.Highest.Should().NotBe(replacementHighest);

        // Only the overridden piece changed
        modified.Highest.Should().Be(replacementHighest);
        modified.Default.Should().Be(original.Default);
        modified.Disabled.Should().Be(original.Disabled);
        modified.Hovered.Should().Be(original.Hovered);
        modified.Focused.Should().Be(original.Focused);
        modified.Pressed.Should().Be(original.Pressed);
        modified.Dragged.Should().Be(original.Dragged);
        modified.Lowest.Should().Be(original.Lowest);
        modified.Low.Should().Be(original.Low);
        modified.High.Should().Be(original.High);
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

    [Fact]
    public void ValueSemantics_Should_TreatStructsAsEqual_When_AllVariantsMatch()
    {
        // Arrange
        var defaultVariant = MakeVariant(20, 30, 40);
        var disabled = MakeVariant(21, 31, 41);
        var hovered = MakeVariant(22, 32, 42);
        var focused = MakeVariant(23, 33, 43);
        var pressed = MakeVariant(24, 34, 44);
        var dragged = MakeVariant(25, 35, 45);
        var lowest = MakeVariant(26, 36, 46);
        var low = MakeVariant(27, 37, 47);
        var high = MakeVariant(28, 38, 48);
        var highest = MakeVariant(29, 39, 49);

        var a = new AllyariaStyle(
            defaultVariant, disabled, hovered, focused, pressed, dragged, lowest, low, high, highest
        );

        var b = new AllyariaStyle(
            defaultVariant, disabled, hovered, focused, pressed, dragged, lowest, low, high, highest
        );

        // Act
        var equal = a == b; // record struct value-equality
        var hashEqual = a.GetHashCode() == b.GetHashCode();

        // Assert
        equal.Should().BeTrue();
        hashEqual.Should().BeTrue();

        // And differing in any one variant should break equality
        var c = b.Cascade(highStyle: MakeVariant(200, 201, 202));
        (c == b).Should().BeFalse();
    }
}
