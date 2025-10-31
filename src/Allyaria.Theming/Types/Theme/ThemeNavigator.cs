namespace Allyaria.Theming.Types.Theme;

public readonly record struct ThemeNavigator(
    IReadOnlyList<ComponentType> ComponentTypes,
    IReadOnlyList<ThemeType> ThemeTypes,
    IReadOnlyList<ComponentState> ComponentStates,
    IReadOnlyList<StyleType> StyleTypes
)
{
    public static readonly ThemeNavigator Initialize = new(
        ComponentTypes: BuildList<ComponentType>(),
        ThemeTypes: BuildList<ThemeType>(),
        ComponentStates: BuildList<ComponentState>(),
        StyleTypes: BuildList<StyleType>()
    );

    private static List<TEnum> BuildList<TEnum>(params TEnum[] items)
        where TEnum : Enum
    {
        if (items.Length is 0)
        {
            return new List<TEnum>();
        }

        if (!(items[0] is ComponentState or ComponentType or ThemeType or StyleType))
        {
            throw new AryArgumentException(message: "Invalid enum type", argName: nameof(items));
        }

        var list = new List<TEnum>(capacity: items.Length);
        list.AddRange(collection: items);

        return list;
    }

    public ThemeNavigator SetComponentStates(params ComponentState[] items)
        => this with
        {
            ComponentStates = BuildList(items: items)
        };

    public ThemeNavigator SetComponentTypes(params ComponentType[] items)
        => this with
        {
            ComponentTypes = BuildList(items: items)
        };

    public ThemeNavigator SetStyleTypes(params StyleType[] items)
        => this with
        {
            StyleTypes = BuildList(items: items)
        };

    public ThemeNavigator SetThemeTypes(params ThemeType[] items)
        => this with
        {
            ThemeTypes = BuildList(items: items)
        };
}
