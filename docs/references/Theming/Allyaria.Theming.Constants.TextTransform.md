# Allyaria.Theming.Constants.TextTransform

`TextTransform` provides a strongly typed collection of constants representing standard CSS text-transform values.
It enables consistent and localized text casing transformations across Allyaria typography and component styles.

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

* Each constant is an immutable `AryStringValue` that maps directly to a CSS `text-transform` keyword.
* Used in typography theming, component styling, and accessibility-driven text presentation.
* Ensures consistent casing transformations across all surfaces without relying on inline CSS or string operations.
* Particularly useful for button labels, headings, and accent text components.

## Members

| Name         | Type             | Description                                |
|--------------|------------------|--------------------------------------------|
| `Capitalize` | `AryStringValue` | Capitalizes the first letter of each word. |
| `Lowercase`  | `AryStringValue` | Transforms all characters to lowercase.    |
| `None`       | `AryStringValue` | Applies no text transformation.            |
| `Uppercase`  | `AryStringValue` | Transforms all characters to uppercase.    |

## Examples

### Minimal Example

```csharp
var transform = TextTransform.Uppercase;
```

### Expanded Example

```csharp
public string BuildTextTransformCss(AryStringValue transform)
{
    return $"text-transform: {transform};";
}

// Example usage:
var css = BuildTextTransformCss(TextTransform.Capitalize);
// "text-transform: capitalize;"
```

> *Rev Date: 2025-10-06*
