# Allyaria.Theming.Values.AryStringValue

`AryStringValue` is a strongly typed wrapper around normalized theming strings. It ensures that all input values are
trimmed, non-null, and free of whitespace or control characters. The type provides reliable equality, comparison, and
CSS serialization capabilities for use in Allyaria’s theming and styling infrastructure.

---

## Constructors

`AryStringValue(string value)`
Initializes a new instance of the `AryStringValue` class, validating and normalizing the specified string.

---

## Properties

| Name    | Type     | Description                                                    |
|---------|----------|----------------------------------------------------------------|
| `Value` | `string` | Gets the normalized string value represented by this instance. |

---

## Methods

| Name                                                 | Returns          | Description                                                                        |
|------------------------------------------------------|------------------|------------------------------------------------------------------------------------|
| `Compare(ValueBase? left, ValueBase? right)`         | `int`            | Compares two theming values for ordering using ordinal string comparison.          |
| `CompareTo(ValueBase? other)`                        | `int`            | Compares this instance to another value using ordinal semantics.                   |
| `Equals(object? obj)`                                | `bool`           | Determines whether the specified object is equal to this instance.                 |
| `Equals(ValueBase? other)`                           | `bool`           | Determines whether another `ValueBase` instance is equal to this one.              |
| `Equals(ValueBase? left, ValueBase? right)`          | `bool`           | Determines whether two theming values are equal.                                   |
| `GetHashCode()`                                      | `int`            | Returns a hash code based on the normalized value using ordinal comparison.        |
| `ToCss(string propertyName)`                         | `string`         | Converts this value into a CSS declaration string for the given property.          |
| `ToString()`                                         | `string`         | Returns the raw string value.                                                      |
| `Parse(string value)`                                | `AryStringValue` | Parses a string into a new `AryStringValue` after validation.                      |
| `TryParse(string value, out AryStringValue? result)` | `bool`           | Attempts to parse a string into an `AryStringValue`. Returns `true` if successful. |

---

## Operators

| Operator                                         | Returns          | Description                                                          |
|--------------------------------------------------|------------------|----------------------------------------------------------------------|
| `==`                                             | `bool`           | Determines whether two instances are equal.                          |
| `!=`                                             | `bool`           | Determines whether two instances are not equal.                      |
| `>`                                              | `bool`           | Determines whether one instance is greater than another.             |
| `<`                                              | `bool`           | Determines whether one instance is less than another.                |
| `>=`                                             | `bool`           | Determines whether one instance is greater than or equal to another. |
| `<=`                                             | `bool`           | Determines whether one instance is less than or equal to another.    |
| `implicit operator AryStringValue(string value)` | `AryStringValue` | Converts a `string` to an `AryStringValue`.                          |
| `implicit operator string(AryStringValue value)` | `string`         | Converts an `AryStringValue` to a `string`.                          |

---

## Events

*None*

---

## Exceptions

* `AryArgumentException` — Thrown when invalid string inputs are `null`, empty, whitespace-only, or contain control
  characters.
* `AryArgumentException` — Thrown when comparing or ordering instances of different concrete types.

---

## Example

```csharp
var primary = new AryStringValue("PrimaryColor");
var secondary = AryStringValue.Parse("SecondaryColor");

if (primary != secondary)
{
    Console.WriteLine(primary.ToCss("--theme-color"));
}
```

---

*Revision Date: 2025-10-17*
