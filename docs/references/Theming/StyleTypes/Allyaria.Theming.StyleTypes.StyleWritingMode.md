# Allyaria.Theming.StyleTypes.StyleWritingMode

`StyleWritingMode` is a sealed style value record representing the CSS `writing-mode` property within the Allyaria
theming system. It inherits from `StyleValueBase` (and therefore implements `IStyleValue`) and provides a strongly typed
wrapper over all standard CSS writing-mode directions.

## Summary

`StyleWritingMode` is an immutable, validated CSS value describing the direction in which text and block content flow.
Its nested `Kind` enum exposes every valid CSS `writing-mode` keyword, mapping each to its canonical CSS string via
`[Description]` attributes. The type supports parsing, safe parsing (`TryParse`), and implicit conversions, while
guaranteeing CSS-safe output through validation performed in `StyleValueBase`.

## Constructors

`StyleWritingMode(Kind kind)` Creates a new instance representing the specified writing mode. The enum value is
converted to its CSS keyword and validated through the `StyleValueBase` base constructor.

## Properties

| Name    | Type     | Description                                                                           |
|---------|----------|---------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `writing-mode` keyword (e.g., `"horizontal-tb"`, `"vertical-rl"`). |

## Methods

| Name                                                    | Returns            | Description                                                                                   |
|---------------------------------------------------------|--------------------|-----------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                  | `StyleWritingMode` | Parses a CSS `writing-mode` keyword string into a strongly typed instance; throws if invalid. |
| `TryParse(string? value, out StyleWritingMode? result)` | `bool`             | Attempts to parse without throwing; returns `true` on success.                                |
| `Equals(object? obj)`                                   | `bool`             | Structural equality comparison.                                                               |
| `Equals(StyleWritingMode? other)`                       | `bool`             | Strongly typed structural equality comparison.                                                |
| `GetHashCode()`                                         | `int`              | Computes a structural hash code.                                                              |

## Nested Types

| Name   | Type   | Description                                          |
|--------|--------|------------------------------------------------------|
| `Kind` | `enum` | Defines all valid CSS `writing-mode` keyword values. |

### `StyleWritingMode.Kind` Members

| Name           | Description                                                                              |
|----------------|------------------------------------------------------------------------------------------|
| `HorizontalTb` | `"horizontal-tb"` — Standard horizontal flow: left→right text, lines stacked top→bottom. |
| `SidewaysLr`   | `"sideways-lr"` — Sideways flow left→right; vertical lines stacked horizontally.         |
| `SidewaysRl`   | `"sideways-rl"` — Sideways flow right→left; vertical lines stacked horizontally.         |
| `VerticalLr`   | `"vertical-lr"` — Vertical flow top→bottom; lines stacked left→right.                    |
| `VerticalRl`   | `"vertical-rl"` — Vertical flow top→bottom; lines stacked right→left.                    |

## Operators

| Operator                                                       | Returns            | Description                                         |
|----------------------------------------------------------------|--------------------|-----------------------------------------------------|
| `implicit operator StyleWritingMode(string? value)`            | `StyleWritingMode` | Converts the string into a typed value via `Parse`. |
| `implicit operator string(StyleWritingMode? value)`            | `string`           | Returns the instance’s CSS keyword or `""` if null. |
| `operator ==(StyleWritingMode? left, StyleWritingMode? right)` | `bool`             | Structural equality comparison.                     |
| `operator !=(StyleWritingMode? left, StyleWritingMode? right)` | `bool`             | Structural inequality comparison.                   |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown when parsing fails (via `Parse` or implicit string→`StyleWritingMode`) because the input does not match a valid
  `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the final CSS string contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class WritingModeDemo
{
    public void Demo()
    {
        // Construct from enum
        var vertical = new StyleWritingMode(StyleWritingMode.Kind.VerticalRl);
        string cssVertical = vertical.Value;         // "vertical-rl"

        // Parse from string
        var horizontal = StyleWritingMode.Parse("horizontal-tb");
        string cssHorizontal = horizontal;           // "horizontal-tb"

        // TryParse
        if (StyleWritingMode.TryParse("sideways-lr", out var parsed))
        {
            string css = parsed!.Value;             // "sideways-lr"
        }

        // Implicit conversion
        StyleWritingMode implicitMode = "vertical-lr";
        string cssImplicit = implicitMode;          // "vertical-lr"
    }
}
```

---

*Revision Date: 2025-11-15*
