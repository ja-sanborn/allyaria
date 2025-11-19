namespace Allyaria.Abstractions.UnitTests.Exceptions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryInvalidOperationExceptionTests
{
    [Fact]
    public void Ctor_Default_Should_SetDefaultErrorCodeAndDefaults_When_Created()
    {
        // Act
        var sut = new AryInvalidOperationException();

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
        sut.InnerException.Should().BeNull();
        sut.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Ctor_WithEmptyErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsEmpty()
    {
        // Arrange
        var message = "Operation is invalid in the current state.";
        var errorCode = string.Empty;

        // Act
        var sut = new AryInvalidOperationException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
    }

    [Fact]
    public void Ctor_WithErrorCode_Should_SetErrorCode_When_Provided()
    {
        // Arrange
        var message = "Operation is invalid in the current state.";
        var errorCode = "ARY.INVALID_OPERATION.SPECIFIC";

        // Act
        var sut = new AryInvalidOperationException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: errorCode);
        sut.Message.Should().Be(expected: message);
        sut.InnerException.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithErrorCodeAndInner_Should_SetErrorCodeAndInner_When_Provided()
    {
        // Arrange
        var message = "Operation is invalid in the current state.";
        var errorCode = "ARY.INVALID_OPERATION.SPECIFIC";
        var inner = new InvalidOperationException(message: "inner failure");

        // Act
        var sut = new AryInvalidOperationException(message: message, errorCode: errorCode, innerException: inner);

        // Assert
        sut.ErrorCode.Should().Be(expected: errorCode);
        sut.Message.Should().Be(expected: message);
        sut.InnerException.Should().BeSameAs(expected: inner);
    }

    [Fact]
    public void Ctor_WithMessage_Should_SetMessageAndDefaultErrorCode_When_MessageProvided()
    {
        // Arrange
        var message = "Operation is invalid in the current state.";

        // Act
        var sut = new AryInvalidOperationException(message: message);

        // Assert
        sut.Message.Should().Be(expected: message);
        sut.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
        sut.InnerException.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndInner_Should_SetMessageInnerAndDefaultErrorCode_When_Provided()
    {
        // Arrange
        var message = "Operation is invalid in the current state.";
        var inner = new InvalidOperationException(message: "inner failure");

        // Act
        var sut = new AryInvalidOperationException(message: message, innerException: inner);

        // Assert
        sut.Message.Should().Be(expected: message);
        sut.InnerException.Should().BeSameAs(expected: inner);
        sut.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
    }

    [Fact]
    public void Ctor_WithNullErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsNull()
    {
        // Arrange
        var message = "Operation is invalid in the current state.";
        string? errorCode = null;

        // Act
        var sut = new AryInvalidOperationException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
    }

    [Fact]
    public void Ctor_WithWhitespaceErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsWhitespace()
    {
        // Arrange
        var message = "Operation is invalid in the current state.";
        var errorCode = "   ";

        // Act
        var sut = new AryInvalidOperationException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
    }
}
