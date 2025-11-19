namespace Allyaria.Abstractions.UnitTests.Exceptions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryArgumentExceptionTests
{
    [Fact]
    public void Ctor_Default_Should_CreateInstanceWithDefaults()
    {
        // Act
        var sut = new AryArgumentException();

        // Assert
        sut.ArgName.Should().BeNull();
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithEmptyErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsEmpty()
    {
        // Arrange
        var message = "Argument issue";
        var errorCode = string.Empty;

        // Act
        var sut = new AryArgumentException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
    }

    [Fact]
    public void Ctor_WithErrorCode_Should_SetErrorCode_When_Provided()
    {
        // Arrange
        var message = "Argument issue";
        var errorCode = "ARY.CUSTOM_ARGUMENT";

        // Act
        var sut = new AryArgumentException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: errorCode);
    }

    [Fact]
    public void Ctor_WithMessage_Should_SetMessage()
    {
        // Act
        var sut = new AryArgumentException(message: "msg");

        // Assert
        sut.Message.Should().Be(expected: "msg");
        sut.ArgName.Should().BeNull();
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgName_Should_SetMessageAndArgName()
    {
        // Act
        var sut = new AryArgumentException(message: "msg", argName: "param");

        // Assert
        sut.Message.Should().Be(expected: "msg");
        sut.ArgName.Should().Be(expected: "param");
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgNameAndArgValue_Should_SetAllProperties()
    {
        // Act
        var sut = new AryArgumentException(message: "msg", argName: "param", argValue: 42);

        // Assert
        sut.Message.Should().Be(expected: "msg");
        sut.ArgName.Should().Be(expected: "param");
        sut.ArgValue.Should().Be(expected: 42);
        sut.InnerException.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgNameAndInner_Should_SetAllProperties()
    {
        // Arrange
        var inner = new InvalidOperationException();

        // Act
        var sut = new AryArgumentException(message: "msg", argName: "param", innerException: inner);

        // Assert
        sut.Message.Should().Be(expected: "msg");
        sut.ArgName.Should().Be(expected: "param");
        sut.InnerException.Should().Be(expected: inner);
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgValue_Should_SetMessageAndArgValue()
    {
        // Act
        var sut = new AryArgumentException(message: "msg", argValue: 42);

        // Assert
        sut.Message.Should().Be(expected: "msg");
        sut.ArgValue.Should().Be(expected: 42);
        sut.ArgName.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgValueAndInner_Should_SetAllProperties()
    {
        // Arrange
        var inner = new InvalidOperationException();

        // Act
        var sut = new AryArgumentException(message: "msg", argValue: 42, innerException: inner);

        // Assert
        sut.Message.Should().Be(expected: "msg");
        sut.ArgValue.Should().Be(expected: 42);
        sut.InnerException.Should().Be(expected: inner);
        sut.ArgName.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndInner_Should_SetMessageAndInner()
    {
        // Arrange
        var inner = new InvalidOperationException();

        // Act
        var sut = new AryArgumentException(message: "msg", innerException: inner);

        // Assert
        sut.Message.Should().Be(expected: "msg");
        sut.InnerException.Should().Be(expected: inner);
        sut.ArgName.Should().BeNull();
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndNameAndValueAndInner_Should_SetAllProperties()
    {
        // Arrange
        var inner = new InvalidOperationException(message: "inner");
        var message = "bad arg";
        var name = "param";
        var value = 123;

        // Act
        var sut = new AryArgumentException(message: message, argName: name, argValue: value, innerException: inner);

        // Assert
        sut.Message.Should().Be(expected: message);
        sut.ArgName.Should().Be(expected: name);
        sut.ArgValue.Should().Be(expected: value);
        sut.InnerException.Should().Be(expected: inner);
    }

    [Fact]
    public void Ctor_WithNullErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsNull()
    {
        // Arrange
        var message = "Argument issue";
        string? errorCode = null;

        // Act
        var sut = new AryArgumentException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
    }

    [Fact]
    public void Ctor_WithWhitespaceErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsWhitespace()
    {
        // Arrange
        var message = "Argument issue";
        var errorCode = "   ";

        // Act
        var sut = new AryArgumentException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.ARGUMENT");
    }
}
