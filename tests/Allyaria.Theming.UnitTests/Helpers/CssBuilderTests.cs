namespace Allyaria.Theming.UnitTests.Helpers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class CssBuilderTests
{
    [Fact]
    public void Add_Should_AddWithoutPrefix_When_PrefixEmpty()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: "BackgroundColor", value: "blue");

        // Assert
        sut.ToString().Should().Be(expected: "background-color:blue;");
    }

    [Fact]
    public void Add_Should_AddWithPrefix_When_PrefixProvided()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: "BackgroundColor", value: "blue", varPrefix: "theme");

        // Assert
        sut.ToString().Should().Be(expected: "--theme-background-color:blue;");
    }

    [Fact]
    public void Add_Should_NotAdd_When_NameIsNullOrWhitespace()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: null, value: "value");
        sut.Add(name: "", value: "value");
        sut.Add(name: "   ", value: "value");

        // Assert
        sut.ToString().Should().BeEmpty();
    }

    [Fact]
    public void Add_Should_NotAdd_When_ToCssName_ReturnsEmpty()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: "###", value: "red");

        // Assert
        sut.ToString().Should().BeEmpty();
    }

    [Fact]
    public void Add_Should_NotAdd_When_ValueIsNullOrWhitespace()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: "Color", value: null);
        sut.Add(name: "Color", value: "");
        sut.Add(name: "Color", value: "   ");

        // Assert
        sut.ToString().Should().BeEmpty();
    }

    [Fact]
    public void Add_Should_NotOverrideExistingKey_When_SamePropertyAddedTwice()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: "BackgroundColor", value: "red");
        sut.Add(name: "BackgroundColor", value: "blue"); // should not replace first value

        // Assert
        sut.ToString().Should().Be(expected: "background-color:red;");
    }

    [Fact]
    public void AddRange_Should_AddMultipleStyles_When_CssListContainsValidPairs()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.AddRange(cssList: "z-index:10;background-color:red;border-width:2px;");

        // Assert
        sut.ToString().Should().Be(expected: "background-color:red;border-width:2px;z-index:10;");
    }

    [Fact]
    public void AddRange_Should_IgnoreInvalidPairs_When_CssListContainsMalformedItems()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.AddRange(cssList: "background-color:red;invalid;:;z-index:10;");

        // Assert
        sut.ToString().Should().Be(expected: "background-color:red;z-index:10;");
    }

    [Fact]
    public void AddRange_Should_NotChangeStyles_When_CssListIsNullOrWhitespace()
    {
        // Arrange
        var sut = new CssBuilder();
        sut.Add(name: "BackgroundColor", value: "red");

        // Act
        sut.AddRange(cssList: null);
        sut.AddRange(cssList: "");
        sut.AddRange(cssList: "   ");

        // Assert
        sut.ToString().Should().Be(expected: "background-color:red;");
    }

    [Fact]
    public void AddRange_Should_NotOverrideExistingStyles_When_DuplicatePropertiesProvided()
    {
        // Arrange
        var sut = new CssBuilder();
        sut.Add(name: "BackgroundColor", value: "red");

        // Act
        sut.AddRange(cssList: "background-color:blue;border-width:1px");

        // Assert
        sut.ToString().Should().Be(expected: "background-color:red;border-width:1px;");
    }

    [Fact]
    public void ToString_Should_ReturnEmpty_When_NoStylesAdded()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        var result = sut.ToString();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ToString_Should_ReturnSortedAndCombinedStyles_When_MultipleAdded()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: "ZIndex", value: "10");
        sut.Add(name: "BackgroundColor", value: "red");
        sut.Add(name: "BorderWidth", value: "2px");

        // Assert
        sut.ToString().Should().Be(expected: "background-color:red;border-width:2px;z-index:10;");
    }
}
