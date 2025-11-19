# Allyaria.Theming.StyleTypes.StyleDisplay

`StyleDisplay` is a sealed style value record representing the CSS `display` property within the Allyaria theming
system. It inherits from `StyleValueBase` (which implements `IStyleValue`) and provides a strongly typed wrapper over
all standard CSS display keywords through its nested `Kind` enumeration.

## Summary

`StyleDisplay` is an immutable, validated CSS style value used to control how an element participates in layout—whether
as a block, inline, flex container, grid container, table element, or not displayed at all. Each enum member maps to the
official CSS display keyword via `[Description]` attributes. The resulting keyword is validated by the `StyleValueBase`
constructor, ensuring safe CSS serialization. The type supports parsing, `TryParse`, and implicit conversions for
ergonomic usage in theming and component styling.

## Constructors

`StyleDisplay(Kind kind)` Creates a new `StyleDisplay` instance using the specified display `kind`. Maps the enum value
to its CSS keyword and validates it via `StyleValueBase`.

## Properties

| Name    | Type     | Description                                                                                           |
|---------|----------|-------------------------------------------------------------------------------------------------------|
| `Value` | `string` | The normalized CSS `display` keyword associated with this instance (e.g., `"flex"`, `"inline-grid"`). |

## Methods

| Name                                                | Returns        | Description                                                                                            |
|-----------------------------------------------------|----------------|--------------------------------------------------------------------------------------------------------|
| `Parse(string? value)`                              | `StyleDisplay` | Parses a CSS `display` string into a `StyleDisplay` by mapping to a `Kind`; throws if invalid.         |
| `TryParse(string? value, out StyleDisplay? result)` | `bool`         | Attempts to parse the input into a `StyleDisplay`, returning `true` on success and assigning `result`. |
| `Equals(object? obj)`                               | `bool`         | Determines structural equality with another object.                                                    |
| `Equals(StyleDisplay? other)`                       | `bool`         | Determines equality with another `StyleDisplay` instance.                                              |
| `GetHashCode()`                                     | `int`          | Returns a hash code based on record semantics.                                                         |

## Nested Types

| Name   | Kind   | Description                               |
|--------|--------|-------------------------------------------|
| `Kind` | `enum` | Defines the supported CSS display values. |

### `StyleDisplay.Kind` Members

| Name               | Description                                                 |
|--------------------|-------------------------------------------------------------|
| `Block`            | `"block"` — Generates a block-level element.                |
| `Flex`             | `"flex"` — Creates a flex container.                        |
| `FlowRoot`         | `"flow-root"` — Establishes a new block formatting context. |
| `Grid`             | `"grid"` — Creates a grid container.                        |
| `Inline`           | `"inline"` — Generates inline-level boxes.                  |
| `InlineBlock`      | `"inline-block"` — Inline-level box with block formatting.  |
| `InlineFlex`       | `"inline-flex"` — Inline-level flex container.              |
| `InlineGrid`       | `"inline-grid"` — Inline-level grid container.              |
| `InlineTable`      | `"inline-table"` — Inline-level table.                      |
| `ListItem`         | `"list-item"` — List item with marker box.                  |
| `None`             | `"none"` — Element is not displayed.                        |
| `Table`            | `"table"` — Block-level table.                              |
| `TableCaption`     | `"table-caption"` — Table caption element.                  |
| `TableCell`        | `"table-cell"` — Table cell.                                |
| `TableColumn`      | `"table-column"` — Table column.                            |
| `TableColumnGroup` | `"table-column-group"` — Group of table columns.            |
| `TableFooterGroup` | `"table-footer-group"` — Table footer row group.            |
| `TableHeaderGroup` | `"table-header-group"` — Table header row group.            |
| `TableRow`         | `"table-row"` — Table row.                                  |
| `TableRowGroup`    | `"table-row-group"` — Table row group.                      |

## Operators

| Operator                                               | Returns        | Description                                                  |
|--------------------------------------------------------|----------------|--------------------------------------------------------------|
| `implicit operator StyleDisplay(string? value)`        | `StyleDisplay` | Parses the string using `Parse`.                             |
| `implicit operator string(StyleDisplay? value)`        | `string`       | Converts the instance to its CSS keyword or an empty string. |
| `operator ==(StyleDisplay? left, StyleDisplay? right)` | `bool`         | Compares two instances for equality.                         |
| `operator !=(StyleDisplay? left, StyleDisplay? right)` | `bool`         | Compares two instances for inequality.                       |

## Events

*None*

## Exceptions

* **`AryArgumentException`**  
  Thrown by `Parse` and implicit string conversion when the input does not correspond to a valid `Kind`.

* **`AryArgumentException`**  
  Thrown indirectly by `StyleValueBase` if the CSS keyword contains invalid or unsafe characters.

## Example

```csharp
using Allyaria.Theming.StyleTypes;

public class DisplayDemo
{
    public void Demo()
    {
        // Create from enum
        var flex = new StyleDisplay(StyleDisplay.Kind.Flex);
        string cssFlex = flex.Value;         // "flex"

        // Parse from string
        var inlineGrid = StyleDisplay.Parse("inline-grid");
        string cssInlineGrid = inlineGrid;   // "inline-grid"

        // TryParse
        if (StyleDisplay.TryParse("table-row-group", out var rowGroup))
        {
            string cssRowGroup = rowGroup!.Value;
        }

        // Implicit from string
        StyleDisplay block = "block";
        string cssBlock = block;             // "block"
    }
}
```

---

*Revision Date: 2025-11-15*
