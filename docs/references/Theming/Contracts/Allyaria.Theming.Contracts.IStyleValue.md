# Allyaria.Theming.Contracts.IStyleValue

`IStyleValue` is an interface representing any validated, CSS-compatible style value used within the Allyaria theming
system. Implementations provide normalized string representations suitable for safe CSS serialization.

## Summary

`IStyleValue` serves as a contract for all style value types used throughout the theming engine. Any implementing type
must supply a `Value` property that returns a normalized, validated, and CSS-safe string ready for inclusion in style
rules or generated CSS variables.

## Constructors

*None*

## Properties

| Name    | Type     | Description                                                                   |
|---------|----------|-------------------------------------------------------------------------------|
| `Value` | `string` | Gets the validated and normalized CSS-safe representation of the style value. |

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
using Allyaria.Theming.Contracts;

public sealed class PixelValue : IStyleValue
{
    public PixelValue(int px)
    {
        Value = $"{px}px";
    }

    public string Value { get; }
}

// Usage:
IStyleValue padding = new PixelValue(8);
string css = padding.Value; // "8px"
```

---

*Revision Date: 2025-11-15*
