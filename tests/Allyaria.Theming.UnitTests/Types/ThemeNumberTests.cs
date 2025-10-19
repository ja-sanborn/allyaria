namespace Allyaria.Theming.UnitTests.Types;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeNumberTests
{
    [Theory]
    [InlineData("1000000", "1000000")] // very large -> G17 (but likely non-scientific string)
    [InlineData("123.45678901", "123.456789")] // 7 fractional digits with rounding
    public void Ctor_Should_Apply_CanonicalNumberFormatting(string input, string expected)
    {
        // Arrange
        // Act
        var sut = new ThemeNumber(input);

        // Assert
        sut.Value.Should().Be(expected);
        sut.LengthUnit.HasValue.Should().BeFalse();
    }

    [Theory]
    [InlineData("12PX", "12px")] // case-insensitive unit => canonical lower-case token
    [InlineData("  -3.5   rem  ", "-3.5rem")] // whitespace between number and unit removed
    [InlineData("+.25px", "0.25px")] // leading + and leading dot normalized
    public void Ctor_Should_Normalize_NumberAndUnit_TokenAndWhitespace(string input, string expectedCanonical)
    {
        // Arrange
        // Act
        var sut = new ThemeNumber(input);

        // Assert
        sut.LengthUnit.HasValue.Should().BeTrue(); // we don't couple to the exact enum theme
        sut.Value.Should().Be(expectedCanonical);
    }

    [Fact]
    public void Ctor_Should_NormalizeAuto_When_ValueIsAuto_IgnoringCasingAndWhitespace()
    {
        // Arrange
        var input = "  AuTo  ";

        // Act
        var sut = new ThemeNumber(input);

        // Assert
        sut.Value.Should().Be("auto");
        sut.Number.Should().Be(0);
        sut.LengthUnit.HasValue.Should().BeFalse();
    }

    [Theory]
    [InlineData("42", 42d, "42")]
    [InlineData("  -3.5  ", -3.5d, "-3.5")]
    [InlineData("+0.25", 0.25d, "0.25")]
    [InlineData(".25", 0.25d, "0.25")]
    public void Ctor_Should_Parse_UnitlessNumbers_With_CanonicalFormatting(string input,
        double expectedNumber,
        string expectedCanonical)
    {
        // Arrange
        // Act
        var sut = new ThemeNumber(input);

        // Assert
        sut.Number.Should().Be(expectedNumber);
        sut.LengthUnit.HasValue.Should().BeFalse();
        sut.Value.Should().Be(expectedCanonical);
    }

    [Fact]
    public void Ctor_Should_Throw_When_InvalidNumeric_Format()
    {
        // Arrange
        var input = "abc%";

        // Act
        var act = () => new ThemeNumber(input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*Invalid numeric format*");
    }

    [Fact]
    public void Ctor_Should_Throw_When_NumberOverflowsDouble_TryParseFails()
    {
        // Arrange
        var input = new string('9', 400) + "px"; // e.g., 9999...(400 digits)px

        // Act
        var act = () => new ThemeNumber(input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*Number is not a finite theme*");
    }

    [Fact]
    public void Ctor_Should_Throw_When_UnsupportedUnit()
    {
        // Arrange
        var input = "12qu"; // bogus unit token

        // Act
        var act = () => new ThemeNumber(input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*Unsupported unit token*'qu'*");
    }

    [Fact]
    public void Ctor_Should_UseInvariantCulture_DotDecimalOnly()
    {
        // Arrange
        var input = "1,5px"; // comma decimal should not parse under invariant rules, regex blocks it

        // Act
        var act = () => new ThemeNumber(input);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*Invalid numeric format*");
    }

    [Fact]
    public void Implicit_FromString_Should_CreateInstance()
    {
        // Arrange
        var input = "7px";

        // Act
        ThemeNumber sut = input;

        // Assert
        sut.Value.Should().Be("7px");
        sut.Number.Should().Be(7);
        sut.LengthUnit.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Implicit_ToString_Should_ReturnCanonicalValue()
    {
        // Arrange
        var sut = new ThemeNumber(" -3.5000 ");

        // Act
        string canonical = sut; // implicit to string

        // Assert
        canonical.Should().Be("-3.5");
    }

    [Fact]
    public void Implicit_ToString_Should_ThrowNullReference_When_SourceIsNull()
    {
        // Arrange
        ThemeNumber? sut = null;

        // Act
        var act = () =>
        {
            string _ = sut!; // triggers implicit operator with null instance
        };

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Parse_Should_ReturnInstance_When_InputValid()
    {
        // Arrange
        var input = "100%";

        // Act
        var sut = ThemeNumber.Parse(input);

        // Assert
        sut.Value.Should().Be("100%");
        sut.Number.Should().Be(100);
        sut.LengthUnit.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Parse_Should_Throw_AllyariaArgumentException_When_NullInput()
    {
        // Arrange
        string? input = null;

        // Act
        var act = () => ThemeNumber.Parse(input!);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*value cannot be null, empty or whitespace.*");
    }

    [Fact]
    public void TryParse_Should_ReturnFalse_And_NullResult_When_Invalid()
    {
        // Arrange
        var input = "oops";

        // Act
        var success = ThemeNumber.TryParse(input, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrue_And_OutputResult_When_Valid()
    {
        // Arrange
        var input = "15px";

        // Act
        var success = ThemeNumber.TryParse(input, out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().NotBeNull();
        result.Value.Should().Be("15px");
        result.Number.Should().Be(15);
        result.LengthUnit.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Whitespace_And_Signs_Should_Be_Normalized_BeforeUnit()
    {
        // Arrange
        var input = " +.250000  PX ";

        // Act
        var sut = new ThemeNumber(input);

        // Assert
        sut.Value.Should().Be("0.25px");
        sut.Number.Should().Be(0.25);
        sut.LengthUnit.HasValue.Should().BeTrue();
    }
}
