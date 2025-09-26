using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a strongly typed CSS size keyword value, limited to the set of allowed absolute and relative sizes.
/// </summary>
public sealed class AllyariaSizeValue : ValueBase
{
    /// <summary>The set of allowed CSS absolute and relative size values. Comparison is case-insensitive.</summary>
    private static readonly HashSet<string> AllowedValues = new(
        new[]
        {
            "larger",
            "smaller",
            "xx-small",
            "x-small",
            "small",
            "medium",
            "large",
            "x-large",
            "xx-large",
            "xxx-large"
        },
        StringComparer.OrdinalIgnoreCase
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaSizeValue" /> class from a raw string. The input is normalized; if
    /// invalid, an <see cref="ArgumentException" /> is thrown.
    /// </summary>
    /// <param name="value">The raw CSS keyword to parse.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value" /> is <c>null</c>, whitespace, or not a recognized CSS global value.
    /// </exception>
    public AllyariaSizeValue(string value)
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
            $"Invalid CSS size value: {value}",
            nameof(value)
        );
    }

    /// <summary>Parses a raw string into an <see cref="AllyariaSizeValue" />.</summary>
    /// <param name="value">The raw CSS keyword.</param>
    /// <returns>A new instance of <see cref="AllyariaSizeValue" /> representing the normalized keyword.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value" /> is invalid.</exception>
    public static AllyariaSizeValue Parse(string value) => new(value);

    /// <summary>Attempts to parse a raw string into an <see cref="AllyariaSizeValue" />.</summary>
    /// <param name="value">The raw CSS keyword.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="AllyariaSizeValue" /> if successful; otherwise <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    public static bool TryParse(string value, out AllyariaSizeValue? result)
    {
        try
        {
            result = new AllyariaSizeValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a string to an <see cref="AllyariaSizeValue" />.</summary>
    /// <param name="value">The raw CSS keyword.</param>
    /// <returns>A new <see cref="AllyariaSizeValue" /> instance.</returns>
    public static implicit operator AllyariaSizeValue(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaSizeValue" /> to its underlying string representation.</summary>
    /// <param name="value">The <see cref="AllyariaSizeValue" /> instance.</param>
    /// <returns>The normalized CSS keyword string.</returns>
    public static implicit operator string(AllyariaSizeValue value) => value.Value;
}
