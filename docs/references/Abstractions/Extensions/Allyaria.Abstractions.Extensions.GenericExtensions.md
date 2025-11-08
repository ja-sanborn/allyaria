# Allyaria.Abstractions.Extensions.GenericExtensions

`GenericExtensions` provides utility extensions for working with nullable value types.
These methods enable concise handling of default values when nullable structs (`T?`) are not assigned, helping to reduce
boilerplate and improve code readability in value initialization and configuration patterns.

## Constructors

`GenericExtensions` is a static class and cannot be instantiated.

## Properties

*None*

## Methods

| Name                                                    | Returns | Description                                                                                                                                                                                                      |
|---------------------------------------------------------|---------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `OrDefault<T>(this T? value, T defaultValue = default)` | `T`     | Returns the provided nullable value if it is not `null`; otherwise, returns the specified `defaultValue` or the default for `T` when omitted. Useful for simplifying null-coalescing logic for nullable structs. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using System;
using Allyaria.Abstractions.Extensions;

public class Example
{
    public void ShowOrDefaultUsage()
    {
        int? number = null;

        // Returns the provided default value (42)
        int result1 = number.OrDefault(42);
        Console.WriteLine(result1); // Output: 42

        // Returns the type’s default (0) because no defaultValue is specified
        int result2 = number.OrDefault();
        Console.WriteLine(result2); // Output: 0

        // Returns the original value since it's not null
        number = 7;
        int result3 = number.OrDefault(99);
        Console.WriteLine(result3); // Output: 7
    }
}
```

---

*Revision Date: 2025-11-08*
