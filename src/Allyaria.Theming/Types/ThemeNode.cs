namespace Allyaria.Theming.Types;

public sealed record ThemeNode : IThemeNode
{
    private readonly Dictionary<Enum, ThemeNode> _children = new();

    public IStyleValue? Style { get; init; }

    public ThemeNode AddChild(Enum key, ThemeNode value)
        => !_children.TryAdd(key: key, value: value)
            ? throw new AryArgumentException(
                message: $"The key '{key}' already exists.", argName: nameof(key), argValue: key
            )
            : this;

    public ThemeNode AddValue(Enum key, IStyleValue? value)
        => !string.IsNullOrWhiteSpace(value: value?.Value)
            ? AddChild(key: key, value: new ThemeNode().SetValue(value: value))
            : this;

    public ThemeNode GetChild(Enum key)
        => !_children.TryGetValue(key: key, value: out var value)
            ? throw new AryArgumentException(
                message: $"The key '{key}' was not found.", argName: nameof(key), argValue: key
            )
            : value;

    public string GetCss(Enum key, string? varPrefix = "")
    {
        var prefix = varPrefix?.ToCssName() ?? string.Empty;
        var property = key.GetDescription().ToCssName();
        var value = GetChild(key: key).Style?.Value.Trim() ?? string.Empty;

        return string.IsNullOrWhiteSpace(value: property) || string.IsNullOrWhiteSpace(value: value)
            ? string.Empty
            : string.IsNullOrWhiteSpace(value: prefix)
                ? $"{property}:{value};"
                : $"--{prefix}-{property}:{value};";
    }

    public ThemeNode SetValue(IStyleValue? value)
        => this with
        {
            Style = value
        };
}
