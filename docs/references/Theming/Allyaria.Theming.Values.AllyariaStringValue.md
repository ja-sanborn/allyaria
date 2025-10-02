# Allyaria.Theming.Values.AllyariaStringValue

`AllyariaStringValue` is a sealed, immutable theming type that represents a *normalized string value*.
It *inherits from `ValueBase`*, which provides canonical string storage, equality, ordering, and CSS emission.
Input is always validated (non-null, trimmed, no whitespace-only or control characters).

---

## Constructors

`AllyariaStringValue(string value)`
Creates a new instance by validating and normalizing the provided string.

* Exceptions:

    * `AllyariaArgumentException` — if the input is null, empty, whitespace-only, or contains control characters.

---

## Properties

| Name    | Type     | Description                  |
|---------|----------|------------------------------|
| `Value` | `string` | Normalized canonical string. |

---

## Methods

| Name                                                      | Returns               | Description                                                              |
|-----------------------------------------------------------|-----------------------|--------------------------------------------------------------------------|
| `Parse(string value)`                                     | `AllyariaStringValue` | Parses a raw string, throws if invalid.                                  |
| `TryParse(string value, out AllyariaStringValue? result)` | `bool`                | Attempts parsing, returns false on failure.                              |
| `Compare(ValueBase? left, ValueBase? right)`              | `int`                 | Static comparison by `Value`. Throws if types differ.                    |
| `CompareTo(ValueBase? other)`                             | `int`                 | Ordinal comparison with another instance.                                |
| `Equals(object? obj)`                                     | `bool`                | Equality check against object.                                           |
| `Equals(ValueBase? other)`                                | `bool`                | Equality by `Value` with another `ValueBase` of the same type.           |
| `GetHashCode()`                                           | `int`                 | Ordinal hash of `Value`.                                                 |
| `ToCss(string propertyName)`                              | `string`              | `"property:value;"` if property provided, otherwise returns raw `Value`. |
| `ToString()`                                              | `string`              | Returns `Value`.                                                         |

---

## Operators

| Operator                                 | Returns               | Description                                                                      |
|------------------------------------------|-----------------------|----------------------------------------------------------------------------------|
| `implicit string -> AllyariaStringValue` | `AllyariaStringValue` | Converts string into validated `AllyariaStringValue`.                            |
| `implicit AllyariaStringValue -> string` | `string`              | Returns canonical `Value`.                                                       |
| `==`, `!=`                               | `bool`                | Ordinal equality by canonical `Value`. Only equal if same runtime type.          |
| `>`, `<`, `>=`, `<=`                     | `bool`                | Ordinal ordering by `Value`. Throws if comparing across different derived types. |

---

## Events

*None*

---

## Exceptions

* `AllyariaArgumentException` — thrown when invalid string input is provided (null/whitespace/control chars) or when
  comparing across different `ValueBase`-derived types.
* `NullReferenceException` — thrown if implicit conversion to string is attempted on a null instance.

---

## Behavior Notes

* Canonical form is always trimmed input with no surrounding whitespace or control characters.
* Input that is empty or only whitespace is rejected.
* Equality and ordering semantics are strict ordinal comparisons of canonical strings.
* Inherits immutability and type-safe comparison rules from `ValueBase`.

---

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Values;

var token = new AllyariaStringValue("  bold  ");
Console.WriteLine(token.Value); // "bold"
Console.WriteLine(token.ToCss("font-weight")); // "font-weight:bold;"
```

### Expanded Example

```csharp
using Allyaria.Theming.Values;

public class StringDemo
{
    public void Demo()
    {
        var v1 = AllyariaStringValue.Parse("center");
        var v2 = new AllyariaStringValue("uppercase");

        Console.WriteLine(v1 == v2);        // false
        Console.WriteLine(v1.ToCss("text-align")); // "text-align:center;"

        if (AllyariaStringValue.TryParse("italic", out var parsed))
        {
            Console.WriteLine(parsed); // "italic"
        }
    }
}
```

> *Rev Date: 2025-10-01*
