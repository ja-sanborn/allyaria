# Allyaria.Abstractions.Extensions.StringExtensions

`StringExtensions` provides rich, culture-aware utilities for working with `string` values, including case-style
conversions, accent normalization, and human-readable transformations.
All methods are **null/whitespace-safe** and return `string.Empty` where appropriate.

---

## Constructors

*Static class — no public constructors.*

---

## Properties

*None*

---

## Methods

| Name                                                         | Returns  | Description                                                                                                                                                                             |
|--------------------------------------------------------------|----------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Capitalize(this string? word, CultureInfo? culture = null)` | `string` | Capitalizes the first letter of a word and lowercases the rest, respecting the specified culture.                                                                                       |
| `FromCamelCase(this string? value)`                          | `string` | Converts a `camelCase` identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                                                                   |
| `FromKebabCase(this string? value)`                          | `string` | Converts a `kebab-case` identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                                                                  |
| `FromPascalCase(this string? value)`                         | `string` | Converts a `PascalCase` identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                                                                  |
| `FromPrefixedCase(this string? value)`                       | `string` | Detects an identifier’s case style (camel, Pascal, snake, kebab) — optionally prefixed by `_` or `-` — and converts it to readable form. Throws `AryArgumentException` if unrecognized. |
| `FromSnakeCase(this string? value)`                          | `string` | Converts a `snake_case` identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                                                                  |
| `NormalizeAccents(this string? value)`                       | `string` | Removes diacritic marks (accents) from a string (e.g., “café” → “cafe”).                                                                                                                |
| `OrDefault(this string? value, string defaultValue = "")`    | `string` | Returns the string if not `null`; otherwise returns `defaultValue` or `string.Empty`.                                                                                                   |
| `ToCamelCase(this string? value)`                            | `string` | Converts a string into `camelCase` form, preserving acronyms and ignoring extra whitespace.                                                                                             |
| `ToKebabCase(this string? value)`                            | `string` | Converts a string into `kebab-case` (lowercased, hyphen-separated).                                                                                                                     |
| `ToPascalCase(this string? value)`                           | `string` | Converts a string into `PascalCase`, preserving acronyms.                                                                                                                               |
| `ToSnakeCase(this string? value)`                            | `string` | Converts a string into `snake_case` (lowercased, underscore-separated).                                                                                                                 |

---

## Operators

*None*

---

## Events

*None*

---

## Exceptions

* Throws `AryArgumentException` when invalid identifiers are supplied to `FromCamelCase`, `FromPascalCase`,
  `FromKebabCase`, `FromSnakeCase`, or `FromPrefixedCase`.

---

## Behavior Notes

* Whitespace and invalid characters are trimmed before processing.
* Case-conversion methods collapse consecutive separators (`-`, `_`, or whitespace) into a single delimiter.
* Acronyms are preserved (e.g., `"HTTPRequest"` → `"HTTP Request"`, `"xml http request"` → `"XmlHttpRequest"`).
* `NormalizeAccents` safely strips Unicode combining marks to ensure normalized search and display behavior.
* Default behavior uses `CultureInfo.InvariantCulture` unless specified otherwise.

---

## Examples

### Minimal Example

```csharp
var readable = "userName".FromCamelCase(); // "User Name"
var kebab = "User Name".ToKebabCase();     // "user-name"
```

### Expanded Example

```csharp
public string GenerateIdentifier(string? input)
{
    // Ensure safe input normalization
    var normalized = input.NormalizeAccents().Trim();

    // Create identifiers for different use-cases
    var camel = normalized.ToCamelCase(); // for JS interop
    var snake = normalized.ToSnakeCase(); // for DB
    var title = normalized.FromPrefixedCase(); // for UI display

    return $"camel: {camel}, snake: {snake}, title: {title}";
}
```

> *Rev Date: 2025-10-06*
