# Allyaria.Theming.Types.HexByte

`HexByte` is a lightweight, immutable struct representing a single 8-bit color channel (0–255) used in RGBA color
models. It supports parsing and formatting hexadecimal strings, comparison and equality operations, normalized
conversions, and linear or gamma-correct interpolation. This type provides a convenient and type-safe way to work with
individual color channel values in Allyaria’s color-processing utilities.

## Constructors

`HexByte()`
Initializes a new instance of `HexByte` with a default value of `0` (hex string `"00"`).

`HexByte(byte value)`
Initializes a new instance of `HexByte` using a raw byte value.

`HexByte(string value)`
Initializes a new instance of `HexByte` from a hexadecimal string (1–2 hex characters). Whitespace is ignored.

## Properties

| Name    | Type   | Description                                                |
|---------|--------|------------------------------------------------------------|
| `Value` | `byte` | Gets the 8-bit channel value represented by this instance. |

## Methods

| Name                                              | Returns   | Description                                                                                              |
|---------------------------------------------------|-----------|----------------------------------------------------------------------------------------------------------|
| `ClampAlpha(double value)`                        | `HexByte` | Clamps a normalized alpha value between 0.0 and 1.0 and converts it to a `HexByte`.                      |
| `CompareTo(HexByte other)`                        | `int`     | Compares this `HexByte` to another based on their numeric values.                                        |
| `Equals(HexByte other)`                           | `bool`    | Indicates whether this instance equals another `HexByte`.                                                |
| `Equals(object? obj)`                             | `bool`    | Determines whether the specified object is equal to this instance.                                       |
| `FromNormalized(double value)`                    | `HexByte` | Creates a `HexByte` from a normalized double value in the range [0, 1]. Throws if invalid or not finite. |
| `GetHashCode()`                                   | `int`     | Returns a hash code for this instance.                                                                   |
| `Parse(string value)`                             | `HexByte` | Parses a 1–2 character hexadecimal string into a `HexByte`.                                              |
| `ToLerpByte(byte end, double factor)`             | `byte`    | Performs linear interpolation in byte space between two values.                                          |
| `ToLerpHexByte(byte end, double factor)`          | `HexByte` | Produces an interpolated `HexByte` using linear (non-gamma) interpolation.                               |
| `ToLerpLinearByte(byte end, double factor)`       | `byte`    | Performs gamma-correct interpolation (linear-light) between two sRGB byte values.                        |
| `ToLerpLinearHexByte(HexByte end, double factor)` | `HexByte` | Produces an interpolated `HexByte` using gamma-correct interpolation.                                    |
| `ToNormalized()`                                  | `double`  | Converts this channel value to a normalized double between 0.0 and 1.0.                                  |
| `ToSrgbLinearValue()`                             | `double`  | Converts this channel’s sRGB value to a linear-light value using the sRGB transfer function.             |
| `ToString()`                                      | `string`  | Returns the uppercase two-character hexadecimal string representation.                                   |
| `TryParse(string? value, out HexByte result)`     | `bool`    | Attempts to parse a hexadecimal string into a `HexByte`. Returns `true` if successful.                   |

## Operators

| Operator                                  | Returns   | Description                                                           |
|-------------------------------------------|-----------|-----------------------------------------------------------------------|
| `==`                                      | `bool`    | Determines whether two `HexByte` instances are equal.                 |
| `!=`                                      | `bool`    | Determines whether two `HexByte` instances are not equal.             |
| `>`                                       | `bool`    | Determines whether one `HexByte` is greater than another.             |
| `<`                                       | `bool`    | Determines whether one `HexByte` is less than another.                |
| `>=`                                      | `bool`    | Determines whether one `HexByte` is greater than or equal to another. |
| `<=`                                      | `bool`    | Determines whether one `HexByte` is less than or equal to another.    |
| `implicit operator HexByte(string value)` | `HexByte` | Converts a valid hexadecimal string to a `HexByte`.                   |
| `implicit operator string(HexByte value)` | `string`  | Converts a `HexByte` to its two-character hexadecimal string.         |
| `implicit operator HexByte(byte value)`   | `HexByte` | Converts a byte value to a `HexByte`.                                 |
| `implicit operator byte(HexByte value)`   | `byte`    | Converts a `HexByte` to its underlying byte value.                    |

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Types;

// Create from a byte
var red = new HexByte(255); // "FF"

// Create from a hex string
var alpha = new HexByte("80");

// Parse and interpolate
var start = new HexByte("00");
var end = new HexByte("FF");
var mid = start.ToLerpLinearHexByte(end, 0.5); // ≈ "BC"

// Normalized conversions
double normalized = red.ToNormalized(); // 1.0
double linear = red.ToSrgbLinearValue(); // ≈ 1.0
```

---

*Revision Date: 2025-10-17*
