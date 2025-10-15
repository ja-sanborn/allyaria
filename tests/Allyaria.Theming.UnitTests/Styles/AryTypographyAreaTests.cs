using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryTypographyAreaTests
{
    [Fact]
    public void Cascade_Should_Return_Equal_Instance_When_No_Overrides_Are_Provided()
    {
        // Arrange
        var surface = new AryTypography(
            new AryStringValue("system-ui"),
            lineHeight: new AryNumberValue("1.6")
        );

        var sut = new AryTypographyArea(surface);

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
        var originalSurface = new AryTypography(
            fontSize: new AryNumberValue("14px"),
            textAlign: new AryStringValue("left")
        );

        var sut = new AryTypographyArea(originalSurface);

        var overrideSurface = new AryTypography(
            fontSize: new AryNumberValue("16px"),
            textAlign: new AryStringValue("right")
        );

        // Act
        var cascaded = sut.Cascade(overrideSurface);

        // Assert
        cascaded.Surface.Should().Be(overrideSurface);
        sut.Surface.Should().Be(originalSurface); // original is unchanged (immutability)
        cascaded.Should().NotBe(sut); // value changed
    }

    [Fact]
    public void Ctor_Should_Set_Surface_To_Default_Typography_When_SurfaceTypography_Is_Null()
    {
        // Arrange
        // (no explicit arrangement required)

        // Act
        var sut = new AryTypographyArea();

        // Assert
        sut.Surface.Should().Be(new AryTypography());
    }

    [Fact]
    public void Ctor_Should_Set_Surface_To_Provided_Typography_When_SurfaceTypography_Is_Provided()
    {
        // Arrange
        var provided = new AryTypography(
            new AryStringValue("Inter, sans-serif"),
            new AryNumberValue("42px"),
            new AryStringValue("italic"),
            new AryStringValue("700"),
            new AryNumberValue("0.1em"),
            new AryNumberValue("2"),
            new AryStringValue("center"),
            new AryStringValue("underline"),
            new AryStringValue("wavy"),
            new AryStringValue("uppercase"),
            new AryStringValue("middle")
        );

        // Act
        var sut = new AryTypographyArea(provided);

        // Assert
        sut.Surface.Should().Be(provided);
    }

    [Fact]
    public void ToTypography_Should_Return_Surface_For_Any_ComponentType()
    {
        // Arrange
        var distinctSurface = new AryTypography(
            fontWeight: new AryStringValue("600"),
            textTransform: new AryStringValue("none")
        );

        var sut = new AryTypographyArea(distinctSurface);

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
        var initialSurface = new AryTypography(textDecorationLine: new AryStringValue("none"));
        var sut = new AryTypographyArea(initialSurface);

        var newSurface = initialSurface.Cascade(textDecorationLine: new AryStringValue("underline"));

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
