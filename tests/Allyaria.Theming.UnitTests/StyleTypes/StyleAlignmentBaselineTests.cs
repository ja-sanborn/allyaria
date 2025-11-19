namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleAlignmentBaselineTests
{
    [Theory]
    [InlineData(StyleAlignmentBaseline.Kind.Alphabetic, "alphabetic")]
    [InlineData(StyleAlignmentBaseline.Kind.Baseline, "baseline")]
    [InlineData(StyleAlignmentBaseline.Kind.Central, "central")]
    [InlineData(StyleAlignmentBaseline.Kind.Ideographic, "ideographic")]
    [InlineData(StyleAlignmentBaseline.Kind.Mathematical, "mathematical")]
    [InlineData(StyleAlignmentBaseline.Kind.Middle, "middle")]
    [InlineData(StyleAlignmentBaseline.Kind.TextBottom, "text-bottom")]
    [InlineData(StyleAlignmentBaseline.Kind.TextTop, "text-top")]
    public void Constructor_Should_SetValueToKindDescription_When_KindIsProvided(StyleAlignmentBaseline.Kind kind,
        string expected)
    {
        // Arrange & Act
        var sut = new StyleAlignmentBaseline(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ReturnValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "middle";

        // Act
        StyleAlignmentBaseline sut = input;

        // Assert
        sut.Value.Should().Be(expected: "middle");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "bad-baseline";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleAlignmentBaseline sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: bad-baseline");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleAlignmentBaseline? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleAlignmentBaseline(kind: StyleAlignmentBaseline.Kind.TextTop);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "text-top");
    }

    [Theory]
    [InlineData("alphabetic", StyleAlignmentBaseline.Kind.Alphabetic)]
    [InlineData("baseline", StyleAlignmentBaseline.Kind.Baseline)]
    [InlineData("central", StyleAlignmentBaseline.Kind.Central)]
    [InlineData("ideographic", StyleAlignmentBaseline.Kind.Ideographic)]
    [InlineData("mathematical", StyleAlignmentBaseline.Kind.Mathematical)]
    [InlineData("middle", StyleAlignmentBaseline.Kind.Middle)]
    [InlineData("text-bottom", StyleAlignmentBaseline.Kind.TextBottom)]
    [InlineData("text-top", StyleAlignmentBaseline.Kind.TextTop)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input,
        StyleAlignmentBaseline.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleAlignmentBaseline.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleAlignmentBaseline(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-baseline";

        // Act
        var act = () => StyleAlignmentBaseline.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: not-a-baseline");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleAlignmentBaseline.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-baseline";

        // Act
        var success = StyleAlignmentBaseline.TryParse(value: invalid, result: out var sut);

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
        var success = StyleAlignmentBaseline.TryParse(value: invalid, result: out var sut);

        // Assert
        success.Should().BeFalse();
        sut.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_ValueIsValid()
    {
        // Arrange
        const string valid = "alphabetic";

        // Act
        var success = StyleAlignmentBaseline.TryParse(value: valid, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: valid);
    }
}
