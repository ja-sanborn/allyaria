using Allyaria.Theming.Abstractions;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.Styles;

/// <summary>
/// Represents a CSS <c>color</c> style wrapper that formats values as a full CSS declaration (i.e.,
/// <c>color:&lt;value&gt;;</c>).
/// </summary>
/// <remarks>
///     <para>This type provides two distinct fallbacks:</para>
///     <list type="bullet">
///         <item>
///             <description>
///             <b>Default-constructed instance</b> (no value supplied): the style resolves to
///             <see cref="Colors.Transparent" />.
///             </description>
///         </item>
///         <item>
///             <description>
///             <b>Invalid or empty string input</b> to the string constructor: the style resolves to
///             <see cref="Colors.Transparent" />.
///             </description>
///         </item>
///     </list>
///     <para>
///     It supports implicit creation from <see cref="string" /> and <see cref="AllyariaCssColor" /> to reduce ceremony in
///     call sites. All conversions are one-way implicit to favor ergonomic use in markup and style construction.
///     </para>
/// </remarks>
public readonly record struct AllyariaStyleColor
{
    /// <summary>
    /// Backing store for the resolved style value. May be <see langword="null" /> for a default-constructed struct.
    /// </summary>
    private readonly StyleValueBase? _style;

    /// <summary>
    /// Initializes a new instance from a raw color string (e.g., <c>"#ff0000"</c>, <c>"red"</c>, <c>"rgb(…)"</c>,
    /// <c>"var(--token)"</c>, or a CSS-wide keyword such as <c>"inherit"</c>).
    /// </summary>
    /// <param name="value">
    /// The color string to wrap. If <see langword="null" />, empty, or invalid, resolves to <see cref="Colors.Transparent" />.
    /// </param>
    /// <remarks>
    /// Parsing attempts occur in the following order:
    /// <list type="number">
    ///     <item>
    ///         <description><see cref="AllyariaCssColor.TryParse(string, out AllyariaCssColor?)" /> (named/hex/rgb/hsl…)</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         <see cref="AllyariaCssGlobal.TryParse(string, out AllyariaCssGlobal?)" /> (e.g., <c>inherit</c>, <c>initial</c>
    ///         )
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         <see cref="AllyariaCssFunction.TryParse(string, out AllyariaCssFunction?)" /> (e.g., <c>var(--color)</c>)
    ///         </description>
    ///     </item>
    /// </list>
    /// If all attempts fail, the instance falls back to <see cref="AllyariaCssColor.Transparent" />.
    /// </remarks>
    public AllyariaStyleColor(string value)
    {
        if (AllyariaCssColor.TryParse(value, out var color))
        {
            _style = color;

            return;
        }

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

        _style = Colors.Transparent;
    }

    /// <summary>Initializes a new instance from an existing <see cref="AllyariaCssColor" />.</summary>
    /// <param name="color">The color value to wrap. Must represent a valid CSS color.</param>
    public AllyariaStyleColor(AllyariaCssColor color) => _style = color;

    /// <summary>
    /// Gets the underlying <see cref="StyleValueBase" />. If the instance was default-constructed and no value is available,
    /// returns the default <see cref="Colors.Transparent" />.
    /// </summary>
    /// <remarks>
    /// This property never returns <see langword="null" />; it always yields a concrete <see cref="StyleValueBase" />.
    /// </remarks>
    public StyleValueBase Style => _style ?? Colors.Transparent;

    /// <summary>
    /// Gets the complete CSS declaration string for this color, including the trailing semicolon; for example:
    /// <c>color:#ff0000;</c>.
    /// </summary>
    /// <remarks>
    /// The inclusion of the trailing semicolon makes the value safe to concatenate with other declarations.
    /// </remarks>
    public string Value => $"color:{Style.Value};";

    /// <summary>Implicitly converts a color <see cref="string" /> into an <see cref="AllyariaStyleColor" />.</summary>
    /// <param name="value">The color string to wrap. If <see langword="null" />, empty, or invalid, resolves to transparent.</param>
    /// <returns>An <see cref="AllyariaStyleColor" /> instance representing the provided value.</returns>
    public static implicit operator AllyariaStyleColor(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaCssColor" /> into an <see cref="AllyariaStyleColor" />.</summary>
    /// <param name="color">The CSS color to wrap.</param>
    /// <returns>An <see cref="AllyariaStyleColor" /> instance representing the provided color.</returns>
    public static implicit operator AllyariaStyleColor(AllyariaCssColor color) => new(color);

    /// <summary>
    /// Implicitly converts an <see cref="AllyariaStyleColor" /> to its CSS declaration <see cref="string" /> (e.g.,
    /// <c>"color:red;"</c>).
    /// </summary>
    /// <param name="color">The wrapper to convert.</param>
    /// <returns>A CSS declaration string including the trailing semicolon.</returns>
    /// <remarks>
    /// This is a convenience for scenarios such as attribute bindings in Blazor where a <see cref="string" /> is required.
    /// </remarks>
    public static implicit operator string(AllyariaStyleColor color) => color.Value;

    /// <summary>
    /// Implicitly unwraps the underlying <see cref="AllyariaCssColor" /> from an <see cref="AllyariaStyleColor" />.
    /// </summary>
    /// <param name="color">The wrapper to convert.</param>
    /// <returns>
    /// The underlying <see cref="AllyariaCssColor" /> if one is stored; otherwise the default fallback
    /// <see cref="Colors.Transparent" />.
    /// </returns>
    /// <remarks>
    /// When the wrapper holds a non-color value (e.g., a global keyword or a function), the value is coerced by the underlying
    /// theming primitives to <see cref="AllyariaCssColor" />. Ensure such coercion is supported by
    /// <see cref="StyleValueBase" /> implementations in <c>Allyaria.Theming.Values</c>.
    /// </remarks>
    public static implicit operator AllyariaCssColor(AllyariaStyleColor color) => (AllyariaCssColor)color.Style;
}
