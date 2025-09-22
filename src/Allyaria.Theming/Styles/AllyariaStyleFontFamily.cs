using Allyaria.Theming.Abstractions;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a strongly typed font-family style in the Allyaria theming system. Provides conversion and wrapping logic
/// for CSS global values, functions, and font-family values.
/// </summary>
public readonly record struct AllyariaStyleFontFamily
{
    /// <summary>Backing field for the style value.</summary>
    private readonly StyleValueBase? _style;

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaStyleFontFamily" /> struct from a raw CSS font-family string.
    /// </summary>
    /// <param name="value">The CSS font-family string.</param>
    public AllyariaStyleFontFamily(string value)
    {
        if (AllyariaCssGlobal.TryParse(value, out var global))
        {
            _style = global;

            return;
        }

        if (AllyariaCssFunction.TryParse(value, out var func))
        {
            _style = func;

            return;
        }

        _style = new AllyariaCssFontFamily(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaStyleFontFamily" /> struct from a strongly typed
    /// <see cref="AllyariaCssFontFamily" />.
    /// </summary>
    /// <param name="family">The CSS font-family instance.</param>
    public AllyariaStyleFontFamily(AllyariaCssFontFamily family) => _style = family;

    /// <summary>
    /// Gets the underlying style value. Returns a default <see cref="AllyariaCssFontFamily" /> with an empty string if unset.
    /// </summary>
    public StyleValueBase Style => _style ?? new AllyariaCssFontFamily(string.Empty);

    /// <summary>Gets the full CSS declaration for this font-family style.</summary>
    public string Value => $"font-family:{Style.Value};";

    /// <summary>Implicitly converts a raw string into an <see cref="AllyariaStyleFontFamily" />.</summary>
    /// <param name="value">The raw CSS font-family string.</param>
    public static implicit operator AllyariaStyleFontFamily(string value) => new(value);

    /// <summary>
    /// Implicitly converts a <see cref="AllyariaCssFontFamily" /> into an <see cref="AllyariaStyleFontFamily" />.
    /// </summary>
    /// <param name="family">The <see cref="AllyariaCssFontFamily" /> instance.</param>
    public static implicit operator AllyariaStyleFontFamily(AllyariaCssFontFamily family) => new(family);

    /// <summary>Implicitly converts an <see cref="AllyariaStyleFontFamily" /> into a CSS declaration string.</summary>
    /// <param name="family">The <see cref="AllyariaStyleFontFamily" /> instance.</param>
    public static implicit operator string(AllyariaStyleFontFamily family) => family.Value;

    /// <summary>
    /// Implicitly converts an <see cref="AllyariaStyleFontFamily" /> into a <see cref="AllyariaCssFontFamily" />.
    /// </summary>
    /// <param name="family">The <see cref="AllyariaStyleFontFamily" /> instance.</param>
    /// <returns>The <see cref="AllyariaCssFontFamily" /> extracted from the underlying style.</returns>
    /// <exception cref="InvalidCastException">Thrown if the underlying style is not a <see cref="AllyariaCssFontFamily" />.</exception>
    public static implicit operator AllyariaCssFontFamily(AllyariaStyleFontFamily family)
        => (AllyariaCssFontFamily)family.Style;
}
