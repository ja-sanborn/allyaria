# Allyaria.Abstractions.Exceptions.AryException

`AryException` is the base exception type for Allyaria applications, used as the common ancestor for all custom
exceptions. It provides standard exception constructors and records a UTC timestamp at creation to aid in diagnostics
and logging across the SDK.

## Constructors

`AryException()`
Initializes a new instance of `AryException`.

`AryException(string? message)`
Initializes a new instance of `AryException` with the specified error `message`.

`AryException(string? message, Exception? innerException)`
Initializes a new instance of `AryException` with the specified error `message` and an `innerException` that caused the
current exception.

## Properties

| Name        | Type             | Description                                                        |
|-------------|------------------|--------------------------------------------------------------------|
| `Timestamp` | `DateTimeOffset` | Gets the UTC timestamp set when the exception instance is created. |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Exceptions;

try
{
    // Some operation that may fail
    DoWork();
}
catch (Exception ex)
{
    // Wrap and rethrow with context
    throw new AryException("Operation failed.", ex);
}

// Or create directly and inspect the timestamp
var e = new AryException("Something went wrong.");
var createdAtUtc = e.Timestamp;
```

---

*Revision Date: 2025-10-17*
