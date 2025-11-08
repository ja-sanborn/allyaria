namespace Allyaria.Abstractions.Exceptions;

/// <summary>
/// Represents errors that occur when a method call is invalid for the object's current state. This exception extends
/// <see cref="AryException" /> to maintain Allyaria-specific exception handling semantics.
/// </summary>
public sealed class AryInvalidOperationException : AryException
{
    /// <summary>Initializes a new instance of the <see cref="AryInvalidOperationException" /> class.</summary>
    public AryInvalidOperationException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryInvalidOperationException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public AryInvalidOperationException(string? message)
        : base(message: message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryInvalidOperationException" /> class with a specified error message and
    /// a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public AryInvalidOperationException(string? message, Exception? innerException)
        : base(message: message, innerException: innerException) { }
}
