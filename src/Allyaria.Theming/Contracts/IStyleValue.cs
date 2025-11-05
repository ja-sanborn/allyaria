namespace Allyaria.Theming.Contracts;

/// <summary>
/// Defines a contract for all style value types used within the Allyaria theming system. Implementations of this interface
/// represent validated, CSS-compatible style values that can be serialized or rendered as strings.
/// </summary>
public interface IStyleValue
{
    /// <summary>
    /// Gets the validated and normalized string value representing this style. This value is guaranteed to be safe for use in
    /// CSS output.
    /// </summary>
    string Value { get; }
}
