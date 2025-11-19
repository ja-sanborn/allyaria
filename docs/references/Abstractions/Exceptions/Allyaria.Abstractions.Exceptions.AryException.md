# Allyaria.Abstractions.Exceptions.AryException

`AryException` is the base exception type for Allyaria applications and libraries. It provides a common foundation for
all custom exceptions in the Allyaria ecosystem by adding a structured error code and a creation timestamp on top of the
standard .NET `Exception` behavior, making it easier to categorize, log, and troubleshoot application errors in a
consistent way.

## Constructors

`AryException(string? message = null, string? errorCode = null, Exception? innerException = null)` Initializes a new
instance of `AryException` with an optional error message, an optional structured error code (defaulting to
`"ARY.UNKNOWN"` when null or empty), and an optional inner exception.

## Properties

| Name        | Type             | Description                                                                                                                                            |
|-------------|------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ErrorCode` | `string`         | Gets the structured error code associated with this exception instance; defaults to `"ARY.UNKNOWN"` when no explicit non-empty error code is provided. |
| `Timestamp` | `DateTimeOffset` | Gets the UTC timestamp indicating when this exception instance was created.                                                                            |

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
using System;
using Allyaria.Abstractions.Exceptions;

public class Example
{
    public void DoSomething()
    {
        try
        {
            // Some domain logic that fails
            throw new AryException(
                message: "An unexpected error occurred while processing the request.",
                errorCode: "ARY.PROCESSING.FAILURE");
        }
        catch (AryException ex)
        {
            Console.WriteLine($"Error code: {ex.ErrorCode}");
            Console.WriteLine($"Created at (UTC): {ex.Timestamp}");
            Console.WriteLine($"Message: {ex.Message}");
        }
    }
}
```

---

*Revision Date: 2025-11-08*
