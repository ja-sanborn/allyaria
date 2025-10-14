using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.UnitTests.Values;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryStringValueTests
{
    [Fact]
    public void Compare_Static_Should_ReturnZero_When_BothNull()
    {
        // Arrange
        AryStringValue? left = null;
        AryStringValue? right = null;

        // Act
        var result = ValueBase.Compare(left, right);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void Compare_Static_Should_ThrowAryArgumentException_When_TypesDiffer_NumberVsString()
    {
        // Arrange
        var left = new AryNumberValue("42px");
        var right = new AryStringValue("beta");

        // Act
        var act = () => ValueBase.Compare(left, right);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("different types");
    }

    [Fact]
    public void Compare_Static_Should_ThrowAryArgumentException_When_TypesDiffer_StringVsNumber()
    {
        // Arrange
        var left = new AryStringValue("alpha");
        var right = new AryNumberValue("1px");

        // Act
        var act = () => ValueBase.Compare(left, right);

        // Assert
        var ex = act.Should().Throw<Exception>().Which;
        ex.GetType().Name.Should().Be("AryArgumentException");
        ex.Message.Should().Contain("different types");
    }

    [Fact]
    public void Compare_Static_Should_TreatNullAsLess_When_LeftNullRightNotNull()
    {
        // Arrange
        AryStringValue? left = null;
        AryStringValue right = new("X");

        // Act
        var result = ValueBase.Compare(left, right);

        // Assert
        result.Should().BeLessThan(0);
    }

    [Fact]
    public void Compare_Static_Should_TreatNullAsLess_When_RightNullLeftNotNull()
    {
        // Arrange
        AryStringValue left = new("X");
        AryStringValue? right = null;

        // Act
        var result = ValueBase.Compare(left, right);

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Compare_Static_Should_UseOrdinalComparison_When_SameType()
    {
        // Arrange
        var a = new AryStringValue("a");
        var b = new AryStringValue("b");

        // Act
        var less = ValueBase.Compare(a, b);
        var greater = ValueBase.Compare(b, a);
        var equal = ValueBase.Compare(new AryStringValue("aa"), new AryStringValue("aa"));

        // Assert
        less.Should().BeLessThan(0);
        greater.Should().BeGreaterThan(0);
        equal.Should().Be(0);
    }

    [Fact]
    public void CompareTo_Should_ConsiderNonNullGreater_When_OtherIsNull()
    {
        // Arrange
        var sut = new AryStringValue("x");

        // Act
        var result = sut.CompareTo(null);

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void CompareTo_Should_ReturnZero_When_SameReference()
    {
        // Arrange
        var sut = new AryStringValue("same");

        // Act
        var result = sut.CompareTo(sut);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void CompareTo_Should_ThrowAryArgumentException_When_TypesDiffer_NumberVsString()
    {
        // Arrange
        var sut = new AryNumberValue("5px");
        var other = new AryStringValue("test");

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
        var sut = new AryStringValue("test");
        var other = new AryNumberValue("100px");

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
        var low = new AryStringValue("alpha");
        var high = new AryStringValue("beta");

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
        var sut = new AryStringValue(input);

        // Assert
        sut.Value.Should().Be("Primary");
        sut.ToString().Should().Be("Primary");
    }

    [Fact]
    public void Equals_Object_Should_ReturnFalse_When_DifferentType()
    {
        // Arrange
        object other = "alpha";
        var sut = new AryStringValue("alpha");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_Object_Should_ReturnTrue_When_SameValueAndType()
    {
        // Arrange
        object other = new AryStringValue("z");
        var sut = new AryStringValue("z");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_When_TypesDiffer_NumberVsString()
    {
        // Arrange
        var sut = new AryNumberValue("10px");
        var other = new AryStringValue("same");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_When_TypesDiffer_StringVsNumber()
    {
        // Arrange
        var sut = new AryStringValue("same");
        var other = new AryNumberValue("1px");

        // Act
        var result = sut.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_Typed_Should_Handle_Null_SameType_And_DifferentValues()
    {
        // Arrange
        AryStringValue? nullOther = null;
        var a1 = new AryStringValue("a");
        var a2 = new AryStringValue("a");
        var b = new AryStringValue("b");

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
        var a1 = new AryStringValue("key");
        var a2 = new AryStringValue("key");
        var b = new AryStringValue("Key");

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
        AryStringValue sut = input;

        // Assert
        sut.Value.Should().Be("Shade");
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnUnderlyingValue()
    {
        // Arrange
        var sut = new AryStringValue("Alpha");

        // Act
        string value = sut;

        // Assert
        value.Should().Be("Alpha");
    }

    [Fact]
    public void OperatorEquality_Should_Handle_Nulls_And_Equality()
    {
        // Arrange
        AryStringValue? n1 = null;
        AryStringValue? n2 = null;
        var a1 = new AryStringValue("a");
        var a2 = new AryStringValue("a");
        var b = new AryStringValue("b");

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
        var sut = AryStringValue.Parse(input);

        // Assert
        sut.Value.Should().Be("Accent");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_InputContainsControlCharacters()
    {
        // Arrange
        var input = "Hello\nWorld";

        // Act
        var act = () => AryStringValue.Parse(input);

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
        var act = () => AryStringValue.Parse(input!);

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
        var act = () => AryStringValue.Parse(invalid);

        // Assert
        act.Should().Throw<Exception>()
            .Which.GetType().Name.Should().Be("AryArgumentException");
    }

    [Fact]
    public void RelationalOperators_Should_UseOrdinalComparison_When_SameType()
    {
        // Arrange
        var low = new AryStringValue("a");
        var high = new AryStringValue("b");
        var eqLeft = new AryStringValue("x");
        var eqRight = new AryStringValue("x");

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
        AryStringValue? none = null;
        var a1 = new AryStringValue("a");
        var a2 = new AryStringValue("a");
        var b = new AryStringValue("b");

        // Act & Assert
        ValueBase.Equals(none, none).Should().BeTrue();
        ValueBase.Equals(a1, none).Should().BeFalse();
        ValueBase.Equals(none, a1).Should().BeFalse();
        ValueBase.Equals(a1, a2).Should().BeTrue();
        ValueBase.Equals(a1, b).Should().BeFalse();
    }

    [Fact]
    public void ToCss_Should_LowercaseAndTrimPropertyName_When_StandardProperty()
    {
        // Arrange
        var sut = new AryStringValue("12px");

        // Act
        var css = sut.ToCss("  Font-Size  ");

        // Assert
        css.Should().Be("font-size:12px;");
    }

    [Fact]
    public void ToCss_Should_PreserveCustomPropertyCase_When_CssVariable()
    {
        // Arrange
        var sut = new AryStringValue("#fff");

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
        var sut = new AryStringValue("red");

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
        var success = AryStringValue.TryParse(invalid, out var result);

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
        var success = AryStringValue.TryParse(invalid!, out var result);

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
        var success = AryStringValue.TryParse(input, out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().NotBeNull();
        result.Value.Should().Be("Ok");
    }
}
