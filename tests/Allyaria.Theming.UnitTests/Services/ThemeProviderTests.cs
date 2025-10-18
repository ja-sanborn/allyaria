namespace Allyaria.Theming.UnitTests.Services;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeProviderTests
{
    [Fact]
    public void Ctor_Should_SetProvidedTheme_And_ThemeType_When_ExplicitValuesAreGiven()
    {
        // Arrange
        var theme = StyleDefaults.Theme;
        var expectedType = ThemeType.Dark;

        // Act
        var sut = new ThemeProvider(theme, expectedType);

        // Assert
        sut.ThemeType.Should().Be(expectedType);
    }

    [Fact]
    public void GetCss_Should_NotThrow_When_CalledWithTypicalParameters()
    {
        // Arrange
        var sut = new ThemeProvider();

        // Act
        var act = () => sut.GetCss(
            ComponentType.Surface,
            ComponentElevation.Mid,
            ComponentState.Default,
            "--ary"
        );

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void GetStyle_Should_NotThrow_When_CalledWithTypicalParameters()
    {
        // Arrange
        var sut = new ThemeProvider();

        // Act
        var act = () => sut.GetStyle(
            ComponentType.Surface,
            ComponentElevation.Low,
            ComponentState.Hovered
        );

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SetBorders_Should_ReturnFalse_And_NotRaiseEvent_When_BordersIsNull()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetBorders();

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
    }

    [Fact]
    public void SetDarkPalette_Should_RaiseThemeChanged_And_ReturnTrue_When_ValueChanges()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetDarkPalette(StyleDefaults.PaletteLight);

        // Assert
        changed.Should().BeTrue();
        raised.Should().BeTrue();
    }

    [Fact]
    public void SetDarkPalette_Should_ReturnFalse_And_NotRaiseEvent_When_PaletteIsNull()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetDarkPalette();

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
    }

    [Fact]
    public void SetHighContrastPalette_Should_RaiseThemeChanged_And_ReturnTrue_When_ValueChanges()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetHighContrastPalette(StyleDefaults.PaletteDark);

        // Assert
        changed.Should().BeTrue();
        raised.Should().BeTrue();
    }

    [Fact]
    public void SetHighContrastPalette_Should_ReturnFalse_And_NotRaiseEvent_When_PaletteIsNull()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetHighContrastPalette();

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
    }

    [Fact]
    public void SetLightPalette_Should_RaiseThemeChanged_And_ReturnTrue_When_ValueChanges()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetLightPalette(StyleDefaults.PaletteDark);

        // Assert
        changed.Should().BeTrue();
        raised.Should().BeTrue();
    }

    [Fact]
    public void SetLightPalette_Should_ReturnFalse_And_NotRaiseEvent_When_PaletteIsNull()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetLightPalette();

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
    }

    [Fact]
    public void SetSpacing_Should_RaiseThemeChanged_And_ReturnTrue_When_ValueChanges()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetSpacing(new ArySpacing(new AryNumberValue("100px")));

        // Assert
        changed.Should().BeTrue();
        raised.Should().BeTrue();
    }

    [Fact]
    public void SetSpacing_Should_ReturnFalse_And_NotRaiseEvent_When_SpacingIsNull()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetSpacing();

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
    }

    [Fact]
    public void SetSurfaceTypography_Should_RaiseThemeChanged_And_ReturnTrue_When_ValueChanges()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetSurfaceTypography(new AryTypography("Arial", "12px", "bold"));

        // Assert
        changed.Should().BeTrue();
        raised.Should().BeTrue();
    }

    [Fact]
    public void SetSurfaceTypography_Should_ReturnFalse_And_NotRaiseEvent_When_TypographyIsNull()
    {
        // Arrange
        var sut = new ThemeProvider();
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetSurfaceTypography();

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
    }

    [Fact]
    public void SetTheme_Should_ReturnFalse_And_NotRaiseEvent_When_SameThemeInstanceProvided()
    {
        // Arrange
        var theme = StyleDefaults.Theme;
        var sut = new ThemeProvider(theme);
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetTheme(theme);

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
    }

    [Fact]
    public void SetTheme_Should_ReturnTrue_And_RaiseEvent_When_NewThemeInstanceProvided()
    {
        // Arrange
        var theme = StyleDefaults.Theme;
        var newTheme = StyleDefaults.Theme.Cascade(StyleDefaults.Theme.Borders.Cascade(new AryNumberValue("100px")));
        var sut = new ThemeProvider(theme);
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetTheme(newTheme);

        // Assert
        changed.Should().BeTrue();
        raised.Should().BeTrue();
    }

    [Fact]
    public void SetThemeType_Should_NotRaiseThemeChanged_When_ThemeTypeIsSame()
    {
        // Arrange
        var sut = new ThemeProvider(themeType: ThemeType.System);
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetThemeType(ThemeType.System);

        // Assert
        changed.Should().BeFalse();
        raised.Should().BeFalse();
        sut.ThemeType.Should().Be(ThemeType.System);
    }

    [Fact]
    public void SetThemeType_Should_RaiseThemeChanged_When_ThemeTypeChanges()
    {
        // Arrange
        var sut = new ThemeProvider(themeType: ThemeType.Light);
        var raised = false;
        sut.ThemeChanged += (_, _) => raised = true;

        // Act
        var changed = sut.SetThemeType(ThemeType.Dark);

        // Assert
        changed.Should().BeTrue();
        raised.Should().BeTrue();
        sut.ThemeType.Should().Be(ThemeType.Dark);
    }

    [Fact]
    public void ThemeType_Should_ReturnConstructorThemeType_When_NoChangesMade()
    {
        // Arrange
        var sut = new ThemeProvider(themeType: ThemeType.Light);

        // Act
        var actual = sut.ThemeType;

        // Assert
        actual.Should().Be(ThemeType.Light);
    }
}
