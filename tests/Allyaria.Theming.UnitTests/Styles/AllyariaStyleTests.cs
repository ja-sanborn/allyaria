using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaStyleTests
{
    [Fact]
    public void Cascade_Should_AdoptAllProvided_When_AllOverridesProvided_WithNewDefault()
    {
        // Arrange
        var original = new AllyariaStyle(MakeVariant(30, 40, 50));

        var newDefault = MakeVariant(100, 110, 120);
        var disabled = MakeVariant(101, 111, 121);
        var hovered = MakeVariant(102, 112, 122);
        var focused = MakeVariant(103, 113, 123);
        var pressed = MakeVariant(104, 114, 124);
        var dragged = MakeVariant(105, 115, 125);
        var lowest = MakeVariant(106, 116, 126);
        var low = MakeVariant(107, 117, 127);
        var high = MakeVariant(108, 118, 128);
        var highest = MakeVariant(109, 119, 129);

        // Act
        var result = original.Cascade(
            newDefault,
            disabled,
            hovered,
            focused,
            pressed,
            dragged,
            lowest,
            low,
            high,
            highest
        );

        // Assert
        result.Default.Should().Be(newDefault);
        result.Disabled.Should().Be(disabled);
        result.Hovered.Should().Be(hovered);
        result.Focused.Should().Be(focused);
        result.Pressed.Should().Be(pressed);
        result.Dragged.Should().Be(dragged);
        result.Lowest.Should().Be(lowest);
        result.Low.Should().Be(low);
        result.High.Should().Be(high);
        result.Highest.Should().Be(highest);
    }

    [Fact]
    public void Cascade_Should_KeepExistingVariants_When_DefaultIsNull_And_AllOthersNull()
    {
        // Arrange
        var original = new AllyariaStyle(
            MakeVariant(10, 20, 30),
            MakeVariant(11, 21, 31),
            MakeVariant(12, 22, 32),
            MakeVariant(13, 23, 33),
            MakeVariant(14, 24, 34),
            MakeVariant(15, 25, 35),
            MakeVariant(16, 26, 36),
            MakeVariant(17, 27, 37),
            MakeVariant(18, 28, 38),
            MakeVariant(19, 29, 39)
        );

        // Act
        var result = original.Cascade(); // all nulls (legacy branch)

        // Assert
        result.Should().Be(original); // every variant should remain identical
    }

    [Fact]
    public void Cascade_Should_OverrideSpecifiedVariants_When_DefaultIsNull()
    {
        // Arrange
        var original = new AllyariaStyle(MakeVariant(50, 60, 70));

        var overrideDisabled = MakeVariant(1, 2, 3);
        var overrideHovered = MakeVariant(4, 5, 6);
        var overrideFocused = MakeVariant(7, 8, 9);
        var overridePressed = MakeVariant(10, 11, 12);
        var overrideDragged = MakeVariant(13, 14, 15);
        var overrideLowest = MakeVariant(16, 17, 18);
        var overrideLow = MakeVariant(19, 20, 21);
        var overrideHigh = MakeVariant(22, 23, 24);
        var overrideHighest = MakeVariant(25, 26, 27);

        // Act
        var result = original.Cascade(
            disabledStyle: overrideDisabled,
            hoveredStyle: overrideHovered,
            focusedStyle: overrideFocused,
            pressedStyle: overridePressed,
            draggedStyle: overrideDragged,
            lowestStyle: overrideLowest,
            lowStyle: overrideLow,
            highStyle: overrideHigh,
            highestStyle: overrideHighest
        );

        // Assert
        // Default remains unchanged since defaultStyle was null
        result.Default.Should().Be(original.Default);

        // Only explicitly provided overrides change
        result.Disabled.Should().Be(overrideDisabled);
        result.Hovered.Should().Be(overrideHovered);
        result.Focused.Should().Be(overrideFocused);
        result.Pressed.Should().Be(overridePressed);
        result.Dragged.Should().Be(overrideDragged);
        result.Lowest.Should().Be(overrideLowest);
        result.Low.Should().Be(overrideLow);
        result.High.Should().Be(overrideHigh);
        result.Highest.Should().Be(overrideHighest);
    }

    [Fact]
    public void Cascade_Should_ReDeriveAllNulls_From_NewDefault()
    {
        // Arrange
        var originalBase = MakeVariant(60, 70, 80);

        var original = new AllyariaStyle(
            originalBase,
            MakeVariant(61, 71, 81),
            MakeVariant(62, 72, 82),
            MakeVariant(63, 73, 83),
            MakeVariant(64, 74, 84),
            MakeVariant(65, 75, 85),
            MakeVariant(66, 76, 86),
            MakeVariant(67, 77, 87),
            MakeVariant(68, 78, 88),
            MakeVariant(69, 79, 89)
        );

        var newDefault = MakeVariant(120, 130, 140);

        // For comparison, construct the "expected" by calling the ctor with only the new default.
        // This uses the same derivation logic as Cascade when defaultStyle is provided.
        var expected = new AllyariaStyle(newDefault);

        // Act
        var result = original.Cascade(newDefault); // new default provided, others null

        // Assert
        result.Default.Should().Be(newDefault);

        // All other variants should be re-derived from the new default (match what ctor would do)
        result.Disabled.Should().Be(expected.Disabled);
        result.Hovered.Should().Be(expected.Hovered);
        result.Focused.Should().Be(expected.Focused);
        result.Pressed.Should().Be(expected.Pressed);
        result.Dragged.Should().Be(expected.Dragged);
        result.Lowest.Should().Be(expected.Lowest);
        result.Low.Should().Be(expected.Low);
        result.High.Should().Be(expected.High);
        result.Highest.Should().Be(expected.Highest);

        // And they should not equal the original overrides (proving re-derivation actually happened)
        result.Disabled.Should().NotBe(original.Disabled);
        result.Hovered.Should().NotBe(original.Hovered);
        result.Focused.Should().NotBe(original.Focused);
        result.Pressed.Should().NotBe(original.Pressed);
        result.Dragged.Should().NotBe(original.Dragged);
        result.Lowest.Should().NotBe(original.Lowest);
        result.Low.Should().NotBe(original.Low);
        result.High.Should().NotBe(original.High);
        result.Highest.Should().NotBe(original.Highest);
    }

    [Fact]
    public void Cascade_Should_UseExplicitOverrides_And_RederiveOthers_When_NewDefaultProvided()
    {
        // Arrange
        var original = new AllyariaStyle(MakeVariant(10, 20, 30));
        var newDefault = MakeVariant(200, 210, 220);

        // provide some explicit overrides; leave the rest null so they get re-derived from new default
        var explicitPressed = MakeVariant(1, 2, 3);
        var explicitHigh = MakeVariant(4, 5, 6);

        var expectedFromCtor = new AllyariaStyle(newDefault); // for null cases

        // Act
        var result = original.Cascade(
            newDefault,
            pressedStyle: explicitPressed,
            highStyle: explicitHigh
        );

        // Assert
        result.Default.Should().Be(newDefault);

        // Explicit overrides win
        result.Pressed.Should().Be(explicitPressed);
        result.High.Should().Be(explicitHigh);

        // All others are re-derived from the new default (match ctor behavior)
        result.Disabled.Should().Be(expectedFromCtor.Disabled);
        result.Hovered.Should().Be(expectedFromCtor.Hovered);
        result.Focused.Should().Be(expectedFromCtor.Focused);
        result.Dragged.Should().Be(expectedFromCtor.Dragged);
        result.Lowest.Should().Be(expectedFromCtor.Lowest);
        result.Low.Should().Be(expectedFromCtor.Low);
        result.Highest.Should().Be(expectedFromCtor.Highest);
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
