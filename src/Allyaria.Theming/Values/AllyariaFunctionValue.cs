using Allyaria.Theming.Abstractions;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a normalized CSS function expression of the strict form <c>{funcName}({inner})</c>. This type validates the
/// function identifier and canonicalizes the stored value to <c>name(inner)</c>. No other forms (e.g., raw tokens like
/// <c>--token</c>) are accepted.
/// </summary>
public sealed record AllyariaFunctionValue : StyleValueBase
{
    /// <summary>
    /// Conservative CSS-like function identifier: starts with a letter/underscore/hyphen, followed by letters, digits,
    /// underscores, or hyphens.
    /// </summary>
    private static readonly Regex IdentifierRegex = new("^[-A-Za-z_][-A-Za-z0-9_]*$", RegexOptions.Compiled);

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaFunctionValue" /> class from a raw string. The input must be in the
    /// exact shape <c>{funcName}({inner})</c>. Nested parentheses are allowed inside <c>{inner}</c>; only the first <c>(</c>
    /// and the final trailing <c>)</c> are considered delimiters for the outer function.
    /// </summary>
    /// <param name="value">Raw CSS text in the form <c>name(inner)</c>.</param>
    /// <remarks>Invalid input yields <see cref="string.Empty" />.</remarks>
    public AllyariaFunctionValue(string value)
        : base(Normalize(value)) { }

    /// <summary>Normalizes a raw CSS function string to a standard <c>name(inner)</c> format.</summary>
    /// <param name="value">Raw CSS text to normalize.</param>
    /// <returns>Normalized CSS string or <see cref="string.Empty" /> if invalid.</returns>
    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var trimmedValue = value.Trim();

        if (!TrySplitFunc(trimmedValue, out var funcName, out var innerExpr))
        {
            return string.Empty;
        }

        if (!IdentifierRegex.IsMatch(funcName) || string.IsNullOrWhiteSpace(innerExpr))
        {
            return string.Empty;
        }

        return $"{funcName}({innerExpr})";
    }

    /// <summary>Attempts to parse a CSS-like function string in the strict form <c>name(inner)</c>.</summary>
    /// <param name="value">The candidate text to parse.</param>
    /// <param name="func">
    /// When this method returns, contains an <see cref="AllyariaFunctionValue" /> whose <see cref="StyleValueBase.Value" /> is
    /// the normalized representation (or empty if parsing fails).
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="value" /> is already in normalized form (exactly equals <c>name(inner)</c>
    /// with a lowercase function name and trimmed inner); otherwise <see langword="false" />.
    /// </returns>
    public static bool TryParse(string value, out AllyariaFunctionValue func)
    {
        func = new AllyariaFunctionValue(value);

        return !string.IsNullOrWhiteSpace(func.Value);
    }

    /// <summary>
    /// Splits a candidate function string into <c>name</c> and <c>inner</c>. Uses the position of the FIRST <c>(</c> and the
    /// LAST <c>)</c> as the outer delimiters, so any parentheses inside <c>inner</c> are ignored for the purpose of splitting.
    /// Requires no space between the name and the first <c>(</c>, and the last character of the string must be <c>)</c>.
    /// </summary>
    /// <param name="text">Input text that should represent a function call.</param>
    /// <param name="name">Outputs the normalized (lowercase, trimmed) function name.</param>
    /// <param name="inner">Outputs the inner expression (trimmed), excluding the outer parentheses.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="text" /> is well-formed; otherwise <see langword="false" />.
    /// </returns>
    private static bool TrySplitFunc(string text, out string name, out string inner)
    {
        name = string.Empty;
        inner = string.Empty;

        var start = text.IndexOf('(');
        var end = text.LastIndexOf(')');

        // Must have at least: n( )
        if (start <= 0 || end != text.Length - 1 || end <= start)
        {
            return false;
        }

        // Name: everything before the first '(' (no whitespace allowed between name and '(').
        var rawName = text[..start];

        if (rawName.EndsWith(' ') || rawName.EndsWith('\t'))
        {
            return false;
        }

        name = rawName.ToLowerInvariant().Trim();

        // Inner: everything between first '(' and last ')', trimmed. Nested parens are allowed and ignored here.
        inner = text.Substring(start + 1, end - start - 1).Trim();

        return true;
    }

    /// <summary>Implicitly converts a <see cref="string" /> to an <see cref="AllyariaFunctionValue" />.</summary>
    /// <param name="value">Raw CSS function string <c>name(inner)</c>.</param>
    /// <returns>An instance with a normalized value or empty when invalid.</returns>
    public static implicit operator AllyariaFunctionValue(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaFunctionValue" /> to <see cref="string" />.</summary>
    /// <param name="calc">Instance to convert.</param>
    /// <returns>
    /// Normalized CSS string or <see cref="string.Empty" /> if <paramref name="calc" /> is <c>null</c> or invalid.
    /// </returns>
    public static implicit operator string(AllyariaFunctionValue? calc) => calc?.Value ?? string.Empty;
}
