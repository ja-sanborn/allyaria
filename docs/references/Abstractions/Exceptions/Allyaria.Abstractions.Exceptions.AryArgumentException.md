# Allyaria.Abstractions.Exceptions.AryArgumentException

`AryArgumentException` is an exception type that represents errors related to invalid or unexpected argument values. It
extends `AryException` and provides a set of static guard methods (`ThrowIfNull`, `ThrowIfEmpty`, `ThrowIfOutOfRange`,
etc.) to enforce argument validation consistently across the Allyaria SDK. This class promotes defensive programming and
helps ensure predictable error handling.

## Constructors

`AryArgumentException()`
Initializes a new instance of `AryArgumentException`.

`AryArgumentException(string? message)`
Initializes a new instance of `AryArgumentException` with a specified error message.

`AryArgumentException(string? message, Exception? innerException)`
Initializes a new instance of `AryArgumentException` with a specified error message and an inner exception that caused
the current exception.

`AryArgumentException(string? message, string? argName)`
Initializes a new instance of `AryArgumentException` with a specified error message and argument name.

`AryArgumentException(string? message, string? argName, Exception? innerException)`
Initializes a new instance of `AryArgumentException` with a specified error message, argument name, and inner exception.

`AryArgumentException(string? message, object? argValue)`
Initializes a new instance of `AryArgumentException` with a specified error message and argument value.

`AryArgumentException(string? message, object? argValue, Exception? innerException)`
Initializes a new instance of `AryArgumentException` with a specified error message, argument value, and inner
exception.

`AryArgumentException(string? message, string? argName, object? argValue)`
Initializes a new instance of `AryArgumentException` with a specified error message, argument name, and argument value.

`AryArgumentException(string? message, string? argName, object? argValue, Exception? innerException)`
Initializes a new instance of `AryArgumentException` with a specified error message, argument name, argument value, and
inner exception.

## Properties

| Name       | Type      | Description                                               |
|------------|-----------|-----------------------------------------------------------|
| `ArgName`  | `string?` | Gets the name of the argument that caused the exception.  |
| `ArgValue` | `object?` | Gets the value of the argument that caused the exception. |

## Methods

| Name                                                                                      | Returns | Description                                                                                                                                         |
|-------------------------------------------------------------------------------------------|---------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| `ThrowIfEmpty<T>(Span<T> argValue, string? argName = null)`                               | `void`  | Throws if the specified span is empty.                                                                                                              |
| `ThrowIfEmpty<T>(ReadOnlySpan<T> argValue, string? argName = null)`                       | `void`  | Throws if the specified read-only span is empty.                                                                                                    |
| `ThrowIfEmpty<T>(in ReadOnlySequence<T> argValue, string? argName = null)`                | `void`  | Throws if the specified read-only sequence is empty.                                                                                                |
| `ThrowIfNull(object? argValue, string? argName = null)`                                   | `void`  | Throws if the specified argument is `null`.                                                                                                         |
| `ThrowIfNullOrDefault<T>(T? argValue, string? argName = null)`                            | `void`  | Throws if the specified value is `null` or equals the default value for the struct type.                                                            |
| `ThrowIfNullOrEmpty(string? argValue, string? argName = null)`                            | `void`  | Throws if the specified string is `null` or empty.                                                                                                  |
| `ThrowIfNullOrEmpty(IEnumerable? argValue, string? argName = null)`                       | `void`  | Throws if the specified non-generic collection is `null` or empty.                                                                                  |
| `ThrowIfNullOrEmpty<T>(IEnumerable<T>? argValue, string? argName = null)`                 | `void`  | Throws if the specified generic collection is `null` or empty.                                                                                      |
| `ThrowIfNullOrEmpty<T>(Memory<T>? argValue, string? argName = null)`                      | `void`  | Throws if the specified memory is `null` or empty.                                                                                                  |
| `ThrowIfNullOrEmpty<T>(ReadOnlyMemory<T>? argValue, string? argName = null)`              | `void`  | Throws if the specified read-only memory is `null` or empty.                                                                                        |
| `ThrowIfNullOrWhiteSpace(string? argValue, string? argName = null)`                       | `void`  | Throws if the specified string is `null`, empty, or whitespace.                                                                                     |
| `ThrowIfOutOfRange<T>(T? argValue, T? min = null, T? max = null, string? argName = null)` | `void`  | Throws if the specified value is outside the inclusive range defined by `min` and `max`, or is not a finite number when `T` is `float` or `double`. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Exceptions;

public class Example
{
    public void ProcessInput(string input, int number)
    {
        AryArgumentException.ThrowIfNullOrWhiteSpace(input, nameof(input));
        AryArgumentException.ThrowIfOutOfRange(number, 1, 100, nameof(number));

        // Proceed safely knowing inputs are valid
        Console.WriteLine($"Input: {input}, Number: {number}");
    }
}
```

---

*Revision Date: 2025-10-17*
