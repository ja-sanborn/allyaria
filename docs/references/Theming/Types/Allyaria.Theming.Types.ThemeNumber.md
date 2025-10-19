# Allyaria.Theming.Types.ThemeNumber

`ThemeNumber` is a normalized numeric theming value with an optional CSS length unit. It parses inputs such as `"12px"`,
`"-3.5 rem"`, `"100%"`, `"42"`, or `"auto"`, and exposes a canonical string (`Value`) along with the parsed components (
`Number` and `LengthUnit`). Ordering, equality, hashing, and CSS serialization are provided via merged base behavior,
using ordinal semantics over the canonical string.

## Constructors

`ThemeNumber(string value)` Initializes a new instance by parsing and normalizing the input (e.g., `"12px"`, `"100%"`,
`"-3.5"`, `"auto"`).

## Properties

| Name         | Type           | Description                                                                   |
|--------------|----------------|-------------------------------------------------------------------------------|
| `Value`      | `string`       | The canonical string form (e.g., `"12px"`, `"100%"`, `"-3.5"`, `"auto"`).     |
| `LengthUnit` | `LengthUnits?` | The parsed CSS length unit, or `null` if unitless or `"auto"`.                |
| `Number`     | `double`       | The parsed numeric value using invariant-culture semantics (0 when `"auto"`). |

## Methods

| Name                                              | Returns       | Description                                                                                                |
|---------------------------------------------------|---------------|------------------------------------------------------------------------------------------------------------|
| `Compare(ThemeBase? left, ThemeBase? right)`      | `int`         | Compares two values using ordinal comparison over their canonical strings; throws if runtime types differ. |
| `CompareTo(ThemeBase? other)`                     | `int`         | Compares this instance to another value using ordinal semantics; throws if runtime types differ.           |
| `Equals(object? obj)`                             | `bool`        | Determines equality with any object; true only for same runtime type and ordinal-equal `Value`.            |
| `Equals(ThemeBase? other)`                        | `bool`        | Determines equality with another value of the same runtime type using ordinal semantics.                   |
| `Equals(ThemeBase? left, ThemeBase? right)`       | `bool`        | Determines equality between two values (both `null` or ordinal-equal).                                     |
| `GetHashCode()`                                   | `int`         | Returns an ordinal hash code for `Value`.                                                                  |
| `ToCss(string propertyName)`                      | `string`      | Formats as a CSS declaration `"property:value;"`; lowercases property unless it starts with `"--"`.        |
| `ToString()`                                      | `string`      | Returns the canonical `Value`.                                                                             |
| `Parse(string value)`                             | `ThemeNumber` | Parses a string into a new `ThemeNumber`.                                                                  |
| `TryParse(string value, out ThemeNumber? result)` | `bool`        | Attempts to parse a string; returns `true` on success with `result` set.                                   |

## Operators

| Operator                                      | Returns       | Description                                                                       |
|-----------------------------------------------|---------------|-----------------------------------------------------------------------------------|
| `==`                                          | `bool`        | Equality based on ordinal comparison of canonical strings.                        |
| `!=`                                          | `bool`        | Inequality based on ordinal comparison.                                           |
| `>`                                           | `bool`        | Greater-than using ordinal ordering; operands must be same runtime type.          |
| `<`                                           | `bool`        | Less-than using ordinal ordering; operands must be same runtime type.             |
| `>=`                                          | `bool`        | Greater-than-or-equal using ordinal ordering; operands must be same runtime type. |
| `<=`                                          | `bool`        | Less-than-or-equal using ordinal ordering; operands must be same runtime type.    |
| `implicit operator ThemeNumber(string value)` | `ThemeNumber` | Converts a `string` to an `ThemeNumber` via parsing/normalization.                |
| `implicit operator string(ThemeNumber value)` | `string`      | Converts an `ThemeNumber` to its canonical `Value`.                               |

## Events

*None*

## Exceptions

* `AryArgumentException` — Thrown for invalid input: `null`, empty/whitespace, control characters, unsupported unit
  token, or non-finite/invalid number.
* `AryArgumentException` — Thrown when comparing instances of different concrete types.
* `AryArgumentException` — Thrown by `ToCss` when `propertyName` is `null`, empty/whitespace, or contains control
  characters.

## Example

```csharp
var width = new ThemeNumber(" 1.5 rem ");
Console.WriteLine(width.Value);          // "1.5rem"
Console.WriteLine(width.Number);         // 1.5
Console.WriteLine(width.LengthUnit);     // LengthUnits.Rem (or enum value)
Console.WriteLine(width.ToCss("width")); // "width:1.5rem;"
```

---

*Revision Date: 2025-10-17*
