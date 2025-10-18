# Allyaria.Theming.Helpers.StyleHelper

`StyleHelper` is a static helper class that provides extension methods for constructing CSS style declarations from
Allyaria theme values. It simplifies the process of generating properly formatted CSS variable names and appending them
to style definitions.

## Constructors

*None*

## Properties

*None*

## Methods

| Name       | Signature                                                                                         | Description                                                                                                                                                                               | Returns  |
|------------|---------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| `ToCss`    | `void ToCss(this StringBuilder builder, ValueBase value, string propertyName, string? varPrefix)` | Appends a CSS declaration for the specified property and value to a `StringBuilder`. If `propertyName` or `value.Value` is null or whitespace, the method does nothing.                   | `void`   |
| `ToPrefix` | `string ToPrefix(this string? prefix)`                                                            | Converts a prefix string into a normalized, lowercase CSS variable prefix, replacing underscores and whitespace with hyphens. Returns an empty string if the input is null or whitespace. | `string` |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
var builder = new StringBuilder();
var colorValue = new AryColorValue("red"); // Example theme value
builder.ToCss(colorValue, "background-color", "app");

string cssOutput = builder.ToString();
// Result: "--app-background-color: red;"
```

---

*Revision Date: 2025-10-17*
