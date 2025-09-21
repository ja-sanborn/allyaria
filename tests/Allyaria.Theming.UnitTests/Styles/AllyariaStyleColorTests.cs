using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaStyleColorTests
{
    [Fact]
    public void Ctor_CssColor_Should_WrapProvidedColor_When_ColorIsValid()
    {
        // Arrange
        AllyariaCssColor.TryParse("#123456", out var parsed)
            .Should()
            .BeTrue("expected test color to parse");

        var cssColor = parsed.Value;

        // Act
        var sut = new AllyariaStyleColor(parsed);

        // Assert
        sut.Style.Value.Should()
            .Be(cssColor);

        sut.Value.Should()
            .Be($"color:{cssColor};");
    }

    [Theory]
    [InlineData("this-is-not-a-color")]
    [InlineData("##badhex")]
    public void Ctor_String_Should_FallbackToTransparent_When_AllParsersFail(string input)
    {
        // Arrange
        var sut = new AllyariaStyleColor(input);

        // Act
        var style = sut.Style;
        var value = sut.Value;

        // Assert
        style.Value.Should()
            .Be(Colors.Transparent.Value);

        value.Should()
            .Be($"color:{Colors.Transparent.Value};");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_String_Should_FallbackToTransparent_When_NullOrWhitespace(string? input)
    {
        // Arrange
        var sut = new AllyariaStyleColor(input!);

        // Act
        var style = sut.Style;
        var value = sut.Value;

        // Assert
        style.Value.Should()
            .Be(Colors.Transparent.Value);

        value.Should()
            .Be($"color:{Colors.Transparent.Value};");
    }

    [Theory]
    [InlineData("inherit")]
    [InlineData("initial")]
    [InlineData("unset")]
    [InlineData("revert")]
    public void Ctor_String_Should_PreferCssGlobal_When_GlobalKeywordProvided(string input)
    {
        // Arrange
        var sut = new AllyariaStyleColor(input);

        // Act
        var value = sut.Value;

        // Assert
        value.Should()
            .Be($"color:{input};");
    }

    [Theory]
    [InlineData("#ff0000")]
    [InlineData("#00FF00")]
    [InlineData("rgb(255, 255, 255)")]
    [InlineData("hsl(0 0% 0%)")]
    [InlineData("white")]
    public void Ctor_String_Should_UseCssColor_When_ColorStringIsValid(string input)
    {
        // Arrange
        var sut = new AllyariaStyleColor(input);

        // Act
        var value = sut.Value;

        // Assert
        value.Should()
            .Be($"color:{sut.Style.Value};"); // declaration must reflect underlying style

        sut.Style.Value.Should()
            .NotBeNullOrWhiteSpace();

        // It should *not* fall back to transparent for valid color inputs.
        sut.Style.Value.Should()
            .NotBe(Colors.Transparent.Value);
    }

    [Theory]
    [InlineData("var(--brand-color)")]
    [InlineData("var(--token, #f00)")]
    public void Ctor_String_Should_UseCssFunction_When_FunctionProvided(string input)
    {
        // Arrange
        var sut = new AllyariaStyleColor(input);

        // Act
        var value = sut.Value;

        // Assert
        value.Should()
            .Be($"color:{input};");
    }

    [Fact]
    public void Declaration_Should_BeSafeToConcatenate_When_CombiningWithOtherDeclarations()
    {
        // Arrange
        var sut = new AllyariaStyleColor("#ff0000");
        var other = "background:#000;";

        // Act
        var combined = sut.Value + other;

        // Assert
        combined.Should()
            .Be($"color:{sut.Style.Value};background:#000;");
    }

    [Fact]
    public void DefaultCtor_Should_FallbackToTransparent_When_NoValueProvided()
    {
        // Arrange
        var sut = new AllyariaStyleColor();

        // Act
        var style = sut.Style;
        var value = sut.Value;

        // Assert
        style.Should()
            .NotBeNull();

        style.Value.Should()
            .Be(Colors.Transparent.Value);

        value.Should()
            .Be($"color:{Colors.Transparent.Value};");
    }

    [Fact]
    public void Implicit_FromCssColor_Should_CreateWrapper_When_ColorProvided()
    {
        // Arrange
        AllyariaCssColor.TryParse("rgb(1,2,3)", out var color)
            .Should()
            .BeTrue();

        var expected = color.Value;

        // Act
        AllyariaStyleColor sut = color;

        // Assert
        sut.Style.Value.Should()
            .Be(expected);

        sut.Value.Should()
            .Be($"color:{expected};");
    }

    [Fact]
    public void Implicit_FromString_Should_CreateInstance_When_StringIsValid()
    {
        // Arrange
        var input = "#abcdef";

        // Act
        AllyariaStyleColor sut = input;

        // Assert
        sut.Value.Should()
            .Be($"color:{sut.Style.Value};");

        sut.Style.Value.Should()
            .NotBe(Colors.Transparent.Value);
    }

    [Fact]
    public void Implicit_FromString_Should_FallbackToTransparent_When_StringIsInvalid()
    {
        // Arrange
        var input = "not-a-real-color";

        // Act
        AllyariaStyleColor sut = input;

        // Assert
        sut.Style.Value.Should()
            .Be(Colors.Transparent.Value);

        sut.Value.Should()
            .Be($"color:{Colors.Transparent.Value};");
    }

    [Fact]
    public void Implicit_ToCssColor_Should_ReturnUnderlyingColor_When_WrappingAColor()
    {
        // Arrange
        const string input = "#0a1b2c";

        AllyariaCssColor.TryParse(input, out var expectedColor)
            .Should()
            .BeTrue();

        var sut = new AllyariaStyleColor(input);

        // Act
        AllyariaCssColor actual = sut;

        // Assert
        actual.Value.Should()
            .Be(expectedColor.Value);
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnCssDeclaration_When_Converted()
    {
        // Arrange
        var sut = new AllyariaStyleColor("#fedcba");

        // Act
        string s = sut;

        // Assert
        s.Should()
            .Be($"color:{sut.Style.Value};");
    }

    [Fact]
    public void Style_Property_Should_NeverReturnNull_When_Accessed()
    {
        // Arrange
        var sut = new AllyariaStyleColor(); // default-constructed

        // Act
        var style = sut.Style;

        // Assert
        style.Should()
            .NotBeNull();

        style.Value.Should()
            .NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Value_Should_IncludeTrailingSemicolon_When_Formatted()
    {
        // Arrange
        var sut = new AllyariaStyleColor("inherit");

        // Act
        var value = sut.Value;

        // Assert
        value.Should()
            .EndWith(";");

        value.Should()
            .Be($"color:{sut.Style.Value};");
    }
}
