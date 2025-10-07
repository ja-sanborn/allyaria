using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaThemeTests
{
    public static IEnumerable<TEnum> All<TEnum>()
        where TEnum : struct, Enum
        => Enum.GetValues<TEnum>();

    public static IEnumerable<object[]> AllEnumCombos()
    {
        foreach (var theme in All<ThemeType>())
        {
            foreach (var component in All<ComponentType>())
            {
                foreach (var elevation in All<ComponentElevation>())
                {
                    foreach (var state in All<ComponentState>())
                    {
                        yield return new object[]
                        {
                            theme,
                            component,
                            elevation,
                            state
                        };
                    }
                }
            }
        }
    }

    [Fact]
    public void Cascade_LightPaletteOverride_Should_AffectOnly_LightThemeCss()
    {
        // Arrange
        var sut = new AllyariaTheme();
        var baseLightCss = sut.ToCss(ThemeType.Light, ComponentType.Surface);
        var baseDarkCss = sut.ToCss(ThemeType.Dark, ComponentType.Surface);
        var baseHcCss = sut.ToCss(ThemeType.HighContrast, ComponentType.Surface);

        // Act
        var updated = sut.Cascade(
            paletteLight: new AllyariaPalette(
                "#101010",
                "#EFEFEF"
            )
        );

        var updatedLightCss = updated.ToCss(ThemeType.Light, ComponentType.Surface);
        var updatedDarkCss = updated.ToCss(ThemeType.Dark, ComponentType.Surface);
        var updatedHcCss = updated.ToCss(ThemeType.HighContrast, ComponentType.Surface);

        // Assert
        updatedLightCss.Should().NotBe(baseLightCss);
        updatedLightCss.Should().Contain("#101010").And.Contain("#EFEFEF");

        // Dark / HighContrast should remain unaffected by a Light-only override
        updatedDarkCss.Should().Be(baseDarkCss);
        updatedHcCss.Should().Be(baseHcCss);
    }

    [Fact]
    public void Cascade_Should_NotMutate_OriginalTheme()
    {
        // Arrange
        var original = new AllyariaTheme();

        // Pick a CSS we can compare (Light + Surface, defaults)
        var originalCss = original.ToCss(ThemeType.Light, ComponentType.Surface);

        // Act
        var updated = original.Cascade(
            paletteLight: new AllyariaPalette(
                "#111111",
                "#EEEEEE"
            )
        );

        // Assert
        // Original should remain the same
        var originalAgain = original.ToCss(ThemeType.Light, ComponentType.Surface);
        originalAgain.Should().Be(originalCss);

        // Updated should differ for Light (we changed Light palette only)
        var updatedCssLight = updated.ToCss(ThemeType.Light, ComponentType.Surface);
        updatedCssLight.Should().NotBe(originalCss);
    }

    [Fact]
    public void Cascade_Should_Return_IdenticalTheme_When_AllOverridesNull()
    {
        // Arrange
        var sut = new AllyariaTheme();

        // Act
        var cascaded = sut.Cascade();

        // Assert
        cascaded.Should().Be(sut);
    }

    [Fact]
    public void Ctor_Default_Should_CreateUsableTheme()
    {
        // Arrange & Act
        var sut = new AllyariaTheme();

        // Assert (smoke: ToStyle/ToCss across a representative set should not throw)
        var act1 = () => sut.ToStyle(ThemeType.Light, ComponentType.Surface);
        var act2 = () => sut.ToCss(ThemeType.Dark, ComponentType.Surface);
        act1.Should().NotThrow();
        act2.Should().NotThrow();
    }

    [Fact]
    public void Ctor_WithNulls_Should_Equal_Default()
    {
        // Arrange
        var expected = new AllyariaTheme();

        // Act
        var actual = new AllyariaTheme(null);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("ally")]
    [InlineData("x")]
    public void ToCss_Should_Handle_VarPrefix(string? prefix)
    {
        // Arrange
        var sut = new AllyariaTheme();

        // Act
        var act = () => sut.ToCss(
            ThemeType.Light, ComponentType.Surface, ComponentElevation.Mid, ComponentState.Default, prefix
        );

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(AllEnumCombos))]
    public void ToCss_Should_NotThrow_For_AllEnumCombinations(ThemeType themeType,
        ComponentType componentType,
        ComponentElevation elevation,
        ComponentState state)
    {
        // Arrange
        var sut = new AllyariaTheme();

        // Act
        var act = () => sut.ToCss(themeType, componentType, elevation, state);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(AllEnumCombos))]
    public void ToStyle_Should_NotThrow_For_AllEnumCombinations(ThemeType themeType,
        ComponentType componentType,
        ComponentElevation elevation,
        ComponentState state)
    {
        // Arrange
        var sut = new AllyariaTheme();

        // Act
        var act = () => sut.ToStyle(themeType, componentType, elevation, state);

        // Assert
        act.Should().NotThrow();
    }
}
