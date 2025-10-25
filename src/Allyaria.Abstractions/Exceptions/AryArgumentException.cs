using System.Buffers;

namespace Allyaria.Abstractions.Exceptions;

/// <summary>
/// Represents errors related to invalid or unexpected argument values. Provides static guard methods to validate arguments
/// consistently.
/// </summary>
public sealed class AryArgumentException : AryException
{
    /// <summary>Initializes a new instance of the <see cref="AryArgumentException" /> class.</summary>
    public AryArgumentException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public AryArgumentException(string? message)
        : base(message: message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message and inner
    /// exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, Exception? innerException)
        : base(message: message, innerException: innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message and argument
    /// name.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    public AryArgumentException(string? message, string? argName)
        : base(message: message)
        => ArgName = argName;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// name, and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, string? argName, Exception? innerException)
        : base(message: message, innerException: innerException)
        => ArgName = argName;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message and argument
    /// value.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argValue">The argument value.</param>
    public AryArgumentException(string? message, object? argValue)
        : base(message: message)
        => ArgValue = argValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// value, and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argValue">The argument value.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, object? argValue, Exception? innerException)
        : base(message: message, innerException: innerException)
        => ArgValue = argValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// name, and argument value.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="argValue">The argument value.</param>
    public AryArgumentException(string? message, string? argName, object? argValue)
        : base(message: message)
    {
        ArgName = argName;
        ArgValue = argValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// name, argument value, and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="argValue">The argument value.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, string? argName, object? argValue, Exception? innerException)
        : base(message: message, innerException: innerException)
    {
        ArgName = argName;
        ArgValue = argValue;
    }

    /// <summary>Gets the name of the argument that caused the exception.</summary>
    public string? ArgName { get; }

    /// <summary>Gets the value of the argument that caused the exception.</summary>
    public object? ArgValue { get; }

    /// <summary>Determines whether the specified non-generic collection has any elements.</summary>
    /// <param name="source">The collection.</param>
    /// <returns><c>true</c> if the collection is not null and contains at least one element; otherwise <c>false</c>.</returns>
    private static bool HasAny(IEnumerable? source)
    {
        if (source is null)
        {
            return false;
        }

        if (source is Array arr)
        {
            return arr.Length > 0;
        }

        if (source is ICollection coll)
        {
            return coll.Count > 0;
        }

        var e = source.GetEnumerator();

        try
        {
            return e.MoveNext();
        }
        finally
        {
            (e as IDisposable)?.Dispose();
        }
    }

    /// <summary>Determines whether the specified generic collection has any elements.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="source">The collection.</param>
    /// <returns><c>true</c> if the collection is not null and contains at least one element; otherwise <c>false</c>.</returns>
    private static bool HasAny<T>(IEnumerable<T>? source)
    {
        if (source is null)
        {
            return false;
        }

        if (source is T[] arr)
        {
            return arr.Length > 0;
        }

        if (source is ICollection<T> coll)
        {
            return coll.Count > 0;
        }

        if (source is IReadOnlyCollection<T> ro)
        {
            return ro.Count > 0;
        }

        using var e = source.GetEnumerator();

        return e.MoveNext();
    }

    /// <summary>Throws an <see cref="AryArgumentException" /> with a standardized message.</summary>
    /// <param name="message">The error message fragment.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="argValue">The argument value.</param>
    private static void ThrowError(string message, string? argName, object? argValue)
    {
        if (string.IsNullOrWhiteSpace(value: argName))
        {
            throw new AryArgumentException(message: $"Value {message}.", argValue: argValue);
        }

        throw new AryArgumentException(message: $"{argName} {message}.", argName: argName, argValue: argValue);
    }

    /// <summary>Throws if the specified span is empty.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="argValue">The span.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfEmpty<T>(Span<T> argValue, string? argName = null)
    {
        if (argValue.Length == 0)
        {
            ThrowError(message: "cannot be empty", argName: argName, argValue: null);
        }
    }

    /// <summary>Throws if the specified read-only span is empty.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="argValue">The span.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfEmpty<T>(ReadOnlySpan<T> argValue, string? argName = null)
    {
        if (argValue.Length == 0)
        {
            ThrowError(message: "cannot be empty", argName: argName, argValue: null);
        }
    }

    /// <summary>Throws if the specified read-only sequence is empty.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="argValue">The sequence.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfEmpty<T>(in ReadOnlySequence<T> argValue, string? argName = null)
    {
        if (argValue.IsEmpty)
        {
            ThrowError(message: "cannot be empty", argName: argName, argValue: argValue);
        }
    }

    /// <summary>Throws if the specified argument is <c>null</c>.</summary>
    /// <param name="argValue">The argument value.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNull(object? argValue, string? argName = null)
    {
        if (argValue is null)
        {
            ThrowError(message: "cannot be null", argName: argName, argValue: argValue);
        }
    }

    /// <summary>
    /// Throws if the specified value is <c>null</c> or equals the default value for <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The struct type.</typeparam>
    /// <param name="argValue">The argument value.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNullOrDefault<T>(T? argValue, string? argName = null)
        where T : struct
    {
        if (argValue is null || argValue.Value.Equals(obj: default(T)))
        {
            ThrowError(message: "cannot be null or the default value", argName: argName, argValue: argValue);
        }
    }

    /// <summary>Throws if the specified string argument is <c>null</c> or empty.</summary>
    /// <param name="argValue">The string argument.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNullOrEmpty(string? argValue, string? argName = null)
    {
        if (string.IsNullOrEmpty(value: argValue))
        {
            ThrowError(message: "cannot be null or empty", argName: argName, argValue: argValue);
        }
    }

    /// <summary>Throws if the specified collection is <c>null</c> or empty.</summary>
    /// <param name="argValue">The collection.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNullOrEmpty(IEnumerable? argValue, string? argName = null)
    {
        if (!HasAny(source: argValue))
        {
            ThrowError(message: "cannot be null or empty", argName: argName, argValue: argValue);
        }
    }

    /// <summary>Throws if the specified generic collection is <c>null</c> or empty.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="argValue">The collection.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNullOrEmpty<T>(IEnumerable<T>? argValue, string? argName = null)
    {
        if (!HasAny(source: argValue))
        {
            ThrowError(message: "cannot be null or empty", argName: argName, argValue: argValue);
        }
    }

    /// <summary>Throws if the specified memory is <c>null</c> or empty.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="argValue">The memory.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNullOrEmpty<T>(Memory<T>? argValue, string? argName = null)
    {
        if (argValue is null || argValue.Value.Length == 0)
        {
            ThrowError(message: "cannot be null or empty", argName: argName, argValue: argValue);
        }
    }

    /// <summary>Throws if the specified read-only memory is <c>null</c> or empty.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="argValue">The memory.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNullOrEmpty<T>(ReadOnlyMemory<T>? argValue, string? argName = null)
    {
        if (argValue is null || argValue.Value.Length == 0)
        {
            ThrowError(message: "cannot be null or empty", argName: argName, argValue: argValue);
        }
    }

    /// <summary>Throws if the specified string argument is <c>null</c>, empty, or whitespace.</summary>
    /// <param name="argValue">The string argument.</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfNullOrWhiteSpace(string? argValue, string? argName = null)
    {
        if (string.IsNullOrWhiteSpace(value: argValue))
        {
            ThrowError(message: "cannot be null, empty or whitespace", argName: argName, argValue: argValue);
        }
    }

    /// <summary>
    /// Throws if the specified value is out of the inclusive range defined by <paramref name="min" /> and
    /// <paramref name="max" />. Also rejects <c>NaN</c> and <c>Infinity</c> for <see cref="double" /> and <see cref="float" />
    /// .
    /// </summary>
    /// <typeparam name="T">The struct type that implements <see cref="IComparable{T}" />.</typeparam>
    /// <param name="argValue">The argument value.</param>
    /// <param name="min">The optional minimum value (inclusive).</param>
    /// <param name="max">The optional maximum value (inclusive).</param>
    /// <param name="argName">The argument name.</param>
    public static void ThrowIfOutOfRange<T>(T? argValue, T? min = null, T? max = null, string? argName = null)
        where T : struct, IComparable<T>
    {
        ThrowIfNull(argValue: argValue, argName: argName);

        if (min is null && max is null)
        {
            return;
        }

        if (min.HasValue && max.HasValue && min.Value.CompareTo(other: max.Value) > 0)
        {
            (min, max) = (max, min);
        }

        if ((argValue is double d && (double.IsNaN(d: d) || double.IsInfinity(d: d))) ||
            (argValue is float f && (float.IsNaN(f: f) || float.IsInfinity(f: f))))
        {
            ThrowError(message: "is not a finite number", argName: argName, argValue: argValue);
        }

        var value = argValue!.Value;
        var belowMin = min.HasValue && value.CompareTo(other: min.Value) < 0;
        var aboveMax = max.HasValue && value.CompareTo(other: max.Value) > 0;

        if (belowMin)
        {
            ThrowError(message: $"must be greater than or equal to {min!.Value}", argName: argName, argValue: argValue);
        }
        else if (aboveMax)
        {
            ThrowError(message: $"must be less than or equal to {max!.Value}", argName: argName, argValue: argValue);
        }
    }
}
