# Allyaria.Components.Blazor.ArySurface

`ArySurface` is a themed visual surface container component that renders a `<div>` with Allyaria-provided theming and
ARIA accessibility support. It is the concrete Blazor component that inherits from `AryComponentBase`, applying the
`"ary-surface"` base CSS class and `ComponentType.Surface` theming identity while exposing all public parameters defined
on `AryComponentBase` for ARIA attributes, theming, and additional HTML attributes.

## Constructors

`ArySurface()` Initializes a new `ArySurface` component instance for use in a Blazor render tree.

## Properties

| Name                   | Type                                   | Description                                                                                                                                                                                                                                                                                       |
|------------------------|----------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `AdditionalAttributes` | `IReadOnlyDictionary<string, object>?` | Gets or sets additional arbitrary HTML attributes to apply to the rendered `<div>`. Attributes that conflict with managed values (such as `class`, `id`, ARIA attributes, `style`, or `tabindex`) are filtered using `AryComponentBase.GetFilteredAttributes`. Inherited from `AryComponentBase`. |
| `AriaDescribedBy`      | `string?`                              | Gets or sets the `aria-describedby` attribute, referencing one or more element IDs that provide descriptive text for the surface. Inherited from `AryComponentBase`.                                                                                                                              |
| `AriaHidden`           | `bool?`                                | Gets or sets whether the rendered element should be hidden from assistive technologies. When `true`, `aria-hidden="true"` is rendered; otherwise the attribute is omitted. Inherited from `AryComponentBase`.                                                                                     |
| `AriaLabel`            | `string?`                              | Gets or sets the `aria-label` attribute, providing an accessible label when no visible label is present. Inherited from `AryComponentBase`.                                                                                                                                                       |
| `AriaLabelledBy`       | `string?`                              | Gets or sets the `aria-labelledby` attribute, referencing one or more element IDs that label the component. Inherited from `AryComponentBase`.                                                                                                                                                    |
| `AriaRole`             | `string?`                              | Gets or sets the ARIA `role` attribute applied to the rendered `<div>`. Inherited from `AryComponentBase`.                                                                                                                                                                                        |
| `ChildContent`         | `RenderFragment?`                      | Gets or sets the content to render inside the surface. This is the primary UI body hosted by the container. Marked as `[EditorRequired]`. Inherited from `AryComponentBase`.                                                                                                                      |
| `Class`                | `string?`                              | Gets or sets additional CSS class names to append to the base `"ary-surface"` class. Combined with the base class to form the final `class` attribute. Inherited from `AryComponentBase`.                                                                                                         |
| `ComponentState`       | `ComponentState`                       | Gets or sets the visual and interactive state of the component (for example, default or disabled), which influences resolved theme values. Inherited from `AryComponentBase`.                                                                                                                     |
| `EffectiveThemeType`   | `ThemeType`                            | Gets or sets the effective theme type cascaded from a parent or application-level theme when `ThemeType` is `ThemeType.System`. Marked as `[CascadingParameter]`. Inherited from `AryComponentBase`.                                                                                              |
| `Id`                   | `string?`                              | Gets or sets the HTML `id` attribute for the rendered `<div>`. When not set, no explicit `id` is rendered. Inherited from `AryComponentBase`.                                                                                                                                                     |
| `Style`                | `string?`                              | Gets or sets additional inline CSS declarations to merge with theme-generated styles. User-provided styles take precedence on conflicts. Inherited from `AryComponentBase`.                                                                                                                       |
| `TabIndex`             | `int?`                                 | Gets or sets the logical tab order for the component, rendered as the `tabindex` attribute when specified. Inherited from `AryComponentBase`.                                                                                                                                                     |
| `ThemeType`            | `ThemeType`                            | Gets or sets the requested theme type for the component. When `ThemeType.System` is used, theming is based on `EffectiveThemeType`. Inherited from `AryComponentBase`.                                                                                                                            |
| `ThemingService`       | `IThemingService`                      | Gets or sets the theming service used to resolve CSS variables and styles based on `ThemeType`, `ComponentType`, and `ComponentState`. Typically provided via Blazor dependency injection. Inherited from `AryComponentBase`.                                                                     |

## Methods

*None*

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```razor
@page "/surface-demo"
@using Allyaria.Components.Blazor

<!-- Example usage of ArySurface in a Blazor app or Razor Class Library -->
<ArySurface AriaLabel="Main content area"
            Class="app-surface"
            ThemeType="ThemeType.Dark">
    <h1>Welcome</h1>
    <p>This content is rendered inside an Allyaria-themed surface.</p>
</ArySurface>
```

---

*Revision Date: 2025-11-18*
