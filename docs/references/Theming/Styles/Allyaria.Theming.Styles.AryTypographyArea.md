# Allyaria.Theming.Styles.AryTypographyArea

`AryTypographyArea` is a readonly record struct that defines the typography configuration for a component in the
Allyaria theming system. It currently manages a single surface-level typography definition but is designed to support
future extensions such as headings, body text, and captions as the theming model evolves.

## Constructors

| Signature                                                    | Description                                                                                                                         |
|--------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------|
| `AryTypographyArea()`                                        | Initializes a new instance of `AryTypographyArea` using default typography values.                                                  |
| `AryTypographyArea(AryTypography? surfaceTypography = null)` | Initializes a new instance with an optional surface typography. If `null`, defaults are applied via a new `AryTypography` instance. |

## Properties

| Name      | Type            | Description                                                                                         |
|-----------|-----------------|-----------------------------------------------------------------------------------------------------|
| `Surface` | `AryTypography` | The typography applied to the component’s primary surface, typically used for general content text. |

## Methods

| Name           | Signature                                                            | Description                                                                                                                              | Returns             |
|----------------|----------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------|---------------------|
| `Cascade`      | `AryTypographyArea Cascade(AryTypography? surfaceTypography = null)` | Returns a new `AryTypographyArea` instance with the specified overrides applied while retaining existing values for unspecified members. | `AryTypographyArea` |
| `ToTypography` | `AryTypography ToTypography(ComponentType type)`                     | Resolves and returns the appropriate `AryTypography` for the given `ComponentType`. Currently returns `Surface` for all types.           | `AryTypography`     |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
// Create a typography area with default surface typography
var typographyArea = new AryTypographyArea();

// Retrieve the typography for a surface component
AryTypography surfaceTypography = typographyArea.ToTypography(ComponentType.Surface);

// Update the typography area with a custom style
var updated = typographyArea.Cascade(new AryTypography(fontWeight: new("600")));
```

---

*Revision Date: 2025-10-17*
