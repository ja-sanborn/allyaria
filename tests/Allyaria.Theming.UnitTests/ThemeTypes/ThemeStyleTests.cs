namespace Allyaria.Theming.UnitTests.ThemeTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeStyleTests
{
    [Fact]
    public void BuildCss_Should_ApplyCssVariablePrefix_When_VarPrefixIsProvided()
    {
        // Arrange
        var sut = new ThemeStyle();

        var backgroundNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.BackgroundColor);
        sut.Set(updater: new ThemeUpdater(Navigator: backgroundNavigator, Value: new StyleString(value: "red")));

        var builder = new CssBuilder();
        var navigator = ThemeNavigator.Initialize; // emit all children

        // Act
        var result = sut.BuildCss(builder: builder, navigator: navigator, varPrefix: "theme-root");
        var css = result.ToString();

        // Assert
        css.Should().Contain(expected: "--theme-root-background-color:red");
    }

    [Fact]
    public void BuildCss_Should_RenderAllChildStyles_When_NavigatorHasNoStyleTypes()
    {
        // Arrange
        var sut = new ThemeStyle();

        var backgroundNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.BackgroundColor);
        var fontSizeNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.FontSize);

        sut.Set(updater: new ThemeUpdater(Navigator: backgroundNavigator, Value: new StyleString(value: "red")));
        sut.Set(updater: new ThemeUpdater(Navigator: fontSizeNavigator, Value: new StyleString(value: "12px")));

        var builder = new CssBuilder();
        var navigator = ThemeNavigator.Initialize; // StyleTypes is empty

        // Act
        var result = sut.BuildCss(builder: builder, navigator: navigator);
        var css = result.ToString();

        // Assert
        css.Should().Contain(expected: "background-color:red");
        css.Should().Contain(expected: "font-size:12px");
    }

    [Fact]
    public void BuildCss_Should_RenderOnlyRequestedStyles_When_NavigatorHasExplicitStyleTypes()
    {
        // Arrange
        var sut = new ThemeStyle();

        var backgroundNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.BackgroundColor);
        var fontSizeNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.FontSize);

        sut.Set(updater: new ThemeUpdater(Navigator: backgroundNavigator, Value: new StyleString(value: "red")));
        sut.Set(updater: new ThemeUpdater(Navigator: fontSizeNavigator, Value: new StyleString(value: "12px")));

        var builder = new CssBuilder();

        var navigator = ThemeNavigator
            .Initialize
            .SetStyleTypes(StyleType.BackgroundColor, StyleType.Color); // Color is not set

        // Act
        var result = sut.BuildCss(builder: builder, navigator: navigator);
        var css = result.ToString();

        // Assert
        css.Should().Contain(expected: "background-color:red");
        css.Should().NotContain(unexpected: "font-size:12px");
    }

    [Fact]
    public void Set_Should_ApplyColorStyles_When_BackgroundColorIsOpaque()
    {
        // Arrange
        var sut = new ThemeStyle();

        var backgroundNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.BackgroundColor);
        var foregroundNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.Color);

        var background = new StyleColor(value: new HexColor(value: "#000000FF"));
        var foreground = new StyleColor(value: new HexColor(value: "#222222FF"));

        // Each Set call will mark the update as color-related and invoke EnsureContrast.
        sut.Set(updater: new ThemeUpdater(Navigator: backgroundNavigator, Value: background));
        sut.Set(updater: new ThemeUpdater(Navigator: foregroundNavigator, Value: foreground));

        // Act
        var css = sut.BuildCss(builder: new CssBuilder(), navigator: ThemeNavigator.Initialize).ToString();

        // Assert
        css.Should().Contain(expected: "background-color:#");
        css.Should().Contain(expected: "color:#");
    }

    [Fact]
    public void Set_Should_RemoveExistingStyle_When_NewValueIsNullOrWhitespace()
    {
        // Arrange
        var sut = new ThemeStyle();
        var navigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.BackgroundColor);

        sut.Set(updater: new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "red")));
        var before = sut.BuildCss(builder: new CssBuilder(), navigator: ThemeNavigator.Initialize).ToString();
        before.Should().Contain(expected: "background-color:red");

        // Act
        // Whitespace value should cause the underlying entry to be removed.
        sut.Set(updater: new ThemeUpdater(Navigator: navigator, Value: new StyleString(value: "   ")));

        // Assert
        var after = sut.BuildCss(builder: new CssBuilder(), navigator: ThemeNavigator.Initialize).ToString();
        after.Should().NotContain(unexpected: "background-color:");
    }

    [Fact]
    public void Set_Should_SkipContrastAdjustment_When_BackgroundColorIsMissing()
    {
        // Arrange
        var sut = new ThemeStyle();
        var accentNavigator = ThemeNavigator.Initialize.SetStyleTypes(StyleType.AccentColor);
        var accent = new StyleColor(value: new HexColor(value: "#FFFFFFFF"));

        // First application: adds accent color, triggers EnsureContrast with no background
        sut.Set(updater: new ThemeUpdater(Navigator: accentNavigator, Value: accent));
        var cssBefore = sut.BuildCss(builder: new CssBuilder(), navigator: ThemeNavigator.Initialize).ToString();

        // Act
        // Second application again triggers EnsureContrast with background still missing,
        // taking the early-return branch in EnsureContrast.
        sut.Set(updater: new ThemeUpdater(Navigator: accentNavigator, Value: accent));

        // Assert
        var cssAfter = sut.BuildCss(builder: new CssBuilder(), navigator: ThemeNavigator.Initialize).ToString();
        cssAfter.Should().Be(expected: cssBefore);
    }
}
