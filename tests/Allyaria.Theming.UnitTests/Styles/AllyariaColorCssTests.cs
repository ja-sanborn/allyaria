using Allyaria.Theming.Contracts;
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Styles;

public sealed class AllyariaColorCssTests
{
    [Fact]
    public void Ctor_CssProperty_Should_ThrowArgumentException_When_CreateFailsForValue()
    {
        // Arrange
        var css = "color: totally-invalid-value;";

        // Act
        var act = () => new AllyariaColorCss(css);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("color: inherit;", typeof(AllyariaGlobalValue))]
    [InlineData("color: var(--primary);", typeof(AllyariaFunctionValue))]
    public void Ctor_CssProperty_Should_UseCreate_ToConstructCorrectValueType(string css, Type expectedType)
    {
        // Arrange & Act
        var sut = new AllyariaColorCss(css);

        // Assert
        sut.CssValue.Should()
            .NotBeNull();

        sut.CssValue!.GetType()
            .Should()
            .Be(expectedType);
    }

    [Theory]
    [InlineData("color: green;", "#008000FF")]
    [InlineData("  color  :  green  ;  ", "#008000FF")]
    [InlineData("COLOR: green", "#008000FF")]
    [InlineData("color: #fff;", "#FFFFFFFF")]
    [InlineData("color: rgb(255, 0, 0);", "#FF0000FF")]
    public void Ctor_CssPropertyString_Should_ParseAndNormalize_When_InputValid(string css, string expectedValueText)
    {
        // Arrange & Act
        var sut = new AllyariaColorCss(css);

        // Assert
        sut.CssName.Should()
            .Be("color");

        sut.CssValue.Should()
            .NotBeNull();

        sut.CssProperty.Should()
            .Be($"color:{expectedValueText};");

        sut.ToString()
            .Should()
            .Be(sut.CssProperty);
    }

