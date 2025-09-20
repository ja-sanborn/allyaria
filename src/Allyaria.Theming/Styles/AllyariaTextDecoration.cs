namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>text-decoration</c> CSS value consisting of a space-separated list of decoration
/// keywords.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts the standard
/// keywords <c>none</c>, <c>underline</c>, <c>overline</c>, and <c>line-through</c>. The value may be a space-separated
/// combination of these tokens, except that <c>none</c> cannot be combined with any other value.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>text-decoration:value;</c> (no spaces around the colon or semicolon).
/// <see cref="ToString" /> calls <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaTextDecoration : IEquatable<AllyariaTextDecoration>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaTextDecoration" /> struct from a raw CSS value.
    /// </summary>
    /// <param name="value">
    /// The raw CSS value, possibly containing space-separated tokens such as <c>"underline overline"</c> or a single token
    /// like <c>"none"</c>.
    /// </param>
    public AllyariaTextDecoration(string value) => Value = Normalize(value);

    /// <summary>
    /// Gets the normalized, space-separated token string that represents this <c>text-decoration</c> value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance by comparing normalized values.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is AllyariaTextDecoration other && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="AllyariaTextDecoration" /> is equal to the current instance by comparing
    /// normalized values.
    /// </summary>
    /// <param name="other">The other instance to compare with.</param>
    /// <returns><see langword="true" /> if equal; otherwise <see langword="false" />.</returns>
    public bool Equals(AllyariaTextDecoration other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>Normalizes and validates a <c>text-decoration</c> value.</summary>
    /// <param name="value">The raw input string containing one or more tokens.</param>
    /// <returns>The normalized, space-separated token string.</returns>
    internal static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var normalized = NormalizeTokens(value);

        return !string.IsNullOrEmpty(normalized)
            ? normalized
            : string.Empty;
    }

    /// <summary>
    /// Normalizes a space-separated token list by validating against an allowed set. The resulting tokens are lowercased,
    /// de-duplicated (order-preserving), and joined with a single space.
    /// </summary>
    /// <param name="value">The input string containing tokens.</param>
    /// <returns>
    /// The canonicalized token string or <c>null</c> if <paramref name="value" /> is <c>null</c> or empty after trimming.
    /// </returns>
    internal static string? NormalizeTokens(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var tokens = value
            .Split(
                new[]
                {
                    ' ',
                    '\t',
                    '\r',
                    '\n'
                }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            )
            .Select(t => t.ToLowerInvariant())
            .ToArray();

        if (tokens.Length == 0)
        {
            return null;
        }

        HashSet<string> allowed = new(StringComparer.Ordinal)
        {
            "none",
            "underline",
            "overline",
            "line-through"
        };

        if (tokens.Any(t => !allowed.Contains(t)))
        {
            return null;
        }

        if (tokens.Contains("none") && tokens.Length > 1)
        {
            return null;
        }

        var list = new HashSet<string>(StringComparer.Ordinal);

        foreach (var token in tokens)
        {
            list.Add(token);
        }

        return string.Join(' ', list).Trim();
    }

    /// <summary>Produces a CSS declaration in the form <c>text-decoration:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string for this value.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"text-decoration:{Value};";

    /// <summary>Returns the CSS declaration produced by <see cref="ToCss" />.</summary>
    /// <returns>The CSS declaration string.</returns>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaTextDecoration" /> using value equality.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if equal; otherwise <see langword="false" />.</returns>
    public static bool operator ==(AllyariaTextDecoration left, AllyariaTextDecoration right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaTextDecoration" />.</summary>
    /// <param name="value">The raw CSS value to convert.</param>
    /// <returns>An <see cref="AllyariaTextDecoration" /> created from <paramref name="value" />.</returns>
    public static implicit operator AllyariaTextDecoration(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaTextDecoration" /> to <see cref="string" />.</summary>
    /// <param name="decoration">The <see cref="AllyariaTextDecoration" /> instance.</param>
    /// <returns>The normalized, space-separated token string represented by <paramref name="decoration" />.</returns>
    public static implicit operator string(AllyariaTextDecoration decoration) => decoration.Value;

    /// <summary>Inequality operator for <see cref="AllyariaTextDecoration" /> using value equality.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if not equal; otherwise <see langword="false" />.</returns>
    public static bool operator !=(AllyariaTextDecoration left, AllyariaTextDecoration right) => !left.Equals(right);
}
