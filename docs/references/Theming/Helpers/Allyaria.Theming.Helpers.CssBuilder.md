# Allyaria.Theming.Helpers.CssBuilder

`CssBuilder` is a fluent, immutable-safe builder for aggregating CSS property/value declarations into a deterministic,
serialized CSS string. It is used throughout the Allyaria theming system to collect strongly-typed style values into a
stable, semicolon-separated `property:value` list that is suitable for inline styles or stylesheet output, ensuring
consistent ordering via an internal `SortedDictionary<string, string>`.

## Constructors

`CssBuilder()` Initializes a new, empty `CssBuilder` instance with no CSS property/value pairs.

## Properties

*None*

## Methods

| Name       | Returns      | Description                                                                                                                                                                                                                                  |
|------------|--------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Add`      | `CssBuilder` | Adds a new CSS property/value pair to the builder if both `name` and `value` are non-empty, normalizing the property name (and optional `varPrefix`) to CSS-compliant form and returning the same `CssBuilder` instance for fluent chaining. |
| `AddRange` | `CssBuilder` | Parses a semicolon-delimited list of `property:value` declarations from `cssList`, adds each valid pair using `Add`, and returns the same `CssBuilder` instance for fluent chaining.                                                         |
| `ToString` | `string`     | Builds and returns the final semicolon-separated CSS string of all accumulated `property:value` pairs, or `string.Empty` if no styles are defined.                                                                                           |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Helpers;

var builder = new CssBuilder()
    .Add("BackgroundColor", "#ffffff")
    .Add("font-size", "16px")
    .AddRange("margin:0;padding:8px");

// Produces a deterministic, semicolon-separated CSS string,
// e.g. "background-color:#ffffff;font-size:16px;margin:0;padding:8px"
string css = builder.ToString();
```

---

*Revision Date: 2025-11-17*
