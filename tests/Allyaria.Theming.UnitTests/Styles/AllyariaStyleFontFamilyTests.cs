using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaStyleFontFamilyTests
{
    [Fact]
    public void Ctor_FromCssFontFamily_Should_UseProvidedInstance()
    {
        // Arrange
        var provided = new AllyariaCssFontFamily("Inter");

        // Act
        var sut = new AllyariaStyleFontFamily(provided);

        // Assert
        sut.Style.Should()
            .BeOfType<AllyariaCssFontFamily>();

        // Ensure it's equivalent by value; if reference equality isn't guaranteed, compare by Value
        sut.Style.Value.Should()
            .Be("Inter");

        sut.Value.Should()
            .Be("font-family:Inter;");
    }

    [Theory]
    [InlineData("Inter", "Inter")]
    [InlineData("'Open Sans', Arial, sans-serif", "\"Open Sans\",Arial,sans-serif")]
    [InlineData("\"Times New Roman\", Times, serif", "\"Times New Roman\",Times,serif")]
    public void Ctor_String_Should_WrapCssFontFamily_When_ValueIsRawFamily(string family, string result)
    {
        // Arrange
        var sut = new AllyariaStyleFontFamily(family);

        // Act
        var style = sut.Style;

        // Assert
        style.Value.Should()
            .Be(result);

        sut.Value.Should()
            .Be($"font-family:{result};");
    }

    [Theory]
    [InlineData("var(--font-body)")]
    [InlineData("env(safe-area-inset-top)")]
    [InlineData("calc(1px + 1px)")] // even if odd for font-family, ensures function parsing path
    public void Ctor_String_Should_WrapCssFunction_When_ValueLooksLikeFunction(string func)
    {
        // Arrange
        var sut = new AllyariaStyleFontFamily(func);

        // Act
        var style = sut.Style;

        // Assert
        style.Should()
            .BeOfType<AllyariaCssFunction>();

        sut.Value.Should()
            .Be($"font-family:{func};");
    }

    [Theory]
    [InlineData("inherit")]
    [InlineData("initial")]
    [InlineData("unset")]
    [InlineData("revert")]
    public void Ctor_String_Should_WrapCssGlobal_When_ValueIsGlobalKeyword(string keyword)
    {
        // Arrange
        var sut = new AllyariaStyleFontFamily(keyword);

        // Act
        var style = sut.Style;

        // Assert
        style.Should()
            .BeOfType<AllyariaCssGlobal>();

        sut.Value.Should()
            .Be($"font-family:{keyword};");
    }

    [Theory]
    [InlineData("Inter", "Inter")]
    [InlineData("'Open Sans', Arial, sans-serif", "\"Open Sans\",Arial,sans-serif")]
    public void Implicit_From_AllyariaCssFontFamily_Should_SetStyle(string family, string result)
    {
        // Arrange
        var cssFamily = new AllyariaCssFontFamily(family);

        // Act
        AllyariaStyleFontFamily sut = cssFamily;

        // Assert
        sut.Style.Should()
            .BeOfType<AllyariaCssFontFamily>();

        sut.Style.Value.Should()
            .Be(result);

        sut.Value.Should()
            .Be($"font-family:{result};");
    }

    [Theory]
    [InlineData("Inter", "Inter")]
    [InlineData("'Open Sans', Arial, sans-serif", "\"Open Sans\",Arial,sans-serif")]
    public void Implicit_To_AllyariaCssFontFamily_Should_ReturnUnderlying_When_StyleIsFontFamily(string family,
        string result)
    {
        // Arrange
        AllyariaStyleFontFamily sut = family;

        // Act
        AllyariaCssFontFamily actual = sut;

        // Assert
        actual.Value.Should()
            .Be(result);
    }

    [Fact]
    public void Implicit_To_AllyariaCssFontFamily_Should_ThrowInvalidCast_When_StyleIsFunction()
    {
        // Arrange
        AllyariaStyleFontFamily sut = "var(--font-body)";

        // Act
        var act = () =>
        {
            AllyariaCssFontFamily _ = sut;
        };

        // Assert
        act.Should()
            .Throw<InvalidCastException>();
    }

    [Fact]
    public void Implicit_To_AllyariaCssFontFamily_Should_ThrowInvalidCast_When_StyleIsGlobal()
    {
        // Arrange
        AllyariaStyleFontFamily sut = "inherit";

        // Act
        var act = () =>
        {
            AllyariaCssFontFamily _ = sut;
        };

        // Assert
        act.Should()
            .Throw<InvalidCastException>();
    }

    [Theory]
    [InlineData("Inter")]
    [InlineData("'Open Sans', Arial, sans-serif")]
    [InlineData("inherit")]
    [InlineData("var(--font-body)")]
    public void Implicit_To_String_Should_ReturnCssDeclaration(string input)
    {
        // Arrange
        AllyariaStyleFontFamily sut = input;

        // Act
        string css = sut;

        // Assert
        css.Should()
            .Be($"font-family:{sut.Style.Value};");

        css.Should()
            .Be(sut.Value);
    }

    [Fact]
    public void Style_Should_ReturnEmptyCssFontFamily_When_DefaultConstructed()
    {
        // Arrange
        var sut = default(AllyariaStyleFontFamily);

        // Act
        var style = sut.Style;
        var asFontFamily = (AllyariaCssFontFamily)style;

        // Assert
        style.Should()
            .BeOfType<AllyariaCssFontFamily>();

        asFontFamily.Value.Should()
            .Be(string.Empty);

        sut.Value.Should()
            .Be("font-family:;");
    }
}
