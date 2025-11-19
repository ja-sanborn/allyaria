# Allyaria.Theming.StyleTypes.StyleGroup

`StyleGroup` is a sealed record that groups four logical CSS values—block-start, block-end, inline-start,
inline-end—into a unified style object. It implements `IStyleValue` so that grouped values can participate seamlessly in
the Allyaria theming pipeline. A `StyleGroup` knows how to generate the correct CSS properties using the associated
`StyleGroupType`.

## Summary

`StyleGroup` represents multi-direction grouped CSS values such as margin, padding, border width, border radius, and
border color groups. Rather than specifying each logical direction individually, a `StyleGroup` collects them into a
single validated style construct. It supports automatic expansion into the correct CSS properties (via `StyleGroupType`)
and exposes a combined logical value through the `Value` property.

## Constructors

`StyleGroup(StyleGroupType type, IStyleValue value)` Creates a group using the same style `value` for all four logical
directions.

`StyleGroup(StyleGroupType type, IStyleValue block, IStyleValue inline)` Creates a group using one value for both
block-start & block-end, and one value for both inline-start & inline-end.

`StyleGroup(StyleGroupType type, IStyleValue blockStart, IStyleValue blockEnd, IStyleValue inlineStart, IStyleValue inlineEnd)`
Creates a group using fully explicit values for each of the four logical directions.

## Properties

| Name          | Type             | Description                                                                        |
|---------------|------------------|------------------------------------------------------------------------------------|
| `BlockEnd`    | `IStyleValue`    | The style value for the block-end logical direction.                               |
| `BlockStart`  | `IStyleValue`    | The style value for the block-start logical direction.                             |
| `InlineEnd`   | `IStyleValue`    | The style value for the inline-end logical direction.                              |
| `InlineStart` | `IStyleValue`    | The style value for the inline-start logical direction.                            |
| `Type`        | `StyleGroupType` | Determines which CSS properties are generated when the group is serialized.        |
| `Value`       | `string`         | Composite logical value in order: `block-end block-start inline-end inline-start`. |

## Methods

| Name                              | Returns  | Description                                                                                        |
|-----------------------------------|----------|----------------------------------------------------------------------------------------------------|
| `ToCss(string? varPrefix = null)` | `string` | Serializes the grouped styles into their actual CSS declarations using property names from `Type`. |
| `Equals(object? obj)`             | `bool`   | Record-based structural equality.                                                                  |
| `Equals(StyleGroup? other)`       | `bool`   | Structural equality between two `StyleGroup` instances.                                            |
| `GetHashCode()`                   | `int`    | Returns a hash code based on record value semantics.                                               |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.StyleTypes;
using Allyaria.Theming.Enumerations;

public class StyleGroupDemo
{
    public void Demo()
    {
        // Create a padding group using separate block and inline sizes
        var padding = new StyleGroup(
            type: StyleGroupType.Padding,
            block: new StyleLength("16px"),
            inline: new StyleLength("8px")
        );

        string logicalValue = padding.Value;
        // "16px 16px 8px 8px"

        // Convert to actual CSS string
        string css = padding.ToCss();
        // Produces:
        // padding-block-end: 16px;
        // padding-block-start: 16px;
        // padding-inline-end: 8px;
        // padding-inline-start: 8px;

        // Single-value constructor applies to all directions
        var margin = new StyleGroup(
            type: StyleGroupType.Margin,
            value: new StyleLength("24px")
        );
    }
}
```

---

*Revision Date: 2025-11-15*
