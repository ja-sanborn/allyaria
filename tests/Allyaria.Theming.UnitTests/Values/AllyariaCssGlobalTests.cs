using Allyaria.Theming.Values;

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaCssGlobalTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("initial-value")]
    [InlineData("inheritance")]
    [InlineData("revertlayer")]
    public void Ctor_Invalid_Input_Yields_Empty_Value(string input)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssGlobal(input);
        string value = sut;

        // Assert
        value.Should()
            .BeEmpty();
    }

    [Theory]
    [InlineData("inherit", "inherit")]
    [InlineData("INITIAL", "initial")]
    [InlineData("UnSeT", "unset")]
    [InlineData("revert", "revert")]
    [InlineData("  ReVeRt-LaYeR  ", "revert-layer")]
    public void Ctor_Normalizes_Valid_Keywords_To_Lowercase(string input, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaCssGlobal(input);
        string value = sut;

        // Assert
        value.Should()
            .Be(expected);
    }

    [Fact]
    public void Ctor_Null_Input_Yields_Empty_Value()
    {
        // Arrange
        string? raw = null;

        // Act
        var sut = new AllyariaCssGlobal(raw!);
        string value = sut;

        // Assert
        value.Should()
            .BeEmpty();
    }

    [Fact]
    public void Implicit_FromString_Parses_And_Normalizes()
    {
        // Arrange
        AllyariaCssGlobal sut = "  ReVeRt-LaYeR  ";

        // Act
        string value = sut;

        // Assert
        value.Should()
            .Be("revert-layer");
    }

    [Fact]
    public void Implicit_ToString_On_Null_Instance_ReturnsEmpty()
    {
        // Arrange
        AllyariaCssGlobal? sut = null;

        // Act
        string value = sut;

        // Assert
        value.Should()
            .BeEmpty();
    }

    [Fact]
    public void RoundTrip_String_Conversion_Works_For_Valid_Value()
    {
        // Arrange
        AllyariaCssGlobal sut = "unset";

        // Act
        string back = sut;
        AllyariaCssGlobal round = back;

        // Assert
        back.Should()
            .Be("unset");

        ((string)round).Should()
            .Be("unset");
    }

    [Fact]
    public void TryParse_Invalid_Returns_False_And_Empty_Instance()
    {
        // Arrange
        var input = "initial-value";

        // Act
        var ok = AllyariaCssGlobal.TryParse(input, out var sut);
        string value = sut;

        // Assert
        ok.Should()
            .BeFalse();

        value.Should()
            .BeEmpty();
    }

    [Fact]
    public void TryParse_Valid_Returns_True_And_Normalized_Instance()
    {
        // Arrange
        var input = "  INITIAL  ";

        // Act
        var ok = AllyariaCssGlobal.TryParse(input, out var sut);
        string value = sut;

        // Assert
        ok.Should()
            .BeTrue();

        value.Should()
            .Be("initial");
    }
}
