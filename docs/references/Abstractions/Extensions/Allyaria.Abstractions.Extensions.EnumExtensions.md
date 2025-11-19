# Allyaria.Abstractions.Extensions.EnumExtensions

`EnumExtensions` provides utility methods for working with .NET `Enum` values, including user-friendly description
retrieval with attribute-based and fallback resolution.
It automatically caches results for performance and supports `[Flags]` enums by handling composite values intelligently.

This type enhances enum usability for UI rendering, logging, and diagnostics by offering fast, readable text conversions
without requiring repeated reflection or manual mapping.

## Constructors

`EnumExtensions` is a static class and cannot be instantiated.

## Properties

*None*

## Methods

| Name                                      | Returns  | Description                                                                                                                                                                                                                                                                               |
|-------------------------------------------|----------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GetDescription(this Enum value)`         | `string` | Retrieves a user-friendly description for the specified enum value. Returns the `DescriptionAttribute` text if defined, otherwise returns a humanized fallback derived from the enum member name. Handles `[Flags]` combinations by resolving and joining individual member descriptions. |
| `GetDescription<TEnum>(this TEnum value)` | `string` | Strongly typed convenience overload of `GetDescription(Enum)` for generic enum types.                                                                                                                                                                                                     |

## Operators

*None*

## Events

*None*

## Exceptions

| Exception Type         | Description                    |
|------------------------|--------------------------------|
| `AryArgumentException` | Thrown when `value` is `null`. |

## Example

```csharp
using System;
using System.ComponentModel;
using Allyaria.Abstractions.Extensions;

[Flags]
public enum Permissions
{
    [Description("Read Access")]
    Read = 1,

    [Description("Write Access")]
    Write = 2,

    Execute = 4
}

public class Example
{
    public void ShowDescriptions()
    {
        var single = Permissions.Read;
        Console.WriteLine(single.GetDescription()); // "Read Access"

        var combined = Permissions.Read | Permissions.Execute;
        Console.WriteLine(combined.GetDescription()); // "Read Access, Execute"
    }
}
```

---

*Revision Date: 2025-11-08*
