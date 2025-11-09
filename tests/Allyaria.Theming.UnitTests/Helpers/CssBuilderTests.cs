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
        sut.ToString().Should().Be(expected: "background-color:blue");
    }

    [Fact]
    public void Add_Should_AddWithPrefix_When_PrefixProvided()
    {
        // Arrange
        var sut = new CssBuilder();

        // Act
        sut.Add(name: "BackgroundColor", value: "blue", varPrefix: "theme");

        // Assert
        sut.ToString().Should().Be(expected: "--theme-background-color:blue");
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
        // "###" cannot be normalized to a valid CSS name, ToCssName returns empty
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
        sut.ToString().Should().Be(expected: "background-color:red");
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
        // SortedDictionary ensures alphabetical order
        sut.ToString().Should().Be(expected: "background-color:red;border-width:2px;z-index:10");
    }
}
