namespace Allyaria.Theming.Contracts;

public abstract record StyleBase : IStyle
{
    private static readonly Regex PropRegex = new(
        pattern: "^[A-Za-z_][A-Za-z0-9_-]*$",
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    private static readonly Regex VarNsRegex = new(
        pattern: "^--([A-Za-z0-9-]+)_([A-Za-z_][A-Za-z0-9_-]*)$",
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    protected StyleBase(string name, string value)
    {
        var newName = ValidateInput(value: name);
        var newValue = ValidateInput(value: value);

        if (TrySplitName(input: newName, name: out var splitName, value: out var splitValue))
        {
            Name = splitName.ToCssName();
            Value = splitValue.Trim();
        }
        else
        {
            Name = ValidateInput(value: newName).ToCssName();
            Value = ValidateInput(value: newValue).Trim();
        }
    }

    /// <summary>True if both <see cref="Name" /> and <see cref="Value" /> are null/empty/whitespace.</summary>
    public bool IsEmpty => string.IsNullOrWhiteSpace(value: Name) && string.IsNullOrWhiteSpace(value: Value);

    /// <summary>True if either <see cref="Name" /> or <see cref="Value" /> is null/empty/whitespace.</summary>
    public bool IsUndefined => string.IsNullOrWhiteSpace(value: Name) || string.IsNullOrWhiteSpace(value: Value);

    /// <summary>The normalized CSS name (e.g., "font-weight", "color").</summary>
    public string Name { get; }

    /// <summary>The raw CSS value (e.g., "600", "\"Times New Roman\"").</summary>
    public string Value { get; }

    private static bool IsEscaped(string s, int i)
    {
        var count = 0;

        for (var j = i - 1; j >= 0 && s[index: j] == '\\'; j--)
        {
            count++;
        }

        return (count & 1) == 1;
    }

    /// <summary>
    /// Renders this style to a CSS declaration (e.g., "font-weight:600;"). When <paramref name="varPrefix" /> is provided, the
    /// name is rendered as a custom property: e.g., <c>--theme_font-weight:600;</c>. Returns <c>string.Empty</c> if
    /// <see cref="IsUndefined" /> is true.
    /// </summary>
    public virtual string ToCss(string? varPrefix = "")
    {
        if (IsUndefined)
        {
            return string.Empty;
        }

        var newPrefix = varPrefix.ToCssName();

        return string.IsNullOrWhiteSpace(value: newPrefix)
            ? $"{Name}:{Value};"
            : $"--{newPrefix}_{Name}:{Value};";
    }

    protected bool TrySplitName(string input, out string name, out string value)
    {
        name = string.Empty;
        value = string.Empty;

        if (string.IsNullOrWhiteSpace(value: input))
        {
            return false;
        }

        var colon = -1;
        bool inSingle = false, inDouble = false;

        for (var i = 0; i < input.Length; i++)
        {
            var ch = input[index: i];

            if (ch == '\'' && !inDouble && !IsEscaped(s: input, i: i))
            {
                inSingle = !inSingle;

                continue;
            }

            if (ch == '"' && !inSingle && !IsEscaped(s: input, i: i))
            {
                inDouble = !inDouble;

                continue;
            }

            if (ch == ':' && !inSingle && !inDouble)
            {
                colon = i;

                break;
            }
        }

        if (colon < 0)
        {
            return false;
        }

        var left = input[..colon];
        var right = input[(colon + 1)..];

        var match = VarNsRegex.Match(input: left);

        if (match.Success)
        {
            name = match.Groups[groupnum: 2].Value;
            value = right;

            return true;
        }

        if (PropRegex.IsMatch(input: left))
        {
            name = left;
            value = right;

            return true;
        }

        name = input;
        value = string.Empty;

        return true;
    }

    protected bool TryValidateInput(string? value, out string result)
    {
        result = value?.Trim() ?? string.Empty;

        return !result.Any(predicate: static c => char.IsControl(c: c));
    }

    protected string ValidateInput(string? value)
        => TryValidateInput(value: value, result: out var result)
            ? result
            : throw new ArgumentException(message: "Invalid value", paramName: nameof(value));
}
