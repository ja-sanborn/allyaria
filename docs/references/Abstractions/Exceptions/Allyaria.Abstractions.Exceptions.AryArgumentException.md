# Allyaria.Abstractions.Exceptions.AryArgumentException

`AryArgumentException` is a specialized exception that represents errors arising from invalid or unexpected argument
values. It extends `AryException` to include contextual details about the argument name and value, supporting Allyaria’s
structured exception model for precise debugging and error categorization.

This type is typically thrown when a method detects that one of its input arguments is invalid, missing, or inconsistent
with expected constraints.

## Constructors

`AryArgumentException(string? message = null, string? argName = null, object? argValue = null, string? errorCode = null, Exception? innerException = null)`
Initializes a new instance of the `AryArgumentException` class with an optional message, argument name, argument value,
structured error code, and inner exception. If no `errorCode` is specified or it is empty, the default value
`"ARY.ARGUMENT"` is used.

## Properties

| Name       | Type      | Description                                                                          |
|------------|-----------|--------------------------------------------------------------------------------------|
| `ArgName`  | `string?` | Gets the name of the argument that caused the exception, or `null` if not provided.  |
| `ArgValue` | `object?` | Gets the value of the argument that caused the exception, or `null` if not provided. |

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
    public void ProcessData(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new AryArgumentException(
                message: "Input cannot be null or empty.",
                argName: nameof(input),
                argValue: input);

        Console.WriteLine($"Processing '{input}'...");
    }
}
```

---

*Revision Date: 2025-11-08*
