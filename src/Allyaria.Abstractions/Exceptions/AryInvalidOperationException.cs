namespace Allyaria.Abstractions.Exceptions;

/// <summary>
/// Represents errors that occur when a method call is invalid for the object's current state. This exception extends
/// <see cref="AryException" /> to maintain Allyaria-specific exception handling semantics and ensure consistent error
/// reporting across the application.
/// </summary>
public sealed class AryInvalidOperationException : AryException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AryInvalidOperationException" /> class with an optional message, error
    /// code, and inner exception.
    /// </summary>
    /// <param name="message">A localized error message that describes the reason for the invalid operation.</param>
    /// <param name="errorCode">
    /// An optional structured error code identifying the invalid operation category. Defaults to
    /// <c>"ARY.INVALID_OPERATION"</c> when <see langword="null" /> or empty.
    /// </param>
    /// <param name="innerException">
    /// The underlying exception that caused the current exception, or <see langword="null" /> if
    /// none.
    /// </param>
    public AryInvalidOperationException(string? message = null,
        string? errorCode = null,
        Exception? innerException = null)
        : base(
            message: message,
            errorCode: errorCode.OrDefaultIfEmpty(defaultValue: "ARY.INVALID_OPERATION"),
            innerException: innerException
        ) { }
}
