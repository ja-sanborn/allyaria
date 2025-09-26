using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaGlobalValueTests
{
    [Theory]
    [InlineData("inherit", "inherit")]
    [InlineData("INHERIT", "inherit")]
    [InlineData(" Initial ", "initial")]
    [InlineData("unset", "unset")]
    [InlineData("Revert", "revert")]
    [InlineData("revert-layer", "revert-layer")]
    public void Ctor_Should_NormalizeValue_When_InputIsValid(string input, string expected)
    {
        // Arrange & Act
        var sut = new AllyariaGlobalValue(input);

        // Assert
        sut.Value.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalid")]
    [InlineData("none")]
    public void Ctor_Should_ThrowArgumentException_When_InputIsInvalid(string? input)
    {
        // Arrange
        var act = () => new AllyariaGlobalValue(input!);

        // Act & Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Fact]
    public void ImplicitOperatorFromString_Should_CreateInstance()
    {
        // Arrange
        AllyariaGlobalValue sut = "unset";

        // Act & Assert
        sut.Value.Should()
            .Be("unset");
    }

    [Fact]
    public void ImplicitOperatorToString_Should_ReturnUnderlyingValue()
    {
        // Arrange
        var sut = new AllyariaGlobalValue("revert-layer");

        // Act
        string result = sut;

        // Assert
        result.Should()
            .Be("revert-layer");
    }

    [Fact]
    public void Parse_Should_ReturnInstance_When_InputIsValid()
    {
        // Arrange & Act
        var result = AllyariaGlobalValue.Parse("inherit");

        // Assert
        result.Value.Should()
            .Be("inherit");
    }

    [Fact]
    public void Parse_Should_ThrowArgumentException_When_InputIsInvalid()
    {
        // Arrange
        var act = () => AllyariaGlobalValue.Parse("not-a-valid");

        // Act & Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_InputIsInvalid()
    {
        // Arrange & Act
        var success = AllyariaGlobalValue.TryParse("bad-value", out var result);

        // Assert
        success.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndInstance_When_InputIsValid()
    {
        // Arrange & Act
        var success = AllyariaGlobalValue.TryParse("initial", out var result);

        // Assert
        success.Should()
            .BeTrue();

        result!.Value.Should()
            .Be("initial");
    }
}
