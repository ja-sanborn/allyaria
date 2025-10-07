# Allyaria.Theming.Values.AryStringValue

`AryStringValue` represents a theming string token with strict normalization (trimmed, non-null, no control characters).
It inherits all comparison, equality, hashing, CSS formatting, and operator behaviors from the underlying value
framework, and exposes them as part of this type’s API.

## Constructors

`AryStringValue(string value)`
Initializes a new instance with `value`, after trimming and validation (rejects null/empty/whitespace and control
characters).

* Exceptions: `AryArgumentException` (invalid input).

## Properties

| Name    | Type     | Description                                                              |
|---------|----------|--------------------------------------------------------------------------|
| `Value` | `string` | The normalized string value (ordinal semantics for comparison/equality). |

## Methods

| Name                                                        | Returns          | Description                                                                                            |
|-------------------------------------------------------------|------------------|--------------------------------------------------------------------------------------------------------|
| `static Compare(ValueBase? left, ValueBase? right)`         | `int`            | Ordinal comparison of two values of the **same runtime type**; `null` is less; throws if types differ. |
| `CompareTo(ValueBase? other)`                               | `int`            | Ordinal comparison with `other`; `this` > `null`; throws if types differ.                              |
| `Equals(object? obj)`                                       | `bool`           | True when `obj` is the same runtime type and `Value` matches ordinally.                                |
| `Equals(ValueBase? other)`                                  | `bool`           | True when both are the same runtime type and `Value` matches ordinally.                                |
| `static Equals(ValueBase? left, ValueBase? right)`          | `bool`           | Null-safe equality helper using ordinal semantics.                                                     |
| `GetHashCode()`                                             | `int`            | Ordinal hash code of `Value`.                                                                          |
| `ToCss(string propertyName)`                                | `string`         | Formats as CSS: `"property:value;"` if `propertyName` is provided; otherwise returns `Value`.          |
| `ToString()`                                                | `string`         | Returns `Value`.                                                                                       |
| `static Parse(string value)`                                | `AryStringValue` | Creates a new instance from `value` (throws on invalid).                                               |
| `static TryParse(string value, out AryStringValue? result)` | `bool`           | Attempts to parse; returns `false` on validation failure.                                              |

## Operators

| Operator                           | Returns          | Description                                                    |
|------------------------------------|------------------|----------------------------------------------------------------|
| `==`                               | `bool`           | Ordinal equality; null-safe.                                   |
| `!=`                               | `bool`           | Ordinal inequality; null-safe.                                 |
| `>`                                | `bool`           | True if left is ordinally greater (types must match).          |
| `>=`                               | `bool`           | True if left is ordinally greater or equal (types must match). |
| `<`                                | `bool`           | True if left is ordinally less (types must match).             |
| `<=`                               | `bool`           | True if left is ordinally less or equal (types must match).    |
| `implicit string → AryStringValue` | `AryStringValue` | Converts a `string` to `AryStringValue` (validates input).     |
| `implicit AryStringValue → string` | `string`         | Extracts the underlying normalized `Value`.                    |

## Events

*None*

## Exceptions

* `AryArgumentException` — Thrown when constructing/parsing with null/empty/whitespace or control characters; or when
  comparing different runtime types.

## Behavior Notes

* Ordering, equality, and hashing use **ordinal** semantics over `Value`.
* Cross-type comparisons (different concrete value types) are **not allowed** and will throw.
* `ToCss` lowercases and trims the property name and appends a semicolon; if no property is provided, it returns the raw
  value.

## Examples

### Minimal Example

```csharp
AryStringValue token = "  solid  ";  // implicit conversion; trims & validates
Console.WriteLine(token.Value);      // "solid"
```

### Expanded Example

```csharp
var a = new AryStringValue("bold");
var b = AryStringValue.Parse("bold");
var same = a == b;                   // true (ordinal)
var css = a.ToCss("font-weight");    // "font-weight:bold;"

if (AryStringValue.TryParse(" underline ", out var parsed))
{
    Console.WriteLine((string)parsed!); // "underline"
}
```

> *Rev Date: 2025-10-06*
