using Allyaria.Theming.Abstractions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents one of the <b>CSS global values</b> that are valid for all CSS properties. These are <c>inherit</c>,
/// <c>initial</c>, <c>unset</c>, <c>revert</c>, and <c>revert-layer</c>. Input is normalized to lowercase; invalid input
/// yields <see cref="string.Empty" />.
/// </summary>
public sealed record AllyariaGlobalValue : StyleValueBase
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
    /// Initializes a new instance of the <see cref="AllyariaGlobalValue" /> class from a raw string. The input is normalized; if
    /// invalid, the <see cref="StyleValueBase.Value" /> is empty.
    /// </summary>
    /// <param name="value">The raw CSS keyword to parse.</param>
    public AllyariaGlobalValue(string value)
        : base(Normalize(value)) { }

    /// <summary>Normalizes a raw CSS global value candidate.</summary>
    /// <param name="value">The candidate string.</param>
    /// <returns>The canonical lowercase keyword when valid; otherwise <see cref="string.Empty" />.</returns>
    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var trimmedValue = value.Trim().ToLowerInvariant();

        return AllowedValues.Contains(trimmedValue)
            ? trimmedValue
            : string.Empty;
    }

    /// <summary>Attempts to parse a raw string into a <see cref="AllyariaGlobalValue" />.</summary>
    /// <param name="value">The raw CSS keyword to parse.</param>
    /// <param name="globalValue">
    /// When this method returns, contains the parsed <see cref="AllyariaGlobalValue" />. Its <see cref="StyleValueBase.Value" />
    /// is the normalized keyword, or empty if parsing fails.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="value" /> is a valid CSS globalValue value; otherwise <see langword="false" />.
    /// </returns>
    public static bool TryParse(string value, out AllyariaGlobalValue globalValue)
    {
        globalValue = new AllyariaGlobalValue(value);

        return !string.IsNullOrWhiteSpace(globalValue.Value);
    }

    /// <summary>Implicit conversion from <see cref="string" /> to <see cref="AllyariaGlobalValue" />.</summary>
    /// <param name="value">The raw CSS keyword.</param>
    /// <returns>An <see cref="AllyariaGlobalValue" /> instance.</returns>
    public static implicit operator AllyariaGlobalValue(string value) => new(value);

    /// <summary>Implicit conversion from <see cref="AllyariaGlobalValue" /> to <see cref="string" />.</summary>
    /// <param name="global">The instance to convert.</param>
    /// <returns>
    /// The normalized CSS keyword or <see cref="string.Empty" /> if <paramref name="global" /> is null or invalid.
    /// </returns>
    public static implicit operator string(AllyariaGlobalValue? global) => global?.Value ?? string.Empty;
}
