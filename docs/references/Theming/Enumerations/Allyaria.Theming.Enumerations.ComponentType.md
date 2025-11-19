# Allyaria.Theming.Enumerations.ComponentType

`ComponentType` is an enumeration defining the semantic and structural UI component categories recognized by the
Allyaria theming system. These values help determine which theme rules, variables, and visual patterns apply to each
component type, enabling consistent styling across the UI.

## Summary

`ComponentType` is an enum representing high-level UI component categories. The Allyaria theming engine uses these
values to apply global rules (such as body or HTML styling), typographic sizing for headings, link-specific behavior, or
general surface/text styling.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name          | Type            | Description                                                                             |
|---------------|-----------------|-----------------------------------------------------------------------------------------|
| `GlobalBody`  | `ComponentType` | Represents the document `<body>` element, providing global background and base styling. |
| `GlobalFocus` | `ComponentType` | Represents global focus outline styling for accessibility.                              |
| `GlobalHtml`  | `ComponentType` | Represents the root `<html>` element, typically carrying global theme variables.        |
| `Heading1`    | `ComponentType` | Represents a top-level heading (`<h1>`).                                                |
| `Heading2`    | `ComponentType` | Represents a second-level heading (`<h2>`).                                             |
| `Heading3`    | `ComponentType` | Represents a third-level heading (`<h3>`).                                              |
| `Heading4`    | `ComponentType` | Represents a fourth-level heading (`<h4>`).                                             |
| `Heading5`    | `ComponentType` | Represents a fifth-level heading (`<h5>`).                                              |
| `Heading6`    | `ComponentType` | Represents a sixth-level heading (`<h6>`).                                              |
| `Link`        | `ComponentType` | Represents a hyperlink (`<a>`), including styled link variants (visited, hover, etc.).  |
| `Surface`     | `ComponentType` | Represents surfaces or containers such as cards, panels, or background sections.        |
| `Text`        | `ComponentType` | Represents general text content such as paragraphs, spans, or inline elements.          |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class Component
{
    public ComponentType Type { get; }

    public Component(ComponentType type)
    {
        Type = type;
    }

    public bool IsHeading()
    {
        return Type is ComponentType.Heading1
            or ComponentType.Heading2
            or ComponentType.Heading3
            or ComponentType.Heading4
            or ComponentType.Heading5
            or ComponentType.Heading6;
    }
}
```

---

*Revision Date: 2025-11-15*
