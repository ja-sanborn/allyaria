namespace Allyaria.Theming.Types;

public readonly record struct ThemeNavigator(
    IReadOnlyList<ComponentType> ComponentTypes,
    IReadOnlyList<ThemeType> ThemeTypes,
    IReadOnlyList<ComponentState> ComponentStates,
    IReadOnlyList<StyleType> StyleTypes
);
