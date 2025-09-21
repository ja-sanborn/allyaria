using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaNumberTests
{
    [Theory]
    [InlineData("1rex", "1rex", 1)]
    [InlineData("2cap", "2cap", 2)]
    [InlineData("3rcap", "3rcap", 3)]
    [InlineData("4rch", "4rch", 4)]
    [InlineData("5ic", "5ic", 5)]
    [InlineData("6ric", "6ric", 6)]
    [InlineData("7lh", "7lh", 7)]
    [InlineData("8rlh", "8rlh", 8)]
    public void AdditionalFontRelativeUnits_ShouldNormalize_AndParse(string input,
        string expectedValue,
        decimal expectedNumber)
    {
        // Arrange

        // Act
        var sut = new AllyariaNumber(input);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be(expectedValue);

        number.Should()
            .Be(expectedNumber);
    }

    [Theory]
    [InlineData("1svw", "1svw", 1)]
    [InlineData("2svh", "2svh", 2)]
    [InlineData("3svi", "3svi", 3)]
    [InlineData("4svb", "4svb", 4)]
    [InlineData("5svmin", "5svmin", 5)]
    [InlineData("6svmax", "6svmax", 6)]
    [InlineData("7lvw", "7lvw", 7)]
    [InlineData("8lvh", "8lvh", 8)]
    [InlineData("9lvi", "9lvi", 9)]
    [InlineData("10lvb", "10lvb", 10)]
    [InlineData("11lvmin", "11lvmin", 11)]
    [InlineData("12lvmax", "12lvmax", 12)]
    [InlineData("13dvw", "13dvw", 13)]
    [InlineData("14dvh", "14dvh", 14)]
    [InlineData("15dvi", "15dvi", 15)]
    [InlineData("16dvb", "16dvb", 16)]
    [InlineData("17dvmin", "17dvmin", 17)]
    [InlineData("18dvmax", "18dvmax", 18)]
    public void AllViewportFamilies_ShouldNormalize_AndParse(string input, string expectedValue, decimal expectedNumber)
    {
        // Arrange

        // Act
        var sut = new AllyariaNumber(input);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be(expectedValue);

        number.Should()
            .Be(expectedNumber);
    }

    [Theory]
    [InlineData("1cqw", "1cqw", 1)]
    [InlineData("2cqh", "2cqh", 2)]
    [InlineData("3cqi", "3cqi", 3)]
    [InlineData("4cqb", "4cqb", 4)]
    [InlineData("5cqmin", "5cqmin", 5)]
    [InlineData("6cqmax", "6cqmax", 6)]
    public void ContainerQueryUnits_ShouldNormalize_AndParse(string input, string expectedValue, decimal expectedNumber)
    {
        // Arrange

        // Act
        var sut = new AllyariaNumber(input);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be(expectedValue);

        number.Should()
            .Be(expectedNumber);
    }

    [Fact]
    public void Implicit_FromString_ShouldNormalize_AndParseNumber()
    {
        // Arrange
        AllyariaNumber sut = "  15.5PT  ";

        // Act
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be("15.5pt");

        number.Should()
            .Be(15.5m);
    }

    [Fact]
    public void Implicit_ToString_On_Null_Instance_ReturnsEmpty()
    {
        // Arrange
        AllyariaNumber? sut = null;

        // Act
        string value = sut;

        // Assert
        value.Should()
            .BeEmpty();
    }

    [Fact]
    public void Implicit_ToString_ShouldReturnNormalizedValue()
    {
        // Arrange
        var sut = new AllyariaNumber("  -2.25ex  ");

        // Act
        string value = sut;

        // Assert
        value.Should()
            .Be("-2.25ex");

        sut.Number.Should()
            .Be(-2.25m);
    }

    [Theory]
    [InlineData("px", "", 0)]
    [InlineData("%", "", 0)]
    [InlineData("12 %", "", 0)]
    [InlineData("12pp", "", 0)]
    [InlineData("abc", "", 0)]
    [InlineData("12.3.4", "", 0)]
    [InlineData("", "", 0)]
    [InlineData("   ", "", 0)]
    public void Invalid_ShouldBecomeEmpty_AndNumberZero(string input, string expectedValue, decimal expectedNumber)
    {
        // Arrange

        // Act
        var sut = new AllyariaNumber(input);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be(expectedValue);

        number.Should()
            .Be(expectedNumber);
    }

    [Theory]
    [InlineData("10px", "10px", 10)]
    [InlineData("  1.5rem  ", "1.5rem", 1.5)]
    [InlineData("12EM", "12em", 12)]
    [InlineData("7Q", "7q", 7)]
    [InlineData("-3.25Ch", "-3.25ch", -3.25)]
    [InlineData("100LVW", "100lvw", 100)]
    [InlineData("22.2dvh", "22.2dvh", 22.2)]
    [InlineData("5cqw", "5cqw", 5)]
    [InlineData("9.75ic", "9.75ic", 9.75)]
    public void Length_ShouldNormalizeUnitsToLower_AndParseNumber(string input,
        string expectedValue,
        decimal expectedNumber)
    {
        // Arrange

        // Act
        var sut = new AllyariaNumber(input);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be(expectedValue);

        number.Should()
            .Be(expectedNumber);
    }

    [Fact]
    public void NullInput_ShouldBecomeEmpty_AndNumberZero()
    {
        // Arrange
        string? input = null;

        // Act
        var sut = new AllyariaNumber(input!);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .BeEmpty();

        number.Should()
            .Be(0);
    }

    [Theory]
    [InlineData("0", "0", 0)]
    [InlineData("12", "12", 12)]
    [InlineData("-3.5", "-3.5", -3.5)]
    [InlineData("  10.0  ", "10.0", 10.0)]
    [InlineData("+4.25", "+4.25", 4.25)]
    public void Numeric_ShouldNormalize_AndParseNumber(string input, string expectedValue, decimal expectedNumber)
    {
        // Arrange

        // Act
        var sut = new AllyariaNumber(input);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be(expectedValue);

        number.Should()
            .Be(expectedNumber);
    }

    [Theory]
    [InlineData("0%", "0%", 0)]
    [InlineData("12%", "12%", 12)]
    [InlineData("-3.5%", "-3.5%", -3.5)]
    [InlineData("  10.0%  ", "10.0%", 10.0)]
    [InlineData("+4.25%", "+4.25%", 4.25)]
    public void Percentage_ShouldNormalize_AndParseNumber(string input, string expectedValue, decimal expectedNumber)
    {
        // Arrange

        // Act
        var sut = new AllyariaNumber(input);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        value.Should()
            .Be(expectedValue);

        number.Should()
            .Be(expectedNumber);
    }

    [Fact]
    public void TryParse_InvalidInput_ReturnsFalse_And_Empty()
    {
        // Arrange

        // Act
        var ok = AllyariaNumber.TryParse("12 %", out var sut);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        ok.Should()
            .BeFalse();

        value.Should()
            .BeEmpty();

        number.Should()
            .Be(0m);
    }

    [Fact]
    public void TryParse_ValidInput_ReturnsTrue_And_Normalized()
    {
        // Arrange

        // Act
        var ok = AllyariaNumber.TryParse("  1.5rem  ", out var sut);
        var value = (string)sut;
        var number = sut.Number;

        // Assert
        ok.Should()
            .BeTrue();

        value.Should()
            .Be("1.5rem");

        number.Should()
            .Be(1.5m);
    }
}
