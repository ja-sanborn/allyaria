namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeApplierTests
{
    [Fact]
    public void Constructor_Should_CreateSingleUpdaterWithExpectedNavigator_When_IsHighContrastIsFalse()
    {
        // Arrange
        var brand = new Brand();
        var themeMapper = new ThemeMapper(brand: brand);
        var componentType = ComponentType.Text;
        var styleType = StyleType.Margin;
        var value = new StyleString(value: "16px");

        // Act
        var sut = new ThemeApplier(
            themeMapper: themeMapper,
            isHighContrast: false,
            componentType: componentType,
            styleType: styleType,
            value: value
        );

        // Assert
        sut.Count.Should().Be(expected: 1);

        var updater = sut[index: 0];
        updater.Value.Should().Be(expected: value);

        var navigator = updater.Navigator;

        navigator.ComponentTypes.Should().ContainSingle()
            .Which.Should().Be(expected: componentType);

        navigator.ThemeTypes.Should().HaveCount(expected: 2)
            .And.Contain(expected: ThemeType.Light)
            .And.Contain(expected: ThemeType.Dark);

        navigator.StyleTypes.Should().ContainSingle()
            .Which.Should().Be(expected: styleType);

        var expectedStates = Enum
            .GetValues<ComponentState>()
            .Where(predicate: s => s is not ComponentState.Hidden and not ComponentState.ReadOnly)
            .ToArray();

        navigator.ComponentStates.Should()
            .BeEquivalentTo(expectation: expectedStates)
            .And.HaveCount(expected: expectedStates.Length);
    }

    [Fact]
    public void Constructor_Should_CreateUpdaterWithHighContrastThemeTypes_When_IsHighContrastIsTrue()
    {
        // Arrange
        var brand = new Brand();
        var themeMapper = new ThemeMapper(brand: brand);
        var componentType = ComponentType.Surface;
        var styleType = StyleType.Padding;
        var value = new StyleString(value: "8px");

        // Act
        var sut = new ThemeApplier(
            themeMapper: themeMapper,
            isHighContrast: true,
            componentType: componentType,
            styleType: styleType,
            value: value
        );

        // Assert
        sut.Count.Should().Be(expected: 1);

        var navigator = sut[index: 0].Navigator;

        navigator.ComponentTypes.Should().ContainSingle()
            .Which.Should().Be(expected: componentType);

        navigator.ThemeTypes.Should().HaveCount(expected: 2)
            .And.Contain(expected: ThemeType.HighContrastLight)
            .And.Contain(expected: ThemeType.HighContrastDark)
            .And.NotContain(unexpected: ThemeType.Light)
            .And.NotContain(unexpected: ThemeType.Dark);

        navigator.StyleTypes.Should().ContainSingle()
            .Which.Should().Be(expected: styleType);
    }

    [Fact]
    public void GetEnumerator_Should_IterateAllUpdaters_When_UsedAsGenericEnumerable()
    {
        // Arrange
        var brand = new Brand();
        var themeMapper = new ThemeMapper(brand: brand);

        var sut = new ThemeApplier(
            themeMapper: themeMapper,
            isHighContrast: false,
            componentType: ComponentType.GlobalBody,
            styleType: StyleType.Display,
            value: new StyleString(value: "block")
        );

        // Act
        var items = sut.ToList();

        // Assert
        items.Should().HaveCount(expected: sut.Count);
        items[index: 0].Should().Be(expected: sut[index: 0]);
    }

    [Fact]
    public void GetEnumerator_Should_IterateAllUpdaters_When_UsedAsNonGenericEnumerable()
    {
        // Arrange
        var brand = new Brand();
        var themeMapper = new ThemeMapper(brand: brand);

        var sut = new ThemeApplier(
            themeMapper: themeMapper,
            isHighContrast: false,
            componentType: ComponentType.Link,
            styleType: StyleType.Color,
            value: new StyleString(value: "#FF00FF")
        );

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
        collected.Should().HaveCount(expected: sut.Count);
        collected.Single().Should().Be(expected: sut[index: 0]);
    }
}
