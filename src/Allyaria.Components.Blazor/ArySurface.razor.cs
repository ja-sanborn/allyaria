namespace Allyaria.Components.Blazor;

/// <summary>
/// Represents a themed visual surface container within the Allyaria component system. Surfaces apply component-level
/// theming, styling, and accessibility metadata inherited from <see cref="AryComponentBase" />.
/// </summary>
public partial class ArySurface : AryComponentBase
{
    /// <summary>
    /// Gets the base CSS class applied to the surface component. The base class is combined with user-specified classes to
    /// produce the final rendered class list.
    /// </summary>
    protected override string BaseClass => "ary-surface";

    /// <summary>
    /// Gets the Allyaria component type value identifying this component as a <c>Surface</c>. The theming engine uses this
    /// type to resolve appropriate CSS variables and visual settings.
    /// </summary>
    protected override ComponentType ComponentType => ComponentType.Surface;
}
