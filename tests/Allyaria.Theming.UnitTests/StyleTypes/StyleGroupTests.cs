namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleGroupTests
{
    [Fact]
    public void BuildCss_Should_AddAllProperties_ToBuilder()
    {
        // Arrange
        var builder = new CssBuilder();
        var type = StyleGroupType.Margin;
        var blockStart = new FakeStyleValue(value: "top");
        var blockEnd = new FakeStyleValue(value: "bottom");
        var inlineStart = new FakeStyleValue(value: "left");
        var inlineEnd = new FakeStyleValue(value: "right");

        var sut = new StyleGroup(
            type: type, blockStart: blockStart, blockEnd: blockEnd, inlineStart: inlineStart, inlineEnd: inlineEnd
        );

        // Act
        sut.BuildCss(builder: builder, varPrefix: "x-");
        var result = builder.ToString();

        // Assert
        result.Should().Contain(expected: "x-margin-block-end:bottom");
        result.Should().Contain(expected: "x-margin-block-start:top");
        result.Should().Contain(expected: "x-margin-inline-end:right");
        result.Should().Contain(expected: "x-margin-inline-start:left");
    }

    [Fact]
    public void Constructor_Should_SetAllValues_When_SingleValueProvided()
    {
        // Arrange
        var type = StyleGroupType.Margin;
        var value = new FakeStyleValue(value: "10px");

        // Act
        var sut = new StyleGroup(type: type, value: value);

        // Assert
        sut.BlockStart.Should().Be(expected: value);
        sut.BlockEnd.Should().Be(expected: value);
        sut.InlineStart.Should().Be(expected: value);
        sut.InlineEnd.Should().Be(expected: value);
        sut.Type.Should().Be(expected: type);
        sut.Value.Should().Be(expected: "10px 10px 10px 10px");
    }

    [Fact]
    public void Constructor_Should_SetBlockAndInline_When_TwoValuesProvided()
    {
        // Arrange
        var type = StyleGroupType.Padding;
        var block = new FakeStyleValue(value: "1rem");
        var inline = new FakeStyleValue(value: "2rem");

        // Act
        var sut = new StyleGroup(type: type, block: block, inline: inline);

        // Assert
        sut.BlockStart.Should().Be(expected: block);
        sut.BlockEnd.Should().Be(expected: block);
        sut.InlineStart.Should().Be(expected: inline);
        sut.InlineEnd.Should().Be(expected: inline);
        sut.Type.Should().Be(expected: type);
        sut.Value.Should().Be(expected: "1rem 1rem 2rem 2rem");
    }

    [Fact]
    public void Constructor_Should_SetEachSide_When_AllFourValuesProvided()
    {
        // Arrange
        var type = StyleGroupType.Margin;
        var blockStart = new FakeStyleValue(value: "top");
        var blockEnd = new FakeStyleValue(value: "bottom");
        var inlineStart = new FakeStyleValue(value: "left");
        var inlineEnd = new FakeStyleValue(value: "right");

        // Act
        var sut = new StyleGroup(
            type: type, blockStart: blockStart, blockEnd: blockEnd, inlineStart: inlineStart, inlineEnd: inlineEnd
        );

        // Assert
        sut.BlockStart.Should().Be(expected: blockStart);
        sut.BlockEnd.Should().Be(expected: blockEnd);
        sut.InlineStart.Should().Be(expected: inlineStart);
        sut.InlineEnd.Should().Be(expected: inlineEnd);
        sut.Type.Should().Be(expected: type);
        sut.Value.Should().Be(expected: "bottom top right left");
    }

    [Fact]
    public void ToCss_Should_HandleNullPrefix_When_Called()
    {
        // Arrange
        var type = StyleGroupType.Margin;
        var sut = new StyleGroup(type: type, value: new FakeStyleValue(value: "1em"));

        // Act
        var css = sut.ToCss(varPrefix: null);

        // Assert
        css.Should().Contain(expected: "margin-block-end:1em");
        css.Should().Contain(expected: "margin-inline-start:1em");
    }

    [Fact]
    public void ToCss_Should_ReturnSerializedCss_When_Called()
    {
        // Arrange
        var type = StyleGroupType.Padding;

        var sut = new StyleGroup(
            type: type,
            blockStart: new FakeStyleValue(value: "10px"),
            blockEnd: new FakeStyleValue(value: "20px"),
            inlineStart: new FakeStyleValue(value: "30px"),
            inlineEnd: new FakeStyleValue(value: "40px")
        );

        // Act
        var css = sut.ToCss(varPrefix: "prefix-");

        // Assert
        css.Should().Contain(expected: "prefix-padding-block-end:20px");
        css.Should().Contain(expected: "prefix-padding-block-start:10px");
        css.Should().Contain(expected: "prefix-padding-inline-end:40px");
        css.Should().Contain(expected: "prefix-padding-inline-start:30px");
    }

    private sealed class FakeStyleValue(string value) : IStyleValue
    {
        public string Value => value;
    }
}
