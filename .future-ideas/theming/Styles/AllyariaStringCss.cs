using Allyaria.Theming.Contracts;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.Styles;

public sealed class AllyariaStringCss : CssBase
{
    public AllyariaStringCss(string cssProperty) : base(cssProperty)
    {
        if (!TryParseCssProperty(cssProperty, out var name, out var value))
        {
            throw new ArgumentException("Unable to parse CSS property.", nameof(cssProperty));
        }

        CssName = name;
        CssValue = Create(value);
    }

    public AllyariaStringCss(string name, ValueBase value) : base(name, value) { }

    protected override ValueBase Create(string value)
    {
        if (AllyariaStringValue.TryParse(value, out var str))
        {
            return str!;
        }

        throw new ArgumentException("Invalid CSS value.", nameof(value));
    }

    public static AllyariaStringCss Parse(string value) => new(value);

    public static bool TryParse(string value, out AllyariaStringCss? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;

            return false;
        }

        try
        {
            result = new AllyariaStringCss(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    public static implicit operator AllyariaStringCss(string value) => new(value);

    public static implicit operator string(AllyariaStringCss value)
        => (value ?? throw new ArgumentNullException(nameof(value))).CssProperty;
}
