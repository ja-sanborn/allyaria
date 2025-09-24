using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaFontFamilyValueTests
{
    [Fact]
    public void Ctor_EscapesInnerDoubleQuotes_AndQuotesOverall()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("Foo\"Bar");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Foo\"Bar"); // Families are unquoted & unescaped

        sut.Value.Should()
            .Be("\"Foo\\\"Bar\"");
    }

    [Fact]
    public void Ctor_QuotesWhenWhitespaceOrCommaOrQuotesPresent()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("Open Sans", "A,B", "A'B");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Open Sans", "A", "B", "A'B"); // unquoted

        sut.Value.Should()
            .Be("\"Open Sans\",A,B,\"A'B\"");
    }

    [Fact]
    public void Ctor_RemovesWhitespaceOnlyAndEmptyCommaTokens()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue(" , ,  , A  ,  ,  B ", "  ", ",,,", " C ");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("A", "B", "C");

        sut.Value.Should()
            .Be("A,B,C");
    }

    [Fact]
    public void Ctor_Should_ThrowArgumentException_When_NoValuesProvided()
    {
        // Arrange
        var act = () => new AllyariaFontFamilyValue();

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("values")
            .WithMessage("*At least one font family name must be provided.*");
    }

    [Fact]
    public void Ctor_SplitsTrimsQuotesAndDeDupesCaseInsensitive_PreservingOrder()
    {
        // Arrange
        var input = new[]
        {
            " Inter  ,  Roboto ", "Open Sans", "'Open Sans'", "inter"
        };

        // Act
        var sut = new AllyariaFontFamilyValue(input);

        // Assert
        sut.Value.Should()
            .Be("Inter,Roboto,\"Open Sans\"");

        sut.Families.Should()
            .Equal("Inter", "Roboto", "Open Sans"); // unquoted
    }

    [Fact]
    public void Ctor_StripsOuterQuotes_ThenCanonicalizes()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("'Open Sans'", "\"Already Quoted\"", "'Inter'");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Open Sans", "Already Quoted", "Inter"); // unquoted

        sut.Value.Should()
            .Be("\"Open Sans\",\"Already Quoted\",Inter");
    }

    [Fact]
    public void Ctor_WhitespaceCharactersTriggerQuoting()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("Tab\tHere", "New\nLine", "FormFeed\fHere");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Tab\tHere", "New\nLine", "FormFeed\fHere"); // unquoted

        sut.Value.Should()
            .Be("\"Tab\tHere\",\"New\nLine\",\"FormFeed\fHere\"");
    }

    [Fact]
    public void DeDuplication_IsCaseInsensitive_AndOrderPreserving()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("Inter", "inter", "\"Inter\"", "Open Sans", "\"open sans\"");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Inter", "Open Sans"); // unquoted

        sut.Value.Should()
            .Be("Inter,\"Open Sans\"");
    }

    [Fact]
    public void Families_IsMemoized_ReturnsSameReferenceOnSubsequentAccess()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("Inter", "Open Sans");

        // Act
        var first = sut.Families;
        var second = sut.Families;

        // Assert
        ReferenceEquals(first, second)
            .Should()
            .BeTrue();
    }

    [Fact]
    public void Families_SplitsByComma_AndTrimsEntries()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue(" A ,  B  ,  \"C C\" ");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("A", "B", "C C"); // unquoted
    }

    [Fact]
    public void Implicit_FromString_CommaSeparated_NormalizesAndQuotes()
    {
        // Arrange
        AllyariaFontFamilyValue sut = "Inter, Open Sans";

        // Act
        string value = sut;
        string[] families = sut;

        // Assert
        value.Should()
            .Be("Inter,\"Open Sans\"");

        families.Should()
            .Equal("Inter", "Open Sans"); // unquoted
    }

    [Fact]
    public void Implicit_ToString_ReturnsNormalizedCommaJoinedValue()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("Inter", "Open  Sans");

        // Act
        string value = sut;

        // Assert
        value.Should()
            .Be("Inter,\"Open  Sans\"");
    }

    [Fact]
    public void Implicit_ToStringArray_ReturnsNormalizedArray()
    {
        // Arrange
        var sut = new AllyariaFontFamilyValue("Inter,  Roboto", "Open Sans");

        // Act
        string[] families = sut;

        // Assert
        families.Should()
            .Equal("Inter", "Roboto", "Open Sans"); // unquoted
    }

    [Fact]
    public void Parse_Should_ReturnNormalized_When_InputIsValid()
    {
        // Arrange
        var input = " Inter , Open Sans , \"Foo Bar\" ";

        // Act
        var sut = AllyariaFontFamilyValue.Parse(input);

        // Assert
        ((string)sut).Should()
            .Be("Inter,\"Open Sans\",\"Foo Bar\"");

        ((string[])sut).Should()
            .Equal("Inter", "Open Sans", "Foo Bar");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Parse_Should_ThrowArgumentException_When_InputIsNullOrWhitespace(string? input)
    {
        // Arrange
        var act = () => AllyariaFontFamilyValue.Parse(input!);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("values");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\r\n")]
    public void TryParse_Should_ReturnFalse_AndNullResult_When_InputIsNullOrWhitespace(string? input)
    {
        // Act
        var ok = AllyariaFontFamilyValue.TryParse(input!, out var result);

        // Assert
        ok.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Theory]
    [InlineData(",")]
    [InlineData(",,")]
    [InlineData(" , , ")]
    [InlineData(" , ,  ,  ")]
    public void TryParse_Should_ReturnFalse_AndNullResult_When_NormalizesToNoTokens(string input)
    {
        // Act
        var ok = AllyariaFontFamilyValue.TryParse(input, out var result);

        // Assert
        ok.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrue_WithNormalizedResult_When_InputIsValid()
    {
        // Arrange
        var input = " Inter ,  Roboto , Open Sans , inter ";

        // Act
        var ok = AllyariaFontFamilyValue.TryParse(input, out var result);

        // Assert
        ok.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        ((string)result).Should()
            .Be("Inter,Roboto,\"Open Sans\"");

        ((string[])result).Should()
            .Equal("Inter", "Roboto", "Open Sans");
    }
}
