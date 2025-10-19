namespace Allyaria.Theming.UnitTests.Types;

// ReSharper disable RedundantArgumentDefaultValue
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeColorTests
{
    [Fact]
    public void Ctor_FromComponents_Should_CreateHexColor_When_ValidInputs()
    {
        // Arrange
        const byte r = 0x12;
        const byte g = 0x34;
        const byte b = 0x56;
        const double a = 1.0;

        // Act
        var sut = new ThemeColor(r, g, b, a);

        // Assert
        sut.Value.Should().Be("#123456FF");
        ((HexColor)sut).R.Value.Should().Be(r);
        ((HexColor)sut).G.Value.Should().Be(g);
        ((HexColor)sut).B.Value.Should().Be(b);
        ((HexColor)sut).A.Value.Should().Be(255);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    public void Ctor_FromComponents_Should_Throw_When_AlphaOutOfRangeOrNonFinite(double alpha)
    {
        // Arrange
        var act = () => new ThemeColor(10, 20, 30, alpha);

        // Act & Assert
        act.Should().Throw<Exception>(); // HexByte.FromNormalized guards the range/finite values
    }

    [Fact]
    public void Ctor_FromHexColor_Should_WrapProvidedColor()
    {
        // Arrange
        var color = new HexColor("#11223344");

        // Act
        var sut = new ThemeColor(color);

        // Assert
        sut.Value.Should().Be("#11223344");
        ((HexColor)sut).Should().Be(color);
    }

    [Fact]
    public void EnsureContrast_Should_AdjustValue_When_ContrastIsInsufficient()
    {
        // Arrange
        var fg = new ThemeColor(Colors.Grey500);
        var bg = new ThemeColor(Colors.Grey500);

        // Act
        var adjusted = fg.EnsureContrast(bg);

        // Assert
        adjusted.Value.Should().NotBe(fg.Value);
        ((HexColor)adjusted).ContrastRatio((HexColor)bg).Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void EnsureContrast_Should_ReturnSameValue_When_AlreadyMeetsThreshold()
    {
        // Arrange
        var sut = new ThemeColor(Colors.White);
        var background = new ThemeColor(Colors.Black);

        // Act
        var adjusted = sut.EnsureContrast(background);

        // Assert
        adjusted.Value.Should().Be(sut.Value);
        ((HexColor)adjusted).ContrastRatio((HexColor)background).Should().BeGreaterThanOrEqualTo(4.5);
    }

    [Fact]
    public void Implicit_FromHexColor_Should_WrapSameValue()
    {
        // Arrange
        HexColor hex = "#ABCDEF80"; // A=0x80

        // Act
        ThemeColor wrapped = hex;

        // Assert
        wrapped.Value.Should().Be("#ABCDEF80");
        ((HexColor)wrapped).Should().Be(hex);
    }

    [Fact]
    public void Implicit_FromString_Should_ParseAndMatchParseMethod()
    {
        // Arrange
        const string input = "rgba(255, 165, 0, 1)"; // Orange

        // Act
        ThemeColor fromImplicit = input;
        var fromParse = ThemeColor.Parse(input);

        // Assert
        fromImplicit.Value.Should().Be(fromParse.Value);
    }

    [Fact]
    public void Implicit_ToHexColor_Should_ExposeUnderlyingHexColor()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Crimson);

        // Act
        HexColor hex = sut;

        // Assert
        hex.ToString().Should().Be(sut.Value);
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnCanonicalString()
    {
        // Arrange
        var sut = new ThemeColor(0x01, 0x02, 0x03, 1.0);

        // Act
        string s = sut;

        // Assert
        s.Should().Be("#010203FF");
    }

    [Fact]
    public void Parse_Should_ReturnParsedColor_When_InputIsValid()
    {
        // Arrange
        const string input = "#ABC"; // expands to #AABBCCFF

        // Act
        var result = ThemeColor.Parse(input);

        // Assert
        result.Value.Should().Be("#AABBCCFF");
    }

    [Fact]
    public void Parse_Should_Throw_When_InputIsInvalid()
    {
        // Arrange
        const string input = "not-a-color";

        // Act
        var act = () => ThemeColor.Parse(input);

        // Assert
        act.Should().Throw<Exception>();
    }

    [Fact]
    public void ToBorder_Should_BehaveAsDivider_When_ComponentFillIsNull()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Blue600);
        var outerBackground = new ThemeColor(Colors.Grey100);

        // Act
        var border = sut.ToBorder(outerBackground);

        // Assert
        border.Should().NotBeNull();
        border.Value.Should().NotBe(string.Empty);

        // divider should differ from the surface in most cases
        border.Value.Should().NotBe(outerBackground.Value);
    }

    [Fact]
    public void ToBorder_Should_ReturnBorder_When_ComponentFillProvided()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Blue600); // content color
        var outerBackground = new ThemeColor(Colors.Grey50);
        var componentFill = new ThemeColor(Colors.Blue100);

        // Act
        var border = sut.ToBorder(outerBackground, componentFill);

        // Assert
        border.Should().NotBeNull();
        border.Value.Should().NotBe(componentFill.Value); // should not simply equal the fill
    }

    [Fact]
    public void ToBorder_Should_ReturnContentColor_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Red500); // foreground/content
        var outerBackground = new ThemeColor(Colors.White);

        // Act
        var border = sut.ToBorder(outerBackground, null, true);

        // Assert
        border.Value.Should().Be(sut.Value);
    }

    [Fact]
    public void ToDisabled_Should_ReduceSaturation()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Pink500);
        var beforeS = ((HexColor)sut).S;

        // Act
        var disabled = sut.ToDisabled();

        // Assert
        var afterS = ((HexColor)disabled).S;
        afterS.Should().BeLessThan(beforeS);
    }

    [Fact]
    public void ToDragged_Should_Lighten_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Blue700);
        var beforeV = ((HexColor)sut).V;

        // Act
        var result = sut.ToDragged();

        // Assert
        result.Should().NotBeSameAs(sut);
        ((HexColor)result).V.Should().NotBe(beforeV); // shifted
    }

    [Fact]
    public void ToDragged_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Blue700);

        // Act
        var result = sut.ToDragged(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void ToElevation1_Should_Adjust_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation1();

        // Assert
        result.Should().NotBeSameAs(sut);
        result.Value.Should().NotBe(sut.Value);
    }

    [Fact]
    public void ToElevation1_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation1(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void ToElevation2_Should_Adjust_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation2();

        // Assert
        result.Value.Should().NotBe(sut.Value);
    }

    [Fact]
    public void ToElevation2_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation2(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void ToElevation3_Should_Adjust_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation3();

        // Assert
        result.Value.Should().NotBe(sut.Value);
    }

    [Fact]
    public void ToElevation3_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation3(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void ToElevation4_Should_Adjust_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation4();

        // Assert
        result.Value.Should().NotBe(sut.Value);
    }

    [Fact]
    public void ToElevation4_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Teal700);

        // Act
        var result = sut.ToElevation4(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void ToFocused_Should_Adjust_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Purple700);

        // Act
        var result = sut.ToFocused();

        // Assert
        result.Value.Should().NotBe(sut.Value);
    }

    [Fact]
    public void ToFocused_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Purple700);

        // Act
        var result = sut.ToFocused(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void ToHovered_Should_Adjust_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Indigo700);

        // Act
        var result = sut.ToHovered();

        // Assert
        result.Value.Should().NotBe(sut.Value);
    }

    [Fact]
    public void ToHovered_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Indigo700);

        // Act
        var result = sut.ToHovered(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void ToPressed_Should_Adjust_When_HighContrastFalse()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Green700);

        // Act
        var result = sut.ToPressed();

        // Assert
        result.Value.Should().NotBe(sut.Value);
    }

    [Fact]
    public void ToPressed_Should_ReturnThis_When_HighContrastTrue()
    {
        // Arrange
        var sut = new ThemeColor(Colors.Green700);

        // Act
        var result = sut.ToPressed(true);

        // Assert
        result.Should().BeSameAs(sut);
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_InputIsInvalid()
    {
        // Arrange
        const string input = "rgb(999, nope)";

        // Act
        var ok = ThemeColor.TryParse(input, out var parsed);

        // Assert
        ok.Should().BeFalse();
        parsed.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_InputIsValid()
    {
        // Arrange
        const string input = "rgb(255,0,0)";

        // Act
        var ok = ThemeColor.TryParse(input, out var parsed);

        // Assert
        ok.Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed.Value.Should().Be("#FF0000FF");
    }

    [Fact]
    public void Value_Should_ExposeCanonicalRRGGBBAA()
    {
        // Arrange
        var sut = new ThemeColor(0xAA, 0xBB, 0xCC);

        // Act
        var value = sut.Value;

        // Assert
        value.Should().Be("#AABBCCFF");
    }
}
