using Allyaria.Theming.Helpers;
using System.Text;

namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleHelperTests
{
    [Fact]
    public void ToCss_Should_AppendCssFromValue_When_AllInputsValid_And_PrefixIsNormalized()
    {
        // Arrange
        var builder = new StringBuilder();
        var value = new AllyariaStringValue("some-theme-value");
        var propertyName = "color";
        var varPrefix = "  My--Theme   Name  ";
        var expectedPrefixedProperty = "--my-theme-name-var-color";
        var expectedCss = $"{expectedPrefixedProperty}:{value.Value};";

        // Act
        builder.ToCss(value, propertyName, varPrefix);

        // Assert
        builder.ToString().Should().Be(expectedCss);
    }

    [Theory]
    [InlineData("----Fancy----", "--fancy-var")]
    [InlineData("- - Fancy - -", "--fancy-var")]
    [InlineData("A  B   C", "--a-b-c-var")]
    [InlineData("Title-Case Prefix", "--title-case-prefix-var")]
    public void ToCss_Should_BuildCssVariableName_With_LowercasedHyphenNormalizedPrefix(string varPrefix,
        string expectedPrefixPortion)
    {
        // Arrange
        var builder = new StringBuilder();
        var value = new AllyariaStringValue("ok");
        var propertyName = "border";
        var expectedPrefixedProperty = $"{expectedPrefixPortion}-{propertyName}";
        var expectedCss = $"{expectedPrefixedProperty}:{value.Value};";

        // Act
        builder.ToCss(value, propertyName, varPrefix);

        // Assert
        builder.ToString().Should().Be(expectedCss);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ToCss_Should_DoNothing_When_PropertyNameIsNullOrWhitespace(string? propertyName)
    {
        // Arrange
        var builder = new StringBuilder();
        var value = new AllyariaStringValue("exists");

        // Act
        builder.ToCss(value, propertyName!, "prefix");

        // Assert
        builder.ToString().Should().BeEmpty();
    }

    [Fact]
    public void ToCss_Should_ThrowNullReferenceException_When_BuilderIsNull_And_InputsValid()
    {
        // Arrange
        StringBuilder? builder = null;
        var value = new AllyariaStringValue("present");
        var propertyName = "color";
        var varPrefix = "the-prefix";

        // Act
        var act = () => builder!.ToCss(value, propertyName, varPrefix);

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ToCss_Should_UsePropertyNameDirectly_When_VarPrefixIsNullOrWhitespace(string? varPrefix)
    {
        // Arrange
        var builder = new StringBuilder();
        var value = new AllyariaStringValue("red");
        var propertyName = "background";
        var expectedCss = $"{propertyName}:{value.Value};";

        // Act
        builder.ToCss(value, propertyName, varPrefix!);

        // Assert
        builder.ToString().Should().Be(expectedCss);
    }
}
