# Allyaria.Theming.Types.HexByte

`HexByte` is an immutable value type representing a single 8-bit color channel as a hexadecimal value. It provides
parsing, formatting, comparison, normalization, and interpolation helpers for working with RGBA/sRGB color channels,
including gamma-correct interpolation for visually accurate blending and convenience conversions between byte,
normalized double, and hex string representations.

## Constructors

`HexByte()` Initializes a new `HexByte` with a value of `0` (hex string `"00"`).

`HexByte(byte value)` Initializes a new `HexByte` from an 8-bit channel `value` (0–255).

`HexByte(string value)` Initializes a new `HexByte` from a 1–2 character hexadecimal `value` string (whitespace allowed
and ignored).

## Properties

| Name    | Type   | Description                                                            |
|---------|--------|------------------------------------------------------------------------|
| `Value` | `byte` | Gets the underlying 8-bit channel value represented by this `HexByte`. |

## Methods

| Name                                              | Returns   | Description                                                                                                                                                                           |
|---------------------------------------------------|-----------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ClampAlpha(double value)`                        | `HexByte` | Clamps a normalized alpha value to the range `[0.0, 1.0]` and converts it to a corresponding `HexByte` via `FromNormalized`.                                                          |
| `CompareTo(HexByte other)`                        | `int`     | Compares this `HexByte` with `other` based on their byte values and returns the relative sort order.                                                                                  |
| `Equals(HexByte other)`                           | `bool`    | Determines whether this instance and the specified `HexByte` have the same underlying byte value.                                                                                     |
| `Equals(object? obj)`                             | `bool`    | Determines whether the specified `obj` is a `HexByte` with the same underlying byte value as this instance.                                                                           |
| `FromNormalized(double value)`                    | `HexByte` | Creates a `HexByte` from a normalized channel `value` in `[0.0, 1.0]`, mapping `value * 255.0` to the nearest byte.                                                                   |
| `GetHashCode()`                                   | `int`     | Returns a hash code for this `HexByte`, based on its underlying `Value`.                                                                                                              |
| `Parse(string value)`                             | `HexByte` | Parses a 1–2 character hexadecimal `value` string (whitespace allowed) into a new `HexByte`, or throws if invalid.                                                                    |
| `ToLerpByte(byte end, double factor)`             | `byte`    | Linearly interpolates from this channel `Value` to `end` in byte space, clamping `factor` to `[0.0, 1.0]` and returning the interpolated byte.                                        |
| `ToLerpHexByte(byte end, double factor)`          | `HexByte` | Linearly interpolates from this channel `Value` to `end` in byte space and returns the result as a `HexByte`.                                                                         |
| `ToLerpLinearByte(byte end, double factor)`       | `byte`    | Performs gamma-correct (linear-light) interpolation from this channel to `end` and returns the interpolated sRGB byte.                                                                |
| `ToLerpLinearHexByte(HexByte end, double factor)` | `HexByte` | Performs gamma-correct (linear-light) interpolation from this channel to `end.Value` and returns the result as a `HexByte`.                                                           |
| `ToNormalized()`                                  | `double`  | Converts this channel value to a normalized double in the range `[0.0, 1.0]` by computing `Value / 255.0`.                                                                            |
| `ToSrgbLinearValue()`                             | `double`  | Converts this sRGB channel to a linear-light value in `[0.0, 1.0]` using the sRGB electro-optical transfer function.                                                                  |
| `ToString()`                                      | `string`  | Returns the two-character uppercase hexadecimal string representation of this `HexByte`.                                                                                              |
| `TryParse(string? value, out HexByte result)`     | `bool`    | Attempts to parse a 1–2 character hexadecimal `value` string into a `HexByte`, returning `true` on success and assigning `result`, or `false` with `result` set to the default value. |

## Operators

| Operator                                   | Returns   | Description                                                                                                  |
|--------------------------------------------|-----------|--------------------------------------------------------------------------------------------------------------|
| `operator ==(HexByte left, HexByte right)` | `bool`    | Returns `true` if `left` and `right` have the same underlying byte value; otherwise `false`.                 |
| `operator !=(HexByte left, HexByte right)` | `bool`    | Returns `true` if `left` and `right` have different underlying byte values; otherwise `false`.               |
| `operator >(HexByte left, HexByte right)`  | `bool`    | Returns `true` if `left` is greater than `right` based on their underlying byte values.                      |
| `operator >=(HexByte left, HexByte right)` | `bool`    | Returns `true` if `left` is greater than or equal to `right` based on their underlying byte values.          |
| `operator <(HexByte left, HexByte right)`  | `bool`    | Returns `true` if `left` is less than `right` based on their underlying byte values.                         |
| `operator <=(HexByte left, HexByte right)` | `bool`    | Returns `true` if `left` is less than or equal to `right` based on their underlying byte values.             |
| `implicit operator HexByte(string value)`  | `HexByte` | Converts a valid 1–2 character hexadecimal `value` string to a `HexByte` by invoking the string constructor. |
| `implicit operator string(HexByte value)`  | `string`  | Converts a `HexByte` to its two-character uppercase hexadecimal string representation.                       |
| `implicit operator HexByte(byte value)`    | `HexByte` | Converts a byte `value` to a `HexByte` with the same underlying channel value.                               |
| `implicit operator byte(HexByte value)`    | `byte`    | Converts a `HexByte` to its underlying byte `Value`.                                                         |

## Events

*None*

## Exceptions

* `AryArgumentException`  
  Thrown by the `HexByte(string value)` constructor (and thus `implicit operator HexByte(string value)` and
  `Parse(string value)`) when the input string is `null`, whitespace-only, contains non-hex characters, or has more than
  two hex characters after trimming.
* `AryArgumentException`  
  Thrown by `FromNormalized(double value)` when `value` is not finite or lies outside the range `[0.0, 1.0]`. This
  contract also applies to callers such as `ClampAlpha(double value)` when the input fails `FromNormalized`’s
  validation.

## Example

```csharp
using Allyaria.Theming.Types;

public class HexByteExample
{
    public void Demo()
    {
        // Create from a hex string
        HexByte red = new HexByte("FF");

        // Create from a byte
        HexByte semiTransparent = new HexByte((byte)128);

        // Normalized alpha clamp and convert
        HexByte clampedAlpha = HexByte.ClampAlpha(1.5); // results in 0xFF

        // Linear interpolation between two channel values
        HexByte start = new HexByte("00");
        HexByte end = new HexByte("FF");
        HexByte mid = start.ToLerpLinearHexByte(end, 0.5); // gamma-correct midpoint

        // Convert to normalized value and string
        double normalized = red.ToNormalized(); // 1.0
        string hex = red;                       // "FF" via implicit conversion
    }
}
```

---

*Revision Date: 2025-11-15*
