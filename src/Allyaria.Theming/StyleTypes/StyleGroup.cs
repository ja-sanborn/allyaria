namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a grouped set of logical CSS values for block and inline directions, implementing <see cref="IStyleValue" />
/// so that it can participate in the Allyaria theming pipeline. A <see cref="StyleGroup" /> computes its final CSS using
/// the associated <see cref="StyleGroupType" />.
/// </summary>
public sealed record StyleGroup : IStyleValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleGroup" /> record with a single <see cref="IStyleValue" /> applied to
    /// all logical directions (block-start, block-end, inline-start, inline-end).
    /// </summary>
    /// <param name="type">
    /// The <see cref="StyleGroupType" /> that describes which CSS properties will be generated for this group.
    /// </param>
    /// <param name="value">The style value applied uniformly to all logical directions in the group.</param>
    public StyleGroup(StyleGroupType type, IStyleValue value)
    {
        BlockEnd = value;
        BlockStart = value;
        InlineEnd = value;
        InlineStart = value;
        Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleGroup" /> record with separate <see cref="IStyleValue" /> values for
    /// block and inline dimensions. The block value is used for both block-start and block-end, and the inline value is used
    /// for both inline-start and inline-end.
    /// </summary>
    /// <param name="type">
    /// The <see cref="StyleGroupType" /> that describes which CSS properties will be generated for this group.
    /// </param>
    /// <param name="block">The style value applied to the block-start and block-end logical directions.</param>
    /// <param name="inline">The style value applied to the inline-start and inline-end logical directions.</param>
    public StyleGroup(StyleGroupType type, IStyleValue block, IStyleValue inline)
    {
        BlockEnd = block;
        BlockStart = block;
        InlineEnd = inline;
        InlineStart = inline;
        Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleGroup" /> record with explicit values for each logical direction
    /// (block-start, block-end, inline-start, inline-end).
    /// </summary>
    /// <param name="type">
    /// The <see cref="StyleGroupType" /> that describes which CSS properties will be generated for this group.
    /// </param>
    /// <param name="blockStart">The style value applied to the block-start logical direction.</param>
    /// <param name="blockEnd">The style value applied to the block-end logical direction.</param>
    /// <param name="inlineStart">The style value applied to the inline-start logical direction.</param>
    /// <param name="inlineEnd">The style value applied to the inline-end logical direction.</param>
    public StyleGroup(StyleGroupType type,
        IStyleValue blockStart,
        IStyleValue blockEnd,
        IStyleValue inlineStart,
        IStyleValue inlineEnd)
    {
        BlockEnd = blockEnd;
        BlockStart = blockStart;
        InlineEnd = inlineEnd;
        InlineStart = inlineStart;
        Type = type;
    }

    /// <summary>Gets the style value associated with the block-end logical direction.</summary>
    public IStyleValue BlockEnd { get; init; }

    /// <summary>Gets the style value associated with the block-start logical direction.</summary>
    public IStyleValue BlockStart { get; init; }

    /// <summary>Gets the style value associated with the inline-end logical direction.</summary>
    public IStyleValue InlineEnd { get; init; }

    /// <summary>Gets the style value associated with the inline-start logical direction.</summary>
    public IStyleValue InlineStart { get; init; }

    /// <summary>
    /// Gets the <see cref="StyleGroupType" /> that determines which underlying CSS properties are produced when this group is
    /// rendered.
    /// </summary>
    public StyleGroupType Type { get; init; }

    /// <summary>
    /// Gets a composite string value representing the grouped styles in the order: block-end, block-start, inline-end,
    /// inline-start. This is the logical value exposed via <see cref="IStyleValue.Value" />.
    /// </summary>
    public string Value => $"{BlockEnd.Value} {BlockStart.Value} {InlineEnd.Value} {InlineStart.Value}";

    /// <summary>
    /// Adds the grouped style values to the provided <see cref="CssBuilder" /> instance using the CSS property names resolved
    /// from <see cref="Type" />.
    /// </summary>
    /// <param name="builder">The <see cref="CssBuilder" /> to which the CSS variables/properties are added.</param>
    /// <param name="varPrefix">
    /// An optional prefix used when emitting CSS custom properties or variables. If <see langword="null" /> or empty, no
    /// prefix is applied.
    /// </param>
    /// <returns>The same <see cref="CssBuilder" /> instance passed in, allowing fluent chaining of method calls.</returns>
    internal CssBuilder BuildCss(CssBuilder builder, string? varPrefix = "")
    {
        var typeDescription = Type.GetDescription();
        var typeList = typeDescription.Split(separator: ',');

        builder
            .Add(name: typeList[0], value: BlockEnd.Value, varPrefix: varPrefix)
            .Add(name: typeList[1], value: BlockStart.Value, varPrefix: varPrefix)
            .Add(name: typeList[2], value: InlineEnd.Value, varPrefix: varPrefix)
            .Add(name: typeList[3], value: InlineStart.Value, varPrefix: varPrefix);

        return builder;
    }

    /// <summary>Builds the CSS representation for this <see cref="StyleGroup" /> and returns it as a string.</summary>
    /// <param name="varPrefix">
    /// An optional prefix used when emitting CSS custom properties or variables. If <see langword="null" />, the default
    /// prefixing behavior for <see cref="CssBuilder" /> is used.
    /// </param>
    /// <returns>A string containing the serialized CSS generated from this <see cref="StyleGroup" />.</returns>
    public string ToCss(string? varPrefix = null)
        => BuildCss(builder: new CssBuilder(), varPrefix: varPrefix).ToString();
}
