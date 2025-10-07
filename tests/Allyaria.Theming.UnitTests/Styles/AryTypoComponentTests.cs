using Allyaria.Theming.Enumerations;
using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryTypoComponentTests
{
    [Fact]
    public void Cascade_Should_ReturnNewInstanceWithOverride_When_SurfaceTypographyProvided()
    {
        // Arrange
        var original = new AryTypo();
        var overrideTypography = new AryTypo(); // distinct instance/value
        var sut = new AryTypoComponent(original);

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
        var original = new AryTypo();
        var sut = new AryTypoComponent(original);

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
        var sut = new AryTypoComponent(null);

        // Assert
        sut.Surface.Should().BeEquivalentTo(new AryTypo());
    }

    [Fact]
    public void Ctor_Should_UseProvidedTypography_When_SurfaceTypographyIsProvided()
    {
        // Arrange
        var provided = new AryTypo();

        // Act
        var sut = new AryTypoComponent(provided);

        // Assert
        sut.Surface.Should().BeEquivalentTo(provided);
    }

    [Fact]
    public void Surface_Should_BeNonNull_AfterConstruction()
    {
        // Arrange & Act
        var sut = new AryTypoComponent();

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
        var expected = new AryTypo();
        var sut = new AryTypoComponent(expected);
        var componentType = (ComponentType)rawType;

        // Act
        var resolved = sut.ToTypography(componentType);

        // Assert
        resolved.Should().BeEquivalentTo(sut.Surface);
    }
}
