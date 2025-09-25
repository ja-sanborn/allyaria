using Allyaria.Theming.Contracts;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a typed CSS declaration for the <c>font-size</c> property. Accepts numeric values with units, CSS keyword
/// sizes, global values, or supported functional values as defined by the project value types.
/// </summary>
public sealed class AllyariaFontSizeCss : CssBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CssBase" /> class from a single CSS declaration string.
    /// </summary>
    /// <param name="cssProperty">A full CSS declaration.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="cssProperty" /> is null/whitespace or cannot be parsed into a valid property and value.
    /// </exception>
    public AllyariaFontSizeCss(string cssProperty)
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
    public AllyariaFontSizeCss(string name, ValueBase value)
        : base(name, value) { }

    /// <summary>Creates a <see cref="ValueBase" /> instance from the provided raw CSS value string.</summary>
    /// <param name="value">The raw CSS value string (already trimmed; trailing semicolon removed).</param>
    /// <returns>A non-null <see cref="ValueBase" /> when parsing succeeds; otherwise <see langword="null" />.</returns>
    protected override ValueBase Create(string value)
    {
        if (AllyariaNumberValue.TryParse(value, out var number))
        {
            return number!;
        }

        if (AllyariaSizeValue.TryParse(value, out var size))
        {
            return size!;
        }

        if (AllyariaGlobalValue.TryParse(value, out var global))
        {
            return global!;
        }

        if (AllyariaFunctionValue.TryParse(value, out var func))
        {
            return func!;
        }

        throw new ArgumentException("Invalid number value.", nameof(value));
    }

    /// <summary>
    /// Parses a raw CSS value string into an <see cref="AllyariaFontSizeCss" /> for the <c>color</c> property.
    /// </summary>
    /// <param name="value">The raw CSS value string.</param>
    /// <returns>An <see cref="AllyariaFontSizeCss" /> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static AllyariaFontSizeCss Parse(string value) => new(value);

    /// <summary>
    /// Attempts to parse a raw CSS value string into an <see cref="AllyariaFontSizeCss" /> for the <c>color</c> property.
    /// </summary>
    /// <param name="value">The raw CSS value string to parse.</param>
    /// <param name="result">When this method returns <see langword="true" />, contains the parsed instance.</param>
    /// <returns><see langword="true" /> if parsing succeeds; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string value, out AllyariaFontSizeCss? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;

            return false;
        }

        try
        {
            result = new AllyariaFontSizeCss(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a raw CSS value string to an <see cref="AllyariaFontSizeCss" />.</summary>
    /// <param name="value">The raw CSS value string.</param>
    /// <returns>An <see cref="AllyariaFontSizeCss" /> instance.</returns>
    public static implicit operator AllyariaFontSizeCss(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaFontSizeCss" /> to its CSS declaration string.</summary>
    /// <param name="value">The instance to convert.</param>
    /// <returns>The full CSS declaration string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <c>null</c>.</exception>
    public static implicit operator string(AllyariaFontSizeCss value)
        => (value ?? throw new ArgumentNullException(nameof(value))).CssProperty;
}
