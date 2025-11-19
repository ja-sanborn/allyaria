namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleDisplayTests
{
    [Theory]
    [InlineData(StyleDisplay.Kind.Block, "block")]
    [InlineData(StyleDisplay.Kind.Flex, "flex")]
    [InlineData(StyleDisplay.Kind.FlowRoot, "flow-root")]
    [InlineData(StyleDisplay.Kind.Grid, "grid")]
    [InlineData(StyleDisplay.Kind.Inline, "inline")]
    [InlineData(StyleDisplay.Kind.InlineBlock, "inline-block")]
    [InlineData(StyleDisplay.Kind.InlineFlex, "inline-flex")]
    [InlineData(StyleDisplay.Kind.InlineGrid, "inline-grid")]
    [InlineData(StyleDisplay.Kind.InlineTable, "inline-table")]
    [InlineData(StyleDisplay.Kind.ListItem, "list-item")]
    [InlineData(StyleDisplay.Kind.None, "none")]
    [InlineData(StyleDisplay.Kind.Table, "table")]
    [InlineData(StyleDisplay.Kind.TableCaption, "table-caption")]
    [InlineData(StyleDisplay.Kind.TableCell, "table-cell")]
    [InlineData(StyleDisplay.Kind.TableColumn, "table-column")]
    [InlineData(StyleDisplay.Kind.TableColumnGroup, "table-column-group")]
    [InlineData(StyleDisplay.Kind.TableFooterGroup, "table-footer-group")]
    [InlineData(StyleDisplay.Kind.TableHeaderGroup, "table-header-group")]
    [InlineData(StyleDisplay.Kind.TableRow, "table-row")]
    [InlineData(StyleDisplay.Kind.TableRowGroup, "table-row-group")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleDisplay.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleDisplay(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "inline-flex";

        // Act
        StyleDisplay sut = input;

        // Assert
        sut.Value.Should().Be(expected: "inline-flex");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-display";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleDisplay sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-display");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleDisplay? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleDisplay(kind: StyleDisplay.Kind.TableHeaderGroup);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "table-header-group");
    }

    [Theory]
    [InlineData("block", StyleDisplay.Kind.Block)]
    [InlineData("flex", StyleDisplay.Kind.Flex)]
    [InlineData("flow-root", StyleDisplay.Kind.FlowRoot)]
    [InlineData("grid", StyleDisplay.Kind.Grid)]
    [InlineData("inline", StyleDisplay.Kind.Inline)]
    [InlineData("inline-block", StyleDisplay.Kind.InlineBlock)]
    [InlineData("inline-flex", StyleDisplay.Kind.InlineFlex)]
    [InlineData("inline-grid", StyleDisplay.Kind.InlineGrid)]
    [InlineData("inline-table", StyleDisplay.Kind.InlineTable)]
    [InlineData("list-item", StyleDisplay.Kind.ListItem)]
    [InlineData("none", StyleDisplay.Kind.None)]
    [InlineData("table", StyleDisplay.Kind.Table)]
    [InlineData("table-caption", StyleDisplay.Kind.TableCaption)]
    [InlineData("table-cell", StyleDisplay.Kind.TableCell)]
    [InlineData("table-column", StyleDisplay.Kind.TableColumn)]
    [InlineData("table-column-group", StyleDisplay.Kind.TableColumnGroup)]
    [InlineData("table-footer-group", StyleDisplay.Kind.TableFooterGroup)]
    [InlineData("table-header-group", StyleDisplay.Kind.TableHeaderGroup)]
    [InlineData("table-row", StyleDisplay.Kind.TableRow)]
    [InlineData("table-row-group", StyleDisplay.Kind.TableRowGroup)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleDisplay.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleDisplay.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleDisplay(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "weird-display";

        // Act
        var act = () => StyleDisplay.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: weird-display");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleDisplay.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "unknown-display";

        // Act
        var success = StyleDisplay.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var success = StyleDisplay.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string valid = "flex";

        // Act
        var success = StyleDisplay.TryParse(value: valid, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "flex");
    }
}
