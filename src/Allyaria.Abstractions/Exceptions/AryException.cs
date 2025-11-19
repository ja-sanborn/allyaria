namespace Allyaria.Abstractions.Exceptions;

/// <summary>
/// Represents the base exception type for Allyaria applications. All custom exceptions within the Allyaria ecosystem
/// should derive from this type.
/// </summary>
public class AryException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AryException" /> class with an optional message, an optional error code,
    /// and an optional inner exception.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception. This value is typically localized before being passed
    /// into the constructor.
    /// </param>
    /// <param name="errorCode">
    /// An optional, structured error code that categorizes the error. When <see langword="null" /> or empty, this value
    /// defaults to <c>"ARY.UNKNOWN"</c>.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or <see langword="null" /> if no inner exception is
    /// specified.
    /// </param>
    public AryException(string? message = null, string? errorCode = null, Exception? innerException = null)
        : base(message: message, innerException: innerException)
        => ErrorCode = errorCode.OrDefaultIfEmpty(defaultValue: "ARY.UNKNOWN");

    /// <summary>Gets the structured error code associated with this exception instance.</summary>
    /// <remarks>
    /// If no explicit error code is provided when constructing the exception, this property defaults to <c>"ARY.UNKNOWN"</c>.
    /// </remarks>
    public string ErrorCode { get; }

    /// <summary>Gets the UTC timestamp indicating when this exception instance was created.</summary>
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
}
