using Allyaria.Theming.Helpers;

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
    /// Backing field containing the normalized, space-separated token string (e.g., <c>"underline"</c>,
    /// <c>"underline overline"</c>, <c>"none"</c>).
    /// </summary>
    private readonly string _value;

    /// <summary>The set of allowed tokens for <c>text-decoration</c>. This set is used during normalization.</summary>
    private static readonly HashSet<string> AllowedTokens = new(StringComparer.Ordinal)
    {
        "none",
        "underline",
        "overline",
        "line-through"
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaTextDecoration" /> struct from a raw CSS value.
    /// </summary>
    /// <param name="value">
    /// The raw CSS value, possibly containing space-separated tokens such as <c>"underline overline"</c> or a single token
    /// like <c>"none"</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="value" /> is <see langword="null" /> or consists
    /// only of whitespace.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> contains tokens that are not allowed or combines <c>none</c> with other tokens.
    /// </exception>
    public AllyariaTextDecoration(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), "text-decoration value cannot be null or whitespace.");
        }

        _value = Normalize(value);
    }

    /// <summary>
    /// Gets the normalized, space-separated token string that represents this <c>text-decoration</c> value.
    /// </summary>
    public string Value => _value;

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
    public bool Equals(AllyariaTextDecoration other) => string.Equals(_value, other._value, StringComparison.Ordinal);

    /// <summary>Returns a hash code for this instance based on the normalized value.</summary>
    /// <returns>A 32-bit signed hash code.</returns>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(_value);

    /// <summary>Normalizes and validates a <c>text-decoration</c> value.</summary>
    /// <param name="raw">The raw input string containing one or more tokens.</param>
    /// <returns>The normalized, space-separated token string.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the value contains tokens that are not allowed or combines <c>none</c> with other tokens.
    /// </exception>
    private static string Normalize(string raw)
    {
        // Use shared token normalizer: trims, lowercases, validates, dedupes, preserves order,
        // and disallows "none" combinations when requested.
        var normalized = StyleHelpers.NormalizeSpaceSeparatedTokens(
            raw,
            AllowedTokens,
            "text-decoration",
            true,
            "none",
            "none, underline, overline, line-through"
        );

        if (string.IsNullOrEmpty(normalized))
        {
            // Constructor already rejects null/whitespace, this is defensive.
            throw new ArgumentException("text-decoration produced no tokens after normalization.", nameof(raw));
        }

        return normalized;
    }

    /// <summary>Produces a CSS declaration in the form <c>text-decoration:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string for this value.</returns>
    public string ToCss() => $"text-decoration:{_value};";

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
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="value" /> is <see langword="null" /> or consists
    /// only of whitespace.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> contains tokens that are not allowed or combines <c>none</c> with other tokens.
    /// </exception>
    public static implicit operator AllyariaTextDecoration(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaTextDecoration" /> to <see cref="string" />.</summary>
    /// <param name="decoration">The <see cref="AllyariaTextDecoration" /> instance.</param>
    /// <returns>The normalized, space-separated token string represented by <paramref name="decoration" />.</returns>
    public static implicit operator string(AllyariaTextDecoration decoration) => decoration._value;

    /// <summary>Inequality operator for <see cref="AllyariaTextDecoration" /> using value equality.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if not equal; otherwise <see langword="false" />.</returns>
    public static bool operator !=(AllyariaTextDecoration left, AllyariaTextDecoration right) => !left.Equals(right);
}
