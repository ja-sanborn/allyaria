namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class PaletteTests
{
    [Fact]
    public void Cascade_Should_RecomputeContrast_And_DeriveBorder_FromResultingValues()
    {
        // Arrange
        var basePalette = new Palette(
            new ThemeColor(Colors.Grey50),
            StyleDefaults.Transparent,
            new ThemeColor(Colors.Grey700)
        );

        var newSurface = new ThemeColor(Colors.Grey50);
        var newBackground = new ThemeColor(Colors.Royalblue);
        var newForegroundBase = new ThemeColor(Colors.Royalblue);

        // Act
        var next = basePalette.Cascade(
            newSurface,
            newBackground,
            newForegroundBase
        );

        // Assert
        var ratio = ((HexColor)next.ForegroundThemeColor).ContrastRatio((HexColor)next.BackgroundThemeColor);
        ratio.Should().BeGreaterThanOrEqualTo(4.5);

        var expected = next.ForegroundThemeColor.ToBorder(newSurface, newBackground, next.IsHighContrast);
        next.BorderThemeColor.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Cascade_Should_RespectExplicitBorder_And_HighContrastOverride()
    {
        // Arrange
        var start = new Palette(
            new ThemeColor(Colors.Grey50),
            new ThemeColor(Colors.Red50),
            new ThemeColor(Colors.Grey900)
        );

        var explicitBorder = new ThemeColor(Colors.Red900);

        // Act
        var next = start.Cascade(borderColor: explicitBorder, isHighContrast: true);

        // Assert
        next.IsHighContrast.Should().BeTrue();
        next.BorderThemeColor.Should().BeEquivalentTo(explicitBorder);
    }

    [Fact]
    public void Cascade_Should_Treat_BackgroundEqualSurface_AsNullFill_ForBorder()
    {
        // Arrange
        var surface = new ThemeColor(Colors.Grey50);

        var sut = new Palette(
            surface,
            new ThemeColor(Colors.Red500),
            new ThemeColor(Colors.Grey900)
        );

        // Act
        var next = sut.Cascade(backgroundColor: surface);

        // Assert
        var expected = next.ForegroundThemeColor.ToBorder(next.SurfaceThemeColor, null, next.IsHighContrast);
        next.BorderThemeColor.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Ctor_Default_Should_UseLightDefaults_And_DeriveBorder_When_NoOverrides()
    {
        // Arrange & Act
        var sut = new Palette();

        // Assert
        sut.IsHighContrast.Should().BeFalse();
        sut.SurfaceThemeColor.Should().BeEquivalentTo(StyleDefaults.BackgroundThemeColorLight);
        sut.BackgroundThemeColor.Should().BeEquivalentTo(StyleDefaults.Transparent);

        var expectedBaseFg = StyleDefaults.ForegroundThemeColorLight;
        var contrasted = expectedBaseFg.EnsureContrast(sut.BackgroundThemeColor);
        sut.ForegroundThemeColor.Should().BeEquivalentTo(contrasted);

        var expectedBorder = sut.ForegroundThemeColor.ToBorder(sut.SurfaceThemeColor);
        sut.BorderThemeColor.Should().BeEquivalentTo(expectedBorder);
    }

    [Fact]
    public void Ctor_HighContrast_Should_UseHcDefaults_And_BorderEqualsForeground()
    {
        // Arrange & Act
        var sut = new Palette(isHighContrast: true);

        // Assert
        sut.IsHighContrast.Should().BeTrue();
        sut.SurfaceThemeColor.Should().BeEquivalentTo(StyleDefaults.BackgroundThemeColorHighContrast);
        sut.BackgroundThemeColor.Should().BeEquivalentTo(StyleDefaults.Transparent);

        var expectedBaseFg = StyleDefaults.ForegroundThemeColorHighContrast;
        var contrasted = expectedBaseFg.EnsureContrast(sut.BackgroundThemeColor);
        sut.ForegroundThemeColor.Should().BeEquivalentTo(contrasted);

        sut.BorderThemeColor.Should().BeEquivalentTo(sut.ForegroundThemeColor);
    }

    [Fact]
    public void Ctor_Should_DeriveBorder_FromFill_When_FillPresent_And_NotEqualSurface()
    {
        // Arrange
        var surface = new ThemeColor(Colors.Grey50);
        var background = new ThemeColor(Colors.Red500);
        var foreground = new ThemeColor(Colors.Grey900);

        // Act
        var sut = new Palette(surface, background, foreground);

        // Assert
        var expectedFg = foreground.EnsureContrast(background);
        var expectedBorder = expectedFg.ToBorder(surface, background);
        sut.BorderThemeColor.Should().BeEquivalentTo(expectedBorder);
    }

    [Fact]
    public void Ctor_Should_RespectExplicitBorder_And_ForegroundContrast()
    {
        // Arrange
        var surface = new ThemeColor(Colors.Grey50);
        var background = new ThemeColor(Colors.Red500);
        var tooLowContrastFg = new ThemeColor(Colors.Red500);
        var explicitBorder = new ThemeColor(Colors.Bluegrey700);

        // Act
        var sut = new Palette(surface, background, tooLowContrastFg, explicitBorder);

        // Assert
        sut.BorderThemeColor.Should().BeEquivalentTo(explicitBorder);
        var ratio = ((HexColor)sut.ForegroundThemeColor).ContrastRatio((HexColor)sut.BackgroundThemeColor);
        ratio.Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void Ctor_Should_TreatTransparentOrSurfaceFill_AsNull_ForBorderDerivation()
    {
        // Arrange
        var surface = new ThemeColor(Colors.Grey50);
        var foreground = new ThemeColor(Colors.Grey900);

        var sutA = new Palette(surface, StyleDefaults.Transparent, foreground);
        var expectedBorderA = sutA.ForegroundThemeColor.ToBorder(sutA.SurfaceThemeColor);
        sutA.BorderThemeColor.Should().BeEquivalentTo(expectedBorderA);

        var sutB = new Palette(surface, surface, foreground);
        var expectedBorderB = sutB.ForegroundThemeColor.ToBorder(sutB.SurfaceThemeColor);
        sutB.BorderThemeColor.Should().BeEquivalentTo(expectedBorderB);
    }

    [Fact]
    public void Focused_Hovered_Pressed_Should_LightenForeground_And_PassHcForOthers()
    {
        // Arrange
        var sut = new Palette(
            new ThemeColor(Colors.Grey50),
            new ThemeColor(Colors.Red500),
            new ThemeColor(Colors.Grey900)
        );

        // Act
        var focused = sut.ToFocused();
        var hovered = sut.ToHovered();
        var pressed = sut.ToPressed();

        // Assert
        focused.ForegroundThemeColor.Should().NotBe(sut.ForegroundThemeColor);
        hovered.ForegroundThemeColor.Should().NotBe(sut.ForegroundThemeColor);
        pressed.ForegroundThemeColor.Should().NotBe(sut.ForegroundThemeColor);

        ((HexColor)focused.ForegroundThemeColor).ContrastRatio((HexColor)focused.BackgroundThemeColor).Should()
            .BeGreaterThanOrEqualTo(4.5);

        ((HexColor)hovered.ForegroundThemeColor).ContrastRatio((HexColor)hovered.BackgroundThemeColor).Should()
            .BeGreaterThanOrEqualTo(4.5);

        ((HexColor)pressed.ForegroundThemeColor).ContrastRatio((HexColor)pressed.BackgroundThemeColor).Should()
            .BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void ToCss_Should_EmitBackground_Foreground_AndBorder_InlineDeclarations_InOrder()
    {
        // Arrange
        var bg = new ThemeColor(Colors.Red500);
        var fg = new ThemeColor(Colors.Grey900);

        var sut = new Palette(
            new ThemeColor(Colors.Grey50),
            bg,
            fg
        );

        // Act
        var css = sut.ToCss();

        // Assert
        var expectedBg = bg.Value;
        var expectedFg = sut.ForegroundThemeColor.Value;
        var expectedBorder = sut.BorderThemeColor.Value;

        css.Should().Contain($"background-color:{expectedBg};")
            .And.Contain($"color:{expectedFg};")
            .And.Contain($"border-color:{expectedBorder};");
    }

    [Fact]
    public void ToDisabled_Should_ReturnDesaturatedPalette()
    {
        // Arrange
        var sut = new Palette(
            new ThemeColor(Colors.Rosybrown),
            new ThemeColor(Colors.Red500),
            new ThemeColor(Colors.Grey900),
            new ThemeColor(Colors.Grey700)
        );

        // Act
        var disabled = sut.ToDisabled();

        // Assert
        disabled.SurfaceThemeColor.Should().NotBe(sut.SurfaceThemeColor);
        disabled.BackgroundThemeColor.Should().NotBe(sut.BackgroundThemeColor);
        disabled.ForegroundThemeColor.Should().NotBe(sut.ForegroundThemeColor);
        disabled.BorderThemeColor.Should().NotBe(sut.BorderThemeColor);
    }

    [Fact]
    public void ToDragged_Should_NotChangeColors_When_HighContrast()
    {
        // Arrange
        var sut = new Palette(
            StyleDefaults.BackgroundThemeColorHighContrast,
            new ThemeColor(Colors.Red500),
            StyleDefaults.ForegroundThemeColorHighContrast,
            isHighContrast: true
        );

        // Act
        var dragged = sut.ToDragged();

        // Assert
        dragged.SurfaceThemeColor.Should().BeEquivalentTo(sut.SurfaceThemeColor);
        dragged.BackgroundThemeColor.Should().BeEquivalentTo(sut.BackgroundThemeColor);
        dragged.BorderThemeColor.Should().BeEquivalentTo(sut.BorderThemeColor);
        dragged.ForegroundThemeColor.Should().BeEquivalentTo(sut.ForegroundThemeColor);
    }

    [Fact]
    public void ToElevationX_Should_NoOp_When_HighContrast()
    {
        // Arrange
        var sut = new Palette(
            StyleDefaults.BackgroundThemeColorHighContrast,
            StyleDefaults.BackgroundThemeColorHighContrast,
            StyleDefaults.ForegroundThemeColorHighContrast,
            isHighContrast: true
        );

        // Act
        var e1 = sut.ToElevation1();
        var e2 = sut.ToElevation2();
        var e3 = sut.ToElevation3();
        var e4 = sut.ToElevation4();

        // Assert
        e1.Should().BeEquivalentTo(sut);
        e2.Should().BeEquivalentTo(sut);
        e3.Should().BeEquivalentTo(sut);
        e4.Should().BeEquivalentTo(sut);
    }
}
