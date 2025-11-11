namespace Allyaria.Theming.UnitTests.StyleTypes;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StyleJustifyTests
{
    [Theory]
    [InlineData(StyleJustify.Kind.Center, "center")]
    [InlineData(StyleJustify.Kind.End, "end")]
    [InlineData(StyleJustify.Kind.FlexEnd, "flex-end")]
    [InlineData(StyleJustify.Kind.FlexStart, "flex-start")]
    [InlineData(StyleJustify.Kind.Normal, "normal")]
    [InlineData(StyleJustify.Kind.SafeCenter, "safe center")]
    [InlineData(StyleJustify.Kind.SpaceAround, "space-around")]
    [InlineData(StyleJustify.Kind.SpaceBetween, "space-between")]
    [InlineData(StyleJustify.Kind.SpaceEvenly, "space-evenly")]
    [InlineData(StyleJustify.Kind.Start, "start")]
    [InlineData(StyleJustify.Kind.Stretch, "stretch")]
    [InlineData(StyleJustify.Kind.UnsafeCenter, "unsafe center")]
    public void Constructor_Should_SetValueToDescription_When_KindProvided(StyleJustify.Kind kind, string expected)
    {
        // Arrange & Act
        var sut = new StyleJustify(kind: kind);

        // Assert
        sut.Value.Should().Be(expected: expected);
    }

    [Fact]
    public void ImplicitConversionFromString_Should_CreateValidInstance_When_ValueIsRecognized()
    {
        // Arrange
        const string input = "stretch";

        // Act
        StyleJustify sut = input;

        // Assert
        sut.Value.Should().Be(expected: "stretch");
    }

    [Fact]
    public void ImplicitConversionFromString_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "not-a-valid";

        // Act
        var act = () =>
        {
            // ReSharper disable once UnusedVariable
            StyleJustify sut = invalid;
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: not-a-valid");
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnEmptyString_When_InstanceIsNull()
    {
        // Arrange
        StyleJustify? sut = null;

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: string.Empty);
    }

    [Fact]
    public void ImplicitConversionToString_Should_ReturnUnderlyingValue_When_InstanceIsNotNull()
    {
        // Arrange
        var sut = new StyleJustify(kind: StyleJustify.Kind.SafeCenter);

        // Act
        string result = sut;

        // Assert
        result.Should().Be(expected: "safe center");
    }

    [Theory]
    [InlineData("center", StyleJustify.Kind.Center)]
    [InlineData("end", StyleJustify.Kind.End)]
    [InlineData("flex-end", StyleJustify.Kind.FlexEnd)]
    [InlineData("flex-start", StyleJustify.Kind.FlexStart)]
    [InlineData("normal", StyleJustify.Kind.Normal)]
    [InlineData("safe center", StyleJustify.Kind.SafeCenter)]
    [InlineData("space-around", StyleJustify.Kind.SpaceAround)]
    [InlineData("space-between", StyleJustify.Kind.SpaceBetween)]
    [InlineData("space-evenly", StyleJustify.Kind.SpaceEvenly)]
    [InlineData("start", StyleJustify.Kind.Start)]
    [InlineData("stretch", StyleJustify.Kind.Stretch)]
    [InlineData("unsafe center", StyleJustify.Kind.UnsafeCenter)]
    public void Parse_Should_ReturnExpectedInstance_When_ValueIsValid(string input, StyleJustify.Kind expectedKind)
    {
        // Arrange & Act
        var sut = StyleJustify.Parse(value: input);

        // Assert
        sut.Value.Should().Be(expected: input);
        sut.Should().Be(expected: new StyleJustify(kind: expectedKind));
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "invalid-justify";

        // Act
        var act = () => StyleJustify.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: invalid-justify");
    }

    [Fact]
    public void Parse_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? invalid = null;

        // Act
        var act = () => StyleJustify.Parse(value: invalid);

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "Invalid style: ");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_ValueIsInvalid()
    {
        // Arrange
        const string invalid = "weird";

        // Act
        var success = StyleJustify.TryParse(value: invalid, result: out var sut);

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
        var success = StyleJustify.TryParse(value: invalid, result: out var sut);

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
        var success = StyleJustify.TryParse(value: input, result: out var sut);

        // Assert
        success.Should().BeTrue();
        sut.Should().NotBeNull();
        sut.Value.Should().Be(expected: "center");
    }
}
