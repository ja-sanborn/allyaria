namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a CSS <c>display</c> value within the Allyaria theming system. Provides a strongly typed wrapper for common
/// CSS display keywords controlling element layout behavior.
/// </summary>
public sealed record StyleDisplay : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleDisplay" /> record using the specified display <see cref="Kind" />.
    /// </summary>
    /// <param name="kind">The display kind that determines how the element is rendered in the layout.</param>
    public StyleDisplay(Kind kind)
        : base(value: kind.GetDescription()) { }

    /// <summary>Defines the supported CSS <c>display</c> property values.</summary>
    public enum Kind
    {
        /// <summary>The element generates a block-level box (starts on a new line, full width by default).</summary>
        [Description(description: "block")]
        Block,

        /// <summary>The element behaves as a flexible container using the Flexbox layout model.</summary>
        [Description(description: "flex")]
        Flex,

        /// <summary>Creates a block-level box that establishes a new block formatting context.</summary>
        [Description(description: "flow-root")]
        FlowRoot,

        /// <summary>The element behaves as a grid container using the CSS Grid layout model.</summary>
        [Description(description: "grid")]
        Grid,

        /// <summary>The element generates one or more inline-level boxes that do not start on a new line.</summary>
        [Description(description: "inline")]
        Inline,

        /// <summary>
        /// The element behaves as an inline-level block container (flows inline but accepts block properties).
        /// </summary>
        [Description(description: "inline-block")]
        InlineBlock,

        /// <summary>The element behaves as an inline-level flex container.</summary>
        [Description(description: "inline-flex")]
        InlineFlex,

        /// <summary>The element behaves as an inline-level grid container.</summary>
        [Description(description: "inline-grid")]
        InlineGrid,

        /// <summary>The element behaves as an inline-level table container.</summary>
        [Description(description: "inline-table")]
        InlineTable,

        /// <summary>The element behaves as a list item with a marker box (e.g., a bullet).</summary>
        [Description(description: "list-item")]
        ListItem,

        /// <summary>The element is not displayed and takes up no layout space.</summary>
        [Description(description: "none")]
        None,

        /// <summary>The element behaves as a block-level table.</summary>
        [Description(description: "table")]
        Table,

        /// <summary>The element behaves as a table caption.</summary>
        [Description(description: "table-caption")]
        TableCaption,

        /// <summary>The element behaves as a table cell.</summary>
        [Description(description: "table-cell")]
        TableCell,

        /// <summary>The element behaves as a table column.</summary>
        [Description(description: "table-column")]
        TableColumn,

        /// <summary>The element behaves as a group of table columns.</summary>
        [Description(description: "table-column-group")]
        TableColumnGroup,

        /// <summary>The element behaves as a group of table footer rows.</summary>
        [Description(description: "table-footer-group")]
        TableFooterGroup,

        /// <summary>The element behaves as a group of table header rows.</summary>
        [Description(description: "table-header-group")]
        TableHeaderGroup,

        /// <summary>The element behaves as a single table row.</summary>
        [Description(description: "table-row")]
        TableRow,

        /// <summary>The element behaves as a group of table rows.</summary>
        [Description(description: "table-row-group")]
        TableRowGroup
    }

    /// <summary>
    /// Parses a string representation of a CSS <c>display</c> value into a <see cref="StyleDisplay" /> instance.
    /// </summary>
    /// <param name="value">The string representation of the display value.</param>
    /// <returns>A new <see cref="StyleDisplay" /> instance representing the parsed value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> does not correspond to a valid <see cref="Kind" />.
    /// </exception>
    public static StyleDisplay Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleDisplay(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

    /// <summary>Attempts to parse a string into a <see cref="StyleDisplay" /> instance.</summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleDisplay" /> instance or <see langword="null" /> if
    /// parsing failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleDisplay? result)
    {
        try
        {
            result = Parse(value: value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a string into a <see cref="StyleDisplay" /> instance.</summary>
    /// <param name="value">The string representation of the display value.</param>
    /// <returns>A <see cref="StyleDisplay" /> instance representing the provided value.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided string cannot be parsed into a valid
    /// <see cref="Kind" />.
    /// </exception>
    public static implicit operator StyleDisplay(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleDisplay" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleDisplay" /> instance to convert.</param>
    /// <returns>
    /// The underlying CSS <c>display</c> string value, or an empty string if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleDisplay? value) => value?.Value ?? string.Empty;
}
