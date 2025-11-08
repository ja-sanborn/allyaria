namespace Allyaria.Abstractions.Validation;

/// <summary>
/// Represents a validation context for an argument, allowing multiple validation checks to be accumulated before throwing
/// a combined <see cref="AryArgumentException" />.
/// </summary>
/// <typeparam name="T">The type of the argument being validated.</typeparam>
public sealed class AryValidation<T>
{
    /// <summary>A collection of accumulated validation errors for the current argument.</summary>
    private readonly List<AryArgumentException> _errors = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AryValidation{T}" /> class with a specified argument value and name.
    /// </summary>
    /// <param name="argValue">The value of the argument to validate.</param>
    /// <param name="argName">The name of the argument being validated.</param>
    public AryValidation(T argValue, string argName)
    {
        ArgValue = argValue;
        ArgName = argName;
    }

    /// <summary>Gets the name of the argument being validated.</summary>
    public string ArgName { get; }

    /// <summary>Gets the value of the argument being validated.</summary>
    public T ArgValue { get; }

    /// <summary>Gets the collection of validation errors accumulated so far.</summary>
    public IReadOnlyList<AryArgumentException> Errors => _errors;

    /// <summary>Gets a value indicating whether the validation context is valid (i.e., contains no errors).</summary>
    public bool IsValid => _errors.Count == 0;

    /// <summary>Adds a validation error to the internal error collection if one exists.</summary>
    /// <param name="ex">The exception to add. If <c>null</c>, no action is taken.</param>
    internal void Add(AryArgumentException? ex)
    {
        if (ex is not null)
        {
            _errors.Add(item: ex);
        }
    }

    /// <summary>
    /// Throws a combined <see cref="AryArgumentException" /> if the current validation context contains errors.
    /// </summary>
    /// <remarks>
    /// When multiple validation errors exist, their messages are concatenated with line breaks into a single exception message
    /// for easier consumption.
    /// </remarks>
    /// <exception cref="AryArgumentException">Thrown when one or more validation errors are present in this context.</exception>
    public void ThrowIfInvalid()
    {
        if (IsValid)
        {
            return;
        }

        var combinedMessage = string.Join(
            separator: Environment.NewLine,
            values: Errors.Select(selector: e => e.Message)
        );

        throw new AryArgumentException(message: combinedMessage, argName: ArgName, argValue: ArgValue);
    }
}
