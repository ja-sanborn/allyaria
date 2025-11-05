namespace Allyaria.Theming.StyleTypes;

/// <summary>
/// Represents a numeric CSS value (integer-based) used within the Allyaria theming system. Provides a strongly typed
/// wrapper for integer-only CSS properties and validates numeric input upon construction.
/// </summary>
public sealed record StyleNumber : StyleValueBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleNumber" /> record using the specified string representation of a
    /// numeric value.
    /// </summary>
    /// <param name="value">The string representation of a numeric value.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="value" /> cannot be parsed as a valid
    /// integer.
    /// </exception>
    public StyleNumber(string value)
        : base(value: value)
    {
        if (string.IsNullOrWhiteSpace(value: Value))
        {
            return;
        }

        if (TryNormalizeInteger(input: Value, number: out var number))
        {
            Number = number;
        }
        else
        {
            throw new ArgumentException(message: $"Invalid number: {Value}");
        }
    }

    /// <summary>Gets the parsed integer value represented by this style number.</summary>
    public int Number { get; }

    /// <summary>Parses the specified string into a <see cref="StyleNumber" /> instance.</summary>
    /// <param name="value">The string representation of the numeric value to parse.</param>
    /// <returns>A new <see cref="StyleNumber" /> instance representing the parsed value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="value" /> cannot be parsed as a valid
    /// integer.
    /// </exception>
    public static StyleNumber Parse(string? value) => new(value: value ?? string.Empty);

    /// <summary>Attempts to normalize and parse an integer value from the specified input string.</summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="number">When this method returns, contains the parsed integer if successful; otherwise, <c>0</c>.</param>
    /// <returns>
    /// <see langword="true" /> if parsing succeeded or the input is empty/whitespace; otherwise, <see langword="false" />.
    /// </returns>
    private static bool TryNormalizeInteger(string input, out int number)
    {
        number = 0;

        return string.IsNullOrWhiteSpace(value: input) || int.TryParse(
            s: input,
            style: NumberStyles.Integer,
            provider: CultureInfo.InvariantCulture,
            result: out number
        );
    }

    /// <summary>Attempts to parse a string into a <see cref="StyleNumber" /> instance.</summary>
    /// <param name="value">The string representation of the numeric value to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the parsed <see cref="StyleNumber" /> instance or <see langword="null" /> if parsing
    /// failed.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(string? value, out StyleNumber? result)
    {
        try
        {
            result = Parse(value: value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicitly converts a string into a <see cref="StyleNumber" /> instance.</summary>
    /// <param name="value">The string representation of the numeric value to convert.</param>
    /// <returns>A <see cref="StyleNumber" /> instance representing the provided value.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided string cannot be parsed as a valid integer.</exception>
    public static implicit operator StyleNumber(string? value) => Parse(value: value);

    /// <summary>Implicitly converts a <see cref="StyleNumber" /> instance to its string representation.</summary>
    /// <param name="value">The <see cref="StyleNumber" /> instance to convert.</param>
    /// <returns>
    /// The original string value represented by the <see cref="StyleNumber" />, or an empty string if
    /// <paramref name="value" /> is <see langword="null" />.
    /// </returns>
    public static implicit operator string(StyleNumber? value) => value?.Value ?? string.Empty;
}
