# Allyaria.Theming.Values.AryImageValue

`AryImageValue` represents a **strongly-typed CSS image value** used within Allyaria theming.
It normalizes input into a canonical CSS `url("…")` token, enforcing strict validation and safe URI rules.
Inherited functionality from `ValueBase` includes ordinal comparison, equality, hashing, CSS serialization, and
validation.

## Constructors

`AryImageValue(string value, AryColorValue? backgroundColor = null)`
Initializes a new instance by normalizing `value` into a CSS-safe image token.
If `backgroundColor` is provided, a **contrast overlay** (`linear-gradient`) is prepended for accessibility.

* Exceptions:

    * `AryArgumentException` — Invalid, empty, or dangerous input.
    * Disallowed URI schemes (e.g., `javascript:`, `ftp:`).

## Properties

| Name    | Type     | Description                                                                                     |
|---------|----------|-------------------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS value (e.g., `url("image.png")` or `linear-gradient(...),url("image.png")`). |

## Methods

| Name                                                       | Returns         | Description                                                                                   |
|------------------------------------------------------------|-----------------|-----------------------------------------------------------------------------------------------|
| `static Parse(string value)`                               | `AryImageValue` | Creates a new instance from the specified CSS or URL string, throwing if invalid.             |
| `static TryParse(string value, out AryImageValue? result)` | `bool`          | Safely attempts to parse the string into a valid `AryImageValue`. Returns `false` on failure. |
| `static implicit operator AryImageValue(string value)`     | `AryImageValue` | Converts a raw string to an `AryImageValue` (validated and normalized).                       |
| `static implicit operator string(AryImageValue value)`     | `string`        | Extracts the normalized string (the CSS `url(...)` form).                                     |
| `CompareTo(ValueBase? other)`                              | `int`           | Ordinal comparison with another value (from `ValueBase`).                                     |
| `Equals(ValueBase? other)`                                 | `bool`          | Ordinal equality comparison (from `ValueBase`).                                               |
| `ToCss(string propertyName)`                               | `string`        | Converts the value to a CSS declaration (e.g., `background-image:url("foo.png");`).           |

## Operators

| Operator                          | Returns         | Description                                    |
|-----------------------------------|-----------------|------------------------------------------------|
| `==`, `!=`                        | `bool`          | Ordinal equality comparison (type-safe).       |
| `>`, `<`, `>=`, `<=`              | `bool`          | Lexicographical ordering (same type only).     |
| `implicit string → AryImageValue` | `AryImageValue` | Converts a string to a normalized image token. |
| `implicit AryImageValue → string` | `string`        | Returns the canonical `url("…")` form.         |

## Exceptions

* `AryArgumentException` —

    * Null, empty, or control-character input.
    * Dangerous schemes (`javascript:`, `vbscript:`).
    * Disallowed URI schemes (only `http`, `https`, `data`, and `blob` allowed).

## Behavior Notes

* Automatically escapes backslashes (`\ → \\`) and trims whitespace.
* Supports extracting the first valid `url(...)` token from complex CSS inputs like:
  `"linear-gradient(white, black), url('icon.svg') repeat-x"`.
  → normalized result: `url("icon.svg")`.
* When `backgroundColor` is specified:

    * Computes luminance with `ColorHelper.RelativeLuminance`.
    * Adds an overlay for contrast:

        * Light background → black 50% overlay.
        * Dark background → white 50% overlay.
* Produces **safe**, **deterministic**, **CSS-valid** output for background or mask-image properties.

## Examples

### Minimal Example

```csharp
var img = new AryImageValue("logo.png");
Console.WriteLine(img.Value); // url("logo.png")
```

### Expanded Example

```csharp
var themedImg = new AryImageValue(
    "https://cdn.example.com/hero.jpg",
    new AryColorValue("#fafafa")
);

Console.WriteLine(themedImg.Value);
// "url("https://cdn.example.com/hero.jpg"),linear-gradient(rgba(0,0,0,0.5),rgba(0,0,0,0.5))"

Console.WriteLine(themedImg.ToCss("background-image"));
// "background-image:url("https://cdn.example.com/hero.jpg"),linear-gradient(rgba(0,0,0,0.5),rgba(0,0,0,0.5));"
```

> *Rev Date: 2025-10-06*
