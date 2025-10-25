namespace Allyaria.Abstractions.Exceptions;

/// <summary>
/// Represents the base exception type for Allyaria applications. All custom exceptions should derive from this type.
/// </summary>
public class AryException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="AryException" /> class.</summary>
    public AryException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public AryException(string? message)
        : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryException" /> class with a specified error message and a reference to
    /// the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public AryException(string? message, Exception? innerException)
        : base(message: message, innerException: innerException) { }

    /// <summary>Gets the timestamp indicating when this exception was created.</summary>
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
}
