namespace Allyaria.Abstractions.Result;

/// <summary>
/// Represents the outcome of an operation that can either succeed or fail, encapsulating success state, error details, and
/// associated metadata.
/// </summary>
public sealed class AryResult
{
    /// <summary>Initializes a new instance of the <see cref="AryResult" /> class.</summary>
    /// <param name="isSuccess">Indicates whether the operation succeeded.</param>
    /// <param name="error">The associated <see cref="Exception" />, if any.</param>
    private AryResult(bool isSuccess, Exception? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>Gets the <see cref="Exception" /> associated with the result, if any.</summary>
    /// <value>
    /// The exception describing the failure cause, or <see langword="null" /> if the result represents success.
    /// </value>
    public Exception? Error { get; }

    /// <summary>Gets a value indicating whether the operation failed.</summary>
    /// <value><see langword="true" /> if the operation failed; otherwise, <see langword="false" />.</value>
    public bool IsFailure => !IsSuccess;

    /// <summary>Gets a value indicating whether the operation succeeded.</summary>
    /// <value><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</value>
    public bool IsSuccess { get; }

    /// <summary>Creates a failure <see cref="AryResult" /> with the specified error information.</summary>
    /// <param name="error">The <see cref="Exception" /> representing the failure cause. Cannot be <see langword="null" />.</param>
    /// <returns>An <see cref="AryResult" /> representing a failed operation.</returns>
    public static AryResult Failure(Exception? error)
        => new(isSuccess: false, error: error ?? new AryException(message: "Unknown error"));

    /// <summary>Creates a successful <see cref="AryResult" />.</summary>
    /// <returns>An <see cref="AryResult" /> representing a successful operation.</returns>
    public static AryResult Success() => new(isSuccess: true);

    /// <summary>Converts this <see cref="AryResult" /> to a failed <see cref="AryResult{T}" /> instance.</summary>
    /// <typeparam name="T">The result type of the target <see cref="AryResult{T}" />.</typeparam>
    /// <returns>A failed <see cref="AryResult{T}" /> with the same error details as this result.</returns>
    /// <exception cref="AryInvalidOperationException">Thrown when attempting to convert a successful result into a failure.</exception>
    public AryResult<T> ToFailure<T>()
        => IsSuccess
            ? throw new AryInvalidOperationException(
                message: "Cannot convert a successful AryResult to AryResult<T> failure."
            )
            : AryResult<T>.Failure(error: Error);
}
