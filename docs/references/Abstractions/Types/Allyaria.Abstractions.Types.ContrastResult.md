# Allyaria.Abstractions.Types.ContrastResult

`ContrastResult` is an immutable value type (`record struct`) that represents the outcome of a color contrast resolution
operation. It encapsulates the resolved foreground color, the calculated contrast ratio between foreground and
background, and whether the minimum contrast requirement is satisfied. This type is primarily used for accessibility and
color contrast computations within the Allyaria framework.

## Constructors

`ContrastResult(HexColor foregroundColor, double contrastRatio, bool isMinimumMet)`
Initializes a new instance of `ContrastResult` with the specified resolved foreground color, calculated contrast ratio,
and minimum threshold flag.

## Properties

| Name              | Type       | Description                                                                                         |
|-------------------|------------|-----------------------------------------------------------------------------------------------------|
| `ForegroundColor` | `HexColor` | Gets the resolved opaque foreground color used in the contrast calculation.                         |
| `ContrastRatio`   | `double`   | Gets the computed contrast ratio between foreground and background colors.                          |
| `IsMinimumMet`    | `bool`     | Gets a value indicating whether the contrast ratio meets or exceeds the required minimum threshold. |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Types;

var result = new ContrastResult(
    new HexColor("#000000"),
    contrastRatio: 7.5,
    isMinimumMet: true
);

Console.WriteLine($"Foreground: {result.ForegroundColor}, Ratio: {result.ContrastRatio}, Meets Min: {result.IsMinimumMet}");
```

---

*Revision Date: 2025-10-17*
