namespace Allyaria.Abstractions.UnitTests.Extensions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryValidationExtensionsTests
{
    [Fact]
    public void Between_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 0); // out of range for (1, 10)

        // Act
        var result = sut.Between(min: 1, max: 10);

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    private static AryValidation<T> CreateValidation<T>(T value) => new(argValue: value, argName: "value");

    [Fact]
    public void EnumDefined_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var invalidEnum = (DayOfWeek)999;
        var sut = CreateValidation(value: invalidEnum);

        // Act
        var result = sut.EnumDefined();

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void EqualTo_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 10);

        // Act
        var result = sut.EqualTo(compare: 5); // not equal -> error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void False_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 42);

        // Act
        var result = sut.False(condition: true); // true => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void GreaterThan_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 5);

        // Act
        var result = sut.GreaterThan(minExclusive: 5); // 5 <= 5 => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void GreaterThanOrEqualTo_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 3);

        // Act
        var result = sut.GreaterThanOrEqualTo(minInclusive: 5); // 3 < 5 => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void InRange_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 0);

        // Act
        var result = sut.InRange(min: 1, max: 10); // 0 < 1 => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void IsAssignableTo_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation<object>(value: 42); // not assignable to string

        // Act
        var result = sut.IsAssignableTo<object, string>();

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void LessThan_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 10);

        // Act
        var result = sut.LessThan(maxExclusive: 10); // 10 >= 10 => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void LessThanOrEqualTo_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 11);

        // Act
        var result = sut.LessThanOrEqualTo(maxInclusive: 10); // 11 > 10 => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void NotDefault_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 0); // default(int) => error

        // Act
        var result = sut.NotDefault();

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void NotEqualTo_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 1);

        // Act
        var result = sut.NotEqualTo(compare: 1); // equal => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void NotNull_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation<string?>(value: null); // null => error

        // Act
        var result = sut.NotNull();

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void NotNullOrEmpty_Collection_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        IReadOnlyCollection<int>? collection = null; // null => error
        var sut = CreateValidation(value: collection);

        // Act
        var result = sut.NotNullOrEmpty();

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void NotNullOrEmpty_String_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation<string?>(value: null); // null => error

        // Act
        var result = sut.NotNullOrEmpty();

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void NotNullOrWhiteSpace_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation<string?>(value: "   "); // whitespace => error

        // Act
        var result = sut.NotNullOrWhiteSpace();

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void SameTypeAs_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation<object>(value: "abc");

        // Act
        var result = sut.SameTypeAs(other: 123); // string vs int => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void True_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 123);

        // Act
        var result = sut.True(condition: false); // false => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }

    [Fact]
    public void When_Should_AddErrorAndReturnSameInstance()
    {
        // Arrange
        var sut = CreateValidation(value: 123);

        // Act
        var result = sut.Check(condition: false, message: "custom error"); // false => error

        // Assert
        result.Should().BeSameAs(expected: sut);
        sut.Errors.Should().HaveCount(expected: 1);
    }
}
