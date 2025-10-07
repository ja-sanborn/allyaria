using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaPaletteStateTests
{
    [Fact]
    public void Ctor_Should_CreateNonNullPalettes_When_PaletteIsNull()
    {
        // Arrange & Act
        var sut = new AllyariaPaletteState(null);

        // Assert
        sut.Default.Should().NotBeNull();
        sut.Disabled.Should().NotBeNull();
        sut.Hovered.Should().NotBeNull();
        sut.Focused.Should().NotBeNull();
        sut.Pressed.Should().NotBeNull();
        sut.Dragged.Should().NotBeNull();
    }

    [Fact]
    public void Ctor_Should_SetDefaultToProvidedInstance_When_PaletteProvided()
    {
        // Arrange
        var provided = new AllyariaPalette();

        // Act
        var sut = new AllyariaPaletteState(provided);

        // Assert
        sut.Default.Should().Be(provided);
    }

    [Theory]
    [InlineData(ComponentState.Disabled)]
    [InlineData(ComponentState.Dragged)]
    [InlineData(ComponentState.Focused)]
    [InlineData(ComponentState.Hovered)]
    [InlineData(ComponentState.Pressed)]
    public void ToPalette_Should_ReturnCorrespondingProperty_When_KnownState(ComponentState state)
    {
        // Arrange
        var sut = new AllyariaPaletteState(new AllyariaPalette());

        var expected = state switch
        {
            ComponentState.Disabled => sut.Disabled,
            ComponentState.Dragged => sut.Dragged,
            ComponentState.Focused => sut.Focused,
            ComponentState.Hovered => sut.Hovered,
            ComponentState.Pressed => sut.Pressed,
            _ => throw new ArgumentOutOfRangeException(nameof(state))
        };

        // Act
        var result = sut.ToPalette(state);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void ToPalette_Should_ReturnDefault_When_StateIsUnrecognized(int unknown)
    {
        // Arrange
        var sut = new AllyariaPaletteState(new AllyariaPalette());
        var bogus = (ComponentState)unknown;

        // Act
        var result = sut.ToPalette(bogus);

        // Assert
        result.Should().Be(sut.Default);
    }
}
