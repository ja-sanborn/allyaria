namespace Allyaria.Theming.Contracts;

/// <summary>
/// Defines an abstraction for managing the persistence of a user's theme preference, including whether it should be
/// remembered across sessions and how changes are observed.
/// </summary>
/// <remarks>
/// Implementations of this interface should handle storage of the persistence flag and raise <see cref="PersistChanged" />
/// when the persistence state is modified.
/// </remarks>
public interface IThemePersistence
{
    /// <summary>
    /// Occurs when the persistence state changes, such as when the user enables or disables the "remember my theme" setting.
    /// </summary>
    event EventHandler<bool>? PersistChanged;

    /// <summary>
    /// Gets a value indicating whether theme persistence is currently enabled. When <see langword="true" />, the selected
    /// theme is saved and restored between sessions.
    /// </summary>
    bool IsPersisted { get; }

    /// <summary>Sets the persisted state, enabling or disabling theme preference storage.</summary>
    /// <param name="isPersisted">
    /// <see langword="true" /> to enable theme persistence; <see langword="false" /> to disable it and clear stored
    /// preferences.
    /// </param>
    void SetPersisted(bool isPersisted);
}
