namespace Allyaria.Abstractions.UnitTests.Result;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryResultTests
{
    [Fact]
    public void Failure_Should_CreateFailedResultWithGivenError_When_ErrorIsNotNull()
    {
        // Arrange
        var error = new InvalidOperationException(message: "boom");

        // Act
        var sut = AryResult.Failure(error: error);

        // Assert
        sut.IsSuccess.Should().BeFalse();
        sut.IsFailure.Should().BeTrue();
        sut.Error.Should().BeSameAs(expected: error);
    }

    [Fact]
    public void Failure_Should_CreateFailedResultWithUnknownError_When_ErrorIsNull()
    {
        // Arrange
        Exception? error = null;

        // Act
        var sut = AryResult.Failure(error: error);

        // Assert
        sut.IsSuccess.Should().BeFalse();
        sut.IsFailure.Should().BeTrue();
        sut.Error.Should().NotBeNull();
        sut.Error.Should().BeOfType<AryException>();
        sut.Error!.Message.Should().Be(expected: "Unknown error");
    }

    [Fact]
    public void IsFailure_Should_BeFalse_When_ResultIsSuccess()
    {
        // Arrange
        var sut = AryResult.Success();

        // Act
        var isFailure = sut.IsFailure;

        // Assert
        isFailure.Should().BeFalse();
    }

    [Fact]
    public void IsFailure_Should_BeTrue_When_ResultIsFailure()
    {
        // Arrange
        var sut = AryResult.Failure(error: new Exception(message: "fail"));

        // Act
        var isFailure = sut.IsFailure;

        // Assert
        isFailure.Should().BeTrue();
    }

    [Fact]
    public void Success_Should_CreateSuccessfulResult_When_Called()
    {
        // Arrange & Act
        var sut = AryResult.Success();

        // Assert
        sut.IsSuccess.Should().BeTrue();
        sut.IsFailure.Should().BeFalse();
        sut.Error.Should().BeNull();
    }

    [Fact]
    public void ToFailure_Should_ReturnFailedGenericResultWithSameError_When_CalledOnFailedResult()
    {
        // Arrange
        var error = new InvalidOperationException(message: "boom");
        var sut = AryResult.Failure(error: error);

        // Act
        var result = sut.ToFailure<string>();

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeSameAs(expected: error);
    }

    [Fact]
    public void ToFailure_Should_ThrowAryInvalidOperationException_When_CalledOnSuccessResult()
    {
        // Arrange
        var sut = AryResult.Success();

        // Act
        var act = () => sut.ToFailure<int>();

        // Assert
        var exception = act.Should().Throw<AryInvalidOperationException>().Which;
        exception.Message.Should().Be(expected: "Cannot convert a successful AryResult to AryResult<T> failure.");
        exception.ErrorCode.Should().Be(expected: "ARY.INVALID_OPERATION");
    }
}
