using Allyaria.Abstractions.Exceptions;

namespace Allyaria.Abstractions.UnitTests.Exceptions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaExceptionTests
{
    [Fact]
    public void Ctor_Default_Should_SetUtcTimestampAndDefaults_When_Created()
    {
        // Arrange
        var before = DateTimeOffset.UtcNow;

        // Act
        var sut = new AllyariaException();
        var after = DateTimeOffset.UtcNow;

        // Assert
        sut.Timestamp.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
        sut.Timestamp.Offset.Should().Be(TimeSpan.Zero);
        sut.InnerException.Should().BeNull();
        sut.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Ctor_WithMessage_Should_SetMessage_When_MessageProvided()
    {
        // Arrange
        var message = "Something went boom";

        // Act
        var sut = new AllyariaException(message);

        // Assert
        sut.Message.Should().Be(message);
    }

    [Fact]
    public void Ctor_WithMessageAndInner_Should_SetMessageAndInner_When_Provided()
    {
        // Arrange
        var inner = new InvalidOperationException("inner failure");
        var message = "Top-level failure";

        // Act
        var sut = new AllyariaException(message, inner);

        // Assert
        sut.Message.Should().Be(message);
        sut.InnerException.Should().BeSameAs(inner);
    }

    [Fact]
    public void Ctor_WithNullMessage_Should_ProvideNonEmptyDefaultMessage_When_MessageIsNull()
    {
        // Arrange
        string? message = null;

        // Act
        var sut = new AllyariaException(message);

        // Assert
        sut.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void MultipleInstances_Should_HaveMonotonicTimestamps_When_CreatedSequentially()
    {
        // Arrange
        var first = new AllyariaException();

        // Act
        var second = new AllyariaException();

        // Assert
        second.Timestamp.Should().BeOnOrAfter(first.Timestamp);
    }

    [Fact]
    public void Timestamp_Should_RemainConstant_When_AccessedMultipleTimes()
    {
        // Arrange
        var sut = new AllyariaException();

        // Act
        var t1 = sut.Timestamp;
        var t2 = sut.Timestamp;

        // Assert
        t2.Should().Be(t1);
    }
}
