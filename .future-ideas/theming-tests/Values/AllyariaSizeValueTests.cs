using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaSizeValueTests
{
    public static IEnumerable<object[]> AllAllowedValuesWithVariants() => new[]
    {
        new object[]
        {
            "larger", "larger"
        },
        new object[]
        {
            "LARGER", "larger"
        },
        new object[]
        {
            " larger ", "larger"
        },
        new object[]
        {
            "smaller", "smaller"
        },
        new object[]
        {
            "SmAlLeR", "smaller"
        },
        new object[]
        {
            "\tSmAlLeR  ", "smaller"
        },
        new object[]
        {
            "xx-small", "xx-small"
        },
        new object[]
        {
            "XX-SMALL", "xx-small"
        },
        new object[]
        {
            "x-small", "x-small"
        },
        new object[]
        {
            "X-Small", "x-small"
        },
        new object[]
        {
            "small", "small"
        },
        new object[]
        {
            " SMALL ", "small"
        },
        new object[]
        {
            "medium", "medium"
        },
        new object[]
        {
            "MeDiUm", "medium"
        },
        new object[]
        {
            "large", "large"
        },
        new object[]
        {
            "LaRgE", "large"
        },
        new object[]
        {
            "x-large", "x-large"
        },
        new object[]
        {
            "X-LARGE", "x-large"
        },
        new object[]
        {
            "xx-large", "xx-large"
        },
        new object[]
        {
            "XX-LARGE", "xx-large"
        },
        new object[]
        {
            "xxx-large", "xxx-large"
        },
        new object[]
        {
            "XXX-LARGE", "xxx-large"
        }
    };

    [Fact]
    public void Ctor_NameAndValue_Should_PreserveProvidedValueInstance_When_Valid()
    {
        // Arrange
        var provided = new AllyariaSizeValue("large");

        // Act
        var sut = new AllyariaFontSizeCss("font-size", provided);

        // Assert
        sut.CssProperty.Should()
            .Be("font-size:large;");
    }

    [Theory]
    [MemberData(nameof(AllAllowedValuesWithVariants))]
    public void Ctor_Should_Normalize_ToLowerAndTrim_When_ValueIsAllowed(string input, string expected)
    {
        // Arrange
        // Act
        var sut = new AllyariaSizeValue(input);

        // Assert
        string actual = sut;

        actual.Should()
            .Be(expected);
    }

    [Fact]
    public void Ctor_Should_ThrowArgumentException_When_ValueIsNull()
    {
        // Arrange
        string value = null!;

        // Act
        var act = () => new AllyariaSizeValue(value);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t \r\n")]
    public void Ctor_Should_ThrowArgumentException_When_ValueIsWhitespace(string value)
    {
        // Arrange
        // Act
        var act = () => new AllyariaSizeValue(value);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("HUGE")]
    [InlineData(" teeny ")]
    [InlineData("100px")]
    [InlineData("inherit")] // valid CSS global, but not allowed here
    public void Ctor_Should_ThrowArgumentException_WithOriginalValueInMessage_When_ValueIsNotAllowed(string value)
    {
        // Arrange
        // Act
        var act = () => new AllyariaSizeValue(value);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value")
            .And.Message.Should()
            .Contain($"Invalid CSS size value: {value}");
    }

    [Theory]
    [InlineData(" Large ", "large")]
    [InlineData("\tSMALL", "small")]
    public void ImplicitConversionFromString_Should_CreateInstance_WithNormalizedValue(string input, string expected)
    {
        // Arrange
        // Act
        AllyariaSizeValue sut = input; // implicit from string

        // Assert
        string actual = sut; // implicit to string

        actual.Should()
            .Be(expected);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingNormalizedValue()
    {
        // Arrange
        var sut = new AllyariaSizeValue(" XXX-LARGE ");

        // Act
        string actual = sut; // implicit to string

        // Assert
        actual.Should()
            .Be("xxx-large");
    }

    [Theory]
    [InlineData(" X-LARGE ", "x-large")]
    [InlineData("medium", "medium")]
    public void Parse_Should_ReturnNormalizedInstance_When_InputIsValid(string input, string expected)
    {
        // Arrange
        // Act
        var result = AllyariaSizeValue.Parse(input);

        // Assert
        string actual = result; // implicit to string

        actual.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData("enormous")]
    [InlineData("auto")]
    public void Parse_Should_ThrowArgumentException_When_InputIsInvalid(string input)
    {
        // Arrange
        // Act
        var act = () => AllyariaSizeValue.Parse(input);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Theory]
    [InlineData("ginormous")]
    [InlineData("unset")] // not allowed in this type
    [InlineData("revert")]
    public void TryParse_Should_ReturnFalse_AndNull_When_InputIsInvalid(string input)
    {
        // Arrange
        // Act
        var success = AllyariaSizeValue.TryParse(input, out var result);

        // Assert
        success.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnFalse_AndNull_When_InputIsNull()
    {
        // Arrange
        string input = null!;

        // Act
        var success = AllyariaSizeValue.TryParse(input, out var result);

        // Assert
        success.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Theory]
    [InlineData(" LARGE ", "large")]
    [InlineData("xX-lArGe", "xx-large")]
    public void TryParse_Should_ReturnTrue_AndResult_When_InputIsValid(string input, string expected)
    {
        // Arrange
        // Act
        var success = AllyariaSizeValue.TryParse(input, out var result);

        // Assert
        success.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        string actual = result;

        actual.Should()
            .Be(expected);
    }
}
