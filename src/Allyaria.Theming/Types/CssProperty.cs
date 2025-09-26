using System.Text.RegularExpressions;

namespace Allyaria.Theming.Types;

/// <summary>
/// Represents a canonical CSS property declaration composed of a normalized property <see cref="Name" /> and raw
/// <see cref="Value" /> (without a trailing semicolon).
/// </summary>
/// <remarks>
///     <para>
///     The <see cref="Name" /> is normalized to lowercase and trimmed; the <see cref="Value" /> is trimmed but otherwise
///     left unchanged. This struct provides parsing helpers and equality semantics for testability.
///     </para>
/// </remarks>
internal readonly struct CssProperty : IEquatable<CssProperty>
{
    /// <summary>
    /// A compiled regular expression that validates canonical CSS property names: lowercase letters with optional hyphen
    /// separators (e.g., <c>background-color</c>).
    /// </summary>
    /// <remarks>Culture-invariant and compiled for performance.</remarks>
    private static readonly Regex CssPropRegex = new(
        "^[a-z]+(?:-[a-z]+)*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    /// <summary>
    /// Gets an empty <see cref="CssProperty" /> with both <see cref="Name" /> and <see cref="Value" /> set to
    /// <see cref="string.Empty" />.
    /// </summary>
    public static readonly CssProperty Empty = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CssProperty" /> struct with empty <see cref="Name" /> and
    /// <see cref="Value" />.
    /// </summary>
    public CssProperty()
        : this(string.Empty, string.Empty) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CssProperty" /> struct by parsing a CSS declaration string.
    /// </summary>
    /// <param name="cssProperty">The CSS declaration to parse, e.g., <c>"color: #fff;"</c>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="cssProperty" /> is null/whitespace or cannot be parsed into a valid name/value pair.
    /// </exception>
    public CssProperty(string cssProperty)
        : this()
    {
        if (!TryParseCssProperty(cssProperty, out var name, out var value))
        {
            throw new ArgumentException("Unable to parse CSS property.", nameof(cssProperty));
        }

        Name = name;
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CssProperty" /> struct with the provided name and value.
    /// </summary>
    /// <param name="name">The CSS property name (e.g., <c>background-color</c>).</param>
    /// <param name="value">The CSS property value (e.g., <c>#fff</c>).</param>
    /// <remarks>
    /// The <paramref name="name" /> is normalized to lowercase and trimmed. The <paramref name="value" /> is trimmed. Invalid
    /// names or empty values will result in an instance that returns <see langname="false" /> from <see cref="IsValid" />.
    /// </remarks>
    public CssProperty(string name, string value)
    {
        Name = string.IsNullOrWhiteSpace(name)
            ? string.Empty
            : name.Trim().ToLowerInvariant();

        Value = string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : value.Trim();
    }

    /// <summary>
    /// Gets the canonical CSS property name (lowercase, hyphenated), or <see cref="string.Empty" /> if not set.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the raw CSS property value, trimmed and without a trailing semicolon, or <see cref="string.Empty" /> if not set.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Determines whether this instance and another specified <see cref="CssProperty" /> object have the same value.
    /// </summary>
    /// <param name="other">The other <see cref="CssProperty" />.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(CssProperty other)
        => string.Equals(Name, other.Name, StringComparison.Ordinal)
            && string.Equals(Value, other.Value, StringComparison.Ordinal);

    /// <summary>Determines whether this instance and a specified object are equal.</summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj) => obj is CssProperty other && Equals(other);

    /// <summary>Determines whether the specified <see cref="CssProperty" /> is equal to this instance.</summary>
    /// <param name="other">The other value to compare with.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(CssProperty? other) => ToCss().Equals(other?.ToCss(), StringComparison.OrdinalIgnoreCase);

    /// <summary>Determines whether two <see cref="CssProperty" /> instances are equal.</summary>
    /// <param name="left">The left-hand instance.</param>
    /// <param name="right">The right-hand instance.</param>
    /// <returns><c>true</c> if both instances are equal; otherwise, <c>false</c>.</returns>
    public static bool Equals(CssProperty? left, CssProperty? right) => left?.Equals(right) is true;

    /// <summary>Returns a hash code for this instance.</summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Name, Value);

    /// <summary>Validates that a CSS property name is in canonical form.</summary>
    /// <param name="name">The property name to validate.</param>
    /// <returns><see langword="true" /> if valid; otherwise, <see langword="false" />.</returns>
    private static bool IsNameValid(string? name) => !string.IsNullOrWhiteSpace(name) && CssPropRegex.IsMatch(name);

    /// <summary>
    /// Indicates whether the property instance is structurally valid: non-empty <see cref="Name" /> (canonical) and non-empty
    /// <see cref="Value" />.
    /// </summary>
    /// <returns><see langword="true" /> if valid; otherwise, <see langword="false" />.</returns>
    public bool IsValid() => !Equals(Empty) && IsNameValid(Name) && !string.IsNullOrWhiteSpace(Value);

    /// <summary>Parses a CSS declaration into a <see cref="CssProperty" />.</summary>
    /// <param name="cssProperty">The CSS declaration, e.g., <c>"margin-top: 1rem;"</c>.</param>
    /// <returns>A new <see cref="CssProperty" />.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="cssProperty" /> is null/whitespace or cannot be parsed.</exception>
    public static CssProperty Parse(string cssProperty) => new(cssProperty);

    /// <summary>Formats the property as a CSS declaration string.</summary>
    /// <returns>
    /// The declaration in the form <c>"name:value;"</c> when <see cref="IsValid" /> is <see langword="true" />; otherwise,
    /// <see cref="string.Empty" />.
    /// </returns>
    public string ToCss()
        => IsValid()
            ? $"{Name}:{Value};"
            : string.Empty;

    /// <summary>Returns the <see cref="Value" /> for convenience (not a full declaration).</summary>
    /// <returns>The raw value string.</returns>
    public override string ToString() => Value;

    /// <summary>Attempts to parse a CSS declaration.</summary>
    /// <param name="cssProperty">The CSS declaration, e.g., <c>"color: #fff;"</c>.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="CssProperty" /> if parsing succeeded; otherwise the default
    /// value.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string cssProperty, out CssProperty? result)
    {
        try
        {
            result = new CssProperty(cssProperty);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Attempts to parse a CSS declaration into a canonical name and raw value.</summary>
    /// <param name="cssProperty">The input declaration (e.g., <c>"color: #fff;"</c>).</param>
    /// <param name="name">When successful, receives the canonical property name.</param>
    /// <param name="value">When successful, receives the raw value with any trailing semicolon removed.</param>
    /// <returns><see langword="true" /> on success; otherwise <see langword="false" />.</returns>
    private static bool TryParseCssProperty(string? cssProperty, out string name, out string value)
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

    /// <summary>Determines whether two instances are equal.</summary>
    public static bool operator ==(CssProperty? left, CssProperty? right) => Equals(left, right);

    /// <summary>
    /// Defines an implicit conversion from <see cref="string" /> to <see cref="CssProperty" /> by parsing the declaration.
    /// </summary>
    /// <param name="cssProperty">The CSS declaration string.</param>
    /// <returns>A parsed <see cref="CssProperty" />.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="cssProperty" /> is null/whitespace or cannot be parsed.</exception>
    public static implicit operator CssProperty(string cssProperty) => new(cssProperty);

    /// <summary>
    /// Defines an implicit conversion from <see cref="CssProperty" /> to <see cref="string" /> returning the raw
    /// <see cref="Value" />.
    /// </summary>
    /// <param name="value">The <see cref="CssProperty" /> instance.</param>
    /// <returns>The raw <see cref="Value" />.</returns>
    public static implicit operator string(CssProperty value) => value.ToString();

    /// <summary>Determines whether two instances are not equal.</summary>
    public static bool operator !=(CssProperty? left, CssProperty? right) => !(left == right);
}
