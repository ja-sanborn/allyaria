# Allyaria.Theming.StyleTypes.StyleAlignmentBaseline

`StyleAlignmentBaseline` is a sealed style value record representing a CSS `alignment-baseline` value within the
Allyaria theming system. It inherits from `StyleValueBase` (which implements `IStyleValue`) and provides a strongly
typed wrapper around standard CSS `alignment-baseline` keywords via the nested `Kind` enumeration.

## Summary

`StyleAlignmentBaseline` is an immutable, validated style value for the CSS `alignment-baseline` property. It exposes an
enum of supported values, maps each enum member to its CSS keyword using `Description` attributes, validates the
resulting string through `StyleValueBase`, and offers parsing plus implicit conversion helpers for ergonomic use in
theming and style generation code.

## Constructors

`StyleAlignmentBaseline(Kind kind)` Initializes a new `StyleAlignmentBaseline` instance for the given baseline alignment
`kind`, mapping it to the corresponding CSS keyword and validating it via the `StyleValueBase` base constructor.

## Properties

| Name    | Type     | Description                                                                                                          |
|---------|----------|----------------------------------------------------------------------------------------------------------------------|
| `Value` | `string` | Gets the validated, normalized CSS `alignment-baseline` string for this instance (e.g., `"alphabetic"`, `"middle"`). |

## Methods

| Name                                                          | Returns                  | Description                                                                                                                                                |
|---------------------------------------------------------------|--------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                                        | `StyleAlignmentBaseline` | Parses the specified CSS `alignment-baseline` string into a `StyleAlignmentBaseline` instance by mapping it to a `Kind` enum; throws if invalid.           |
| `TryParse(string? value, out StyleAlignmentBaseline? result)` | `bool`                   | Attempts to parse `value` into a `StyleAlignmentBaseline`; returns `true` and sets `result` on success, or `false` and sets `result` to `null` on failure. |
| `Equals(object? obj)`                                         | `bool`                   | Determines whether the current instance is equal to another object (record structural equality).                                                           |
| `Equals(StyleAlignmentBaseline? other)`                       | `bool`                   | Determines whether the current instance is equal to another `StyleAlignmentBaseline` instance.                                                             |
| `GetHashCode()`                                               | `int`                    | Returns a hash code based on the record’s value equality semantics.                                                                                        |

## Nested Types

| Name   | Kind   | Description                                                                                                 |
|--------|--------|-------------------------------------------------------------------------------------------------------------|
| `Kind` | `enum` | Defines the supported CSS `alignment-baseline` values used to construct `StyleAlignmentBaseline` instances. |

### `StyleAlignmentBaseline.Kind` Members

| Name           | Description                                                                                                |
|----------------|------------------------------------------------------------------------------------------------------------|
| `Alphabetic`   | Aligns the element with the alphabetic baseline of its parent (`"alphabetic"`).                            |
| `Baseline`     | Aligns the element with the dominant baseline of its parent (`"baseline"`).                                |
| `Central`      | Aligns the geometric center of the element’s box with the parent’s alignment baseline (`"central"`).       |
| `Ideographic`  | Aligns the element with the ideographic baseline used for East Asian text (`"ideographic"`).               |
| `Mathematical` | Aligns the element with the mathematical baseline used for formulas or similar content (`"mathematical"`). |
| `Middle`       | Aligns the element with the vertical middle of its parent’s box (`"middle"`).                              |
| `TextBottom`   | Aligns the element with the bottom of the parent’s text content area (`"text-bottom"`).                    |
| `TextTop`      | Aligns the element with the top of the parent’s text content area (`"text-top"`).                          |

## Operators

| Operator                                                                   | Returns                  | Description                                                                                                                 |
|----------------------------------------------------------------------------|--------------------------|-----------------------------------------------------------------------------------------------------------------------------|
| `implicit operator StyleAlignmentBaseline(string? value)`                  | `StyleAlignmentBaseline` | Parses `value` into a `StyleAlignmentBaseline` using `Parse`; throws if the string does not correspond to any valid `Kind`. |
| `implicit operator string(StyleAlignmentBaseline? value)`                  | `string`                 | Converts a `StyleAlignmentBaseline` instance to its underlying CSS string, or an empty string if `value` is `null`.         |
| `operator ==(StyleAlignmentBaseline? left, StyleAlignmentBaseline? right)` | `bool`                   | Determines whether two `StyleAlignmentBaseline` instances are equal (record structural equality).                           |
| `operator !=(StyleAlignmentBaseline? left, StyleAlignmentBaseline? right)` | `bool`                   | Determines whether two `StyleAlignmentBaseline` instances are not equal.                                                    |

## Events

*None*

## Exceptions

* `AryArgumentException`  
  Thrown by the `StyleValueBase` constructor (and thus by `StyleAlignmentBaseline(Kind kind)`) when the derived CSS
  value contains control characters or is otherwise considered invalid for use as a CSS value.

* `AryArgumentException`  
  Thrown by `Parse(string? value)` when the provided `value` does not correspond to any valid `Kind` member.

* `AryArgumentException`  
  Thrown by the implicit conversion `operator StyleAlignmentBaseline(string? value)` when the string cannot be parsed
  into a valid `Kind` (it delegates to `Parse`).

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class AlignmentBaselineExample
{
    public void Demo()
    {
        // Create from enum
        var alphabetic = new StyleAlignmentBaseline(StyleAlignmentBaseline.Kind.Alphabetic);
        string cssAlphabetic = alphabetic.Value; // "alphabetic"

        // Parse from string
        var middle = StyleAlignmentBaseline.Parse("middle");
        string cssMiddle = middle;               // implicit to string => "middle"

        // TryParse with graceful failure
        if (StyleAlignmentBaseline.TryParse("text-bottom", out var textBottom))
        {
            string cssTextBottom = textBottom!.Value; // "text-bottom"
        }

        // Implicit construction from string
        StyleAlignmentBaseline baseline = "ideographic";
        string cssBaseline = baseline;           // "ideographic"
    }
}
```

---

*Revision Date: 2025-11-15*
