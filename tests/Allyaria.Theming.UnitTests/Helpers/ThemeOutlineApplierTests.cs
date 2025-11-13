namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeOutlineApplierTests
{
    [Fact]
    public void Constructor_Should_AddOutlineOffsetStyleAndWidthUpdaters_When_StandardTheme()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        const bool isHighContrast = false;
        const ComponentType componentType = ComponentType.Text;
        const PaletteType paletteType = PaletteType.Secondary;

        // Act
        var sut = new ThemeOutlineApplier(
            themeMapper: themeMapper,
            isHighContrast: isHighContrast,
            componentType: componentType,
            paletteType: paletteType
        );

        // Assert
        // There should be exactly one updater for each outline-related style type
        var offsetUpdater = sut.Single(predicate: u => u.Navigator.StyleTypes.Single() == StyleType.OutlineOffset);
        var styleUpdater = sut.Single(predicate: u => u.Navigator.StyleTypes.Single() == StyleType.OutlineStyle);
        var widthUpdater = sut.Single(predicate: u => u.Navigator.StyleTypes.Single() == StyleType.OutlineWidth);

        // All three should target the requested component type
        offsetUpdater.Navigator.ComponentTypes.Single().Should().Be(expected: componentType);
        styleUpdater.Navigator.ComponentTypes.Single().Should().Be(expected: componentType);
        widthUpdater.Navigator.ComponentTypes.Single().Should().Be(expected: componentType);

        // All three are created via CreateUpdater, so they should target all interactive states
        var expectedStates = new[]
        {
            ComponentState.Default,
            ComponentState.Disabled,
            ComponentState.Dragged,
            ComponentState.Focused,
            ComponentState.Hovered,
            ComponentState.Pressed,
            ComponentState.Visited
        };

        offsetUpdater.Navigator.ComponentStates
            .OrderBy(keySelector: x => x)
            .Should()
            .BeEquivalentTo(expectation: expectedStates.OrderBy(keySelector: x => x));

        styleUpdater.Navigator.ComponentStates
            .OrderBy(keySelector: x => x)
            .Should()
            .BeEquivalentTo(expectation: expectedStates.OrderBy(keySelector: x => x));

        widthUpdater.Navigator.ComponentStates
            .OrderBy(keySelector: x => x)
            .Should()
            .BeEquivalentTo(expectation: expectedStates.OrderBy(keySelector: x => x));

        // Basic sanity on value types without depending on internal namespaces
        offsetUpdater.Value.Should().NotBeNull();
        styleUpdater.Value.Should().NotBeNull();
        widthUpdater.Value.Should().NotBeNull();

        offsetUpdater.Value!.GetType().Name.Should().Contain(expected: "StyleLength");
        widthUpdater.Value!.GetType().Name.Should().Contain(expected: "StyleLength");
        styleUpdater.Value!.GetType().Name.Should().Contain(expected: "StyleBorderOutlineStyle");
    }

    [Fact]
    public void Constructor_Should_ConfigureOutlineColorUpdaters_When_StandardTheme()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        const bool isHighContrast = false;
        const ComponentType componentType = ComponentType.Link;
        const PaletteType paletteType = PaletteType.Primary;

        // Act
        var sut = new ThemeOutlineApplier(
            themeMapper: themeMapper,
            isHighContrast: isHighContrast,
            componentType: componentType,
            paletteType: paletteType
        );

        // Assert
        sut.Count.Should().Be(expected: 17); // 14 outline-color (2 themes * 7 states) + 3 extra outline updaters

        var outlineColorUpdaters = sut
            .Where(predicate: u => u.Navigator.StyleTypes.Single() == StyleType.OutlineColor)
            .ToList();

        outlineColorUpdaters.Count.Should().Be(expected: 14);

        // All color updaters should target the provided component
        outlineColorUpdaters
            .SelectMany(selector: u => u.Navigator.ComponentTypes)
            .Distinct()
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    componentType
                }
            );

        // Outline color is mapped for each interactive state (7 of them)
        outlineColorUpdaters
            .SelectMany(selector: u => u.Navigator.ComponentStates)
            .Distinct()
            .OrderBy(keySelector: x => x)
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    ComponentState.Default,
                    ComponentState.Disabled,
                    ComponentState.Dragged,
                    ComponentState.Focused,
                    ComponentState.Hovered,
                    ComponentState.Pressed,
                    ComponentState.Visited
                }.OrderBy(keySelector: x => x)
            );

        // Each color updater should be scoped to a single state
        outlineColorUpdaters
            .All(predicate: u => u.Navigator.ComponentStates.Count == 1)
            .Should()
            .BeTrue();

        // Standard contrast should use Dark/Light theme types
        outlineColorUpdaters
            .SelectMany(selector: u => u.Navigator.ThemeTypes)
            .Distinct()
            .OrderBy(keySelector: x => x)
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    ThemeType.Dark,
                    ThemeType.Light
                }.OrderBy(keySelector: x => x)
            );
    }

    [Fact]
    public void Constructor_Should_TargetHighContrastThemeTypes_When_IsHighContrastIsTrue()
    {
        // Arrange
        var themeMapper = new ThemeMapper();
        const bool isHighContrast = true;
        const ComponentType componentType = ComponentType.Surface;
        const PaletteType paletteType = PaletteType.Surface;

        // Act
        var sut = new ThemeOutlineApplier(
            themeMapper: themeMapper,
            isHighContrast: isHighContrast,
            componentType: componentType,
            paletteType: paletteType
        );

        // Assert
        // All updaters (color + extra outline properties) should use only high contrast theme types
        var themeTypes = sut
            .SelectMany(selector: u => u.Navigator.ThemeTypes)
            .Distinct()
            .OrderBy(keySelector: x => x)
            .ToList();

        themeTypes.Should().BeEquivalentTo(
            expectation: new[]
            {
                ThemeType.HighContrastDark,
                ThemeType.HighContrastLight
            }.OrderBy(keySelector: x => x)
        );

        // Still should have the same total number of updaters as the non-HC case
        sut.Count.Should().Be(expected: 17);

        // And all updaters should be scoped to the requested component
        sut.SelectMany(selector: u => u.Navigator.ComponentTypes)
            .Distinct()
            .Should()
            .BeEquivalentTo(
                expectation: new[]
                {
                    componentType
                }
            );
    }
}
