namespace Allyaria.Abstractions.Enumerations;

/// <summary>Defines the possible interactive states of a component.</summary>
public enum ComponentState
{
    /// <summary>The component is fully active and supports user interaction.</summary>
    Active,

    /// <summary>The component is displayed in a non-editable state but remains visible and accessible.</summary>
    ReadOnly,

    /// <summary>
    /// The component is disabled, preventing user interaction and typically rendered with reduced emphasis.
    /// </summary>
    Disabled,

    /// <summary>The component is hidden, preventing the control from being displayed in the user interface.</summary>
    Hidden
}
