namespace Allyaria.Abstractions.UnitTests.Validation;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryValidationTests
{
    [Fact]
    public void Add_Should_AddError_When_ExceptionIsNotNull()
    {
        // Arrange
        var sut = new AryValidation<int>(argValue: 1, argName: "value");
        var error = new AryArgumentException(message: "Bad arg", argName: "value", argValue: 1);

        // Act
        sut.Add(ex: error);

        // Assert
        sut.Errors.Should().ContainSingle()
            .Which.Should().BeSameAs(expected: error);

        sut.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Add_Should_NotAddError_When_ExceptionIsNull()
    {
        // Arrange
        var sut = new AryValidation<int>(argValue: 1, argName: "value");

        // Act
        sut.Add(ex: null);

        // Assert
        sut.Errors.Should().BeEmpty();
        sut.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Ctor_Should_SetProperties_When_Initialized()
    {
        // Arrange
        var value = 42;
        var name = "testParam";

        // Act
        var sut = new AryValidation<int>(argValue: value, argName: name);

        // Assert
        sut.ArgValue.Should().Be(expected: value);
        sut.ArgName.Should().Be(expected: name);
        sut.Errors.Should().BeEmpty();
        sut.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Errors_Should_ExposeReadOnlyList_When_Queried()
    {
        // Arrange
        var sut = new AryValidation<string>(argValue: "data", argName: "input");
        var error = new AryArgumentException(message: "invalid", argName: "input", argValue: "data");

        // Act
        sut.Add(ex: error);
        var errors = sut.Errors;

        // Assert
        errors.Should().BeOfType<List<AryArgumentException>>();
        errors.Should().ContainSingle().Which.Should().Be(expected: error);
    }

    [Fact]
    public void ThrowIfInvalid_Should_NotThrow_When_NoErrorsPresent()
    {
        // Arrange
        var sut = new AryValidation<int>(argValue: 1, argName: "param");

        // Act
        var act = () => sut.ThrowIfInvalid();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfInvalid_Should_ThrowCombinedAryArgumentException_When_ErrorsExist()
    {
        // Arrange
        var sut = new AryValidation<int>(argValue: 99, argName: "num");
        var error1 = new AryArgumentException(message: "error one", argName: "num", argValue: 99);
        var error2 = new AryArgumentException(message: "error two", argName: "num", argValue: 99);
        sut.Add(ex: error1);
        sut.Add(ex: error2);

        // Act
        var act = () => sut.ThrowIfInvalid();

        // Assert
        var ex = act.Should().Throw<AryArgumentException>().Which;
        ex.ArgName.Should().Be(expected: "num");
        ex.ArgValue.Should().Be(expected: 99);
        ex.Message.Should().Contain(expected: "error one");
        ex.Message.Should().Contain(expected: "error two");
        ex.Message.Should().Contain(expected: Environment.NewLine);
    }
}
