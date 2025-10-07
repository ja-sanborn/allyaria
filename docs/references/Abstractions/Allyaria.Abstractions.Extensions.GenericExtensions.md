# Allyaria.Abstractions.Extensions.GenericExtensions

`GenericExtensions` provides extension methods for nullable value types, offering convenient defaults when a value is
not present. These helpers simplify handling of `Nullable<T>` types by providing safe fallback behavior.

---

## Constructors

*Static class — no public constructors.*

---

## Properties

*None*

---

## Methods

| Name                                                    | Returns | Description                                                                                                                                        |
|---------------------------------------------------------|---------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| `OrDefault<T>(this T? value, T defaultValue = default)` | `T`     | Returns the provided nullable value if it is not `null`; otherwise returns the specified `defaultValue`, or `default` for `T` if none is provided. |

---

## Operators

*None*

---

## Events

*None*

---

## Exceptions

*None*

---

## Behavior Notes

* This method provides a concise and expressive way to handle nullable structs without explicit `HasValue` checks.
* When `defaultValue` is omitted, the method uses the language default for `T`.
* Equivalent to `value ?? defaultValue` but expressed as a reusable extension method.

---

## Examples

### Minimal Example

```csharp
int? number = null;
int result = number.OrDefault(); // result = 0
```

### Expanded Example

```csharp
public void LogRetryCount(int? retryCount)
{
    int count = retryCount.OrDefault(3);
    Console.WriteLine($"Retrying {count} times...");
}
```

> *Rev Date: 2025-10-06*
