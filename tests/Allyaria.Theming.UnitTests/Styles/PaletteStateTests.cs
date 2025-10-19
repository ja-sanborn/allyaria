namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class PaletteStateTests
{
    [Fact]
    public void Ctor_Should_Derive_All_State_Palettes_From_Default_When_BaselineProvided()
    {
        // Arrange
        var baseline = new Palette(
            null,
            StyleDefaults.PaletteLight.BackgroundColor,
            StyleDefaults.PaletteLight.ForegroundColor
        );

        // Act
        var sut = new PaletteState(baseline);

        // Assert
        sut.Default.Should().Be(baseline);
        sut.Disabled.Should().Be(baseline.ToDisabled());
        sut.Hovered.Should().Be(baseline.ToHovered());
        sut.Focused.Should().Be(baseline.ToFocused());
        sut.Pressed.Should().Be(baseline.ToPressed());
        sut.Dragged.Should().Be(baseline.ToDragged());
    }

    [Theory]
    [InlineData(ComponentState.Disabled)]
    [InlineData(ComponentState.Dragged)]
    [InlineData(ComponentState.Focused)]
    [InlineData(ComponentState.Hovered)]
    [InlineData(ComponentState.Pressed)]
    public void ToPalette_Should_Return_CorrespondingPalette_When_StateIsRecognized(ComponentState state)
    {
        // Arrange
        var basePalette = new Palette(); // deterministic defaults
        var sut = new PaletteState(basePalette);

        // Act
        var result = sut.ToPalette(state);

        // Assert
        switch (state)
        {
            case ComponentState.Disabled:
                result.Should().Be(sut.Disabled);

                break;
            case ComponentState.Dragged:
                result.Should().Be(sut.Dragged);

                break;
            case ComponentState.Focused:
                result.Should().Be(sut.Focused);

                break;
            case ComponentState.Hovered:
                result.Should().Be(sut.Hovered);

                break;
            case ComponentState.Pressed:
                result.Should().Be(sut.Pressed);

                break;
            default:
                throw new InvalidOperationException("Unreachable for recognized states.");
        }
    }

    [Fact]
    public void ToPalette_Should_Return_Default_When_StateIsUnrecognized()
    {
        // Arrange
        var basePalette = new Palette();
        var sut = new PaletteState(basePalette);
        var unknown = (ComponentState)999_999; // ensure it hits the default branch

        // Act
        var result = sut.ToPalette(unknown);

        // Assert
        result.Should().Be(sut.Default);
    }
}
