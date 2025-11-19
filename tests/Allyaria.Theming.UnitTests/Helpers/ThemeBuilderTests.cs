namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeBuilderTests
{
    [Fact]
    public void Build_Should_CreateTheme_When_NotPreviouslyCreated()
    {
        // Arrange
        var sut = new ThemeBuilder();

        // Act
        var theme1 = sut.Build();
        var theme2 = sut.Build();

        // Assert
        theme1.Should().NotBeNull();
        theme2.Should().NotBeNull();
        theme2.Should().NotBeSameAs(unexpected: theme1);
    }

    [Fact]
    public void Build_Should_SkipCreate_When_AlreadyCreated()
    {
        // Arrange
        var sut = new ThemeBuilder();
        sut.Create();

        // Act
        var theme = sut.Build();

        // Assert
        theme.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_BeFluent_And_InitializeTheme_WithCustomBrand()
    {
        // Arrange
        var brand = new Brand();
        var sut = new ThemeBuilder();

        // Act
        var result = sut.Create(brand: brand);
        var theme = sut.Build();

        // Assert
        result.Should().BeSameAs(expected: sut);
        theme.Should().NotBeNull();
    }

    private static ThemeUpdater CreateUpdater(IReadOnlyList<ThemeType>? themeTypes,
        IReadOnlyList<ComponentState>? componentStates,
        IReadOnlyList<StyleType>? styleTypes)
    {
        var navigator = new ThemeNavigator(
            ComponentTypes: new[]
            {
                ComponentType.GlobalBody
            },
            ThemeTypes: themeTypes ?? Array.Empty<ThemeType>(),
            ComponentStates: componentStates ?? Array.Empty<ComponentState>(),
            StyleTypes: styleTypes ?? Array.Empty<StyleType>()
        );

        return new ThemeUpdater(
            Navigator: navigator,
            Value: new StyleString(value: "value")
        );
    }

    [Fact]
    public void Set_Should_ReturnSameBuilder_When_UpdaterIsValid()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Light
            },
            componentStates: new[]
            {
                ComponentState.Default
            },
            styleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        // Act
        var result = sut.Set(updater: updater);

        // Assert
        result.Should().BeSameAs(expected: sut);
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_ComponentStateIsHidden()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Light
            },
            componentStates: new[]
            {
                ComponentState.Hidden
            },
            styleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Hidden and read-only states cannot be set directly.*");
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_ComponentStateIsReadOnly()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Light
            },
            componentStates: new[]
            {
                ComponentState.ReadOnly
            },
            styleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Hidden and read-only states cannot be set directly.*");
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_ModifyingFocusedOutlineOffset()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Light
            },
            componentStates: new[]
            {
                ComponentState.Focused
            },
            styleTypes: new[]
            {
                StyleType.OutlineOffset
            }
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Cannot change focused outline offset, style or width.*");
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_ModifyingFocusedOutlineStyle()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Light
            },
            componentStates: new[]
            {
                ComponentState.Focused
            },
            styleTypes: new[]
            {
                StyleType.OutlineStyle
            }
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Cannot change focused outline offset, style or width.*");
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_ModifyingFocusedOutlineWidth()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Light
            },
            componentStates: new[]
            {
                ComponentState.Focused
            },
            styleTypes: new[]
            {
                StyleType.OutlineWidth
            }
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Cannot change focused outline offset, style or width.*");
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_SystemThemeIsPresent()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.System
            },
            componentStates: Array.Empty<ComponentState>(),
            styleTypes: Array.Empty<StyleType>()
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "System theme cannot be set directly.*");
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_TryingToSetHighContrastDarkTheme()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.HighContrastDark
            },
            componentStates: new[]
            {
                ComponentState.Default
            },
            styleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Cannot alter High Contrast themes.*");
    }

    [Fact]
    public void Set_Should_ThrowAryArgumentException_When_TryingToSetHighContrastLightTheme()
    {
        // Arrange
        var sut = new ThemeBuilder();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.HighContrastLight
            },
            componentStates: new[]
            {
                ComponentState.Default
            },
            styleTypes: new[]
            {
                StyleType.BackgroundColor
            }
        );

        // Act
        var act = () => sut.Set(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Cannot alter High Contrast themes.*");
    }
}
