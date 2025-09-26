using Allyaria.Theming.Types;

namespace Allyaria.Theming.UnitTests.Types;

public sealed class CssPropertyTests
{
    [Fact]
    public void DefaultCtor_Should_CreateEmptyInstance_When_Invoked()
    {
        // Arrange & Act
        var sut = new CssProperty();

        // Assert
        sut.Name.Should()
            .Be(string.Empty);

        sut.Value.Should()
            .Be(string.Empty);

        sut.IsValid()
            .Should()
            .BeFalse();

        sut.ToCss()
            .Should()
            .Be(string.Empty);

        sut.Should()
            .Be(CssProperty.Empty);
    }

    [Fact]
    public void EqualsNullableOverload_Should_BeTrue_When_DiffersOnlyByValueCasing()
    {
        // Arrange
        var a = new CssProperty("color", "#fff");
        var b = new CssProperty("color", "#FFF");

        // Act
        var equal = a.Equals((CssProperty?)b);

        // Assert
        equal.Should()
            .BeTrue(); // Nullable overload compares ToCss() case-insensitively
    }

    [Fact]
    public void EqualsObject_Should_BeFalse_When_OnlyValueCasingDiffers()
    {
        // Arrange
        var sut = new CssProperty("color", "#fff");
        object other = new CssProperty("color", "#FFF");

        // Act
        var equal = sut.Equals(other);

        // Assert
        equal.Should()
            .BeFalse(); // object.Equals uses ordinal comparison of Value
    }

    [Fact]
    public void EqualsObject_Should_BeTrue_When_NameAndValueMatchExactly()
    {
        // Arrange
        var sut = new CssProperty("color", "#fff");
        object other = new CssProperty("color", "#fff");

        // Act
        var equal = sut.Equals(other);

        // Assert
        equal.Should()
            .BeTrue();

        sut.GetHashCode()
            .Should()
            .Be(new CssProperty("color", "#fff").GetHashCode());
    }

    [Fact]
    public void ImplicitFromString_Should_ParseDeclaration_When_AssignedFromString()
    {
        // Arrange
        CssProperty sut = "border-color: #123456;";

        // Act & Assert
        sut.Name.Should()
            .Be("border-color");

        sut.Value.Should()
            .Be("#123456");

        sut.ToCss()
            .Should()
            .Be("border-color:#123456;");
    }

    [Fact]
    public void ImplicitToString_Should_ReturnValue_When_AssignedToString()
    {
        // Arrange
        var sut = new CssProperty("font-weight", "bold");

        // Act
        string value = sut;

        // Assert
        value.Should()
            .Be("bold");
    }

    [Fact]
    public void IsValid_Should_BeFalse_When_EmptyInstance()
    {
        // Arrange
        var sut = CssProperty.Empty;

        // Act
        var valid = sut.IsValid();

        // Assert
        valid.Should()
            .BeFalse();
    }

    [Fact]
    public void NameValueCtor_Should_NormalizeNameAndTrimValue_When_InputsHaveWhitespaceAndCasing()
    {
        // Arrange
        var name = "  Background-Color  ";
        var value = "  #FFF  ";

        // Act
        var sut = new CssProperty(name, value);

        // Assert
        sut.Name.Should()
            .Be("background-color");

        sut.Value.Should()
            .Be("#FFF");

        sut.IsValid()
            .Should()
            .BeTrue();

        sut.ToCss()
            .Should()
            .Be("background-color:#FFF;");
    }

    [Theory]
    [InlineData(null, "red")]
    [InlineData("", "red")]
    [InlineData("   ", "red")]
    [InlineData("color", null)]
    [InlineData("color", "")]
    [InlineData("color", "   ")]
    [InlineData("background--color", "red")] // invalid name shape
    [InlineData("color!", "red")] // invalid character
    public void NameValueCtor_Should_ReportInvalid_When_NameOrValueInvalid(string? name, string? value)
    {
        // Arrange & Act
        var sut = new CssProperty(name ?? string.Empty, value ?? string.Empty);

        // Assert
        sut.IsValid()
            .Should()
            .BeFalse();

        sut.ToCss()
            .Should()
            .Be(string.Empty);
    }

    [Fact]
    public void OperatorEquality_Should_BeTrue_When_DiffersOnlyByValueCasing()
    {
        // Arrange
        CssProperty? a = new("color", "#fff");
        CssProperty? b = new("color", "#FFF");

        // Act
        var equal = a == b;

        // Assert
        equal.Should()
            .BeTrue(); // operator== uses the nullable Equals overload behavior
    }

    [Fact]
    public void OperatorEquality_Should_ReturnFalse_When_BothNull()
    {
        // Arrange
        CssProperty? a = null;
        CssProperty? b = null;

        // Act
        var equal = a == b;

        // Assert
        equal.Should()
            .BeFalse(); // matches static Equals(left,right) behavior
    }

