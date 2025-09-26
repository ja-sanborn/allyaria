using Allyaria.Theming.Contracts;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a strongly-typed CSS <c>color</c> declaration that validates and normalizes its value for use within the
/// Allyaria theming engine.
/// </summary>
/// <remarks>
/// This type enforces that the CSS property name is exactly <c>color</c>. It supports construction from:
/// <list type="bullet">
///     <item>
///         <description>a raw color value (e.g., <c>#fff</c>, <c>rgb(0 0 0)</c>, <c>rebeccapurple</c>)</description>
///     </item>
///     <item>
///         <description>a pre-parsed <see cref="ValueBase" /> derivative (e.g., <see cref="AllyariaColorValue" />)</description>
///     </item>
///     <item>
///         <description>a full CSS declaration string <c>"color: …"</c> (name must be <c>color</c>)</description>
///     </item>
/// </list>
/// </remarks>
public sealed class AllyariaColorCss : CssBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CssBase" /> class from a single CSS declaration string.
    /// </summary>
    /// <param name="cssProperty">A full CSS declaration.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="cssProperty" /> is null/whitespace or cannot be parsed into a valid property and value.
    /// </exception>
    public AllyariaColorCss(string cssProperty)
        : base(cssProperty)
    {
        if (!TryParseCssProperty(cssProperty, out var name, out var value))
        {
            throw new ArgumentException("Unable to parse CSS property.", nameof(cssProperty));
        }

        CssName = name;
        CssValue = Create(value);
    }

    /// <summary>Initializes a new instance with an explicit property name and a pre-parsed value.</summary>
    /// <param name="name">The CSS property name (must be <c>color</c>).</param>
    /// <param name="value">A non-null, validated CSS value.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="name" /> or <paramref name="value" /> cannot be
    /// parsed.
    /// </exception>
    public AllyariaColorCss(string name, ValueBase value)
        : base(name, value) { }

    /// <summary>Parses a raw CSS value string into a concrete <see cref="ValueBase" /> (color/global/function).</summary>
    /// <param name="value">The raw CSS value string to parse.</param>
    /// <returns>A concrete <see cref="ValueBase" /> representing the parsed value.</returns>
    /// <exception cref="ArgumentException">Thrown when the value cannot be parsed.</exception>
    protected override ValueBase Create(string value)
    {
        if (AllyariaColorValue.TryParse(value, out var color))
        {
            return color!;
        }

        if (AllyariaStringValue.TryParse(value, out var str))
        {
            return str!;
        }

        throw new ArgumentException("Invalid CSS value.", nameof(value));
    }

    /// <summary>Parses a raw CSS value string into an <see cref="AllyariaColorCss" /> for the <c>color</c> property.</summary>
    /// <param name="value">The raw CSS value string.</param>
    /// <returns>An <see cref="AllyariaColorCss" /> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static AllyariaColorCss Parse(string value) => new(value);

    /// <summary>
    /// Attempts to parse a raw CSS value string into an <see cref="AllyariaColorCss" /> for the <c>color</c> property.
    /// </summary>
    /// <param name="value">The raw CSS value string to parse.</param>
    /// <param name="result">When this method returns <see langword="true" />, contains the parsed instance.</param>
    /// <returns><see langword="true" /> if parsing succeeds; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string value, out AllyariaColorCss? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;

            return false;
        }

        try
        {
            result = new AllyariaColorCss(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a raw CSS value string to an <see cref="AllyariaColorCss" />.</summary>
    /// <param name="value">The raw CSS value string.</param>
    /// <returns>An <see cref="AllyariaColorCss" /> instance.</returns>
    public static implicit operator AllyariaColorCss(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaColorCss" /> to its CSS declaration string.</summary>
    /// <param name="value">The instance to convert.</param>
    /// <returns>The full CSS declaration string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <c>null</c>.</exception>
    public static implicit operator string(AllyariaColorCss value)
        => (value ?? throw new ArgumentNullException(nameof(value))).CssProperty;
}
