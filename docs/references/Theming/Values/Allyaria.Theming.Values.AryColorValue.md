# Allyaria.Theming.Values.AryColorValue

`AryColorValue` is a strongly typed, immutable theming color value expressed canonically as `#RRGGBBAA`. It supports
construction from RGBA components, `HexColor`, and parseable color strings (hex, Material Design names, and CSS web
color names).

## Constructors

`AryColorValue(byte red, byte green, byte blue, double alpha = 1.0)`
Initializes a new instance from RGBA components; `alpha` is clamped to a valid byte when converted.

`AryColorValue(HexColor color)`
Initializes a new instance from an existing `HexColor` without further parsing or normalization.

## Properties

| Name    | Type       | Description                                                         |
|---------|------------|---------------------------------------------------------------------|
| `Color` | `HexColor` | Gets the underlying RGBA components as a strongly typed `HexColor`. |
| `Value` | `string`   | Gets the canonical uppercase string representation `#RRGGBBAA`.     |

## Methods

| Name                                                                                                      | Returns         | Description                                                                                                   |
|-----------------------------------------------------------------------------------------------------------|-----------------|---------------------------------------------------------------------------------------------------------------|
| `EnsureContrast(AryColorValue backgroundColor)`                                                           | `AryColorValue` | Ensures at least a 4.5:1 contrast ratio against `backgroundColor`, returning an adjusted color if needed.     |
| `Parse(string value)`                                                                                     | `AryColorValue` | Parses a color string (hex, Material name, or CSS web color) into an `AryColorValue`.                         |
| `ToBorder(AryColorValue outerBackground, AryColorValue? componentFill = null, bool highContrast = false)` | `AryColorValue` | Resolves an accessible border color (≥3:1 contrast), preserving hue where possible.                           |
| `ToDisabled()`                                                                                            | `AryColorValue` | Returns a desaturated variant for disabled UI states.                                                         |
| `ToDragged(bool highContrast = false)`                                                                    | `AryColorValue` | Returns a brightened variant for dragged states; unchanged in high-contrast mode.                             |
| `ToElevation1(bool highContrast = false)`                                                                 | `AryColorValue` | Slightly elevates/lightens the color (Elevation 1); unchanged in high-contrast mode.                          |
| `ToElevation2(bool highContrast = false)`                                                                 | `AryColorValue` | Moderately elevates/lightens the color (Elevation 2); unchanged in high-contrast mode.                        |
| `ToElevation3(bool highContrast = false)`                                                                 | `AryColorValue` | Further elevates/lightens the color (Elevation 3); unchanged in high-contrast mode.                           |
| `ToElevation4(bool highContrast = false)`                                                                 | `AryColorValue` | Strongly elevates/lightens the color (Elevation 4); unchanged in high-contrast mode.                          |
| `ToFocused(bool highContrast = false)`                                                                    | `AryColorValue` | Slightly lighter variant for focused state; unchanged in high-contrast mode.                                  |
| `ToHovered(bool highContrast = false)`                                                                    | `AryColorValue` | Slightly lighter variant for hovered state; unchanged in high-contrast mode.                                  |
| `ToPressed(bool highContrast = false)`                                                                    | `AryColorValue` | Noticeably lighter variant for pressed state; unchanged in high-contrast mode.                                |
| `TryParse(string value, out AryColorValue? result)`                                                       | `bool`          | Attempts to parse a color string; returns `true` on success and sets `result`.                                |
| `Compare(ValueBase? left, ValueBase? right)`                                                              | `int`           | Compares two values using ordinal comparison of their canonical strings; `null` is treated as less.           |
| `CompareTo(ValueBase? other)`                                                                             | `int`           | Compares this instance to another value using ordinal semantics; requires same runtime type.                  |
| `Equals(object? obj)`                                                                                     | `bool`          | Determines equality with any object; true only for same runtime type and ordinal-equal `Value`.               |
| `Equals(ValueBase? other)`                                                                                | `bool`          | Determines equality with another theming value of the same runtime type using ordinal semantics.              |
| `Equals(ValueBase? left, ValueBase? right)`                                                               | `bool`          | Determines equality between two values (`null`-safe).                                                         |
| `GetHashCode()`                                                                                           | `int`           | Returns an ordinal hash code for `Value`.                                                                     |
| `ToCss(string propertyName)`                                                                              | `string`        | Formats the value as a CSS declaration `"property:value;"`; lowercases property unless it starts with `"--"`. |
| `ToString()`                                                                                              | `string`        | Returns the canonical `Value`.                                                                                |

## Operators

| Operator                                          | Returns         | Description                                                                                            |
|---------------------------------------------------|-----------------|--------------------------------------------------------------------------------------------------------|
| `==`                                              | `bool`          | Equality based on ordinal comparison of canonical strings; requires same runtime type when non-`null`. |
| `!=`                                              | `bool`          | Inequality based on ordinal comparison.                                                                |
| `>`                                               | `bool`          | Greater-than using ordinal ordering; operands must be the same runtime type.                           |
| `<`                                               | `bool`          | Less-than using ordinal ordering; operands must be the same runtime type.                              |
| `>=`                                              | `bool`          | Greater-than-or-equal using ordinal ordering; operands must be the same runtime type.                  |
| `<=`                                              | `bool`          | Less-than-or-equal using ordinal ordering; operands must be the same runtime type.                     |
| `implicit operator AryColorValue(HexColor value)` | `AryColorValue` | Wraps a `HexColor` as an `AryColorValue`.                                                              |
| `implicit operator HexColor(AryColorValue value)` | `HexColor`      | Extracts the underlying `HexColor`.                                                                    |
| `implicit operator AryColorValue(string value)`   | `AryColorValue` | Parses a color string into an `AryColorValue`.                                                         |
| `implicit operator string(AryColorValue value)`   | `string`        | Converts to the canonical `#RRGGBBAA` string.                                                          |

## Events

*None*

## Exceptions

* `AryArgumentException` — Thrown by `Parse` (and by implicit construction from `string`) when the color string cannot
  be parsed.
* `AryArgumentException` — Thrown by `ToCss` when `propertyName` is `null`, empty/whitespace, or contains control
  characters.
* `AryArgumentException` — Thrown by comparisons (`Compare`, `CompareTo`, relational operators) when operands are
  different runtime types.

## Example

```csharp
var bg = (AryColorValue)"#FFFFFF";
var fg = (AryColorValue)"#0066CC";

// Ensure readable foreground against background and emit CSS:
var readable = fg.EnsureContrast(bg);
Console.WriteLine(readable.ToCss("color")); // e.g., "color:#0D5DB3FF;"
```

---

*Revision Date: 2025-10-17*
