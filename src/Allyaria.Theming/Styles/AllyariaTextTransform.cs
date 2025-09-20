using Allyaria.Theming.Helpers;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a validated, immutable <c>text-transform</c> CSS value.
/// <para>
/// This <see langword="readonly" /> <see langword="struct" /> implements value-based equality and accepts the standard
/// keywords: <c>none</c>, <c>capitalize</c>, <c>uppercase</c>, and <c>lowercase</c>. Additionally, <c>var(...)</c> is
/// supported for CSS custom properties and passed through unchanged.
/// </para>
/// <para>
/// Rendering: <see cref="ToCss" /> returns <c>text-transform:value;</c> (no spaces). <see cref="ToString" /> calls
/// <see cref="ToCss" />.
/// </para>
/// </summary>
public readonly struct AllyariaTextTransform : IEquatable<AllyariaTextTransform>
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaTextTransform" /> struct from a raw CSS value.</summary>
    /// <param name="value">Raw CSS value (e.g., <c>"uppercase"</c>, <c>"capitalize"</c>, <c>"var(--tt)"</c>).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is null, whitespace, or not valid for
    /// <c>text-transform</c>.
    /// </exception>
    public AllyariaTextTransform(string value) => Value = Normalize(value);

    /// <summary>Gets the normalized CSS value represented by this instance.</summary>
    public string Value { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is AllyariaTextTransform other && Equals(other);

    /// <inheritdoc />
    public bool Equals(AllyariaTextTransform other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <inheritdoc />
    public override int GetHashCode()
        => Value is null
            ? 0
            : StringComparer.Ordinal.GetHashCode(Value);

    /// <summary>Normalizes and validates a <c>text-transform</c> value.</summary>
    private static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        // Preserve original casing for function identifiers; lower-case only when treating as keywords or unit tokens.
        var trim = value.Trim();

        // Accept common CSS function forms without altering the content.
        if (StyleHelpers.IsCssFunction(trim, "var"))
        {
            return trim;
        }

        // Keyword path (lower-case & validate).
        var lower = trim.ToLowerInvariant();

        if (lower is "none" or "capitalize" or "uppercase" or "lowercase")
        {
            return lower;
        }

        // Failed normalization.
        throw new ArgumentException($"Unable to normalize text-transform: {value}.", nameof(value));
    }

    /// <summary>Produces a CSS declaration in the form <c>text-transform:value;</c> (no spaces).</summary>
    /// <returns>The CSS declaration string.</returns>
    public string ToCss()
        => string.IsNullOrWhiteSpace(Value)
            ? string.Empty
            : $"text-transform:{Value};";

    /// <summary>Returns the CSS declaration string produced by <see cref="ToCss" />.</summary>
    public override string ToString() => ToCss();

    /// <summary>Equality operator for <see cref="AllyariaTextTransform" />.</summary>
    public static bool operator ==(AllyariaTextTransform left, AllyariaTextTransform right) => left.Equals(right);

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaTextTransform" />.</summary>
    public static implicit operator AllyariaTextTransform(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaTextTransform" /> to <see cref="string" />.</summary>
    public static implicit operator string(AllyariaTextTransform transform) => transform.Value;

    /// <summary>Inequality operator for <see cref="AllyariaTextTransform" />.</summary>
    public static bool operator !=(AllyariaTextTransform left, AllyariaTextTransform right) => !left.Equals(right);
}
