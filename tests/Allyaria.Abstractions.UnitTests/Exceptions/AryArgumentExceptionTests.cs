using System.Buffers;

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
    public void Ctor_WithMessage_Should_SetMessage()
    {
        // Act
        var sut = new AryArgumentException("msg");

        // Assert
        sut.Message.Should().Be("msg");
        sut.ArgName.Should().BeNull();
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgName_Should_SetMessageAndArgName()
    {
        // Act
        var sut = new AryArgumentException(message: "msg", argName: "param");

        // Assert
        sut.Message.Should().Be("msg");
        sut.ArgName.Should().Be("param");
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgNameAndArgValue_Should_SetAllProperties()
    {
        // Act
        var sut = new AryArgumentException(message: "msg", argName: "param", argValue: 42);

        // Assert
        sut.Message.Should().Be("msg");
        sut.ArgName.Should().Be("param");
        sut.ArgValue.Should().Be(42);
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
        sut.Message.Should().Be("msg");
        sut.ArgName.Should().Be("param");
        sut.InnerException.Should().Be(inner);
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndArgValue_Should_SetMessageAndArgValue()
    {
        // Act
        var sut = new AryArgumentException(message: "msg", argValue: 42);

        // Assert
        sut.Message.Should().Be("msg");
        sut.ArgValue.Should().Be(42);
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
        sut.Message.Should().Be("msg");
        sut.ArgValue.Should().Be(42);
        sut.InnerException.Should().Be(inner);
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
        sut.Message.Should().Be("msg");
        sut.InnerException.Should().Be(inner);
        sut.ArgName.Should().BeNull();
        sut.ArgValue.Should().BeNull();
    }

    [Fact]
    public void Ctor_WithMessageAndNameAndValueAndInner_Should_SetAllProperties()
    {
        // Arrange
        var inner = new InvalidOperationException("inner");
        var message = "bad arg";
        var name = "param";
        var value = 123;

        // Act
        var sut = new AryArgumentException(message: message, argName: name, argValue: value, innerException: inner);

        // Assert
        sut.Message.Should().Be(message);
        sut.ArgName.Should().Be(name);
        sut.ArgValue.Should().Be(value);
        sut.InnerException.Should().Be(inner);
    }

    [Fact]
    public void ThrowIfEmptyReadOnlySequence_Should_NotThrow_When_HasElements()
    {
        // Arrange
        var sequence = new ReadOnlySequence<byte>(
            new byte[]
            {
                1
            }
        );

        // Act
        var act = () => AryArgumentException.ThrowIfEmpty(argValue: sequence, argName: "seq");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfEmptyReadOnlySequence_Should_Throw_When_Empty()
    {
        // Arrange
        var sequence = new ReadOnlySequence<byte>(Array.Empty<byte>());

        // Act
        var act = () => AryArgumentException.ThrowIfEmpty(argValue: sequence, argName: "seq");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("seq");
    }

    [Fact]
    public void ThrowIfEmptyReadOnlySpan_Should_NotThrow_When_HasElements()
    {
        // Arrange + Act
        var act = () =>
        {
            ReadOnlySpan<int> span =
            [
                1
            ];

            AryArgumentException.ThrowIfEmpty(argValue: span, argName: "ros");
        };

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfEmptyReadOnlySpan_Should_Throw_When_Empty()
    {
        // Arrange + Act
        var act = () =>
        {
            ReadOnlySpan<int> span = Array.Empty<int>();
            AryArgumentException.ThrowIfEmpty(argValue: span, argName: "ros");
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("ros");
    }

    [Fact]
    public void ThrowIfEmptySpan_Should_NotThrow_When_HasElements()
    {
        // Arrange + Act
        var act = () =>
        {
            int[] array =
            [
                1
            ];

            var span = array.AsSpan();
            AryArgumentException.ThrowIfEmpty(argValue: span, argName: "span");
        };

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfEmptySpan_Should_Throw_When_Empty()
    {
        // Arrange + Act
        var act = () =>
        {
            var span = Array.Empty<int>().AsSpan();
            AryArgumentException.ThrowIfEmpty(argValue: span, argName: "span");
        };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("span");
    }

    [Fact]
    public void ThrowIfNull_Should_NotThrow_When_ValueIsNotNull()
    {
        // Arrange
        var value = new object();

        // Act
        var act = () => AryArgumentException.ThrowIfNull(value);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ThrowIfNull_Should_Throw_When_ValueIsNull(object? argName)
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNull(argValue: null, argName: argName?.ToString());

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage("*cannot be null*");
    }

    [Fact]
    public void ThrowIfNullOrDefault_Should_NotThrow_When_NonDefaultStruct()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrDefault<int>(argValue: 1, argName: "num");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrDefault_Should_Throw_When_DefaultStruct()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrDefault<int>(argValue: 0, argName: "num");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("num");
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Generic_Should_NotThrow_When_ArrayHasElements()
    {
        // Arrange
        var arr = new[]
        {
            1
        };

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: arr, argName: "arr");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Generic_Should_NotThrow_When_EnumerableEnumeratorHasElements()
    {
        // Arrange
        var someEnum = new EnumerableOnly<int>(Enumerable.Repeat(element: 1, count: 1));

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: someEnum, argName: "enm");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Generic_Should_NotThrow_When_IReadOnlyCollectionHasElements()
    {
        // Arrange
        var ro = new MinimalReadOnlyCollection<int>(
            source: new[]
            {
                7
            }, count: 1
        );

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: ro, argName: "ro");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Generic_Should_Throw_When_ArrayEmpty()
    {
        // Arrange
        var arr = Array.Empty<int>();

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: arr, argName: "arr");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("arr");
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Generic_Should_Throw_When_EnumerableEnumeratorEmpty()
    {
        // Arrange
        var emptyEnum = new EnumerableOnly<int>(Enumerable.Empty<int>());

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: emptyEnum, argName: "enm");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("enm");
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Generic_Should_Throw_When_IReadOnlyCollectionEmpty()
    {
        // Arrange
        var emptyRo = new MinimalReadOnlyCollection<int>(source: Enumerable.Empty<int>(), count: 0);

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: emptyRo, argName: "ro");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("ro");
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonGeneric_Should_NotThrow_When_ArrayHasElements()
    {
        // Arrange
        IEnumerable value = new[]
        {
            1,
            2,
            3
        };

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: value, argName: "items");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonGeneric_Should_NotThrow_When_EnumeratorHasElements()
    {
        // Arrange
        IEnumerable value = Enumerable.Repeat(element: 42, count: 1);

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: value, argName: "items");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonGeneric_Should_Throw_When_ArrayIsEmpty()
    {
        // Arrange
        IEnumerable value = Array.Empty<int>();

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: value, argName: "items");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("items");
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonGeneric_Should_Throw_When_EnumeratorHasNoElements()
    {
        // Arrange
        IEnumerable value = Enumerable.Empty<int>();

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: value, argName: "items");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("items");
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonGeneric_Should_Throw_When_ICollectionIsEmpty()
    {
        // Arrange + Act
        // ReSharper disable once CollectionNeverUpdated.Local
        var value = new ArrayList();
        var act = () => { AryArgumentException.ThrowIfNullOrEmpty(argValue: value, argName: "items"); };

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("items");
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonGeneric_Should_Throw_When_ValueIsNull()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: (IEnumerable?)null, argName: "items");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("items");
    }

    [Fact]
    public void ThrowIfNullOrEmptyEnumerable_NonGeneric_Should_NotThrow_When_HasElements()
    {
        // Arrange
        var list = new ArrayList
        {
            1
        };

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: list, argName: "list");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmptyEnumerable_Should_Throw_When_EmptyCollection()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        var list = new List<int>();

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: list, argName: "list");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("list");
    }

    [Fact]
    public void ThrowIfNullOrEmptyGenericEnumerable_Should_NotThrow_When_HasElements()
    {
        // Arrange
        var list = new List<int>
        {
            1
        };

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: list, argName: "list");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmptyGenericEnumerable_Should_Throw_When_Null()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: (IEnumerable<int>?)null, argName: "list");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("list");
    }

    [Fact]
    public void ThrowIfNullOrEmptyMemory_Should_NotThrow_When_HasElements()
    {
        // Arrange
        Memory<int>? memory = new[]
        {
            1
        }.AsMemory();

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: memory, argName: "mem");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmptyMemory_Should_Throw_When_Empty()
    {
        // Arrange
        Memory<int>? memory = new Memory<int>(Array.Empty<int>());

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: memory, argName: "mem");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("mem");
    }

    [Fact]
    public void ThrowIfNullOrEmptyReadOnlyMemory_Should_NotThrow_When_HasElements()
    {
        // Arrange
        ReadOnlyMemory<int>? memory = new ReadOnlyMemory<int>(
            new[]
            {
                1
            }
        );

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: memory, argName: "romem");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrEmptyReadOnlyMemory_Should_Throw_When_Empty()
    {
        // Arrange
        ReadOnlyMemory<int>? memory = new ReadOnlyMemory<int>(Array.Empty<int>());

        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: memory, argName: "romem");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("romem");
    }

    [Fact]
    public void ThrowIfNullOrEmptyString_Should_NotThrow_When_NonEmpty()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: "ok", argName: "s");

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ThrowIfNullOrEmptyString_Should_Throw_When_NullOrEmpty(string? value)
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrEmpty(argValue: value, argName: "s");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("s");
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_Should_NotThrow_When_HasNonWhitespace()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrWhiteSpace(argValue: "a", argName: "s");

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ThrowIfNullOrWhiteSpace_Should_Throw_When_NullOrWhitespace(string? value)
    {
        // Act
        var act = () => AryArgumentException.ThrowIfNullOrWhiteSpace(argValue: value, argName: "s");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("s");
    }

    [Fact]
    public void ThrowIfOutOfRange_Should_NotThrow_ForFiniteFloatWithinRange()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfOutOfRange<float>(argValue: 1.5f, min: 1f, max: 2f, argName: "f");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfOutOfRange_Should_NotThrow_When_MinAndMaxAreNull()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfOutOfRange<int>(argValue: 5, min: null, max: null, argName: "n");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfOutOfRange_Should_NotThrow_When_MinGreaterThanMax_But_ValueWithinSwappedRange()
    {
        // Arrange + Act
        var act = () => AryArgumentException.ThrowIfOutOfRange<int>(argValue: 7, min: 10, max: 5, argName: "n");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfOutOfRange_Should_NotThrow_When_ValueWithinRange()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfOutOfRange<int>(argValue: 7, min: 5, max: 10, argName: "n");

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void ThrowIfOutOfRange_Should_Throw_When_NotFiniteDouble(double value)
    {
        // Act
        var act = () => AryArgumentException.ThrowIfOutOfRange<double>(
            argValue: value, min: 0d, max: 100d, argName: "d"
        );

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("d");
    }

    [Fact]
    public void ThrowIfOutOfRange_Should_Throw_When_ValueAboveMax()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfOutOfRange<int>(argValue: 20, min: 5, max: 10, argName: "n");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("n");
    }

    [Fact]
    public void ThrowIfOutOfRange_Should_Throw_When_ValueBelowMin()
    {
        // Act
        var act = () => AryArgumentException.ThrowIfOutOfRange<int>(argValue: 1, min: 5, max: 10, argName: "n");

        // Assert
        act.Should().Throw<AryArgumentException>()
            .Which.ArgName.Should().Be("n");
    }

    private sealed class EnumerableOnly<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;

        public EnumerableOnly(IEnumerable<T> source) => _source = source;

        public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private sealed class MinimalReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly IEnumerable<T> _source;

        public MinimalReadOnlyCollection(IEnumerable<T> source, int count)
        {
            _source = source;
            Count = count;
        }

        public int Count { get; }

        public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
