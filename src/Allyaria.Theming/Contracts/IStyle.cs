namespace Allyaria.Theming.Contracts;

/// <summary>Public contract for a style entry that can render to CSS.</summary>
public interface IStyle
{
    /// <summary>True if both <see cref="Name" /> and <see cref="Value" /> are null/empty/whitespace.</summary>
    bool IsEmpty { get; }

    /// <summary>True if either <see cref="Name" /> or <see cref="Value" /> is null/empty/whitespace.</summary>
    bool IsUndefined { get; }

    /// <summary>The normalized CSS name (e.g., "font-weight", "color").</summary>
    string Name { get; }

    /// <summary>The raw CSS value (e.g., "600", "\"Times New Roman\"").</summary>
    string Value { get; }

    /// <summary>
    /// Renders this style to a CSS declaration (e.g., "font-weight:600;"). When <paramref name="varPrefix" /> is provided, the
    /// name is rendered as a custom property: e.g., <c>--theme_font-weight:600;</c>. Returns <c>string.Empty</c> if
    /// <see cref="IsUndefined" /> is true.
    /// </summary>
    string ToCss(string? varPrefix = "");
}
