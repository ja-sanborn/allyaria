namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class TypographyAreaTests
{
    [Fact]
    public void Cascade_Should_Return_Equal_Instance_When_No_Overrides_Are_Provided()
    {
        // Arrange
        var surface = new Typography(
            new ThemeString("system-ui"),
            lineHeight: new ThemeNumber("1.6")
        );

        var sut = new TypographyArea(surface);

        // Act
        var cascaded = sut.Cascade();

        // Assert
        cascaded.Should().Be(sut);
        cascaded.Surface.Should().Be(surface);
    }

    [Fact]
    public void Cascade_Should_Return_New_Instance_With_Override_When_SurfaceTypography_Is_Provided()
    {
        // Arrange
        var originalSurface = new Typography(
            fontSize: new ThemeNumber("14px"),
            textAlign: new ThemeString("left")
        );

        var sut = new TypographyArea(originalSurface);

        var overrideSurface = new Typography(
            fontSize: new ThemeNumber("16px"),
            textAlign: new ThemeString("right")
        );

        // Act
        var cascaded = sut.Cascade(overrideSurface);

        // Assert
        cascaded.Surface.Should().Be(overrideSurface);
        sut.Surface.Should().Be(originalSurface); // original is unchanged (immutability)
        cascaded.Should().NotBe(sut); // theme changed
    }

    [Fact]
    public void Ctor_Should_Set_Surface_To_Default_Typography_When_SurfaceTypography_Is_Null()
    {
        // Arrange
        // (no explicit arrangement required)

        // Act
        var sut = new TypographyArea();

        // Assert
        sut.Surface.Should().Be(new Typography());
    }

    [Fact]
    public void Ctor_Should_Set_Surface_To_Provided_Typography_When_SurfaceTypography_Is_Provided()
    {
        // Arrange
        var provided = new Typography(
            new ThemeString("Inter, sans-serif"),
            new ThemeNumber("42px"),
            new ThemeString("italic"),
            new ThemeString("700"),
            new ThemeNumber("0.1em"),
            new ThemeNumber("2"),
            new ThemeString("center"),
            new ThemeString("underline"),
            new ThemeString("wavy"),
            new ThemeString("uppercase"),
            new ThemeString("middle")
        );

        // Act
        var sut = new TypographyArea(provided);

        // Assert
        sut.Surface.Should().Be(provided);
    }

    [Fact]
    public void ToTypography_Should_Return_Surface_For_Any_ComponentType()
    {
        // Arrange
        var distinctSurface = new Typography(
            fontWeight: new ThemeString("600"),
            textTransform: new ThemeString("none")
        );

        var sut = new TypographyArea(distinctSurface);

        // Act
        // Verify across all enum values to cover any future additions while still asserting current behavior
        var allTypes = Enum.GetValues(typeof(ComponentType)).Cast<ComponentType>().ToArray();
        var results = allTypes.Select(sut.ToTypography).ToArray();

        // Assert
        results.Should().HaveCount(allTypes.Length);
        results.Should().OnlyContain(t => t.Equals(distinctSurface));
    }

    [Fact]
    public void With_Expression_Should_Create_New_Instance_With_Updated_Surface()
    {
        // Arrange
        var initialSurface = new Typography(textDecorationLine: new ThemeString("none"));
        var sut = new TypographyArea(initialSurface);

        var newSurface = initialSurface.Cascade(textDecorationLine: new ThemeString("underline"));

        // Act
        var updated = sut with
        {
            Surface = newSurface
        };

        // Assert
        updated.Surface.Should().Be(newSurface);
        sut.Surface.Should().Be(initialSurface); // original unchanged
        updated.Should().NotBe(sut);
    }
}