    [Theory]
    [InlineData("background: rebeccapurple;")] // wrong property name for this concrete type
    [InlineData("color rebeccapurple")] // missing colon
    [InlineData("color: ;")] // missing value
    [InlineData("color: not-a-color;")] // invalid color
    public void Ctor_CssPropertyString_Should_ThrowArgumentException_When_InputInvalid(string css)
    {
        // Arrange
        var act = () => new AllyariaColorCss(css);

        // Assert
        act.Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void Ctor_NameAndColorValue_Should_SetProperties_And_CanonicalizeName()
    {
        // Arrange
        var colorValue = new AllyariaColorValue("#ff00ffff"); // magenta with full alpha

        // Act
        var sut = new AllyariaColorCss("CoLoR", colorValue);

        // Assert
        sut.CssName.Should()
            .Be("color");

        sut.CssValue.Should()
            .BeSameAs(colorValue);

        sut.CssProperty.Should()
            .Be($"color:{colorValue.Value};");
    }

    [Fact]
    public void Ctor_NameAndFunctionValue_Should_SupportFunctions_Eg_Var()
    {
        // Arrange
        var func = new AllyariaFunctionValue("var(--brand-color)");

        // Act
        var sut = new AllyariaColorCss("color", func);

        // Assert
        sut.CssProperty.Should()
            .Be("color:var(--brand-color);");

        sut.CssValue!.ToString()
            .Should()
            .Be("var(--brand-color)");
    }

    [Fact]
    public void Ctor_NameAndGlobalValue_Should_SupportGlobalKeywords()
    {
        // Arrange
        var global = new AllyariaGlobalValue("INHERIT");

        // Act
        var sut = new AllyariaColorCss("color", global);

        // Assert
        sut.CssProperty.Should()
            .Be("color:inherit;");

        sut.CssValue!.ToString()
            .Should()
            .Be("inherit");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("background_color")] // invalid characters
    public void Ctor_NameAndValue_Should_ThrowArgumentException_When_NameInvalid(string? badName)
    {
        // Arrange
        var value = new AllyariaColorValue("#112233");

        // Act
        var act = () => new AllyariaColorCss(badName!, value);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("name");
    }

    [Fact]
    public void Ctor_NameAndValue_Should_ThrowArgumentNullException_When_ValueNull()
    {
        // Arrange
        ValueBase? value = null;

        // Act
        var act = () => new AllyariaColorCss("color", value!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("value");
    }

    [Fact]
    public void Equality_Should_BeFalse_For_DifferentValues_And_HandleNulls()
    {
        // Arrange
        var a = new AllyariaColorCss("color: green;");
        var b = new AllyariaColorCss("color: #fff;");
        AllyariaColorCss? n = null;

        // Act & Assert
        (a == b).Should()
            .BeFalse();

        (a != b).Should()
            .BeTrue();

        a.Equals(b)
            .Should()
            .BeFalse();

        CssBase.Equals(a, null)
            .Should()
            .BeFalse();

        CssBase.Equals(null, b)
            .Should()
            .BeFalse();

        CssBase.Equals(n, n)
            .Should()
            .BeTrue(); // both null

        a.Equals((object?)null)
            .Should()
            .BeFalse();
    }

    [Fact]
    public void Equality_Should_BeTrue_For_SameCssProperty()
    {
        // Arrange
        var a = new AllyariaColorCss("color: #FF0000;");
        var b = new AllyariaColorCss("color: #FF0000FF;");

        // Act & Assert
        (a == b).Should()
            .BeTrue();

        (a != b).Should()
            .BeFalse();

        a.Equals(b)
            .Should()
            .BeTrue();

        CssBase.Equals(a, b)
            .Should()
            .BeTrue();

        a.GetHashCode()
            .Should()
            .Be(b.GetHashCode());
    }

    [Theory]
    [InlineData("color: white", "color:#FFFFFFFF;")]
    [InlineData("color: #000", "color:#000000FF;")]
    [InlineData("color: rgb(255, 0, 0)", "color:#FF0000FF;")]
    public void Implicit_FromString_Should_CreateInstance_WithExpectedCss(string raw, string expectedCss)
    {
        // Arrange
        AllyariaColorCss sut = raw;

        // Act & Assert
        sut.CssProperty.Should()
            .Be(expectedCss);
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnCssDeclaration()
    {
        // Arrange
        AllyariaColorCss sut = "color: #abcdef";

        // Act
        string css = sut;

        // Assert
        css.Should()
            .Be("color:#ABCDEFFF;");
    }

    [Fact]
    public void Implicit_ToString_Should_ThrowArgumentNullException_When_InstanceNull()
    {
        // Arrange
        AllyariaColorCss instance = null!;

        // Act
        var act = () =>
        {
            string _ = instance;
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("color: var(--brand);", "var(--brand)")]
    [InlineData("color: VAR(--Accent-Color)", "var(--Accent-Color)")]
    public void Parse_Should_ReturnInstance_WithFunctionValue_When_CssPropertyHasFunction(string css, string expected)
    {
        // Arrange & Act
        var sut = AllyariaColorCss.Parse(css);

        // Assert
        sut.CssName.Should()
            .Be("color");

        sut.CssValue.Should()
            .BeOfType<AllyariaFunctionValue>();

        sut.CssValue!.ToString()
            .Should()
            .Be(expected);

        sut.CssProperty.Should()
            .Be($"color:{expected};");
    }

    [Theory]
    [InlineData("color: inherit;", "inherit")]
    [InlineData("color: REVERT-LAYER", "revert-layer")]
    public void Parse_Should_ReturnInstance_WithGlobalValue_When_CssPropertyHasGlobalKeyword(string css,
        string expected)
    {
        // Arrange & Act
        var sut = AllyariaColorCss.Parse(css);

        // Assert
        sut.CssName.Should()
            .Be("color");

        sut.CssValue.Should()
            .BeOfType<AllyariaGlobalValue>();

        sut.CssValue!.ToString()
            .Should()
            .Be(expected);

        sut.CssProperty.Should()
            .Be($"color:{expected};");
    }

    [Theory]
    [InlineData("background12: inherit;")] // wrong property name
    [InlineData("color: not-a-color;")] // invalid color/function/global
    public void TryParse_Should_ReturnFalse_When_InvalidDeclaration(string css)
    {
        // Arrange & Act
        var ok = AllyariaColorCss.TryParse(css, out var result);

        // Assert
        ok.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsNullOrWhitespace()
    {
        // Arrange & Act
        var okNull = AllyariaColorCss.TryParse(null!, out var r1);
        var okEmpty = AllyariaColorCss.TryParse(string.Empty, out var r2);
        var okWs = AllyariaColorCss.TryParse("   ", out var r3);

        // Assert
        okNull.Should()
            .BeFalse();

        okEmpty.Should()
            .BeFalse();

        okWs.Should()
            .BeFalse();

        r1.Should()
            .BeNull();

        r2.Should()
            .BeNull();

        r3.Should()
            .BeNull();
    }

    [Theory]
    [InlineData("color: inherit;", "color:inherit;")]
    [InlineData("color: var(--c);", "color:var(--c);")]
    public void TryParse_Should_ReturnTrue_WithExpectedCss_For_GlobalAndFunctionValues(string css, string expected)
    {
        // Arrange & Act
        var ok = AllyariaColorCss.TryParse(css, out var result);

        // Assert
        ok.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        result!.CssProperty.Should()
            .Be(expected);
    }
}
