using Allyaria.Theming.Abstractions;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a CSS function expression (e.g., <c>calc(...)</c>, <c>min(...)</c>, <c>max(...)</c>) and also supports CSS
/// custom properties via <c>var(--...)</c>. Ensures the stored value is normalized to a standard format.
/// </summary>
public sealed record AllyariaCssFunction : StyleValueBase
{
    /// <summary>
    /// Conservative CSS-like function identifier: starts with a letter/underscore/hyphen, followed by letters, digits,
    /// underscores, or hyphens.
    /// </summary>
    private static readonly Regex IdentifierRegex = new("^[-A-Za-z_][-A-Za-z0-9_]*$", RegexOptions.Compiled);

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaCssFunction" /> class. Accepts a raw CSS function or variable
    /// string and normalizes/validates it.
    /// </summary>
    /// <param name="value">
    /// Raw CSS text. Supported forms when no function name is supplied:
    /// <list type="bullet">
    ///     <item>
    ///         <description><c>--token</c> → normalized to <c>var(--token)</c></description>
    ///     </item>
    ///     <item>
    ///         <description><c>func(inner)</c> (no space before <c>(</c>) → preserved if <c>func</c> is a valid identifier</description>
    ///     </item>
    /// </list>
    /// Invalid input yields <see cref="string.Empty" />.
    /// </param>
    public AllyariaCssFunction(string value)
        : base(Normalize(string.Empty, value)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaCssFunction" /> class with an explicit function name.
    /// </summary>
    /// <param name="name">CSS function name (e.g., <c>calc</c>, <c>min</c>, <c>max</c>, or <c>var</c>).</param>
    /// <param name="value">
    /// Raw CSS text. Must start with <paramref name="name" /> immediately followed by <c>(</c> and end with <c>)</c>, unless
    /// <paramref name="name" /> is <c>var</c>, in which case either <c>--token</c> or <c>var(...)</c> are accepted.
    /// </param>
    public AllyariaCssFunction(string name, string value)
        : base(Normalize(name, value)) { }

    /// <summary>Normalizes a raw CSS function/variable string to a standard format.</summary>
    /// <param name="name">
    /// Function name. When blank:
    /// <list type="bullet">
    ///     <item>
    ///         <description><c>--token</c> → <c>var(--token)</c></description>
    ///     </item>
    ///     <item>
    ///         <description><c>func(inner)</c> → preserved if valid and well-formed</description>
    ///     </item>
    /// </list>
    /// </param>
    /// <param name="value">Raw CSS text to normalize.</param>
    /// <returns>Normalized CSS string or <see cref="string.Empty" /> if invalid.</returns>
    private static string Normalize(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var trimmedValue = value.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            if (trimmedValue.StartsWith("--", StringComparison.Ordinal))
            {
                return $"var({trimmedValue})";
            }

            if (!TrySplitFunc(trimmedValue, out var funcName1, out var innerExpr1))
            {
                return string.Empty;
            }

            if (!IdentifierRegex.IsMatch(funcName1))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(funcName1) || string.IsNullOrWhiteSpace(innerExpr1))
            {
                return string.Empty;
            }

            return $"{funcName1}({innerExpr1})";
        }

        var trimmedName = name.Trim().ToLowerInvariant();

        if (!trimmedValue.StartsWith(trimmedName, StringComparison.OrdinalIgnoreCase))
        {
            return string.Empty;
        }

        if (!TrySplitFunc(trimmedValue, out var funcName2, out var innerExpr2))
        {
            return string.Empty;
        }

        if (!funcName2.Equals(trimmedName, StringComparison.Ordinal))
        {
            return string.Empty;
        }

        innerExpr2 = innerExpr2.Trim();

        return innerExpr2.Length == 0
            ? string.Empty
            : $"{trimmedName}({innerExpr2})";
    }

    /// <summary>
    /// Splits a candidate function string into <c>name</c> and <c>inner</c>. Requires no space between name and <c>(</c>, and
    /// a trailing <c>)</c>.
    /// </summary>
    private static bool TrySplitFunc(string text, out string name, out string inner)
    {
        name = string.Empty;
        inner = string.Empty;

        var start = text.IndexOf('(');
        var end = text.LastIndexOf(')');

        if (start <= 0 || end != text.Length - 1 || end <= start)
        {
            return false;
        }

        name = text[..start].ToLowerInvariant().Trim();
        inner = text.Substring(start + 1, end - start - 1).Trim();

        return true;
    }

    /// <summary>Implicitly converts a <see cref="string" /> to an <see cref="AllyariaCssFunction" />.</summary>
    /// <param name="value">Raw CSS function/variable string.</param>
    /// <returns>An instance with a normalized value or empty when invalid.</returns>
    public static implicit operator AllyariaCssFunction(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaCssFunction" /> to <see cref="string" />.</summary>
    /// <param name="calc">Instance to convert.</param>
    /// <returns>
    /// Normalized CSS string or <see cref="string.Empty" /> if <paramref name="calc" /> is <c>null</c> or invalid.
    /// </returns>
    public static implicit operator string(AllyariaCssFunction? calc) => calc?.Value ?? string.Empty;
}
