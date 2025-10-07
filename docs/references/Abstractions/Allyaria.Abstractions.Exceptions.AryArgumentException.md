# Allyaria.Abstractions.Exceptions.AryArgumentException

`AryArgumentException` represents errors related to invalid or unexpected argument values within Allyaria APIs.
It extends `AryException` and provides a rich set of static guard methods to validate method parameters consistently,
improving safety and readability across the codebase.

---

## Constructors

`AryArgumentException()`
Initializes a new instance of the `AryArgumentException` class.

* Exceptions: *None*

`AryArgumentException(string? message)`
Initializes a new instance of the `AryArgumentException` class with a specified error message.

* Exceptions: *None*

`AryArgumentException(string? message, Exception? innerException)`
Initializes a new instance of the `AryArgumentException` class with a specified error message and an inner exception.

* Exceptions: *None*

`AryArgumentException(string? message, string? argName)`
Initializes a new instance of the `AryArgumentException` class with a specified error message and argument name.

* Exceptions: *None*

`AryArgumentException(string? message, string? argName, Exception? innerException)`
Initializes a new instance of the `AryArgumentException` class with a specified error message, argument name, and inner
exception.

* Exceptions: *None*

`AryArgumentException(string? message, object? argValue)`
Initializes a new instance of the `AryArgumentException` class with a specified error message and argument value.

* Exceptions: *None*

`AryArgumentException(string? message, object? argValue, Exception? innerException)`
Initializes a new instance of the `AryArgumentException` class with a specified error message, argument value, and inner
exception.

* Exceptions: *None*

`AryArgumentException(string? message, string? argName, object? argValue)`
Initializes a new instance of the `AryArgumentException` class with a specified error message, argument name, and
argument value.

* Exceptions: *None*

`AryArgumentException(string? message, string? argName, object? argValue, Exception? innerException)`
Initializes a new instance of the `AryArgumentException` class with a specified error message, argument name, argument
value, and inner exception.

* Exceptions: *None*

---

## Properties

| Name       | Type      | Description                                               |
|------------|-----------|-----------------------------------------------------------|
| `ArgName`  | `string?` | Gets the name of the argument that caused the exception.  |
| `ArgValue` | `object?` | Gets the value of the argument that caused the exception. |

---

## Methods

| Name                                                                                      | Returns | Description                                                                                                       |
|-------------------------------------------------------------------------------------------|---------|-------------------------------------------------------------------------------------------------------------------|
| `ThrowIfEmpty<T>(Span<T> argValue, string? argName = null)`                               | `void`  | Throws if the specified span is empty.                                                                            |
| `ThrowIfEmpty<T>(ReadOnlySpan<T> argValue, string? argName = null)`                       | `void`  | Throws if the specified read-only span is empty.                                                                  |
| `ThrowIfEmpty<T>(in ReadOnlySequence<T> argValue, string? argName = null)`                | `void`  | Throws if the specified read-only sequence is empty.                                                              |
| `ThrowIfNull(object? argValue, string? argName = null)`                                   | `void`  | Throws if the specified argument is `null`.                                                                       |
| `ThrowIfNullOrDefault<T>(T? argValue, string? argName = null)`                            | `void`  | Throws if the specified value is `null` or equals its default value.                                              |
| `ThrowIfNullOrEmpty(string? argValue, string? argName = null)`                            | `void`  | Throws if the specified string argument is `null` or empty.                                                       |
| `ThrowIfNullOrEmpty(IEnumerable? argValue, string? argName = null)`                       | `void`  | Throws if the specified collection is `null` or empty.                                                            |
| `ThrowIfNullOrEmpty<T>(IEnumerable<T>? argValue, string? argName = null)`                 | `void`  | Throws if the specified generic collection is `null` or empty.                                                    |
| `ThrowIfNullOrEmpty<T>(Memory<T>? argValue, string? argName = null)`                      | `void`  | Throws if the specified memory block is `null` or empty.                                                          |
| `ThrowIfNullOrEmpty<T>(ReadOnlyMemory<T>? argValue, string? argName = null)`              | `void`  | Throws if the specified read-only memory block is `null` or empty.                                                |
| `ThrowIfNullOrWhiteSpace(string? argValue, string? argName = null)`                       | `void`  | Throws if the specified string argument is `null`, empty, or whitespace.                                          |
| `ThrowIfOutOfRange<T>(T? argValue, T? min = null, T? max = null, string? argName = null)` | `void`  | Throws if the value is out of range (below `min` or above `max`). Rejects `NaN` or `Infinity` for floating types. |

---

## Operators

*None*

---

## Events

*None*

---

## Exceptions

* Throws `AryArgumentException` when validation fails (from any static guard method).

---

## Behavior Notes

* All `ThrowIf*` methods use standardized error message patterns (`"cannot be null"`, `"cannot be empty"`, etc.).
* Messages include the argument name when provided, otherwise fall back to a generic form.
* Range checks automatically normalize inverted min/max bounds.
* Floating-point inputs reject `NaN` and infinite values.

---

## Examples

### Minimal Example

```csharp
AryArgumentException.ThrowIfNull(name, nameof(name));
```

### Expanded Example

```csharp
public void ProcessData(IList<int> data, string label)
{
    AryArgumentException.ThrowIfNullOrEmpty(data, nameof(data));
    AryArgumentException.ThrowIfNullOrWhiteSpace(label, nameof(label));
    AryArgumentException.ThrowIfOutOfRange(data.Count, 1, 100, nameof(data));

    Console.WriteLine($"Processing '{label}' with {data.Count} items.");
}
```

> *Rev Date: 2025-10-06*
