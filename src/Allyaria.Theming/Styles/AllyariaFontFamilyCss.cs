using Allyaria.Theming.Contracts;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a strongly-typed CSS declaration for the <c>font-family</c> property, providing parsing and validation
/// against Allyaria value types.
/// </summary>
/// <remarks>
/// This type validates input via Allyaria value parsers and throws on invalid values. Exceptions thrown here are intended
/// for developers (not end users).
/// </remarks>
public sealed class AllyariaFontFamilyCss : CssBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CssBase" /> class from a single CSS declaration string.
    /// </summary>
    /// <param name="cssProperty">A full CSS declaration, e.g., <c>"color: #fff;"</c>. A trailing semicolon is optional.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="cssProperty" /> is null/whitespace or cannot be parsed into a valid property and value.
    /// </exception>
    public AllyariaFontFamilyCss(string cssProperty)
        : base(cssProperty)
    {
        if (!TryParseCssProperty(cssProperty, out var name, out var value))
        {
            throw new ArgumentException("Unable to parse CSS property.", nameof(cssProperty));
        }

        CssName = name;
        CssValue = Create(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CssBase" /> class from a name and a pre-parsed <see cref="ValueBase" />.
    /// </summary>
    /// <param name="name">The CSS property name in any case (will be canonicalized).</param>
    /// <param name="value">The parsed CSS value instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name" /> is null/whitespace or invalid.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <see langword="null" />.</exception>
    public AllyariaFontFamilyCss(string name, ValueBase value)
        : base(name, value) { }

    /// <summary>Creates a <see cref="ValueBase" /> instance from the provided raw CSS value string.</summary>
    /// <param name="value">The raw CSS value string (already trimmed; trailing semicolon removed).</param>
    /// <returns>A non-null <see cref="ValueBase" /> when parsing succeeds; otherwise <see langword="null" />.</returns>
    protected override ValueBase Create(string value)
    {
        if (AllyariaGlobalValue.TryParse(value, out var global))
        {
            return global!;
        }

        if (AllyariaFunctionValue.TryParse(value, out var func))
        {
            return func!;
        }

        AllyariaFontFamilyValue.TryParse(value, out var family);

        return family!;
    }

    /// <summary>
    /// Parses a raw CSS value string into an <see cref="AllyariaFontFamilyCss" /> for the <c>color</c> property.
    /// </summary>
    /// <param name="value">The raw CSS value string.</param>
    /// <returns>An <see cref="AllyariaFontFamilyCss" /> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static AllyariaFontFamilyCss Parse(string value) => new(value);

    /// <summary>
    /// Attempts to parse a raw CSS value string into an <see cref="AllyariaFontFamilyCss" /> for the <c>color</c> property.
    /// </summary>
    /// <param name="value">The raw CSS value string to parse.</param>
    /// <param name="result">When this method returns <see langword="true" />, contains the parsed instance.</param>
    /// <returns><see langword="true" /> if parsing succeeds; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string value, out AllyariaFontFamilyCss? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;

            return false;
        }

        try
        {
            result = new AllyariaFontFamilyCss(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a raw CSS value string to an <see cref="AllyariaFontFamilyCss" />.</summary>
    /// <param name="value">The raw CSS value string.</param>
    /// <returns>An <see cref="AllyariaFontFamilyCss" /> instance.</returns>
    public static implicit operator AllyariaFontFamilyCss(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaFontFamilyCss" /> to its CSS declaration string.</summary>
    /// <param name="value">The instance to convert.</param>
    /// <returns>The full CSS declaration string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <c>null</c>.</exception>
    public static implicit operator string(AllyariaFontFamilyCss value)
        => (value ?? throw new ArgumentNullException(nameof(value))).CssProperty;
}
