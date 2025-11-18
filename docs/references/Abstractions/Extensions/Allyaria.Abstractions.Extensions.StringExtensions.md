# Allyaria.Abstractions.Extensions.StringExtensions

`StringExtensions` provides a comprehensive suite of string manipulation and normalization utilities, including
PascalCase, camelCase, snake_case, and kebab-case conversions; culture-aware capitalization; whitespace and diacritic
normalization; and safe null handling.

All methods are designed to be **null-, empty-, and whitespace-safe**, returning `string.Empty` when appropriate. They
are optimized for common developer workflows in naming, text formatting, and UI-friendly label generation.

## Constructors

`StringExtensions` is a static class and cannot be instantiated.

## Properties

*None*

## Methods

| Name                                                             | Returns   | Description                                                                                                                                                                             |
|------------------------------------------------------------------|-----------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Capitalize(this string? word, CultureInfo? culture = null)`     | `string`  | Converts the first character to uppercase and the remaining characters to lowercase, using the specified culture (default is invariant).                                                |
| `FromCamelCase(this string? value)`                              | `string`  | Converts a camelCase identifier into a space-separated human-readable string. Throws `AryArgumentException` if invalid.                                                                 |
| `FromKebabCase(this string? value)`                              | `string`  | Converts a kebab-case identifier into a space-separated human-readable string. Throws `AryArgumentException` if invalid.                                                                |
| `FromPascalCase(this string? value)`                             | `string`  | Converts a PascalCase identifier into a space-separated human-readable string. Throws `AryArgumentException` if invalid.                                                                |
| `FromPrefixedCase(this string? value)`                           | `string`  | Detects the case style (Pascal, camel, snake, or kebab) after trimming leading `_` or `-` and converts it to a human-readable string. Throws `AryArgumentException` if detection fails. |
| `FromSnakeCase(this string? value)`                              | `string`  | Converts a snake_case identifier into a space-separated human-readable string. Throws `AryArgumentException` if invalid.                                                                |
| `NormalizeAccents(this string? value)`                           | `string`  | Removes diacritic marks (accents) from letters using Unicode normalization.                                                                                                             |
| `OrDefaultIfEmpty(this string? value, string defaultValue = "")` | `string`  | Returns the string if non-empty/non-whitespace; otherwise returns the specified default (defaults to `string.Empty`).                                                                   |
| `OrDefaultIfNull(this string? value, string defaultValue = "")`  | `string`  | Returns the string if non-null; otherwise returns the specified default (defaults to `string.Empty`).                                                                                   |
| `OrNull(this string? value)`                                     | `string?` | Returns the string if not null or white space; otherwise returns null.                                                                                                                  |
| `ToCamelCase(this string? value)`                                | `string`  | Converts text or concatenated identifiers to camelCase form (lowercase first character, capitalized subsequent words).                                                                  |
| `ToCssName(this string? name)`                                   | `string`  | Converts a string into a CSS-compatible, hyphenated lowercase name (e.g., `"Font_Size"` → `"font-size"`).                                                                               |
| `ToKebabCase(this string? value)`                                | `string`  | Converts text into kebab-case form (words separated by `-`, all lowercase).                                                                                                             |
| `ToPascalCase(this string? value)`                               | `string`  | Converts text into PascalCase form (capitalizing each word while preserving acronyms).                                                                                                  |
| `ToSnakeCase(this string? value)`                                | `string`  | Converts text into snake_case form (words separated by `_`, all lowercase).                                                                                                             |
| `TryParseEnum<TEnum>(this string? value, out TEnum result)`      | `bool`    | Attempts to parse the specified string into an enumeration value of type                                                                                                                |

## Operators

*None*

## Events

*None*

## Exceptions

| Exception Type         | Description                                                                                                                    |
|------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| `AryArgumentException` | Thrown by conversion methods (`FromCamelCase`, `FromPascalCase`, etc.) when the input fails to match the expected case format. |

## Example

```csharp
using System;
using Allyaria.Abstractions.Extensions;

public class Example
{
    public void DemonstrateStringConversions()
    {
        Console.WriteLine("helloWorld".FromCamelCase());   // Output: "Hello World"
        Console.WriteLine("MyExampleValue".FromPascalCase()); // Output: "My Example Value"
        Console.WriteLine("user_name".FromSnakeCase());     // Output: "User Name"
        Console.WriteLine("font-size".FromKebabCase());     // Output: "Font Size"

        Console.WriteLine("Hello world".ToKebabCase());     // Output: "hello-world"
        Console.WriteLine("hello world".ToSnakeCase());     // Output: "hello_world"
        Console.WriteLine("example text".ToPascalCase());   // Output: "ExampleText"
        Console.WriteLine("Another Example".ToCamelCase()); // Output: "anotherExample"

        Console.WriteLine(" Café ".NormalizeAccents());     // Output: "Cafe"
        Console.WriteLine(((string?)null).OrDefaultIfEmpty("default")); // Output: "default"
    }
}
```

---

*Revision Date: 2025-11-17*
