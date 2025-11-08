namespace Allyaria.Abstractions.UnitTests.Result;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryResultTTests
{
    [Fact]
    public void Failure_Should_CreateFailedResultWithGivenError_When_ErrorIsNotNull()
    {
        // Arrange
        var error = new InvalidOperationException(message: "bad op");

        // Act
        var sut = AryResult<string>.Failure(error: error);

        // Assert
        sut.IsSuccess.Should().BeFalse();
        sut.IsFailure.Should().BeTrue();
        sut.Error.Should().BeSameAs(expected: error);
        sut.Value.Should().BeNull();
    }

    [Fact]
    public void Failure_Should_CreateFailedResultWithUnknownError_When_ErrorIsNull()
    {
        // Arrange
        Exception? error = null;

        // Act
        var sut = AryResult<string>.Failure(error: error);

        // Assert
        sut.IsSuccess.Should().BeFalse();
        sut.IsFailure.Should().BeTrue();
        sut.Error.Should().NotBeNull();
        sut.Error.Should().BeOfType<AryException>();
        sut.Error!.Message.Should().Be(expected: "Unknown error");
        sut.Value.Should().BeNull();
    }

    [Fact]
    public void IsFailure_Should_ReturnFalse_When_IsSuccessIsTrue()
    {
        // Arrange
        var sut = AryResult<int>.Success(value: 10);

        // Act
        var isFailure = sut.IsFailure;

        // Assert
        isFailure.Should().BeFalse();
    }

    [Fact]
    public void IsFailure_Should_ReturnTrue_When_IsSuccessIsFalse()
    {
        // Arrange
        var sut = AryResult<string>.Failure(error: new Exception(message: "fail"));

        // Act
        var isFailure = sut.IsFailure;

        // Assert
        isFailure.Should().BeTrue();
    }

    [Fact]
    public void Success_Should_CreateSuccessfulResult_When_Called()
    {
        // Arrange
        var value = 123;

        // Act
        var sut = AryResult<int>.Success(value: value);

        // Assert
        sut.IsSuccess.Should().BeTrue();
        sut.IsFailure.Should().BeFalse();
        sut.Value.Should().Be(expected: value);
        sut.Error.Should().BeNull();
    }

    [Fact]
    public void ToFailure_Should_ReturnFailedAryResultWithSameError_When_CalledOnFailure()
    {
        // Arrange
        var error = new InvalidOperationException(message: "boom");
        var sut = AryResult<string>.Failure(error: error);

        // Act
        var result = sut.ToFailure();

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeSameAs(expected: error);
    }

    [Fact]
    public void ToFailure_Should_ThrowAryInvalidOperationException_When_CalledOnSuccess()
    {
        // Arrange
        var sut = AryResult<string>.Success(value: "ok");

        // Act
        var act = () => sut.ToFailure();

        // Assert
        var ex = act.Should().Throw<AryInvalidOperationException>().Which;
        ex.Message.Should().Be(expected: "Cannot convert a successful AryResult<T> to AryResult failure.");
        ex.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
    }
}
