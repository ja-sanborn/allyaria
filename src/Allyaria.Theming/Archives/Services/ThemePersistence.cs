namespace Allyaria.Theming.Services;

/// <summary>
/// Provides a concrete implementation of <see cref="IThemePersistence" /> that manages the persisted state of the theme
/// preference in memory. This service notifies subscribers when the persistence setting changes.
/// </summary>
/// <remarks>
/// This implementation does not directly interact with storage mechanisms; it is intended to act as an observable state
/// container for higher-level components that perform the actual storage or retrieval of theme settings.
/// </remarks>
public sealed class ThemePersistence : IThemePersistence
{
    /// <summary>
    /// Backing field for the <see cref="IsPersisted" /> property. Represents the current persistence state.
    /// </summary>
    private bool _isPersisted;

    /// <summary>
    /// Occurs when the persistence state changes, such as when the user enables or disables the "remember my theme" option.
    /// </summary>
    public event EventHandler<bool>? PersistChanged;

    /// <summary>
    /// Gets a value indicating whether theme persistence is currently enabled. When <see langword="true" />, the selected
    /// theme is intended to be saved and restored across sessions.
    /// </summary>
    public bool IsPersisted => _isPersisted;

    /// <summary>Updates the persisted state flag and notifies subscribers when a change occurs.</summary>
    /// <param name="isPersisted">
    /// <see langword="true" /> to enable persistence; <see langword="false" /> to disable
    /// persistence.
    /// </param>
    /// <remarks>If the new value is the same as the current state, no event is raised.</remarks>
    public void SetPersisted(bool isPersisted)
    {
        if (_isPersisted == isPersisted)
        {
            return;
        }

        _isPersisted = isPersisted;
        PersistChanged?.Invoke(this, _isPersisted);
    }
}
