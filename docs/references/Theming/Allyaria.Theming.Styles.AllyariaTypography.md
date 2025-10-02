# Allyaria.Theming.Styles.AllyariaTypography

`AllyariaTypography` is a strongly typed typography definition for Allyaria theming.
It encapsulates optional font properties and provides conversion to inline CSS and CSS variables.
Unset values are omitted from CSS emission. Supports non-destructive overrides via `Cascade`.

## Constructors

`AllyariaTypography(AllyariaStringValue? fontFamily = null, AllyariaNumberValue? fontSize = null, AllyariaStringValue? fontStyle = null, AllyariaStringValue? fontWeight = null, AllyariaNumberValue? letterSpacing = null, AllyariaNumberValue? lineHeight = null, AllyariaStringValue? textAlign = null, AllyariaStringValue? textDecoration = null, AllyariaStringValue? textTransform = null, AllyariaStringValue? verticalAlign = null, AllyariaNumberValue? wordSpacing = null)`
Initializes typography with optional font family, size, style, weight, spacing, alignment, and decoration values.
Any parameter left `null` is considered unset.

* Exceptions: *None*

## Properties

| Name             | Type                   | Description                                                |
|------------------|------------------------|------------------------------------------------------------|
| `FontFamily`     | `AllyariaStringValue?` | Font family token (e.g., `Inter, "Segoe UI", sans-serif`). |
| `FontSize`       | `AllyariaNumberValue?` | Font size token (e.g., `14px`, `1rem`).                    |
| `FontStyle`      | `AllyariaStringValue?` | Font style (e.g., `normal`, `italic`).                     |
| `FontWeight`     | `AllyariaStringValue?` | Font weight (e.g., `400`, `bold`, `600`).                  |
| `LetterSpacing`  | `AllyariaNumberValue?` | Letter spacing (e.g., `0.02em`).                           |
| `LineHeight`     | `AllyariaNumberValue?` | Line height (e.g., `1.5`, `24px`).                         |
| `TextAlign`      | `AllyariaStringValue?` | Text alignment (e.g., `start`, `center`).                  |
| `TextDecoration` | `AllyariaStringValue?` | Text decoration (e.g., `underline`, `none`).               |
| `TextTransform`  | `AllyariaStringValue?` | Text transform (e.g., `uppercase`).                        |
| `VerticalAlign`  | `AllyariaStringValue?` | Vertical alignment (e.g., `baseline`, `middle`).           |
| `WordSpacing`    | `AllyariaNumberValue?` | Word spacing (e.g., `0.1em`).                              |

## Methods

| Name                                                                                                                                                    | Returns              | Description                                                                                      |
|---------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------|--------------------------------------------------------------------------------------------------|
| `Cascade(fontFamily, fontSize, fontStyle, fontWeight, letterSpacing, lineHeight, textAlign, textDecoration, textTransform, verticalAlign, wordSpacing)` | `AllyariaTypography` | Returns a new instance with overrides applied; retains existing values where not specified.      |
| `ToCss()`                                                                                                                                               | `string`             | Builds inline CSS declarations for non-null properties.                                          |
| `ToCssVars(prefix = "")`                                                                                                                                | `string`             | Builds CSS variable declarations. Normalizes prefix to kebab-case; defaults to `--aa-` if empty. |

## Operators

| Operator    | Returns | Description                                           |
|-------------|---------|-------------------------------------------------------|
| `==` / `!=` | `bool`  | Equality comparison (inherited from `record struct`). |

## Events

*None*

## Exceptions

*None*

## Behavior Notes

* All properties are optional.
* Unset values are not included in generated CSS.
* `ToCssVars` normalizes prefixes:

    * `"Editor Typography"` → `--editor-typography-`.
    * Empty prefix defaults to `--aa-`.
* Designed for inline CSS and theme variable emission.
* Immutable; use `Cascade` to apply overrides non-destructively.

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

var typography = new AllyariaTypography(fontSize: new AllyariaNumberValue("16px"));
Console.WriteLine(typography.ToCss()); 
// "font-size:16px;"
```

### Expanded Example

```csharp
using Allyaria.Theming.Styles;
using Allyaria.Theming.Values;

public class TypographyDemo
{
    public void ApplyTypography()
    {
        var baseTypography = new AllyariaTypography(
            fontFamily: new AllyariaStringValue("system-ui, sans-serif"),
            fontSize: new AllyariaNumberValue("14px"),
            lineHeight: new AllyariaNumberValue("1.5")
        );

        var updated = baseTypography.Cascade(
            fontWeight: new AllyariaStringValue("600"),
            letterSpacing: new AllyariaNumberValue("0.02em")
        );

        Console.WriteLine("Base: " + baseTypography.ToCss());
        Console.WriteLine("Updated: " + updated.ToCssVars("editor"));
    }
}
```

> *Rev Date: 2025-10-01*
