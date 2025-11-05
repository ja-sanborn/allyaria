namespace Allyaria.Theming.Contracts;

/// <summary>
/// Serves as the abstract base class for all style value types within the Allyaria theming system. Implements
/// <see cref="IStyleValue" /> and provides common validation and normalization behavior for CSS-compatible string values.
/// </summary>
public abstract record StyleValueBase : IStyleValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleValueBase" /> record with the specified string value.
    /// </summary>
    /// <param name="value">
    /// The CSS-compatible string value to assign. The value is automatically validated and trimmed during initialization.
    /// </param>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> contains invalid or control
    /// characters.
    /// </exception>
    protected StyleValueBase(string value) => Value = ValidateInput(value: value);

    /// <summary>Gets the validated and normalized string value associated with this style instance.</summary>
    public string Value { get; }

    /// <summary>Attempts to validate and normalize a string input for use as a CSS-compatible value.</summary>
    /// <param name="value">The input string to validate.</param>
    /// <param name="result">
    /// When this method returns, contains the trimmed version of the input string if validation succeeds, or an empty string
    /// if the input was <see langword="null" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the input contains no invalid control characters and is suitable for use as a style value;
    /// otherwise, <see langword="false" />.
    /// </returns>
    protected bool TryValidateInput(string? value, out string result)
    {
        result = value?.Trim() ?? string.Empty;

        return !result.Any(predicate: static c => char.IsControl(c: c));
    }

    /// <summary>Validates and normalizes a string input, ensuring it is safe and CSS-compatible.</summary>
    /// <param name="value">The input string to validate and normalize.</param>
    /// <returns>The trimmed version of the input string if validation succeeds.</returns>
    /// <exception cref="AryArgumentException">
    /// Thrown when the provided <paramref name="value" /> contains control characters or is otherwise deemed invalid for use
    /// as a CSS value.
    /// </exception>
    protected string ValidateInput(string? value)
        => TryValidateInput(value: value, result: out var result)
            ? result
            : throw new AryArgumentException(message: "Invalid value", argName: nameof(value), argValue: value);
}
