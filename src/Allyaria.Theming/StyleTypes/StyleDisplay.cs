namespace Allyaria.Theming.StyleTypes;

public sealed record StyleDisplay : StyleValueBase
{
    public StyleDisplay(Kind kind)
        : base(value: kind.GetDescription()) { }

    public enum Kind
    {
        [Description(description: "block")]
        Block,

        [Description(description: "flex")]
        Flex,

        [Description(description: "flow-root")]
        FlowRoot,

        [Description(description: "grid")]
        Grid,

        [Description(description: "inline")]
        Inline,

        [Description(description: "inline-block")]
        InlineBlock,

        [Description(description: "inline-flex")]
        InlineFlex,

        [Description(description: "inline-grid")]
        InlineGrid,

        [Description(description: "inline-table")]
        InlineTable,

        [Description(description: "list-item")]
        ListItem,

        [Description(description: "none")]
        None,

        [Description(description: "table")]
        Table,

        [Description(description: "table-caption")]
        TableCaption,

        [Description(description: "table-cell")]
        TableCell,

        [Description(description: "table-column")]
        TableColumn,

        [Description(description: "table-column-group")]
        TableColumnGroup,

        [Description(description: "table-footer-group")]
        TableFooterGroup,

        [Description(description: "table-header-group")]
        TableHeaderGroup,

        [Description(description: "table-row")]
        TableRow,

        [Description(description: "table-row-group")]
        TableRowGroup
    }

    public static StyleDisplay Parse(string? value)
        => Enum.TryParse(value: value, ignoreCase: true, result: out Kind kind)
            ? new StyleDisplay(kind: kind)
            : throw new AryArgumentException(message: $"Invalid style: {value}", argName: nameof(value));

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

    public static implicit operator StyleDisplay(string? value) => Parse(value: value);

    public static implicit operator string(StyleDisplay? value) => value?.Value ?? string.Empty;
}
