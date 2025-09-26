using System.Text.RegularExpressions;

namespace Allyaria.Theming.Contracts;

/// <summary>Provides a base abstraction for CSS property/value pairs within Allyaria theming.</summary>
/// <remarks>
/// This type enforces canonical CSS property naming (lowercase, hyphen-delimited) and produces normalized CSS declarations
/// in the form <c>"name: value;"</c>.
/// </remarks>
public abstract class CssBase : IEquatable<CssBase>
{
    /// <summary>
    /// A compiled regular expression that validates canonical CSS property names (lowercase letters with optional <c>-</c>
    /// separators, e.g., <c>background-color</c>).
    /// </summary>
    private static readonly Regex CssPropRegex = new(
        "^[a-z]+(?:-[a-z]+)*$", RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="CssBase" /> class from a single CSS declaration string.
    /// </summary>
    /// <param name="cssProperty">A full CSS declaration.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="cssProperty" /> is null/whitespace or cannot be parsed into a valid property and value.
    /// </exception>
    protected CssBase(string cssProperty) => _ = cssProperty;

    /// <summary>
    /// Initializes a new instance of the <see cref="CssBase" /> class from a name and a pre-parsed <see cref="ValueBase" />.
    /// </summary>
    /// <param name="name">The CSS property name in any case (will be canonicalized).</param>
    /// <param name="value">The parsed CSS value instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name" /> is null/whitespace or invalid.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <see langword="null" />.</exception>
    protected CssBase(string name, ValueBase value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        var canonical = name.Trim().ToLowerInvariant();

        if (!IsNameValid(canonical))
        {
            throw new ArgumentException("Invalid CSS property name.", nameof(name));
        }

        CssName = canonical;
        CssValue = value;
    }

    /// <summary>Gets the canonical CSS property name (lowercase with <c>-</c> separators).</summary>
    public string CssName { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets the complete CSS declaration (e.g., <c>"color: #fff;"</c>) or an empty string if <see cref="CssValue" /> is
    /// <see langword="null" />.
    /// </summary>
    public string CssProperty
        => CssValue is null
            ? string.Empty
            : $"{CssName}:{CssValue.Value};";

    /// <summary>Gets the parsed CSS value for this property, if any.</summary>
    public ValueBase? CssValue { get; protected init; }

    /// <summary>Creates a <see cref="ValueBase" /> instance from the provided raw CSS value string.</summary>
    /// <param name="value">The raw CSS value string (already trimmed; trailing semicolon removed).</param>
    /// <returns>A non-null <see cref="ValueBase" /> when parsing succeeds; otherwise <see langword="null" />.</returns>
    protected abstract ValueBase Create(string value);

    /// <summary>Determines whether the specified <see cref="object" /> is equal to the current instance.</summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><see langword="true" /> if equal; otherwise <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is CssBase other && Equals(other);

    /// <summary>Determines whether the specified <see cref="CssBase" /> is equal to the current instance.</summary>
    /// <param name="other">The other instance.</param>
    /// <returns><see langword="true" /> if equal; otherwise <see langword="false" />.</returns>
    public bool Equals(CssBase? other)
        => other is not null
            && string.Equals(CssProperty, other.CssProperty, StringComparison.Ordinal)
            && ValueEquals(other.CssValue);

    /// <summary>Determines whether two <see cref="CssBase" /> instances are equal.</summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true" /> if equal; otherwise <see langword="false" />.</returns>
    public static bool Equals(CssBase? left, CssBase? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    /// <summary>Returns a hash code for the current instance.</summary>
    /// <returns>An ordinal hash of <see cref="CssProperty" />.</returns>
    public override int GetHashCode() => CssProperty.GetHashCode(StringComparison.Ordinal);

    /// <summary>Determines whether a CSS property name is valid in canonical form.</summary>
    /// <param name="name">The name to validate.</param>
    /// <returns><see langword="true" /> if valid; otherwise <see langword="false" />.</returns>
    private static bool IsNameValid(string? name) => !string.IsNullOrWhiteSpace(name) && CssPropRegex.IsMatch(name);

    /// <summary>Converts the current instance to its CSS declaration string.</summary>
    /// <returns>The CSS declaration (e.g., <c>"color: #fff;"</c>) or an empty string.</returns>
    public override string ToString() => CssProperty;

    /// <summary>Attempts to parse a CSS declaration into a canonical name and raw value.</summary>
    /// <param name="cssProperty">The input declaration (e.g., <c>"color: #fff;"</c>).</param>
    /// <param name="name">When successful, receives the canonical property name.</param>
    /// <param name="value">When successful, receives the raw value with any trailing semicolon removed.</param>
    /// <returns><see langword="true" /> on success; otherwise <see langword="false" />.</returns>
    protected bool TryParseCssProperty(string cssProperty, out string name, out string value)
    {
        name = value = string.Empty;

        if (string.IsNullOrWhiteSpace(cssProperty) || !cssProperty.Contains(':', StringComparison.Ordinal))
        {
            return false;
        }

        var split = cssProperty.Split(':');
        var prop = split[0].Trim().ToLowerInvariant();
        var joined = string.Join(':', split.Skip(1)).Trim();

        var val = joined.EndsWith(';')
            ? joined[..^1].TrimEnd()
            : joined;

        if (!IsNameValid(prop) || string.IsNullOrWhiteSpace(val))
        {
            return false;
        }

        name = prop;
        value = val;

        return true;
    }

    /// <summary>Determines whether two <see cref="CssBase" /> values are equal by comparing their values.</summary>
    /// <param name="other">The other value to compare to <see cref="CssValue" />.</param>
    /// <returns><see langword="true" /> if values are equal; otherwise <see langword="false" />.</returns>
    private bool ValueEquals(ValueBase? other) => CssValue?.Equals(other) ?? other is null;

    /// <summary>Determines whether two <see cref="CssBase" /> instances are equal.</summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true" /> if equal; otherwise <see langword="false" />.</returns>
    public static bool operator ==(CssBase? left, CssBase? right) => Equals(left, right);

    /// <summary>Determines whether two <see cref="CssBase" /> instances are not equal.</summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true" /> if not equal; otherwise <see langword="false" />.</returns>
    public static bool operator !=(CssBase? left, CssBase? right) => !Equals(left, right);
}
