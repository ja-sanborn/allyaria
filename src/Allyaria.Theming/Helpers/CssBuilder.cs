namespace Allyaria.Theming.Helpers;

public sealed class CssBuilder
{
    private readonly SortedDictionary<string, string> _styles =
        [];

    public CssBuilder Add(string name, string value, string? varPrefix = "")
    {
        if (string.IsNullOrWhiteSpace(value: name) || string.IsNullOrWhiteSpace(value: value))
        {
            return this;
        }

        var property = name.ToCssName();

        if (string.IsNullOrWhiteSpace(value: property))
        {
            return this;
        }

        var prefix = varPrefix.ToCssName();

        var propertyName = string.IsNullOrWhiteSpace(value: prefix)
            ? property
            : $"--{prefix}-{property}";

        _ = _styles.TryAdd(key: propertyName, value: value);

        return this;
    }

    public override string ToString()
    {
        if (_styles.Count is 0)
        {
            return string.Empty;
        }

        var list = new List<string>();

        foreach (var styleValue in _styles)
        {
            list.Add(item: $"{styleValue.Key}:{styleValue.Value}");
        }

        return string.Join(separator: ';', values: list);
    }
}
