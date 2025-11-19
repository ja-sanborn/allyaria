# Allyaria.Abstractions.Exceptions.AryInvalidOperationException

`AryInvalidOperationException` represents errors that occur when a method call is invalid for the object's current
state. It extends `AryException` to maintain Allyaria’s structured exception handling semantics and ensure consistent,
application-wide error reporting.

This exception is typically thrown when a requested operation cannot be performed due to the object being in an
inappropriate state — for example, when calling a method at an invalid time or after disposal.

## Constructors

`AryInvalidOperationException(string? message = null, string? errorCode = null, Exception? innerException = null)`
Initializes a new instance of the `AryInvalidOperationException` class with an optional message, structured error code,
and inner exception. If no `errorCode` is specified or it is empty, the default value `"ARY.INVALID_OPERATION"` is used.

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
    private bool isInitialized;

    public void Execute()
    {
        if (!isInitialized)
            throw new AryInvalidOperationException(
                message: "Cannot execute before initialization.");

        Console.WriteLine("Operation executed successfully.");
    }

    public void Initialize()
    {
        isInitialized = true;
    }
}
```

---

*Revision Date: 2025-11-08*
