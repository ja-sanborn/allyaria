using System.Collections.Immutable;

namespace Allyaria.Theming.Helpers;

public sealed class CssBuilder
{
    private ImmutableHashSet<string> _values = ImmutableHashSet<string>.Empty;

    public CssBuilder Add(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _values = _values.Add(value);
        }

        return this;
    }

    public CssBuilder Add<TStyle>(string? propertyName, TStyle? value, string? varPrefix)
        where TStyle : struct, IStyleValue
    {
        if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(value?.Value))
        {
            return this;
        }

        var prefix = varPrefix.ToCssName();

        var prefixedProperty = string.IsNullOrWhiteSpace(prefix)
            ? propertyName
            : $"--{prefix}-{propertyName}";

        return Add(value.ToCssProperty(prefixedProperty));
    }

    public override string ToString() => string.Join(string.Empty, _values);
}
