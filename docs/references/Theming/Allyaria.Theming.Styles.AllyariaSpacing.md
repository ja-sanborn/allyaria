# Allyaria.Theming.Styles.AllyariaSpacing

`AllyariaSpacing` is a strongly typed spacing definition for Allyaria theming.
It represents **margins and paddings per side**, each as an optional `AllyariaNumberValue` (e.g., `16px`, `1rem`, `8%`).
Unset sides are omitted from CSS emission. Supports non-destructive overrides via `Cascade`.

## Constructors

`AllyariaSpacing(AllyariaNumberValue? marginTop = null, AllyariaNumberValue? marginRight = null, AllyariaNumberValue? marginBottom = null, AllyariaNumberValue? marginLeft = null, AllyariaNumberValue? paddingTop = null, AllyariaNumberValue? paddingRight = null, AllyariaNumberValue? paddingBottom = null, AllyariaNumberValue? paddingLeft = null)`
Initializes spacing with optional per-side margins and paddings (TRBL order). Unset values are ignored when generating
CSS.

* Exceptions: *None*

## Properties

| Name            | Type                   | Description                               |
|-----------------|------------------------|-------------------------------------------|
| `MarginTop`     | `AllyariaNumberValue?` | Margin-top value, or `null` if unset.     |
| `MarginRight`   | `AllyariaNumberValue?` | Margin-right value, or `null` if unset.   |
| `MarginBottom`  | `AllyariaNumberValue?` | Margin-bottom value, or `null` if unset.  |
| `MarginLeft`    | `AllyariaNumberValue?` | Margin-left value, or `null` if unset.    |
| `PaddingTop`    | `AllyariaNumberValue?` | Padding-top value, or `null` if unset.    |
| `PaddingRight`  | `AllyariaNumberValue?` | Padding-right value, or `null` if unset.  |
| `PaddingBottom` | `AllyariaNumberValue?` | Padding-bottom value, or `null` if unset. |
| `PaddingLeft`   | `AllyariaNumberValue?` | Padding-left value, or `null` if unset.   |

## Methods

| Name                                                                                                              | Returns           | Description                                                                                                   |
|-------------------------------------------------------------------------------------------------------------------|-------------------|---------------------------------------------------------------------------------------------------------------|
| `Cascade(marginTop, marginRight, marginBottom, marginLeft, paddingTop, paddingRight, paddingBottom, paddingLeft)` | `AllyariaSpacing` | Returns a new spacing definition with specified overrides, retaining existing values where not provided.      |
| `ToCss()`                                                                                                         | `string`          | Builds inline CSS declarations (e.g., `margin-top:8px;padding-right:12px;`). Only non-null sides are emitted. |
| `ToCssVars(prefix = "")`                                                                                          | `string`          | Builds CSS custom property declarations. Prefix normalized to kebab-case; defaults to `--aa-` if omitted.     |

## Operators

| Operator    | Returns | Description          |
|-------------|---------|----------------------|
| `==` / `!=` | `bool`  | Equality comparison. |

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* Unset values (`null`) are skipped when emitting CSS.
* `ToCssVars` normalizes prefixes:

    * `"editor theme"` → `--editor-theme-`.
    * Empty prefix defaults to `--aa-`.
* Designed for inline styles and CSS variable emission in theming scenarios.
* Immutable; use `Cascade` to apply changes non-destructively.

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

var spacing = new AllyariaSpacing(paddingTop: new AllyariaNumberValue("8px"));
Console.WriteLine(spacing.ToCss()); 
// "padding-top:8px;"
```

### Expanded Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

public class SpacingDemo
{
    public void ApplySpacing()
    {
        var baseSpacing = new AllyariaSpacing(
            marginTop: new AllyariaNumberValue("8px"),
            paddingLeft: new AllyariaNumberValue("16px")
        );

        var updated = baseSpacing.Cascade(
            marginBottom: new AllyariaNumberValue("12px"),
            paddingRight: new AllyariaNumberValue("24px")
        );

        Console.WriteLine("Base: " + baseSpacing.ToCss());
        Console.WriteLine("Updated: " + updated.ToCssVars("btn"));
    }
}
```

> *Rev Date: 2025-10-01*
