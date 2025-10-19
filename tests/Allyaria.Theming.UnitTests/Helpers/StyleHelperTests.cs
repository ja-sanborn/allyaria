namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleHelperTests
{
    [Fact]
    public void ToCss_Should_AppendMultipleDeclarations_When_CalledMultipleTimes()
    {
        // Arrange
        var builder = new StringBuilder();
        var red = new ThemeString("red");
        var blue = new ThemeString("blue");

        // Act
        builder.ToCss("color", red, "theme");
        builder.ToCss("border-color", blue, "theme");

        // Assert
        builder.ToString().Should().Be("--theme-color:red;--theme-border-color:blue;");
    }

    [Fact]
    public void ToCss_Should_AppendPropertyWithNormalizedPrefix_When_VarPrefixIsProvided()
    {
        // Arrange
        var builder = new StringBuilder();
        var value = new ThemeString("#fff");

        // Act
        builder.ToCss("background", value, "Main Theme");

        // Assert
        builder.ToString().Should().Be("--main-theme-background:#fff;");
    }

    [Fact]
    public void ToCss_Should_AppendPropertyWithoutPrefix_When_VarPrefixIsNullOrWhitespace()
    {
        // Arrange
        var builder = new StringBuilder();
        var value = new ThemeString("blue");

        // Act
        builder.ToCss("color", value, null);

        // Assert
        builder.ToString().Should().Be("color:blue;");
    }

    [Fact]
    public void ToCss_Should_DoNothing_When_PropertyNameIsNullOrWhitespace()
    {
        // Arrange
        var builder = new StringBuilder();
        var value = new ThemeString("red");

        // Act
        builder.ToCss(null!, value, "theme");
        builder.ToCss(" ", value, "theme");

        // Assert
        builder.ToString().Should().BeEmpty();
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("   ", "")]
    [InlineData("MyPrefix", "myprefix")]
    [InlineData("My Prefix", "my-prefix")]
    [InlineData("My--Prefix", "my-prefix")]
    [InlineData("--Double--Dash--", "double-dash")]
    [InlineData("  Mixed- SPACES  ", "mixed-spaces")]
    public void ToPrefix_Should_ReturnNormalizedPrefix_When_InputHasVariousFormats(string? input, string expected)
    {
        // Act
        var result = input.ToCssName();

        // Assert
        result.Should().Be(expected);
    }
}
