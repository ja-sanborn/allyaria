using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents one of the <b>CSS global values</b> that are valid for all CSS properties. These are <c>inherit</c>,
/// <c>initial</c>, <c>unset</c>, <c>revert</c>, and <c>revert-layer</c>. Input is normalized to lowercase; invalid input
/// causes an exception during construction.
/// </summary>
public sealed class AllyariaGlobalValue : ValueBase
{
    /// <summary>The set of allowed CSS global values. Comparison is case-insensitive.</summary>
    private static readonly HashSet<string> AllowedValues = new(
        new[]
        {
            "inherit",
            "initial",
            "unset",
            "revert",
            "revert-layer"
        },
        StringComparer.OrdinalIgnoreCase
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaGlobalValue" /> class from a raw string. The input is normalized;
    /// if invalid, an <see cref="ArgumentException" /> is thrown.
    /// </summary>
    /// <param name="value">The raw CSS keyword to parse.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, whitespace, or not a recognized CSS global value.
    /// </exception>
    public AllyariaGlobalValue(string value)
        : base(Normalize(value)) { }

    /// <summary>Normalizes a raw CSS global value candidate.</summary>
    /// <param name="value">The candidate string.</param>
    /// <returns>The canonical lowercase keyword when valid.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, whitespace, or not a recognized CSS global value.
    /// </exception>
    private static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var trimmedValue = value.Trim().ToLowerInvariant();

        if (AllowedValues.Contains(trimmedValue))
        {
            return trimmedValue;
        }

        throw new ArgumentException(
            $"Invalid CSS global value: {value}",
            nameof(value)
        );
    }

    /// <summary>Parses a raw string into an <see cref="AllyariaGlobalValue" />.</summary>
    /// <param name="value">The raw CSS keyword.</param>
    /// <returns>A new instance of <see cref="AllyariaGlobalValue" /> representing the normalized keyword.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static AllyariaGlobalValue Parse(string value) => new(value);

    /// <summary>Attempts to parse a raw string into an <see cref="AllyariaGlobalValue" />.</summary>
    /// <param name="value">The raw CSS keyword.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="AllyariaGlobalValue" /> if successful; otherwise <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    public static bool TryParse(string value, out AllyariaGlobalValue? result)
    {
        try
        {
            result = new AllyariaGlobalValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a string to an <see cref="AllyariaGlobalValue" />.</summary>
    /// <param name="value">The raw CSS keyword.</param>
    /// <returns>A new <see cref="AllyariaGlobalValue" /> instance.</returns>
    public static implicit operator AllyariaGlobalValue(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaGlobalValue" /> to its underlying string representation.</summary>
    /// <param name="value">The <see cref="AllyariaGlobalValue" /> instance.</param>
    /// <returns>The normalized CSS keyword string.</returns>
    public static implicit operator string(AllyariaGlobalValue value) => value.Value;
}
