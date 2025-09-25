using Allyaria.Theming.Contracts;
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaFontFamilyCssTests
{
    [Fact]
    public void Ctor_WithFullDeclaration_Should_SetNameAndValue_When_FontFamilyValue()
    {
        // Arrange
        var declaration = "font-family: \"Open Sans\", system-ui, sans-serif";

        // Act
        var sut = new AllyariaFontFamilyCss(declaration);

        // Assert
        sut.CssName.Should()
            .Be("font-family");

        sut.CssValue.Should()
            .BeAssignableTo<AllyariaFontFamilyValue>();

        sut.CssProperty.Should()
            .Contain("font-family")
            .And.Contain("Open Sans");
    }

    [Fact]
    public void Ctor_WithFullDeclaration_Should_SetNameAndValue_When_FunctionValue()
    {
        // Arrange
        var declaration = "font-family: var(--app-font)";

        // Act
        var sut = new AllyariaFontFamilyCss(declaration);

        // Assert
        sut.CssName.Should()
            .Be("font-family");

        sut.CssValue.Should()
            .BeAssignableTo<AllyariaFunctionValue>();

        sut.CssProperty.Should()
            .Contain("font-family")
            .And.Contain("var(--app-font)");
    }

    [Fact]
    public void Ctor_WithFullDeclaration_Should_SetNameAndValue_When_GlobalValue()
    {
        // Arrange
        var declaration = "font-family: inherit";

        // Act
        var sut = new AllyariaFontFamilyCss(declaration);

        // Assert
        sut.CssName.Should()
            .Be("font-family");

        sut.CssValue.Should()
            .BeAssignableTo<AllyariaGlobalValue>();

        sut.CssProperty.Should()
            .Contain("font-family")
            .And.Contain("inherit");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("inherit")] // missing property name/colon should fail parse-at-constructor level
    [InlineData("font1family: serif")] // wrong property name format (no hyphen) should fail TryParseCssProperty
    public void Ctor_WithFullDeclaration_Should_ThrowArgumentException_When_DeclarationInvalid(string? declaration)
    {
        // Arrange
        // Act
        var act = () => new AllyariaFontFamilyCss(declaration!);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Unable to parse CSS property*")
            .And.ParamName.Should()
            .Be("cssProperty");
    }

    [Fact]
    public void Ctor_WithNameAndPreparsedValue_Should_Initialize_When_InputsValid()
    {
        // Arrange
        var name = "FoNt-FaMiLy"; // casing should be canonicalized by base
        var value = Substitute.For<ValueBase>();

        // Act
        var sut = new AllyariaFontFamilyCss(name, value);

        // Assert
        sut.CssName.Should()
            .Be("font-family");

        sut.CssValue.Should()
            .BeSameAs(value);

        sut.CssProperty.Should()
            .Contain("font-family");
    }

    [Fact]
    public void Implicit_FromString_Should_CreateInstance_FromFullDeclaration()
    {
        // Arrange
        AllyariaFontFamilyCss sut = "font-family: system-ui, sans-serif";

        // Act
        var property = sut.CssProperty;

        // Assert
        property.Should()
            .Contain("font-family")
            .And.Contain("system-ui");
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnCssProperty()
    {
        // Arrange
        var sut = new AllyariaFontFamilyCss("font-family: \"Inter\", sans-serif");

        // Act
        string property = sut;

        // Assert
        property.Should()
            .Contain("font-family")
            .And.Contain("Inter");
    }

    [Fact]
    public void Implicit_ToString_Should_ThrowArgumentNullException_When_InstanceIsNull()
    {
        // Arrange
        AllyariaFontFamilyCss? sut = null;

        // Act
        var act = () =>
        {
            var _ = (string)sut!;
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .And.ParamName.Should()
            .Be("value");
    }

    [Fact]
    public void Parse_Should_CreateInstance_When_FullDeclarationProvided()
    {
        // Arrange
        var declaration = "font-family: serif";

        // Act
        var sut = AllyariaFontFamilyCss.Parse(declaration);

        // Assert
        sut.CssName.Should()
            .Be("font-family");

        sut.CssProperty.Should()
            .Contain("serif");
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("inherit", false)] // raw value (no property name) fails by current implementation
    [InlineData("font-family: monospace", true)]
    public void TryParse_Should_ReturnExpectedBool_And_SetResultWhenSuccessful(string? input, bool expected)
    {
        // Arrange
        // Act
        var success = AllyariaFontFamilyCss.TryParse(input!, out var result);

        // Assert
        success.Should()
            .Be(expected);

        if (expected)
        {
            result.Should()
                .NotBeNull();

            result.CssName.Should()
                .Be("font-family");

            result.CssProperty.Should()
                .Contain("monospace");
        }
        else
        {
            result.Should()
                .BeNull();
        }
    }
}
