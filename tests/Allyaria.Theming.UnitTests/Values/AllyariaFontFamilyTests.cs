using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaFontFamilyTests
{
    [Fact]
    public void Ctor_EscapesInnerDoubleQuotes_AndQuotesOverall()
    {
        // Arrange
        var sut = new AllyariaFontFamily("Foo\"Bar");

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
        var sut = new AllyariaFontFamily("Open Sans", "A,B", "A'B");

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
        var sut = new AllyariaFontFamily(" , ,  , A  ,  ,  B ", "  ", ",,,", " C ");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("A", "B", "C");

        sut.Value.Should()
            .Be("A,B,C");
    }

    [Fact]
    public void Ctor_SplitsTrimsQuotesAndDeDupesCaseInsensitive_PreservingOrder()
    {
        // Arrange
        var input = new[]
        {
            " Inter  ,  Roboto ",
            "Open Sans",
            "'Open Sans'",
            "inter"
        };

        // Act
        var sut = new AllyariaFontFamily(input);

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
        var sut = new AllyariaFontFamily("'Open Sans'", "\"Already Quoted\"", "'Inter'");

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
        var sut = new AllyariaFontFamily("Tab\tHere", "New\nLine", "FormFeed\fHere");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Tab\tHere", "New\nLine", "FormFeed\fHere"); // unquoted

        sut.Value.Should()
            .Be("\"Tab\tHere\",\"New\nLine\",\"FormFeed\fHere\"");
    }

    [Fact]
    public void Ctor_WithNoArgs_YieldsEmptyValueAndNoFamilies()
    {
        // Arrange // Act
        var sut = new AllyariaFontFamily();

        // Assert
        sut.Value.Should()
            .BeEmpty();

        sut.Families.Should()
            .BeEmpty();
    }

    [Fact]
    public void Ctor_WithNullParamsArray_TreatedAsEmpty()
    {
        // Arrange
        string[]? raw = null;

        // Act
        var sut = new AllyariaFontFamily(raw!);

        // Assert
        sut.Value.Should()
            .BeEmpty();

        sut.Families.Should()
            .BeEmpty();
    }

    [Fact]
    public void DeDuplication_IsCaseInsensitive_AndOrderPreserving()
    {
        // Arrange
        var sut = new AllyariaFontFamily("Inter", "inter", "\"Inter\"", "Open Sans", "\"open sans\"");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Inter", "Open Sans"); // unquoted

        sut.Value.Should()
            .Be("Inter,\"Open Sans\"");
    }

    [Fact]
    public void Families_SplitsByComma_AndTrimsEntries()
    {
        // Arrange
        var sut = new AllyariaFontFamily(" A ,  B  ,  \"C C\" ");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("A", "B", "C C"); // unquoted
    }

    [Fact]
    public void Implicit_FromEmptyString_YieldsEmpty()
    {
        // Arrange
        var raw = "";

        // Act
        AllyariaFontFamily sut = raw;

        // Assert
        ((string)sut).Should()
            .BeEmpty();

        ((string[])sut).Should()
            .BeEmpty();
    }

    [Fact]
    public void Implicit_FromNullStringArray_YieldsEmpty()
    {
        // Arrange
        string[]? nullArray = null;

        // Act
        AllyariaFontFamily sut = nullArray!;

        // Assert
        ((string)sut).Should()
            .BeEmpty();

        ((string[])sut).Should()
            .BeEmpty();
    }

    [Fact]
    public void Implicit_FromString_CommaSeparated_NormalizesAndQuotes()
    {
        // Arrange
        AllyariaFontFamily sut = "Inter, Open Sans";

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
    public void Implicit_FromStringArray_NormalizesAndQuotes()
    {
        // Arrange
        string[] raw = { "Inter", "Open Sans" };

        // Act
        AllyariaFontFamily sut = raw;

        // Assert
        ((string)sut).Should()
            .Be("Inter,\"Open Sans\"");

        ((string[])sut).Should()
            .Equal("Inter", "Open Sans"); // unquoted
    }

    [Fact]
    public void Implicit_ToString_ReturnsNormalizedCommaJoinedValue()
    {
        // Arrange
        var sut = new AllyariaFontFamily("Inter", "Open  Sans");

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
        var sut = new AllyariaFontFamily("Inter,  Roboto", "Open Sans");

        // Act
        string[] families = sut;

        // Assert
        families.Should()
            .Equal("Inter", "Roboto", "Open Sans"); // unquoted
    }

    [Fact]
    public void TryParse_ValidInput_ReturnsTrue_WithNormalizedValue_AndFamilies()
    {
        // Arrange
        var raw = " Inter , Open Sans ";

        // Act
        var ok = AllyariaFontFamily.TryParse(raw, out var sut);

        // Assert
        ok.Should().BeTrue();
        ((string)sut).Should().Be("Inter,\"Open Sans\"");
        ((string[])sut).Should().Equal("Inter", "Open Sans"); // unquoted
    }

    [Fact]
    public void TryParse_InvalidInput_ReturnsFalse_And_Empty()
    {
        // Arrange
        var raw = "   ,  ,  ";

        // Act
        var ok = AllyariaFontFamily.TryParse(raw, out var sut);

        // Assert
        ok.Should().BeFalse();
        ((string)sut).Should().BeEmpty();
        ((string[])sut).Should().BeEmpty();
    }
}
