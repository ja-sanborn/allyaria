namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeConfiguratorTests
{
    private static ThemeUpdater CreateUpdater(IEnumerable<ComponentType>? componentTypes = null,
        IEnumerable<ThemeType>? themeTypes = null,
        IEnumerable<ComponentState>? componentStates = null,
        IEnumerable<StyleType>? styleTypes = null,
        IStyleValue? value = null)
    {
        var navigator = new ThemeNavigator(
            ComponentTypes: (componentTypes ?? Array.Empty<ComponentType>()).ToArray(),
            ThemeTypes: (themeTypes ?? Array.Empty<ThemeType>()).ToArray(),
            ComponentStates: (componentStates ?? Array.Empty<ComponentState>()).ToArray(),
            StyleTypes: (styleTypes ?? Array.Empty<StyleType>()).ToArray()
        );

        return new ThemeUpdater(Navigator: navigator, Value: value);
    }

    [Fact]
    public void GetEnumerator_Generic_Should_IterateAllUpdaters()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updaters = new[]
        {
            CreateUpdater(
                themeTypes: new[]
                {
                    ThemeType.Light
                }, value: new StyleString(value: "a")
            ),
            CreateUpdater(
                themeTypes: new[]
                {
                    ThemeType.Dark
                }, value: new StyleString(value: "b")
            )
        };

        foreach (var updater in updaters)
        {
            sut.Override(updater: updater);
        }

        // Act
        var collected = sut.ToList();

        // Assert
        collected.Should().BeEquivalentTo(expectation: updaters, config: options => options.WithStrictOrdering());
    }

    [Fact]
    public void GetEnumerator_NonGeneric_Should_IterateAllUpdaters()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updaters = new[]
        {
            CreateUpdater(
                themeTypes: new[]
                {
                    ThemeType.Light
                }, value: new StyleString(value: "x")
            ),
            CreateUpdater(
                themeTypes: new[]
                {
                    ThemeType.Dark
                }, value: new StyleString(value: "y")
            )
        };

        foreach (var updater in updaters)
        {
            sut.Override(updater: updater);
        }

        var enumerable = (IEnumerable)sut;

        // Act
        var collected = new List<ThemeUpdater>();
        var enumerator = enumerable.GetEnumerator();
        using var enumerator1 = enumerator as IDisposable;

        while (enumerator.MoveNext())
        {
            collected.Add(item: (ThemeUpdater)enumerator.Current!);
        }

        // Assert
        collected.Should().BeEquivalentTo(expectation: updaters, config: options => options.WithStrictOrdering());
    }

    [Fact]
    public void Indexer_Should_ReturnUpdaterAtSpecifiedIndex()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater1 = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Light
            }, value: new StyleString(value: "one")
        );

        var updater2 = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.Dark
            }, value: new StyleString(value: "two")
        );

        sut.Override(updater: updater1)
            .Override(updater: updater2);

        // Act
        var first = sut[index: 0];
        var second = sut[index: 1];

        // Assert
        first.Should().Be(expected: updater1);
        second.Should().Be(expected: updater2);
    }

    [Fact]
    public void Override_Should_AddUpdater_When_OverrideIsValid()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            componentTypes: new[]
            {
                ComponentType.Text
            },
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
                StyleType.Color
            },
            value: new StyleString(value: "red")
        );

        // Act
        var returned = sut.Override(updater: updater);

        // Assert
        sut.Count.Should().Be(expected: 1);
        sut[index: 0].Should().Be(expected: updater);
        returned.Should().BeSameAs(expected: sut);
    }

    [Fact]
    public void Override_Should_AllowMultipleUpdaters_When_AllAreValid()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater1 = CreateUpdater(
            componentTypes: new[]
            {
                ComponentType.Surface
            },
            themeTypes: new[]
            {
                ThemeType.Light
            },
            componentStates: new[]
            {
                ComponentState.Hovered
            },
            styleTypes: new[]
            {
                StyleType.BackgroundColor
            },
            value: new StyleString(value: "blue")
        );

        var updater2 = CreateUpdater(
            componentTypes: new[]
            {
                ComponentType.Text
            },
            themeTypes: new[]
            {
                ThemeType.Dark
            },
            componentStates: new[]
            {
                ComponentState.Disabled
            },
            styleTypes: new[]
            {
                StyleType.BorderColor
            },
            value: new StyleString(value: "gray")
        );

        // Act
        sut.Override(updater: updater1)
            .Override(updater: updater2);

        // Assert
        sut.Count.Should().Be(expected: 2);
        sut[index: 0].Should().Be(expected: updater1);
        sut[index: 1].Should().Be(expected: updater2);
    }

    [Fact]
    public void Override_Should_Succeed_When_FocusedButOutlineStyleNotChanged()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            componentStates: new[]
            {
                ComponentState.Focused
            },
            styleTypes: new[]
            {
                StyleType.Color
            },
            value: new StyleString(value: "green")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().NotThrow();
        sut.Count.Should().Be(expected: 1);
        sut[index: 0].Should().Be(expected: updater);
    }

    [Fact]
    public void Override_Should_Throw_When_FocusedOutlineOffsetChanged()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            componentStates: new[]
            {
                ComponentState.Focused
            },
            styleTypes: new[]
            {
                StyleType.OutlineOffset
            },
            value: new StyleString(value: "1px")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "Cannot change focused outline offset, style or width.", StringComparison.Ordinal
                )
            );
    }

    [Fact]
    public void Override_Should_Throw_When_FocusedOutlineStyleChanged()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            componentStates: new[]
            {
                ComponentState.Focused
            },
            styleTypes: new[]
            {
                StyleType.OutlineStyle
            },
            value: new StyleString(value: "dashed")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "Cannot change focused outline offset, style or width.", StringComparison.Ordinal
                )
            );
    }

    [Fact]
    public void Override_Should_Throw_When_FocusedOutlineWidthChanged()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            componentStates: new[]
            {
                ComponentState.Focused
            },
            styleTypes: new[]
            {
                StyleType.OutlineWidth
            },
            value: new StyleString(value: "3px")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "Cannot change focused outline offset, style or width.", StringComparison.Ordinal
                )
            );
    }

    [Fact]
    public void Override_Should_Throw_When_HiddenStateIncluded()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            componentStates: new[]
            {
                ComponentState.Hidden
            },
            value: new StyleString(value: "hidden")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "Hidden and read-only states cannot be set directly.", StringComparison.Ordinal
                )
            );
    }

    [Fact]
    public void Override_Should_Throw_When_HighContrastDarkThemeIncluded()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.HighContrastDark
            },
            value: new StyleString(value: "hc-dark")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "Cannot alter High Contrast themes.", StringComparison.Ordinal
                )
            );
    }

    [Fact]
    public void Override_Should_Throw_When_HighContrastLightThemeIncluded()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.HighContrastLight
            },
            value: new StyleString(value: "hc-light")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "Cannot alter High Contrast themes.", StringComparison.Ordinal
                )
            );
    }

    [Fact]
    public void Override_Should_Throw_When_ReadOnlyStateIncluded()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            componentStates: new[]
            {
                ComponentState.ReadOnly
            },
            value: new StyleString(value: "readonly")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "Hidden and read-only states cannot be set directly.", StringComparison.Ordinal
                )
            );
    }

    [Fact]
    public void Override_Should_Throw_When_SystemThemeIncluded()
    {
        // Arrange
        var sut = new ThemeConfigurator();

        var updater = CreateUpdater(
            themeTypes: new[]
            {
                ThemeType.System
            },
            value: new StyleString(value: "ignored")
        );

        // Act
        var act = () => sut.Override(updater: updater);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Where(
                exceptionExpression: ex => ex.Message.Contains(
                    "System theme cannot be set directly.", StringComparison.Ordinal
                )
            );
    }
}
