namespace Allyaria.Theming.UnitTests.Types;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeStringTests
{
    [Fact]
    public void Compare_Static_Should_ReturnZero_When_BothNull()
    {
        // Arrange
        ThemeString? left = null;
        ThemeString? right = null;

        // Act
        var result = ThemeBase.Compare(left, right);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void Compare_Static_Should_ThrowAryArgumentException_When_TypesDiffer_NumberVsString()
    {
        // Arrange
        var left = new ThemeNumber("42px");
        var right = new ThemeString("beta");

        // Act
        var act = () => ThemeBase.Compare(left, right);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("different types");
    }

    [Fact]
    public void Compare_Static_Should_ThrowAryArgumentException_When_TypesDiffer_StringVsNumber()
    {
        // Arrange
        var left = new ThemeString("alpha");
        var right = new ThemeNumber("1px");

        // Act
        var act = () => ThemeBase.Compare(left, right);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("different types");
    }

    [Fact]
    public void Compare_Static_Should_TreatNullAsLess_When_LeftNullRightNotNull()
    {
        // Arrange
        ThemeString? left = null;
        ThemeString right = new("X");

        // Act
        var result = ThemeBase.Compare(left, right);

        // Assert
        result.Should().BeLessThan(0);
    }

    [Fact]
    public void Compare_Static_Should_TreatNullAsLess_When_RightNullLeftNotNull()
    {
        // Arrange
        ThemeString left = new("X");
        ThemeString? right = null;

        // Act
        var result = ThemeBase.Compare(left, right);

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Compare_Static_Should_UseOrdinalComparison_When_SameType()
    {
        // Arrange
        var a = new ThemeString("a");
        var b = new ThemeString("b");

        // Act
        var less = ThemeBase.Compare(a, b);
        var greater = ThemeBase.Compare(b, a);
        var equal = ThemeBase.Compare(new ThemeString("aa"), new ThemeString("aa"));

        // Assert
        less.Should().BeLessThan(0);
        greater.Should().BeGreaterThan(0);
        equal.Should().Be(0);
    }

    [Fact]
    public void CompareTo_Should_ConsiderNonNullGreater_When_OtherIsNull()
    {
        // Arrange
        var sut = new ThemeString("x");

        // Act
        var result = sut.CompareTo(null);

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void CompareTo_Should_ReturnZero_When_SameReference()
    {
        // Arrange
        var sut = new ThemeString("same");

        // Act
        var result = sut.CompareTo(sut);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void CompareTo_Should_ThrowAryArgumentException_When_TypesDiffer_NumberVsString()
    {
        // Arrange
        var sut = new ThemeNumber("5px");
        var other = new ThemeString("test");

        // Act
        var act = () => sut.CompareTo(other);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("different types");
    }

    [Fact]
    public void CompareTo_Should_ThrowAryArgumentException_When_TypesDiffer_StringVsNumber()
    {
        // Arrange
        var sut = new ThemeString("test");
        var other = new ThemeNumber("100px");

        // Act
        var act = () => sut.CompareTo(other);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("different types");
    }

    [Fact]
    public void CompareTo_Should_UseOrdinalOrdering_When_SameTypeDifferentValues()
    {
        // Arrange
        var low = new ThemeString("alpha");
        var high = new ThemeString("beta");

        // Act
        var cmp1 = low.CompareTo(high);
        var cmp2 = high.CompareTo(low);

        // Assert
        cmp1.Should().BeLessThan(0);
        cmp2.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Ctor_Should_StoreTrimmedValue_When_InputHasWhitespace()
    {
        // Arrange
        var input = "  Primary  ";

        // Act
        var sut = new ThemeString(input);

        // Assert
        sut.Value.Should().Be("Primary");
        sut.ToString().Should().Be("Primary");
    }

    [Fact]
    public void Equals_Object_Should_ReturnFalse_When_DifferentType()
    {
        // Arrange
        object other = "alpha";
        var sut = new ThemeString("alpha");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_Object_Should_ReturnTrue_When_SameValueAndType()
    {
        // Arrange
        object other = new ThemeString("z");
        var sut = new ThemeString("z");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_When_TypesDiffer_NumberVsString()
    {
        // Arrange
        var sut = new ThemeNumber("10px");
        var other = new ThemeString("same");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_When_TypesDiffer_StringVsNumber()
    {
        // Arrange
        var sut = new ThemeString("same");
        var other = new ThemeNumber("1px");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_Typed_Should_Handle_Null_SameType_And_DifferentValues()
    {
        // Arrange
        ThemeString? nullOther = null;
        var a1 = new ThemeString("a");
        var a2 = new ThemeString("a");
        var b = new ThemeString("b");

        // Act & Assert
        a1.Equals(nullOther).Should().BeFalse();
        a1.Equals(a2).Should().BeTrue();
        a1.Equals(b).Should().BeFalse();
        a1.Equals(a1).Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_Should_BeOrdinalBased_OnValue()
    {
        // Arrange
        var a1 = new ThemeString("key");
        var a2 = new ThemeString("key");
        var b = new ThemeString("Key");

        // Act
        var h1 = a1.GetHashCode();
        var h2 = a2.GetHashCode();
        var hb = b.GetHashCode();

        // Assert
        h1.Should().Be(h2);
        h1.Should().NotBe(hb);
    }

    [Fact]
    public void Implicit_FromString_Should_CreateNormalizedInstance()
    {
        // Arrange
        var input = "  Shade  ";

        // Act
        ThemeString sut = input;

        // Assert
        sut.Value.Should().Be("Shade");
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnUnderlyingValue()
    {
        // Arrange
        var sut = new ThemeString("Alpha");

        // Act
        string value = sut;

        // Assert
        value.Should().Be("Alpha");
    }

    [Fact]
    public void OperatorEquality_Should_Handle_Nulls_And_Equality()
    {
        // Arrange
        ThemeString? n1 = null;
        ThemeString? n2 = null;
        var a1 = new ThemeString("a");
        var a2 = new ThemeString("a");
        var b = new ThemeString("b");

        // Act & Assert
        (n1 == n2).Should().BeTrue();
        (a1 == a2).Should().BeTrue();
        (a1 != b).Should().BeTrue();
        (a1 != a2).Should().BeFalse();
        (n1 == a1).Should().BeFalse();
        (a1 == n1).Should().BeFalse();
    }

    [Fact]
    public void Parse_Should_ReturnInstance_When_InputIsValid()
    {
        // Arrange
        var input = "  Accent ";

        // Act
        var sut = ThemeString.Parse(input);

        // Assert
        sut.Value.Should().Be("Accent");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_InputContainsControlCharacters()
    {
        // Arrange
        var input = "Hello\nWorld";

        // Act
        var act = () => ThemeString.Parse(input);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("control characters");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_InputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var act = () => ThemeString.Parse(input!);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Parse_Should_ThrowAryArgumentException_When_InputIsNullEmptyOrWhitespace(string invalid)
    {
        // Arrange + Act
        var act = () => ThemeString.Parse(invalid);

        // Assert
        act.Should().Throw<Exception>()
            .Which.GetType().Name.Should().Be("AryArgumentException");
    }

    [Fact]
    public void RelationalOperators_Should_UseOrdinalComparison_When_SameType()
    {
        // Arrange
        var low = new ThemeString("a");
        var high = new ThemeString("b");
        var eqLeft = new ThemeString("x");
        var eqRight = new ThemeString("x");

        // Act & Assert
        (high > low).Should().BeTrue();
        (low < high).Should().BeTrue();
        (eqLeft >= eqRight).Should().BeTrue();
        (eqLeft <= eqRight).Should().BeTrue();
        (high >= low).Should().BeTrue();
        (low <= high).Should().BeTrue();
    }

    [Fact]
    public void StaticEquals_Should_ReturnExpected_ForAllNullCombos()
    {
        // Arrange
        ThemeString? none = null;
        var a1 = new ThemeString("a");
        var a2 = new ThemeString("a");
        var b = new ThemeString("b");

        // Act & Assert
        ThemeBase.Equals(none, none).Should().BeTrue();
        ThemeBase.Equals(a1, none).Should().BeFalse();
        ThemeBase.Equals(none, a1).Should().BeFalse();
        ThemeBase.Equals(a1, a2).Should().BeTrue();
        ThemeBase.Equals(a1, b).Should().BeFalse();
    }

    [Fact]
    public void ToCss_Should_LowercaseAndTrimPropertyName_When_StandardProperty()
    {
        // Arrange
        var sut = new ThemeString("12px");

        // Act
        var css = sut.ToCss("  Font-Size  ");

        // Assert
        css.Should().Be("font-size:12px;");
    }

    [Fact]
    public void ToCss_Should_PreserveCustomPropertyCase_When_CssVariable()
    {
        // Arrange
        var sut = new ThemeString("#fff");

        // Act
        var css = sut.ToCss("   --Theme-Primary   ");

        // Assert
        css.Should().Be("--Theme-Primary:#fff;");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void ToCss_Should_ThrowAryArgumentException_When_PropertyNameIsNullOrWhiteSpace(string? invalid)
    {
        // Arrange
        var sut = new ThemeString("red");

        // Act
        var act = () => sut.ToCss(invalid!);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("propertyName");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\r\n")]
    [InlineData("\u0007")] // bell control char
    public void TryParse_Should_ReturnFalseAndNull_When_InputIsInvalid(string invalid)
    {
        // Arrange + Act
        var success = ThemeString.TryParse(invalid, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_InputIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var success = ThemeString.TryParse(invalid!, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_InputIsValid()
    {
        // Arrange
        var input = "  Ok  ";

        // Act
        var success = ThemeString.TryParse(input, out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().NotBeNull();
        result.Value.Should().Be("Ok");
    }
}
