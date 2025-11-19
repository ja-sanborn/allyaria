# Allyaria.Theming.Enumerations.FontFaceType

`FontFaceType` is an enumeration describing the font classifications supported by the Allyaria theming system. These
values are used to map UI components to appropriate typography styles based on semantic purpose, readability needs, or
brand design requirements.

## Summary

`FontFaceType` is an enum representing different categories of font faces. The theming engine applies these
classifications to automatically select or generate CSS variables, font stacks, and typography rules across the UI.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name        | Type           | Description                                                                                                                     |
|-------------|----------------|---------------------------------------------------------------------------------------------------------------------------------|
| `Monospace` | `FontFaceType` | A monospaced font where every character has equal width. Suitable for code, tabular data, or technical text.                    |
| `SansSerif` | `FontFaceType` | A sans-serif font characterized by clean, modern strokes without decorative endings. Used for general UI text and body content. |
| `Serif`     | `FontFaceType` | A serif font with decorative strokes on letter endings. Commonly used for headings, branding, or formal typography.             |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class TypographySettings
{
    public FontFaceType FontFace { get; set; } = FontFaceType.SansSerif;

    public void UseMonospace()
    {
        FontFace = FontFaceType.Monospace;
    }
}
```

---

*Revision Date: 2025-11-15*
