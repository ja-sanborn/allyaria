# Allyaria.Abstractions.Extensions.EnumExtensions

`EnumExtensions` provides extension methods for working with `Enum` values, focusing on retrieving user-friendly
descriptions from `[Description]` attributes with intelligent fallbacks and caching for performance.

---

## Constructors

*Static class — no public constructors.*

---

## Properties

*None*

---

## Methods

| Name                                      | Returns  | Description                                                                                                                                                                                |
|-------------------------------------------|----------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GetDescription(this Enum value)`         | `string` | Retrieves a user-friendly description for the specified enum value. Uses `[Description]` when available; otherwise, generates a humanized fallback. Caches results per enum type and name. |
| `GetDescription<TEnum>(this TEnum value)` | `string` | Generic overload of `GetDescription` for strongly-typed enum calls. Equivalent to `((Enum)value).GetDescription()`.                                                                        |

---

## Operators

*None*

---

## Events

*None*

---

## Exceptions

* Throws `AryArgumentException` if `value` is `null`.

---

## Behavior Notes

* Description results are **cached** per `(enum Type, member name)` to minimize reflection overhead.
* For `[Flags]` enums (e.g., `"Read, Write"`), each constituent member is resolved individually and joined with `", "`.
* Fallbacks use `StringExtensions.FromPascalCase()` to produce readable text when no `[Description]` attribute is
  present.
* Works with both standard and flagged enums of any underlying type.

---

## Examples

### Minimal Example

```csharp
public enum AccessLevel
{
    Read,
    Write,
    Execute
}

var desc = AccessLevel.Read.GetDescription(); 
// => "Read"
```

### Expanded Example

```csharp
[Flags]
public enum AccessRights
{
    [Description("Read Access")]
    Read = 1,
    [Description("Write Access")]
    Write = 2,
    Execute = 4
}

// Cached lookups and fallback humanization
var single = AccessRights.Execute.GetDescription();  // "Execute"
var combined = (AccessRights.Read | AccessRights.Write).GetDescription(); // "Read Access, Write Access"
```

> *Rev Date: 2025-10-06*
