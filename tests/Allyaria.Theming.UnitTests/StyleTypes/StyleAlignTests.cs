namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleAlignTests
{
    [Theory]
    [InlineData(StyleAlign.Kind.Baseline, "baseline")]
    [InlineData(StyleAlign.Kind.Center, "center")]
    [InlineData(StyleAlign.Kind.End, "end")]
    [InlineData(StyleAlign.Kind.FirstBaseline, "first baseline")]
    [InlineData(StyleAlign.Kind.FlexEnd, "flex-end")]
    [InlineData(StyleAlign.Kind.FlexStart, "flex-start")]
    [InlineData(StyleAlign.Kind.LastBaseline, "last baseline")]
    [InlineData(StyleAlign.Kind.Normal, "normal")]
    [InlineData(StyleAlign.Kind.SafeCenter, "safe center")]
    [InlineData(StyleAlign.Kind.SpaceAround, "space-around")]
    [InlineData(StyleAlign.Kind.SpaceBetween, "space-between")]
    [InlineData(StyleAlign.Kind.SpaceEvenly, "space-evenly")]
    [InlineData(StyleAlign.Kind.Start, "start")]
    [InlineData(StyleAlign.Kind.Stretch, "stretch")]
    [InlineData(StyleAlign.Kind.UnsafeCenter, "unsafe center")]
    public void Constructor_Should_SetValueToKindDescription_When_KindProvided(StyleAlign.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleAlign(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ReturnStyleAlign_When_ValueIsValid()
    {
        // Arrange
        const string input = "start";

        // Act
        StyleAlign sut = input;

        // Assert
        sut.Value.Should().Be(expected: "start");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-value";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleAlign sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-value");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_StyleAlignIsNull()
    {
        // Arrange
        StyleAlign? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_StyleAlignIsNotNull()
    {
        // Arrange
        var sut = new StyleAlign(kind: StyleAlign.Kind.SpaceBetween);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "space-between");
    }

    [Theory]
    [InlineData("baseline", StyleAlign.Kind.Baseline)]
    [InlineData("center", StyleAlign.Kind.Center)]
    [InlineData("first baseline", StyleAlign.Kind.FirstBaseline)]
    [InlineData("flex-end", StyleAlign.Kind.FlexEnd)]
    [InlineData("safe center", StyleAlign.Kind.SafeCenter)]
    [InlineData("unsafe center", StyleAlign.Kind.UnsafeCenter)]
    public void Parse_Should_ReturnStyleAlign_When_ValueMatchesKnownKind(string input, StyleAlign.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleAlign.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        var expected = new StyleAlign(kind: expectedKind);
        sut.Should().Be(expected: expected);
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-valid-align";

        // Act
        var act = () => StyleAlign.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: not-a-valid-align");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleAlign.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-align";

        // Act
        var success = StyleAlign.TryParse(value: invalid, result: out var sut);

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
        var success = StyleAlign.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string input = "center";

        // Act
        var success = StyleAlign.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "center");
    }
}
