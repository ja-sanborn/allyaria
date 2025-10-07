# Allyaria.Theming.Constants.TextAlign

`TextAlign` provides a strongly typed collection of constants representing standard CSS text alignment values.
It enables consistent alignment behavior across Allyaria components and theming systems with type-safe property
definitions.

## Constructors

*Static class — no constructors.*

## Properties

*None*

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Each property represents a CSS `text-align` keyword encapsulated in an immutable `AryStringValue`.
* The `Start` and `End` options respect the document’s writing mode and text direction (LTR/RTL).
* The constants are used by Allyaria typography and layout systems to ensure consistent rendering and localization-aware
  text alignment.
* Values can be serialized directly into CSS or used as part of theming definitions.

## Members

| Name      | Type             | Description                                                                |
|-----------|------------------|----------------------------------------------------------------------------|
| `Center`  | `AryStringValue` | Represents center-aligned text.                                            |
| `End`     | `AryStringValue` | Represents alignment at the end of the inline direction (RTL/LTR aware).   |
| `Justify` | `AryStringValue` | Represents justified text alignment.                                       |
| `Left`    | `AryStringValue` | Represents left-aligned text.                                              |
| `Match`   | `AryStringValue` | Represents alignment inherited from the parent element (`match-parent`).   |
| `Right`   | `AryStringValue` | Represents right-aligned text.                                             |
| `Start`   | `AryStringValue` | Represents alignment at the start of the inline direction (RTL/LTR aware). |

## Examples

### Minimal Example

```csharp
var align = TextAlign.Center;
```

### Expanded Example

```csharp
public string BuildTextAlignCss(AryStringValue align)
{
    return $"text-align: {align};";
}

// Example usage:
var css = BuildTextAlignCss(TextAlign.Justify);
// "text-align: justify;"
```

> *Rev Date: 2025-10-06*
