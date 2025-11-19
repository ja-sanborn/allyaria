# Allyaria.Theming.Enumerations.StyleGroupType

`StyleGroupType` is an enumeration defining logical groups of related CSS properties used by the Allyaria theming
engine. These groupings represent sets of semantically connected CSS attributes—such as border, margin, or padding
properties—allowing unified styling operations when applying themes or generating isolated component styles.

## Summary

`StyleGroupType` is an enum representing thematically grouped CSS property sets. It enables the theming system to modify
related CSS rules as coherent units, simplifying style generation and ensuring consistent directional behavior in block
and inline contexts.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name           | Type             | Description                                                                                                                                     |
|----------------|------------------|-------------------------------------------------------------------------------------------------------------------------------------------------|
| `BorderColor`  | `StyleGroupType` | Group of border color properties: `border-block-end-color`, `border-block-start-color`, `border-inline-end-color`, `border-inline-start-color`. |
| `BorderRadius` | `StyleGroupType` | Group of border radius properties: `border-end-end-radius`, `border-end-start-radius`, `border-start-end-radius`, `border-start-start-radius`.  |
| `BorderStyle`  | `StyleGroupType` | Group of border style properties: `border-block-end-style`, `border-block-start-style`, `border-inline-end-style`, `border-inline-start-style`. |
| `BorderWidth`  | `StyleGroupType` | Group of border width properties: `border-block-end-width`, `border-block-start-width`, `border-inline-end-width`, `border-inline-start-width`. |
| `Margin`       | `StyleGroupType` | Group of margin properties: `margin-block-end`, `margin-block-start`, `margin-inline-end`, `margin-inline-start`.                               |
| `Padding`      | `StyleGroupType` | Group of padding properties: `padding-block-end`, `padding-block-start`, `padding-inline-end`, `padding-inline-start`.                          |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class CssGroupExample
{
    public StyleGroupType Group { get; } = StyleGroupType.BorderColor;

    public string GetCssProperties()
    {
        return Group switch
        {
            StyleGroupType.BorderColor =>
                "border-block-end-color, border-block-start-color, border-inline-end-color, border-inline-start-color",
            StyleGroupType.Padding =>
                "padding-block-start, padding-block-end, padding-inline-start, padding-inline-end",
            _ => "unknown"
        };
    }
}
```

---

*Revision Date: 2025-11-15*
