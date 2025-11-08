namespace Allyaria.Abstractions.UnitTests.Exceptions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryExceptionTests
{
    [Fact]
    public void Ctor_Default_Should_SetUtcTimestampAndDefaults_When_Created()
    {
        // Arrange
        var before = DateTimeOffset.UtcNow;

        // Act
        var sut = new AryException();
        var after = DateTimeOffset.UtcNow;

        // Assert
        sut.Timestamp.Should().BeOnOrAfter(expected: before).And.BeOnOrBefore(expected: after);
        sut.Timestamp.Offset.Should().Be(expected: TimeSpan.Zero);
        sut.InnerException.Should().BeNull();
        sut.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Ctor_WithEmptyErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsEmpty()
    {
        // Arrange
        var message = "Something went boom";
        var errorCode = string.Empty;

        // Act
        var sut = new AryException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.UNKNOWN");
    }

    [Fact]
    public void Ctor_WithErrorCode_Should_SetErrorCode_When_Provided()
    {
        // Arrange
        var message = "Something went boom";
        var errorCode = "ARY.SPECIFIC_ERROR";

        // Act
        var sut = new AryException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: errorCode);
    }

    [Fact]
    public void Ctor_WithMessage_Should_SetMessage_When_MessageProvided()
    {
        // Arrange
        var message = "Something went boom";

        // Act
        var sut = new AryException(message: message);

        // Assert
        sut.Message.Should().Be(expected: message);
    }

    [Fact]
    public void Ctor_WithMessageAndInner_Should_SetMessageAndInner_When_Provided()
    {
        // Arrange
        var inner = new InvalidOperationException(message: "inner failure");
        var message = "Top-level failure";

        // Act
        var sut = new AryException(message: message, innerException: inner);

        // Assert
        sut.Message.Should().Be(expected: message);
        sut.InnerException.Should().BeSameAs(expected: inner);
    }

    [Fact]
    public void Ctor_WithNullErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsNull()
    {
        // Arrange
        var message = "Something went boom";
        string? errorCode = null;

        // Act
        var sut = new AryException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.UNKNOWN");
    }

    [Fact]
    public void Ctor_WithNullMessage_Should_ProvideNonEmptyDefaultMessage_When_MessageIsNull()
    {
        // Arrange
        string? message = null;

        // Act
        var sut = new AryException(message: message);

        // Assert
        sut.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Ctor_WithWhitespaceErrorCode_Should_DefaultErrorCode_When_ErrorCodeIsWhitespace()
    {
        // Arrange
        var message = "Something went boom";
        var errorCode = "   ";

        // Act
        var sut = new AryException(message: message, errorCode: errorCode);

        // Assert
        sut.ErrorCode.Should().Be(expected: "ARY.UNKNOWN");
    }

    [Fact]
    public void MultipleInstances_Should_HaveMonotonicTimestamps_When_CreatedSequentially()
    {
        // Arrange
        var first = new AryException();

        // Act
        var second = new AryException();

        // Assert
        second.Timestamp.Should().BeOnOrAfter(expected: first.Timestamp);
    }

    [Fact]
    public void Timestamp_Should_RemainConstant_When_AccessedMultipleTimes()
    {
        // Arrange
        var sut = new AryException();

        // Act
        var t1 = sut.Timestamp;
        var t2 = sut.Timestamp;

        // Assert
        t2.Should().Be(expected: t1);
    }
}
