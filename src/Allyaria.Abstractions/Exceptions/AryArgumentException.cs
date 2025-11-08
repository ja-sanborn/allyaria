namespace Allyaria.Abstractions.Exceptions;

/// <summary>
/// Represents errors that occur due to invalid or unexpected argument values. This exception extends
/// <see cref="AryException" /> to provide additional context about the argument name and value that caused the issue,
/// supporting Allyaria’s structured exception handling model.
/// </summary>
public sealed class AryArgumentException : AryException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with optional message, argument name,
    /// argument value, error code, and inner exception details.
    /// </summary>
    /// <param name="message">A localized message describing the nature of the argument error.</param>
    /// <param name="argName">The name of the argument that caused the exception. May be <see langword="null" />.</param>
    /// <param name="argValue">The value of the argument that caused the exception. May be <see langword="null" />.</param>
    /// <param name="errorCode">
    /// An optional structured error code identifying the type of argument error. Defaults to <c>"ARY.ARGUMENT"</c> when not
    /// specified.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or <see langword="null" /> if
    /// none.
    /// </param>
    public AryArgumentException(string? message = null,
        string? argName = null,
        object? argValue = null,
        string? errorCode = null,
        Exception? innerException = null)
        : base(
            message: message,
            errorCode: errorCode.OrDefault(defaultValue: "ARY.ARGUMENT"),
            innerException: innerException
        )
    {
        ArgName = argName;
        ArgValue = argValue;
    }

    /// <summary>Gets the name of the argument that caused the exception.</summary>
    /// <value>A <see cref="string" /> representing the argument name, or <see langword="null" /> if not provided.</value>
    public string? ArgName { get; }

    /// <summary>Gets the value of the argument that caused the exception.</summary>
    /// <value>
    /// An <see cref="object" /> representing the problematic argument value, or <see langword="null" /> if not provided.
    /// </value>
    public object? ArgValue { get; }
}
