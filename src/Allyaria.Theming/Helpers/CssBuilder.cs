using System.Collections.Immutable;

namespace Allyaria.Theming.Helpers;

public sealed class CssBuilder
{
    private ImmutableHashSet<string> _values = ImmutableHashSet<string>.Empty;

    public CssBuilder Add(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value: value))
        {
            _values = _values.Add(item: value);
        }

        return this;
    }

    public CssBuilder Add<TStyle>(string? propertyName, TStyle? value, string? varPrefix)
        where TStyle : struct, IStyleValue
    {
        if (string.IsNullOrWhiteSpace(value: propertyName) || string.IsNullOrWhiteSpace(value: value?.Value))
        {
            return this;
        }

        var prefix = varPrefix.ToCssName();

        var prefixedProperty = string.IsNullOrWhiteSpace(value: prefix)
            ? propertyName
            : $"--{prefix}-{propertyName}";

        return Add(value: value.ToCssProperty(propertyName: prefixedProperty));
    }

    public override string ToString() => string.Join(separator: string.Empty, values: _values);
}
