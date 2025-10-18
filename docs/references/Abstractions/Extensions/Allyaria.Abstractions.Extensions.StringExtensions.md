# Allyaria.Abstractions.Extensions.StringExtensions

`StringExtensions` is a static utility class that provides a comprehensive set of string manipulation helpers. It
includes conversions between naming conventions (PascalCase, camelCase, snake_case, kebab-case), culture-aware
capitalization, diacritic (accent) normalization, and identifier validation. All methods are null- and whitespace-safe,
returning empty strings when appropriate.

## Constructors

*None*

## Properties

*None*

## Methods

| Name                                                         | Returns  | Description                                                                                                                                       |
|--------------------------------------------------------------|----------|---------------------------------------------------------------------------------------------------------------------------------------------------|
| `Capitalize(this string? word, CultureInfo? culture = null)` | `string` | Converts the first character of a word to uppercase and the remaining characters to lowercase, respecting cultural casing rules.                  |
| `FromCamelCase(this string? value)`                          | `string` | Converts a camelCase identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                               |
| `FromKebabCase(this string? value)`                          | `string` | Converts a kebab-case identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                              |
| `FromPascalCase(this string? value)`                         | `string` | Converts a PascalCase identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                              |
| `FromPrefixedCase(this string? value)`                       | `string` | Attempts to detect and convert prefixed identifiers (with `_` or `-`) into human-readable strings. Throws `AryArgumentException` if unrecognized. |
| `FromSnakeCase(this string? value)`                          | `string` | Converts a snake_case identifier into a human-readable string with spaces. Throws `AryArgumentException` if invalid.                              |
| `NormalizeAccents(this string? value)`                       | `string` | Removes diacritic marks (accents) from a string using Unicode normalization.                                                                      |
| `OrDefault(this string? value, string defaultValue = "")`    | `string` | Returns the provided string if not `null`; otherwise, returns the specified `defaultValue` or an empty string.                                    |
| `ToCamelCase(this string? value)`                            | `string` | Converts a string into camelCase form, respecting acronyms and whitespace.                                                                        |
| `ToKebabCase(this string? value)`                            | `string` | Converts a string into kebab-case (lowercase, words separated by hyphens).                                                                        |
| `ToPascalCase(this string? value)`                           | `string` | Converts a string into PascalCase, preserving acronyms and capitalization.                                                                        |
| `ToSnakeCase(this string? value)`                            | `string` | Converts a string into snake_case (lowercase, words separated by underscores).                                                                    |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Extensions;

string pascal = "UserAccountInfo";
string readable = pascal.FromPascalCase(); // "User Account Info"

string kebab = readable.ToKebabCase(); // "user-account-info"
string snake = readable.ToSnakeCase(); // "user_account_info"
string camel = readable.ToCamelCase(); // "userAccountInfo"

string accented = "Café Déjà Vu";
string normalized = accented.NormalizeAccents(); // "Cafe Deja Vu"
```

---

*Revision Date: 2025-10-17*
