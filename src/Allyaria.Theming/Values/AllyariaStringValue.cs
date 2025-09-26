using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.Values;

/// <summary>Represents a theming string value with enforced normalization (trimmed, non-null, non-whitespace).</summary>
public sealed class AllyariaStringValue : ValueBase
{
    /// <summary>Initializes a new instance of the <see cref="AllyariaStringValue" /> class.</summary>
    /// <param name="value">The string value to normalize and store.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is <c>null</c>, empty, or whitespace.</exception>
    public AllyariaStringValue(string value)
        : base(Normalize(value)) { }

    /// <summary>Normalizes the input string by trimming and validating it.</summary>
    /// <param name="value">The string value to normalize.</param>
    /// <returns>The normalized (trimmed) string.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is <c>null</c>, empty, or whitespace.</exception>
    private static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        return value.Trim();
    }

    /// <summary>Parses the specified string into an <see cref="AllyariaStringValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <returns>A new <see cref="AllyariaStringValue" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is <c>null</c>, empty, or whitespace.</exception>
    public static AllyariaStringValue Parse(string value) => new(value);

    /// <summary>Attempts to parse the specified string into an <see cref="AllyariaStringValue" />.</summary>
    /// <param name="value">The input string to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="AllyariaStringValue" /> if parsing succeeded, or <c>null</c>
    /// if it failed.
    /// </param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string value, out AllyariaStringValue? result)
    {
        try
        {
            result = new AllyariaStringValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Defines an implicit conversion from <see cref="string" /> to <see cref="AllyariaStringValue" />.</summary>
    /// <param name="value">The string value to convert.</param>
    /// <returns>A new <see cref="AllyariaStringValue" /> containing the normalized <paramref name="value" />.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is <c>null</c>, empty, or whitespace.</exception>
    public static implicit operator AllyariaStringValue(string value) => new(value);

    /// <summary>Defines an implicit conversion from <see cref="AllyariaStringValue" /> to <see cref="string" />.</summary>
    /// <param name="value">The <see cref="AllyariaStringValue" /> instance.</param>
    /// <returns>The underlying normalized string value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <c>null</c>.</exception>
    public static implicit operator string(AllyariaStringValue value) => value.Value;
}
