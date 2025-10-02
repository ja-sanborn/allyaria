# Allyaria.Theming.Values.AllyariaNumberValue

`AllyariaNumberValue` is a sealed, immutable theming type that represents a numeric value with an optional CSS length
unit. It *inherits from `ValueBase`*, gaining canonical string storage, equality, ordering, and CSS emission. Input
strings are validated and normalized into a canonical representation, while numeric and unit components are parsed
for programmatic use.

---

## Constructors

`AllyariaNumberValue(string value)`
Parses and normalizes a CSS numeric string (e.g., `"12px"`, `"100%"`, `"-3.5"`, `"auto"`).
After parsing:

* `Value` stores the canonical form.

* `Number` stores the parsed double (0 when `"auto"`).

* `LengthUnit` stores the parsed unit or `null` when unitless/auto.

* Exceptions:

    * `AllyariaArgumentException` — when the input is null/whitespace, invalid format, non-finite number, or unsupported
      unit.

---

## Properties

| Name         | Type           | Description                                       |
|--------------|----------------|---------------------------------------------------|
| `Value`      | `string`       | Canonical string form (e.g., `"12px"`, `"auto"`). |
| `Number`     | `double`       | Parsed numeric component (0 if `"auto"`).         |
| `LengthUnit` | `LengthUnits?` | Parsed unit enum (null if unitless or `"auto"`).  |

---

## Methods

### AllyariaNumberValue-specific

| Name                                                      | Returns               | Description                                         |
|-----------------------------------------------------------|-----------------------|-----------------------------------------------------|
| `Parse(string value)`                                     | `AllyariaNumberValue` | Parses a numeric string or throws on invalid input. |
| `TryParse(string value, out AllyariaNumberValue? result)` | `bool`                | Attempts parsing; returns false on failure.         |

### Inherited from `ValueBase`

| Name                                         | Returns  | Description                                                        |
|----------------------------------------------|----------|--------------------------------------------------------------------|
| `Compare(ValueBase? left, ValueBase? right)` | `int`    | Static comparison by `Value`. Throws if runtime types differ.      |
| `CompareTo(ValueBase? other)`                | `int`    | Ordinal comparison with another `AllyariaNumberValue`.             |
| `Equals(object? obj)`                        | `bool`   | Equality by canonical `Value`.                                     |
| `Equals(ValueBase? other)`                   | `bool`   | Equality with another `ValueBase` of the same type.                |
| `GetHashCode()`                              | `int`    | Ordinal hash of `Value`.                                           |
| `ToCss(string propertyName)`                 | `string` | `"property:value;"` when property provided, otherwise raw `Value`. |
| `ToString()`                                 | `string` | Returns `Value`.                                                   |

---

## Operators

| Operator                                 | Returns               | Description                                                                                |
|------------------------------------------|-----------------------|--------------------------------------------------------------------------------------------|
| `implicit string -> AllyariaNumberValue` | `AllyariaNumberValue` | Parses and normalizes a string.                                                            |
| `implicit AllyariaNumberValue -> string` | `string`              | Returns canonical string (`Value`).                                                        |
| `==`, `!=`                               | `bool`                | Ordinal equality by canonical `Value`. Only equal for same type.                           |
| `>`, `<`, `>=`, `<=`                     | `bool`                | Ordinal ordering by canonical `Value`. Throws if comparing across different derived types. |

---

## Events

*None*

---

## Exceptions

* `AllyariaArgumentException` — invalid/null input, unsupported unit, non-finite number, or comparison across different
  `ValueBase`-derived types.
* `NullReferenceException` — if converting a null instance to string via implicit operator.

---

## Behavior Notes

* Recognizes `"auto"` as a special case: canonical `Value` = `"auto"`, `Number` = 0, `LengthUnit` = null.
* Supported units are those defined in `LengthUnits` enum via `DescriptionAttribute` (e.g., `px`, `rem`, `%`, `em`).
* Input allows optional sign, decimals, whitespace around number and between number and unit.
* Canonical formatting uses:

    * `"0.#######"` for typical values.
    * `"G17"` for very small/large magnitudes to avoid scientific notation.
* Equality and ordering are strict ordinal comparisons of canonical string values.

---

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Values;

var size = new AllyariaNumberValue(" 12 px ");
Console.WriteLine(size.Value);   // "12px"
Console.WriteLine(size.Number);  // 12
Console.WriteLine(size.LengthUnit); // LengthUnits.Px
```

### Expanded Example

```csharp
using Allyaria.Theming.Values;

public class NumberDemo
{
    public void Demo()
    {
        var v1 = AllyariaNumberValue.Parse("100%");
        var v2 = AllyariaNumberValue.Parse("-3.5rem");
        var v3 = new AllyariaNumberValue("auto");

        Console.WriteLine(v1.ToCss("width"));  // "width:100%;"
        Console.WriteLine(v2.Number);          // -3.5
        Console.WriteLine(v3.Value);           // "auto"

        if (AllyariaNumberValue.TryParse("42em", out var parsed))
        {
            Console.WriteLine(parsed!.LengthUnit); // LengthUnits.Em
        }
    }
}
```

> *Rev Date: 2025-10-01*
