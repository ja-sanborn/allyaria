using System.ComponentModel;

namespace Allyaria.Abstractions.UnitTests.Extensions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class EnumExtensionsTests
{
    private enum DescribedEnumA
    {
        [Description(description: "Alpha A")]
        X = 0,

        // Whitespace DescriptionAttribute should be ignored and fallback used.
        [Description(description: "   ")]
        Z = 1
    }

    private enum DescribedEnumB
    {
        [Description(description: "Bravo B")]
        X = 0
    }

    private enum MixedDescriptionEnum
    {
        [Description(description: "Has Description")]

        // ReSharper disable once UnusedMember.Local
        WithDescription = 0,

        // Single-letter to keep fallback deterministic across humanizers.
        Y = 1
    }

    [Flags]
    private enum TestFlags
    {
        [Description(description: "Read Only")]
        Read = 1,

        // No Description: fallback should be the humanized name; we pick a single-letter to avoid depending on implementation details.
        W = 2

        // Intentionally DO NOT define a combined member so ToString() yields "Read, W".
        // ReadWrite = Read | W
    }

    [Fact]
    public void GetDescription_Should_CachePerType_When_SameMemberNameExistsAcrossDifferentEnums()
    {
        // Arrange
        var a = (Enum)DescribedEnumA.X; // "Alpha A"
        var b = (Enum)DescribedEnumB.X; // "Bravo B"

        // Act
        var aDescription = a.GetDescription();
        var bDescription = b.GetDescription();

        // Assert
        aDescription.Should().Be(expected: "Alpha A");
        bDescription.Should().Be(expected: "Bravo B");
    }

    [Fact]
    public void GetDescription_Should_Fallback_ToHumanizedName_When_Description_IsMissingOrWhitespace()
    {
        // Arrange
        var missingDescription = (Enum)MixedDescriptionEnum.Y; // no Description
        var whitespaceDescription = (Enum)DescribedEnumA.Z; // [Description("   ")]

        // Act
        var missingResult = missingDescription.GetDescription();
        var whitespaceResult = whitespaceDescription.GetDescription();

        // Assert
        // We deliberately use single-letter enum names so that a humanizer will return the same text.
        missingResult.Should().Be(expected: "Y");
        whitespaceResult.Should().Be(expected: "Z");
    }

    [Fact]
    public void GetDescription_Should_Handle_MixedDescriptionAndFallback_InFlagsCombination()
    {
        // Arrange
        var described = TestFlags.Read; // "Read Only"
        var fallback = TestFlags.W; // "W"
        var sut = (Enum)(described | fallback);

        // Act
        var result = sut.GetDescription();

        // Assert
        result.Should().Be(expected: "Read Only, W");
    }

    [Fact]
    public void GetDescription_Should_Handle_SingleValueFlagsWithoutComma_When_NoCombination()
    {
        // Arrange
        var sut = (Enum)TestFlags.W;

        // Act
        var result = sut.GetDescription();

        // Assert
        result.Should().Be(expected: "W");
    }

    [Fact]
    public void GetDescription_Should_JoinDescriptionsWithCommaSpace_When_FlagsCombination()
    {
        // Arrange
        var sut = (Enum)(TestFlags.Read | TestFlags.W); // ToString() => "Read, W"

        // Act
        var result = sut.GetDescription();

        // Assert
        result.Should().Be(expected: "Read Only, W");
    }

    [Fact]
    public void GetDescription_Should_Return_DescriptionAttribute_When_Present()
    {
        // Arrange
        var sut = DescribedEnumA.X; // [Description("Alpha A")]

        // Act
        var result = ((Enum)sut).GetDescription();

        // Assert
        result.Should().Be(expected: "Alpha A");
    }

    [Fact]
    public void GetDescription_Should_ThrowAllyariaArgumentException_When_ValueIsNull()
    {
        // Arrange + Act
        var act = () =>
        {
            Enum value = null!;

            return value.GetDescription();
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .And.Should().BeOfType<AryArgumentException>();
    }

    [Fact]
    public void GetDescriptionTEnum_Should_ReturnSameAsNonGeneric_When_UsingStronglyTypedOverload()
    {
        // Arrange
        var sut = TestFlags.Read;

        // Act
        var genericResult = sut.GetDescription(); // generic overload
        var nongenericResult = ((Enum)sut).GetDescription(); // non-generic overload

        // Assert
        genericResult.Should().Be(expected: nongenericResult);
        genericResult.Should().Be(expected: "Read Only");
    }
}
