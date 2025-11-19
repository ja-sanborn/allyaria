# Allyaria.Theming.StyleTypes.StyleAlign

`StyleAlign` is a sealed style value type representing a CSS alignment value within the Allyaria theming system. It
derives from `StyleValueBase` (which implements `IStyleValue`) and provides a strongly typed wrapper over standard CSS
alignment keywords via the nested `Kind` enumeration.

## Summary

`StyleAlign` is an immutable, validated style value for CSS alignment-related properties such as `align-items`,
`justify-content`, and `align-self`. It exposes a `Kind` enum of supported alignment keywords, automatically maps each
enum value to its corresponding CSS string (using `Description` attributes), validates the resulting string through
`StyleValueBase`, and offers parsing and implicit conversion helpers for ergonomic use in theming code.

## Constructors

`StyleAlign(Kind kind)` Initializes a new `StyleAlign` instance for the given alignment `kind`, mapping it to the
corresponding CSS keyword and validating it via the `StyleValueBase` constructor.

## Properties

| Name    | Type     | Description                                                                                               |
|---------|----------|-----------------------------------------------------------------------------------------------------------|
| `Value` | `string` | Gets the validated, normalized CSS alignment string for this instance (e.g., `"center"`, `"flex-start"`). |

## Methods

| Name                                              | Returns      | Description                                                                                                                                       |
|---------------------------------------------------|--------------|---------------------------------------------------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                            | `StyleAlign` | Parses the provided CSS alignment `value` into a `StyleAlign` by mapping it to a `Kind` enum; throws if invalid.                                  |
| `TryParse(string? value, out StyleAlign? result)` | `bool`       | Attempts to parse `value` into a `StyleAlign`; returns `true` and outputs `result` on success, or `false` with `result` set to `null` on failure. |
| `Equals(object? obj)`                             | `bool`       | Indicates whether the current instance is equal to another object (record structural equality).                                                   |
| `Equals(StyleAlign? other)`                       | `bool`       | Indicates whether the current instance is equal to another `StyleAlign` instance.                                                                 |
| `GetHashCode()`                                   | `int`        | Returns a hash code based on the record’s value equality semantics.                                                                               |

## Nested Types

| Name   | Kind   | Description                                                                          |
|--------|--------|--------------------------------------------------------------------------------------|
| `Kind` | `enum` | Defines the supported CSS alignment values used to construct `StyleAlign` instances. |

### `StyleAlign.Kind` Members

| Name            | Description                                                                                           |
|-----------------|-------------------------------------------------------------------------------------------------------|
| `Baseline`      | Aligns the baseline of the element with the baseline of its parent (`"baseline"`).                    |
| `Center`        | Centers the element along the alignment axis (`"center"`).                                            |
| `End`           | Aligns the element to the end of the alignment container (`"end"`).                                   |
| `FirstBaseline` | Aligns the first baseline of the element with the first baseline of its parent (`"first baseline"`).  |
| `FlexEnd`       | Aligns the element to the end of the flex container’s cross axis (`"flex-end"`).                      |
| `FlexStart`     | Aligns the element to the start of the flex container’s cross axis (`"flex-start"`).                  |
| `LastBaseline`  | Aligns the last baseline of the element with the last baseline of its parent (`"last baseline"`).     |
| `Normal`        | Uses the default alignment behavior for the property (`"normal"`).                                    |
| `SafeCenter`    | Centers the element safely without overflowing its container (`"safe center"`).                       |
| `SpaceAround`   | Distributes elements evenly with space around each item (`"space-around"`).                           |
| `SpaceBetween`  | Distributes elements evenly with the first item at the start and last at the end (`"space-between"`). |
| `SpaceEvenly`   | Distributes elements evenly with equal spacing between and around them (`"space-evenly"`).            |
| `Start`         | Aligns the element to the start of the alignment container (`"start"`).                               |
| `Stretch`       | Stretches the element to fill the container along the alignment axis (`"stretch"`).                   |
| `UnsafeCenter`  | Centers the element even if it may cause overflow (`"unsafe center"`).                                |

## Operators

| Operator                                           | Returns      | Description                                                                                                       |
|----------------------------------------------------|--------------|-------------------------------------------------------------------------------------------------------------------|
| `implicit operator StyleAlign(string? value)`      | `StyleAlign` | Parses `value` into a `StyleAlign` using `Parse`; throws if the string does not match any valid `Kind`.           |
| `implicit operator string(StyleAlign? value)`      | `string`     | Converts a `StyleAlign` instance to its underlying CSS alignment string, or an empty string if `value` is `null`. |
| `operator ==(StyleAlign? left, StyleAlign? right)` | `bool`       | Determines whether two `StyleAlign` instances are equal (record structural equality).                             |
| `operator !=(StyleAlign? left, StyleAlign? right)` | `bool`       | Determines whether two `StyleAlign` instances are not equal.                                                      |

## Events

*None*

## Exceptions

* `AryArgumentException`  
  Thrown by the `StyleValueBase` constructor (and thus by `StyleAlign(Kind kind)` indirectly) if the derived CSS value
  contains control characters or is otherwise deemed invalid for use as a CSS value.

* `AryArgumentException`  
  Thrown by `Parse(string? value)` when `value` does not match any valid `Kind` alignment keyword.

* `AryArgumentException`  
  Thrown by the implicit conversion `operator StyleAlign(string? value)` when `value` cannot be parsed into a valid
  `Kind` (it delegates to `Parse`).

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class AlignExample
{
    public void Demo()
    {
        // Create from enum
        var center = new StyleAlign(StyleAlign.Kind.Center);
        string cssCenter = center.Value;          // "center"

        // Parse from string
        var stretch = StyleAlign.Parse("stretch");
        string cssStretch = stretch;              // implicit to string => "stretch"

        // TryParse with graceful failure
        if (StyleAlign.TryParse("space-between", out var between))
        {
            string cssBetween = between!.Value;   // "space-between"
        }

        // Implicit construction from string
        StyleAlign start = "flex-start";
        string cssStart = start;                  // "flex-start"
    }
}
```

---

*Revision Date: 2025-11-15*
