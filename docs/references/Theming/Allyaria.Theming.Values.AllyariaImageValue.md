# Allyaria.Theming.Values.AllyariaImageValue

`AllyariaImageValue` is a sealed, immutable theming type that represents a `CSS image value` normalized into a canonical
`url("…")` form.
It *inherits from `ValueBase`*, gaining string-based equality, ordering, hashing, and CSS declaration emission.
Parsing enforces security restrictions (no `javascript:` or `vbscript:` schemes, only safe protocols allowed).

---

## Constructors

`AllyariaImageValue(string value)`
Initializes and normalizes a CSS image value. If the input contains a `url(...)` token, only the `first URL` is
extracted. Otherwise the string is validated, unwrapped of quotes, escaped, and wrapped as `url("…")`.

* Exceptions:

    * `AllyariaArgumentException` — if the input is `null`, empty, whitespace, contains control chars, or resolves to a
      disallowed URI scheme (e.g., `javascript:`).

---

## Properties

| Name    | Type     | Description                                 |
|---------|----------|---------------------------------------------|
| `Value` | `string` | Canonical normalized CSS `url("…")` string. |

---

## Methods

| Name                                                                                          | Returns              | Description                                                                                                                                                                                                                             |
|-----------------------------------------------------------------------------------------------|----------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Parse(string value)`                                                                         | `AllyariaImageValue` | Parses and normalizes input into an image value. Throws on invalid input.                                                                                                                                                               |
| `TryParse(string value, out AllyariaImageValue? result)`                                      | `bool`               | Attempts to parse; returns false on failure.                                                                                                                                                                                            |
| `ToCssBackground(AllyariaColorValue backgroundColor, bool stretch = true)`                    | `string`             | Builds `background-image` CSS (with contrast overlay). If `stretch` is true, also outputs position, repeat, and size. Overlay color depends on luminance of background (`rgba(0,0,0,0.5)` for light, `rgba(255,255,255,0.5)` for dark). |
| `ToCssVarsBackground(string prefix, AllyariaColorValue backgroundColor, bool stretch = true)` | `string`             | Builds CSS variable declarations for a background image (with overlay). Uses provided prefix for variable names.                                                                                                                        |
| `Compare(ValueBase? left, ValueBase? right)`                                                  | `int`                | Static comparison using ordinal semantics. Throws if operand types differ.                                                                                                                                                              |
| `CompareTo(ValueBase? other)`                                                                 | `int`                | Compares with another value of the same type by `Value`.                                                                                                                                                                                |
| `Equals(object? obj)`                                                                         | `bool`               | Equality check against object.                                                                                                                                                                                                          |
| `Equals(ValueBase? other)`                                                                    | `bool`               | Equality check against another `ValueBase` of the same type.                                                                                                                                                                            |
| `GetHashCode()`                                                                               | `int`                | Ordinal hash of `Value`.                                                                                                                                                                                                                |
| `ToCss(string propertyName)`                                                                  | `string`             | Formats `"property:value;"` or returns raw `Value` if no property provided.                                                                                                                                                             |
| `ToString()`                                                                                  | `string`             | Returns `Value`.                                                                                                                                                                                                                        |

---

## Operators

| Operator                                | Returns              | Description                                                                           |
|-----------------------------------------|----------------------|---------------------------------------------------------------------------------------|
| `implicit string -> AllyariaImageValue` | `AllyariaImageValue` | Parses and normalizes string into image value.                                        |
| `implicit AllyariaImageValue -> string` | `string`             | Returns canonical `url("…")` string.                                                  |
| `==`, `!=`                              | `bool`               | Ordinal equality via `ValueBase`. Only equal for same type + same `Value`.            |
| `>`, `<`, `>=`, `<=`                    | `bool`               | Ordinal ordering via `ValueBase`. Throws if comparing across different derived types. |

---

## Events

*None*

---

## Exceptions

* `AllyariaArgumentException` — invalid input (null/whitespace/control chars), unsupported URI scheme, or unsafe schemes
  like `javascript:`.
* `NullReferenceException` — if implicit conversion to string is called on a null instance.

---

## Behavior Notes

* Canonicalizes all inputs into `url("…")`.
* If input includes `url(...)` tokens, only the `first` is kept.
* Escapes backslashes (`\`) and double quotes (`"`) inside URL values.
* Supports relative URLs, `http`, `https`, `data`, and `blob` schemes.
* Unsafe schemes (`javascript:`, `vbscript:`) are blocked.
* Provides contrast overlays when building background CSS to ensure legibility.
* Equality and ordering semantics come from `ValueBase` (ordinal comparison of `Value`).

---

## Examples

### Minimal Example

```csharp
using Allyaria.Theming.Values;

var image = new AllyariaImageValue("logo.png");
Console.WriteLine(image.Value); 
// url("logo.png")
Console.WriteLine(image.ToCss("background-image")); 
// background-image:url("logo.png");
```

### Expanded Example

```csharp
using Allyaria.Theming.Values;
using Allyaria.Theming.Constants;

public class ImageDemo
{
    public void ApplyImage()
    {
        var bg = new AllyariaImageValue("https://example.com/bg.jpg");

        string css = bg.ToCssBackground(Colors.White);
        Console.WriteLine(css);
        // background-image:linear-gradient(rgba(0,0,0,0.5),rgba(0,0,0,0.5)),url("https://example.com/bg.jpg");
        // background-position:center;background-repeat:no-repeat;background-size:cover

        string vars = bg.ToCssVarsBackground("--btn-", Colors.Black);
        Console.WriteLine(vars);
        // --btn-background-image:linear-gradient(rgba(255,255,255,0.5),rgba(255,255,255,0.5)),url("https://example.com/bg.jpg");
        // --btn-background-position:center;--btn-background-repeat:no-repeat;--btn-background-size:cover
    }
}
```

> *Rev Date: 2025-10-01*
