namespace Allyaria.Abstractions.Extensions;

/// <summary>
/// Provides generic extension methods focused on nullable value types, enabling convenient defaults when a value is not
/// present.
/// </summary>
public static class GenericExtensions
{
    /// <summary>
    /// Returns the provided nullable value if it is not <c>null</c>; otherwise, returns the specified default value or
    /// <see langword="default" /> for <typeparamref name="T" /> if no default is provided.
    /// </summary>
    /// <typeparam name="T">A value type.</typeparam>
    /// <param name="value">The nullable value to evaluate.</param>
    /// <param name="defaultValue">
    /// The value to return when <paramref name="value" /> is <c>null</c>. If omitted, <see langword="default" /> for
    /// <typeparamref name="T" /> is used.
    /// </param>
    /// <returns>
    /// The underlying value when <paramref name="value" /> has a value; otherwise, <paramref name="defaultValue" /> if
    /// provided, or <see langword="default" /> for <typeparamref name="T" />.
    /// </returns>
    public static T OrDefault<T>(this T? value, T defaultValue = default)
        where T : struct
        => value ?? defaultValue;
}
