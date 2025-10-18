# Allyaria.Abstractions.Extensions.GenericExtensions

`GenericExtensions` is a static utility class that provides generic extension methods for nullable value types. It
simplifies working with `Nullable<T>` by enabling concise syntax for returning default values when a nullable variable
has no assigned value. This helps eliminate common null-checking patterns in value-type operations across the Allyaria
SDK.

## Constructors

*None*

## Properties

*None*

## Methods

| Name                                                    | Returns | Description                                                                                                                                        |
|---------------------------------------------------------|---------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| `OrDefault<T>(this T? value, T defaultValue = default)` | `T`     | Returns the provided nullable value if it has a value; otherwise, returns the specified `defaultValue`, or the default of `T` if none is provided. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Extensions;

int? count = null;
int result = count.OrDefault(10); // Returns 10

double? value = 3.14;
double safeValue = value.OrDefault(); // Returns 3.14
```

---

*Revision Date: 2025-10-17*
