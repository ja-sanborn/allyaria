# Allyaria.Abstractions.Extensions.EnumExtensions

`EnumExtensions` is a static utility class that provides extension methods for working with enumeration (`Enum`) values.
It simplifies retrieving descriptive text from `DescriptionAttribute` decorations or automatically generates readable
names when attributes are not present. Results are cached to maximize performance and reduce reflection overhead.

## Constructors

*None*

## Properties

*None*

## Methods

| Name                                      | Returns  | Description                                                                                                                                                                                                                                                    |
|-------------------------------------------|----------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GetDescription(this Enum value)`         | `string` | Retrieves a user-friendly description for the given enum value. If a `DescriptionAttribute` is present, its description is returned; otherwise, the member name is converted from PascalCase. Supports composite `[Flags]` enums by joining constituent names. |
| `GetDescription<TEnum>(this TEnum value)` | `string` | Strongly-typed overload of `GetDescription(Enum)`. Retrieves the same descriptive string for the specified enum value.                                                                                                                                         |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Abstractions.Extensions;
using System.ComponentModel;

[Flags]
public enum FileAccessMode
{
    [Description("Read only")]
    Read = 1,
    [Description("Write only")]
    Write = 2,
    Execute = 4
}

public class Example
{
    public void ShowDescriptions()
    {
        var single = FileAccessMode.Read.GetDescription();      // "Read only"
        var combined = (FileAccessMode.Read | FileAccessMode.Write).GetDescription(); // "Read only, Write only"
        var fallback = FileAccessMode.Execute.GetDescription(); // "Execute"
    }
}
```

---

*Revision Date: 2025-10-17*
