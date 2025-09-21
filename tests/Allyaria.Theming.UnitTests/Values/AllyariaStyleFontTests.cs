using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaStyleFont_PublicSurfaceTests
{
    [Fact]
    public void Ctor_EscapesInnerDoubleQuotes_AndQuotesOverall()
    {
        // Arrange
        var sut = new AllyariaStyleFont("Foo\"Bar");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("\"Foo\\\"Bar\"");

        sut.Value.Should()
            .Be("\"Foo\\\"Bar\"");
    }

    [Fact]
    public void Ctor_QuotesWhenWhitespaceOrCommaOrQuotesPresent()
    {
        // Arrange
        var sut = new AllyariaStyleFont("Open Sans", "A,B", "A'B");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("\"Open Sans\"", "A", "B", "\"A'B\"");

        sut.Value.Should()
            .Be("\"Open Sans\",A,B,\"A'B\"");
    }

    [Fact]
    public void Ctor_RemovesWhitespaceOnlyAndEmptyCommaTokens()
    {
        // Arrange
        var sut = new AllyariaStyleFont(" , ,  , A  ,  ,  B ", "  ", ",,,", " C ");

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
            " Inter  ,  Roboto ", // comma split + trim
            "Open Sans", // requires quotes
            "'Open Sans'", // already quoted → strip → canonical re-quote
            "inter" // duplicate (case-insensitive) removed
        };

        // Act
        var sut = new AllyariaStyleFont(input);

        // Assert
        sut.Value.Should()
            .Be("Inter,Roboto,\"Open Sans\"");

        sut.Families.Should()
            .Equal("Inter", "Roboto", "\"Open Sans\"");
    }

    [Fact]
    public void Ctor_StripsOuterQuotes_ThenCanonicalizes()
    {
        // Arrange
        var sut = new AllyariaStyleFont("'Open Sans'", "\"Already Quoted\"", "'Inter'");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("\"Open Sans\"", "\"Already Quoted\"", "Inter");

        sut.Value.Should()
            .Be("\"Open Sans\",\"Already Quoted\",Inter");
    }

    [Fact]
    public void Ctor_WhitespaceCharactersTriggerQuoting()
    {
        // Arrange
        var sut = new AllyariaStyleFont("Tab\tHere", "New\nLine", "FormFeed\fHere");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("\"Tab\tHere\"", "\"New\nLine\"", "\"FormFeed\fHere\"");

        sut.Value.Should()
            .Be("\"Tab\tHere\",\"New\nLine\",\"FormFeed\fHere\"");
    }

    [Fact]
    public void Ctor_WithNoArgs_YieldsEmptyValueAndNoFamilies()
    {
        // Arrange // Act
        var sut = new AllyariaStyleFont();

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
        var sut = new AllyariaStyleFont(raw!);

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
        var sut = new AllyariaStyleFont("Inter", "inter", "\"Inter\"", "Open Sans", "\"open sans\"");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("Inter", "\"Open Sans\"");

        sut.Value.Should()
            .Be("Inter,\"Open Sans\"");
    }

    [Fact]
    public void Families_SplitsByComma_AndTrimsEntries()
    {
        // Arrange
        var sut = new AllyariaStyleFont(" A ,  B  ,  \"C C\" ");

        // Act
        var families = sut.Families;

        // Assert
        families.Should()
            .Equal("A", "B", "\"C C\"");
    }

    [Fact]
    public void Implicit_FromEmptyString_YieldsEmpty()
    {
        // Arrange
        var raw = "";

        // Act
        AllyariaStyleFont sut = raw;

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
        AllyariaStyleFont sut = nullArray!;

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
        AllyariaStyleFont sut = "Inter, Open Sans";

        // Act
        string value = sut;
        string[] families = sut;

        // Assert
        value.Should()
            .Be("Inter,\"Open Sans\"");

        families.Should()
            .Equal("Inter", "\"Open Sans\"");
    }

    [Fact]
    public void Implicit_FromStringArray_NormalizesAndQuotes()
    {
        // Arrange
        string[] raw =
        {
            "Inter", "Open Sans"
        };

        // Act
        AllyariaStyleFont sut = raw;

        // Assert
        ((string)sut).Should()
            .Be("Inter,\"Open Sans\"");

        ((string[])sut).Should()
            .Equal("Inter", "\"Open Sans\"");
    }

    [Fact]
    public void Implicit_ToString_ReturnsNormalizedCommaJoinedValue()
    {
        // Arrange
        var sut = new AllyariaStyleFont("Inter", "Open  Sans");

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
        var sut = new AllyariaStyleFont("Inter,  Roboto", "Open Sans");

        // Act
        string[] families = sut;

        // Assert
        families.Should()
            .Equal("Inter", "Roboto", "\"Open Sans\"");
    }
}
