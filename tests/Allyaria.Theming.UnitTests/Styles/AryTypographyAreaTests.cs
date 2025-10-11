using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryTypographyAreaTests
{
    [Fact]
    public void Cascade_Should_ReturnNewInstanceWithOverride_When_SurfaceTypographyProvided()
    {
        // Arrange
        var original = new AryTypography();
        var overrideTypography = new AryTypography(); // distinct instance/value
        var sut = new AryTypographyArea(original);

        // Act
        var result = sut.Cascade(overrideTypography);

        // Assert
        result.Surface.Should().BeEquivalentTo(overrideTypography);
        sut.Surface.Should().BeEquivalentTo(original); // original remains unchanged (immutability)
    }

    [Fact]
    public void Cascade_Should_ReturnSameValue_When_SurfaceTypographyIsNull()
    {
        // Arrange
        var original = new AryTypography();
        var sut = new AryTypographyArea(original);

        // Act
        var result = sut.Cascade();

        // Assert
        result.Should().Be(sut); // value equality for record struct
        result.Surface.Should().BeEquivalentTo(original);
    }

    [Fact]
    public void Ctor_Should_UseDefaultTypography_When_SurfaceTypographyIsNull()
    {
        // Arrange
        // (nothing to arrange)

        // Act
        var sut = new AryTypographyArea(null);

        // Assert
        sut.Surface.Should().BeEquivalentTo(new AryTypography());
    }

    [Fact]
    public void Ctor_Should_UseProvidedTypography_When_SurfaceTypographyIsProvided()
    {
        // Arrange
        var provided = new AryTypography();

        // Act
        var sut = new AryTypographyArea(provided);

        // Assert
        sut.Surface.Should().BeEquivalentTo(provided);
    }

    [Fact]
    public void Surface_Should_BeNonNull_AfterConstruction()
    {
        // Arrange & Act
        var sut = new AryTypographyArea();

        // Assert
        sut.Surface.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0)] // default enum value
    [InlineData(1)] // plausible non-default
    [InlineData(999)] // out-of-range cast still valid for enums
    public void ToTypography_Should_ReturnSurface_For_AnyComponentType(int rawType)
    {
        // Arrange
        var expected = new AryTypography();
        var sut = new AryTypographyArea(expected);
        var componentType = (ComponentType)rawType;

        // Act
        var resolved = sut.ToTypography(componentType);

        // Assert
        resolved.Should().BeEquivalentTo(sut.Surface);
    }
}
