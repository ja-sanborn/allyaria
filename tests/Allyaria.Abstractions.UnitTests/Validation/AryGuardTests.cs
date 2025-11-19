namespace Allyaria.Abstractions.UnitTests.Validation;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryGuardTests
{
    private enum TestEnum
    {
        First = 0,

        // ReSharper disable once UnusedMember.Local
        Second = 1
    }

    [Fact]
    public void Between_Should_NotThrow_When_ValueWithinExclusiveRange()
    {
        // Arrange
        var value = 5;

        // Act
        var act = () => AryGuard.Between(value: value, min: 0, max: 10);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Between_Should_ThrowAryArgumentException_When_ValueIsAtMinimumBoundary()
    {
        // Arrange
        var value = 0;

        // Act
        var act = () => AryGuard.Between(value: value, min: 0, max: 10);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must be between 0 and 10 (exclusive).");
    }

    [Fact]
    public void EnumDefined_Should_NotThrow_When_ValueIsDefined()
    {
        // Arrange
        var value = TestEnum.First;

        // Act
        var act = () => AryGuard.EnumDefined(value: value);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void EnumDefined_Should_ThrowAryArgumentException_When_ValueIsNotDefined()
    {
        // Arrange
        var value = (TestEnum)42;

        // Act
        var act = () => AryGuard.EnumDefined(value: value);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value is not a valid value for this enum.");
    }

    [Fact]
    public void EqualTo_Should_NotThrow_When_ValuesAreEqual()
    {
        // Arrange
        var value = 10;

        // Act
        var act = () => AryGuard.EqualTo(value: value, compare: 10);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void EqualTo_Should_ThrowAryArgumentException_When_ValuesAreNotEqual()
    {
        // Arrange
        var value = 10;

        // Act
        var act = () => AryGuard.EqualTo(value: value, compare: 5);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must be equal to 5.");
    }

    [Fact]
    public void False_Should_NotThrow_When_ConditionIsFalse()
    {
        // Arrange
        const bool condition = false;

        // Act
        var act = () => AryGuard.False(condition: condition);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void False_Should_ThrowAryArgumentException_When_ConditionIsTrue()
    {
        // Arrange
        const bool condition = true;

        // Act
        var act = () => AryGuard.False(condition: condition);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "condition");
        ex.Message.Should().Be(expected: "condition must be false.");
    }

    [Fact]
    public void For_Should_ReturnValidationContext_When_Called()
    {
        // Arrange
        const int value = 123;

        // Act
        var result = AryGuard.For(value: value);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AryValidation<int>>();
    }

    [Fact]
    public void GreaterThan_Should_NotThrow_When_ValueIsGreaterThanMinimum()
    {
        // Arrange
        var value = 10;

        // Act
        var act = () => AryGuard.GreaterThan(value: value, minExclusive: 5);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void GreaterThan_Should_ThrowAryArgumentException_When_ValueIsLessThanOrEqualToMinimum()
    {
        // Arrange
        var value = 5;

        // Act
        var act = () => AryGuard.GreaterThan(value: value, minExclusive: 5);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must be greater than 5.");
    }

    [Fact]
    public void GreaterThanOrEqualTo_Should_NotThrow_When_ValueIsEqualToMinimum()
    {
        // Arrange
        var value = 5;

        // Act
        var act = () => AryGuard.GreaterThanOrEqualTo(value: value, minInclusive: 5);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void GreaterThanOrEqualTo_Should_ThrowAryArgumentException_When_ValueIsLessThanMinimum()
    {
        // Arrange
        var value = 4;

        // Act
        var act = () => AryGuard.GreaterThanOrEqualTo(value: value, minInclusive: 5);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must be greater than or equal to 5.");
    }

    [Fact]
    public void InRange_Should_NotThrow_When_ValueWithinInclusiveRange()
    {
        // Arrange
        var value = 5;

        // Act
        var act = () => AryGuard.InRange(value: value, min: 1, max: 10);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void InRange_Should_ThrowAryArgumentException_When_ValueIsBelowMinimum()
    {
        // Arrange
        var value = 0;

        // Act
        var act = () => AryGuard.InRange(value: value, min: 1, max: 10);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must be between 1 and 10 (inclusive).");
    }

    [Fact]
    public void IsAssignableTo_Should_NotThrow_When_ValueIsAssignableToTargetType()
    {
        // Arrange
        object value = "text";

        // Act
        var act = () => AryGuard.IsAssignableTo<string>(value: value);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void IsAssignableTo_Should_ThrowAryArgumentException_When_ValueIsNotAssignableToTargetType()
    {
        // Arrange
        object value = 42;

        // Act
        var act = () => AryGuard.IsAssignableTo<string>(value: value);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: $"value must be assignable to {typeof(string).FullName}.");
    }

    [Fact]
    public void LessThan_Should_NotThrow_When_ValueIsLessThanMaximum()
    {
        // Arrange
        var value = 5;

        // Act
        var act = () => AryGuard.LessThan(value: value, maxExclusive: 10);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void LessThan_Should_ThrowAryArgumentException_When_ValueIsGreaterThanOrEqualToMaximum()
    {
        // Arrange
        var value = 10;

        // Act
        var act = () => AryGuard.LessThan(value: value, maxExclusive: 10);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must be less than 10.");
    }

    [Fact]
    public void LessThanOrEqualTo_Should_NotThrow_When_ValueIsEqualToMaximum()
    {
        // Arrange
        var value = 10;

        // Act
        var act = () => AryGuard.LessThanOrEqualTo(value: value, maxInclusive: 10);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void LessThanOrEqualTo_Should_ThrowAryArgumentException_When_ValueIsGreaterThanMaximum()
    {
        // Arrange
        var value = 11;

        // Act
        var act = () => AryGuard.LessThanOrEqualTo(value: value, maxInclusive: 10);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must be less than or equal to 10.");
    }

    [Fact]
    public void NotDefault_Should_NotThrow_When_ValueIsNotDefault()
    {
        // Arrange
        var value = 1;

        // Act
        var act = () => AryGuard.NotDefault(value: value);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void NotDefault_Should_ThrowAryArgumentException_When_ValueIsDefault()
    {
        // Arrange
        var value = 0;

        // Act
        var act = () => AryGuard.NotDefault(value: value);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.Message.Should().Be(expected: "value cannot be the default value.");
    }

    [Fact]
    public void NotEqualTo_Should_NotThrow_When_ValuesAreNotEqual()
    {
        // Arrange
        var value = 1;

        // Act
        var act = () => AryGuard.NotEqualTo(value: value, compare: 2);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void NotEqualTo_Should_ThrowAryArgumentException_When_ValuesAreEqual()
    {
        // Arrange
        var value = 3;

        // Act
        var act = () => AryGuard.NotEqualTo(value: value, compare: 3);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.ArgValue.Should().Be(expected: value);
        ex.Message.Should().Be(expected: "value must not be equal to 3.");
    }

    [Fact]
    public void NotNull_Should_NotThrow_When_ValueIsNotNull()
    {
        // Arrange
        var value = new object();

        // Act
        var act = () => AryGuard.NotNull(value: value);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void NotNull_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        object? value = null;

        // Act
        var act = () => AryGuard.NotNull(value: value);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.Message.Should().Be(expected: "value cannot be null.");
    }

    [Fact]
    public void NotNullOrEmpty_Collection_Should_NotThrow_When_CollectionHasItems()
    {
        // Arrange
        IReadOnlyCollection<int> collection = new List<int>
        {
            1
        };

        // Act
        var act = () => AryGuard.NotNullOrEmpty(collection: collection);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void NotNullOrEmpty_Collection_Should_ThrowAryArgumentException_When_CollectionIsNull()
    {
        // Arrange
        IReadOnlyCollection<int>? collection = null;

        // Act
        var act = () => AryGuard.NotNullOrEmpty(collection: collection);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "collection");
        ex.Message.Should().Be(expected: "collection cannot be null or an empty collection.");
    }

    [Fact]
    public void NotNullOrEmpty_String_Should_NotThrow_When_ValueIsNonEmpty()
    {
        // Arrange
        const string value = "abc";

        // Act
        var act = () => AryGuard.NotNullOrEmpty(value: value);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void NotNullOrEmpty_String_Should_ThrowAryArgumentException_When_ValueIsNull()
    {
        // Arrange
        string? value = null;

        // Act
        var act = () => AryGuard.NotNullOrEmpty(value: value);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.Message.Should().Be(expected: "value cannot be null or empty.");
    }

    [Fact]
    public void NotNullOrWhiteSpace_Should_NotThrow_When_ValueHasNonWhitespaceCharacters()
    {
        // Arrange
        const string value = "abc";

        // Act
        var act = () => AryGuard.NotNullOrWhiteSpace(value: value);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void NotNullOrWhiteSpace_Should_ThrowAryArgumentException_When_ValueIsWhitespace()
    {
        // Arrange
        const string value = "   ";

        // Act
        var act = () => AryGuard.NotNullOrWhiteSpace(value: value);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "value");
        ex.Message.Should().Be(expected: "value cannot be null, empty, or whitespace.");
    }

    [Fact]
    public void SameType_Should_NotThrow_When_TypesAreTheSame()
    {
        // Arrange
        var first = 1;
        var second = 2;

        // Act
        var act = () => AryGuard.SameType(value1: first, value2: second);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SameType_Should_ThrowAryArgumentException_When_FirstValueIsNull()
    {
        // Arrange
        object? first = null;
        var second = 1.0;

        // Act
        var act = () => AryGuard.SameType(value1: first, value2: second);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "first");
        ex.ArgValue.Should().Be(expected: second);
        ex.Message.Should().Be(expected: "first cannot be compared because one or both values are null.");
    }

    [Fact]
    public void SameType_Should_ThrowAryArgumentException_When_SecondValueIsNull()
    {
        // Arrange
        var first = 1.0;
        object? second = null;

        // Act
        var act = () => AryGuard.SameType(value1: first, value2: second);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "first");
        ex.ArgValue.Should().Be(expected: first);
        ex.Message.Should().Be(expected: "first cannot be compared because one or both values are null.");
    }

    [Fact]
    public void SameType_Should_ThrowAryArgumentException_When_TypesDiffer()
    {
        // Arrange
        var first = 1;
        var second = 1.0;

        // Act
        var act = () => AryGuard.SameType(value1: first, value2: second);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "first");
        ex.ArgValue.Should().Be(expected: first);

        ex.Message.Should().Be(
            expected:
            $"first type mismatch: {typeof(int).FullName} cannot be compared with {typeof(double).FullName}."
        );
    }

    [Fact]
    public void True_Should_NotThrow_When_ConditionIsTrue()
    {
        // Arrange
        const bool condition = true;

        // Act
        var act = () => AryGuard.True(condition: condition);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void True_Should_ThrowAryArgumentException_When_ConditionIsFalse()
    {
        // Arrange
        const bool condition = false;

        // Act
        var act = () => AryGuard.True(condition: condition);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: "condition");
        ex.Message.Should().Be(expected: "condition must be true.");
    }

    [Fact]
    public void When_Should_NotThrow_When_ConditionIsTrue()
    {
        // Arrange
        const bool condition = true;

        // Act
        var act = () => AryGuard.Check(condition: condition, argName: "myParam", message: "should not fail");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_Should_ThrowAryArgumentException_When_ConditionIsFalse()
    {
        // Arrange
        const bool condition = false;
        const string argName = "myParam";
        const string message = "custom message";

        // Act
        var act = () => AryGuard.Check(condition: condition, argName: argName, message: message);

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
        ex.ArgName.Should().Be(expected: argName);
        ex.Message.Should().Be(expected: message);
    }
}