    [Fact]
    public void OperatorInequality_Should_BeTrue_When_NameOrValueDiffer()
    {
        // Arrange
        CssProperty? a = new("color", "#fff");
        CssProperty? b = new("background-color", "#fff");
        CssProperty? c = new("color", "#000");

        // Act & Assert
        (a != b).Should()
            .BeTrue();

        (a != c).Should()
            .BeTrue();
    }

    [Fact]
    public void Parse_Should_ReturnParsedInstance_When_ValidDeclaration()
    {
        // Arrange
        var declaration = "margin-top: 1rem;";

        // Act
        var sut = CssProperty.Parse(declaration);

        // Assert
        sut.Name.Should()
            .Be("margin-top");

        sut.Value.Should()
            .Be("1rem");

        sut.IsValid()
            .Should()
            .BeTrue();

        sut.ToCss()
            .Should()
            .Be("margin-top:1rem;");
    }

    [Fact]
    public void Parse_Should_ThrowArgumentException_When_DeclarationInvalid()
    {
        // Arrange
        var declaration = "margin-top 1rem"; // no colon

        // Act
        var act = () => CssProperty.Parse(declaration);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("cssProperty");
    }

    [Fact]
    public void StaticEquals_Should_ReturnFalse_When_BothNull()
    {
        // Arrange
        CssProperty? a = null;
        CssProperty? b = null;

        // Act
        var equal = CssProperty.Equals(a, b);

        // Assert
        equal.Should()
            .BeFalse(); // documented current behavior of implementation
    }

    [Fact]
    public void StringCtor_Should_ParseAndNormalize_When_ValidDeclarationWithSpacesAndSemicolon()
    {
        // Arrange
        var declaration = "  Color :   #fff ;  ";

        // Act
        var sut = new CssProperty(declaration);

        // Assert
        sut.Name.Should()
            .Be("color");

        sut.Value.Should()
            .Be("#fff");

        sut.IsValid()
            .Should()
            .BeTrue();

        sut.ToCss()
            .Should()
            .Be("color:#fff;");
    }

    [Fact]
    public void StringCtor_Should_PreserveValueColons_When_ValueContainsAdditionalColons()
    {
        // Arrange
        var declaration = "background-image:url(data:image/png;base64,abc==);";

        // Act
        var sut = new CssProperty(declaration);

        // Assert
        sut.Name.Should()
            .Be("background-image");

        sut.Value.Should()
            .Be("url(data:image/png;base64,abc==)");

        sut.ToCss()
            .Should()
            .Be("background-image:url(data:image/png;base64,abc==);");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("color")] // missing colon
    [InlineData("color:")] // empty value
    [InlineData(":red")] // empty name
    [InlineData("color ; red")] // malformed
    [InlineData("background--color: red")] // invalid name
    public void StringCtor_Should_ThrowArgumentException_When_DeclarationInvalid(string? declaration)
    {
        // Arrange
        var act = () => _ = new CssProperty(declaration!);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("cssProperty")
            .WithMessage("*Unable to parse CSS property.*");
    }

    [Fact]
    public void ToCss_Should_ReturnEmpty_When_Invalid()
    {
        // Arrange
        var sut = new CssProperty("color", ""); // invalid (empty value)

        // Act
        var css = sut.ToCss();

        // Assert
        css.Should()
            .Be(string.Empty);
    }

    [Fact]
    public void ToString_Should_ReturnRawValue_When_Called()
    {
        // Arrange
        var sut = new CssProperty("color", "  #abc  ");

        // Act
        var str = sut.ToString();

        // Assert
        str.Should()
            .Be("#abc");
    }

    [Theory]
    [InlineData("Color: RED;", true, "color", "RED")]
    [InlineData("content: \":\";", true, "content", "\":\"")]
    [InlineData("color:red;;", true, "color", "red;")] // parser strips only one trailing ';'
    [InlineData("  padding-left :  0  ", true, "padding-left", "0")]
    [InlineData("1color: red", false, "", "")]
    [InlineData("color", false, "", "")]
    [InlineData("color:", false, "", "")]
    public void TryParse_Should_ReturnExpectedResultAndOutput_When_GivenVariousInputs(string input,
        bool expectedSuccess,
        string expectedName,
        string expectedValue)
    {
        // Arrange & Act
        var success = CssProperty.TryParse(input, out var result);

        // Assert
        success.Should()
            .Be(expectedSuccess);

        if (expectedSuccess)
        {
            result.Should()
                .NotBeNull();

            result.Value.Name.Should()
                .Be(expectedName);

            result.Value.Value.Should()
                .Be(expectedValue);

            result.Value.IsValid()
                .Should()
                .BeTrue();
        }
        else
        {
            result.Should()
                .BeNull();
        }
    }
}
