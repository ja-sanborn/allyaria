using Allyaria.Theming.Values;
using System.Globalization;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaNumberValueTests
{
    [Theory]
    [InlineData("10px", 10)]
    [InlineData("-.75rem", -0.75)]
    [InlineData("2.5em", 2.5)]
    [InlineData("100q", 100)]
    [InlineData("1svw", 1)]
    [InlineData("3cqmin", 3)]
    [InlineData("4vh", 4)]
    public void Constructor_Should_Accept_Lengths_When_InputHasSupportedUnit(string input, double expectedNumber)
    {
        // Arrange & Act
        var sut = new AllyariaNumberValue(input);

        // Assert
        string normalized = sut;

        normalized.Should()
            .Be(input);

        sut.Number.Should()
            .Be(expectedNumber);
    }

    [Theory]
    [InlineData("0%")]
    [InlineData("12.5%")]
    [InlineData("-33.33%")]
    public void Constructor_Should_Accept_Percentages_When_InputEndsWithPercent(string input)
    {
        // Arrange & Act
        var sut = new AllyariaNumberValue(input);

        // Assert
        string normalized = sut;

        normalized.Should()
            .Be(input.ToLowerInvariant());

        sut.Number.Should()
            .Be(double.Parse(input[..^1], NumberStyles.Float, CultureInfo.InvariantCulture));
    }

    [Theory]
    [InlineData("0")]
    [InlineData("42")]
    [InlineData("-17")]
    [InlineData(".5")]
    [InlineData("5.")]
    [InlineData("-0.75")]
    public void Constructor_Should_Accept_PlainNumbers_When_InputIsNumeric(string input)
    {
        // Arrange & Act
        var sut = new AllyariaNumberValue(input);

        // Assert
        string normalized = sut;

        normalized.Should()
            .Be(input.ToLowerInvariant());
    }

    [Fact]
    public void Constructor_Should_Normalize_ToLower_And_Trim_When_InputHasWhitespaceAndUppercase()
    {
        // Arrange
        var input = "  10PX  ";

        // Act
        var sut = new AllyariaNumberValue(input);
        string normalized = sut; // implicit AllyariaNumberValue -> string

        // Assert
        normalized.Should()
            .Be("10px");

        sut.Number.Should()
            .Be(10d);
    }

    [Theory]
    [InlineData("10 px")] // space is not allowed by IsValid
    [InlineData("10px;")] // semicolon not allowed
    [InlineData("calc(1)")] // parentheses not allowed
    [InlineData("#123")] // hash not allowed
    public void Constructor_Should_ThrowArgumentException_When_InputContainsInvalidCharacters(string input)
    {
        // Arrange
        var act = () => new AllyariaNumberValue(input);

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_ThrowArgumentException_When_InputIsNullOrWhiteSpace(string? input)
    {
        // Arrange
        var act = () => new AllyariaNumberValue(input!);

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("px")] // unit only, no numeric part
    [InlineData("rem")] // unit only
    [InlineData("10p")] // unsupported unit
    [InlineData("+%")] // percent with no number
    public void Constructor_Should_ThrowArgumentException_When_LengthOrPercentageIsMalformed(string input)
    {
        // Arrange
        var act = () => new AllyariaNumberValue(input);

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Fact]
    public void Implicit_AllyariaNumberValueToString_Should_ReturnEmpty_When_InstanceIsNull()
    {
        // Arrange
        AllyariaNumberValue? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should()
            .Be(string.Empty);
    }

    [Fact]
    public void Implicit_StringToAllyariaNumberValue_Should_ConstructInstance_When_InputIsValid()
    {
        // Arrange
        AllyariaNumberValue sut = "2.5rem";

        // Act
        string normalized = sut;

        // Assert
        normalized.Should()
            .Be("2.5rem");

        sut.Number.Should()
            .Be(2.5d);
    }

    [Fact]
    public void Implicit_StringToAllyariaNumberValue_Should_ThrowArgumentException_When_InputIsInvalid()
    {
        // Arrange
        var act = () =>
        {
            AllyariaNumberValue _ = "bad value";
        };

        // Act / Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("1px")]
    [InlineData(
        "1e3px"
    )] // exponent is allowed for validation, but Number should read only the leading numeric via regex
    [InlineData("1e3")] // plain number with exponent: valid input, Number ignores exponent per regex design
    public void Number_Should_IgnoreExponentNotation_When_ValueContainsExponent(string input)
    {
        // Arrange
        var sut = new AllyariaNumberValue(input);

        // Act
        var number = sut.Number;

        // Assert
        number.Should()
            .Be(1d);
    }

    [Theory]
    [InlineData("NaN", 0d)]
    [InlineData("Infinity", 0d)]
    [InlineData("-Infinity", 0d)]
    public void Number_Should_ReturnZero_When_No_Leading_Numeric_Match_Even_If_Input_Is_A_Valid_Number_String(
        string input,
        double expected)
    {
        // Arrange
        // These inputs are valid per Double.TryParse (Normalize accepts them),
        // but the Number regex only matches leading digits/decimal—not special tokens—so Match.Success == false.
        var sut = new AllyariaNumberValue(input);

        // Act
        var number = sut.Number;

        // Assert
        number.Should()
            .Be(
                expected,
                "NumberPrefixRegex won't match special numeric tokens like NaN/Infinity, exercising the non-match path"
            );
    }

    [Fact]
    public void Parse_Should_ReturnEquivalentValue_When_ValidInput()
    {
        // Arrange
        var input = " 12PX ";

        // Act
        var sut = AllyariaNumberValue.Parse(input);

        // Assert
        string normalized = sut;

        normalized.Should()
            .Be("12px");

        sut.Number.Should()
            .Be(12d);
    }

    [Fact]
    public void Parse_Should_ThrowArgumentException_When_InvalidInput()
    {
        // Arrange
        var input = "nope";

        // Act
        var act = () => AllyariaNumberValue.Parse(input);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Fact]
    public void TryParse_Should_ReturnFalse_And_NullResult_When_InputIsInvalid()
    {
        // Arrange
        var input = "10 px";

        // Act
        var success = AllyariaNumberValue.TryParse(input, out var result);

        // Assert
        success.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrue_And_OutResult_When_InputIsValid()
    {
        // Arrange
        var input = "50%";

        // Act
        var success = AllyariaNumberValue.TryParse(input, out var result);

        // Assert
        success.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        string normalized = result;

        normalized.Should()
            .Be("50%");

        result.Number.Should()
            .Be(50d);
    }

    [Fact]
    public void Validation_Should_BeCultureInvariant_When_CurrentCultureUsesCommaDecimal()
    {
        // Arrange
        var original = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

        try
        {
            // Act
            var valid = new AllyariaNumberValue("1.5"); // dot should still be valid due to Invariant parsing
            var invalidAct = () => new AllyariaNumberValue("1,5"); // comma should be invalid

            // Assert
            string normalized = valid;

            normalized.Should()
                .Be("1.5");

            invalidAct.Should()
                .Throw<ArgumentException>()
                .WithParameterName("value");
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }
}
