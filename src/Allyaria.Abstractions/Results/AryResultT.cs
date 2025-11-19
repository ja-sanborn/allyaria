namespace Allyaria.Abstractions.Results;

/// <summary>
/// Represents the outcome of an operation that may return a value of type <typeparamref name="T" />. Encapsulates success
/// and failure states, error details, and contextual information.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public sealed class AryResult<T>
{
    /// <summary>Initializes a new instance of the <see cref="AryResult{T}" /> class.</summary>
    /// <param name="isSuccess">Indicates whether the operation succeeded.</param>
    /// <param name="value">The result value, if successful.</param>
    /// <param name="error">The associated <see cref="Exception" />, if any.</param>
    private AryResult(bool isSuccess, T? value = default, Exception? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    /// <summary>Gets the <see cref="Exception" /> associated with this result, if any.</summary>
    /// <value>The exception describing the failure cause, or <see langword="null" /> if the operation succeeded.</value>
    public Exception? Error { get; }

    /// <summary>Gets a value indicating whether the operation failed.</summary>
    /// <value><see langword="true" /> if the operation failed; otherwise, <see langword="false" />.</value>
    public bool IsFailure => !IsSuccess;

    /// <summary>Gets a value indicating whether the operation succeeded.</summary>
    /// <value><see langword="true" /> if the operation succeeded; otherwise, <see langword="false" />.</value>
    public bool IsSuccess { get; }

    /// <summary>Gets the result value of the operation, if successful.</summary>
    /// <value>
    /// The value produced by the operation. This property is <see langword="null" /> if the operation failed.
    /// </value>
    public T? Value { get; }

    /// <summary>Creates a failed <see cref="AryResult{T}" /> with the specified error information.</summary>
    /// <param name="error">The exception representing the failure cause.</param>
    /// <returns>A failed <see cref="AryResult{T}" /> containing the provided error details.</returns>
    public static AryResult<T> Failure(Exception? error)
        => new(isSuccess: false, error: error ?? new AryException(message: "Unknown error"));

    /// <summary>Creates a successful <see cref="AryResult{T}" /> with the specified value.</summary>
    /// <param name="value">The result value to encapsulate.</param>
    /// <returns>A successful <see cref="AryResult{T}" /> containing the specified value.</returns>
    public static AryResult<T> Success(T value) => new(isSuccess: true, value: value);

    /// <summary>Converts this <see cref="AryResult{T}" /> to a failed <see cref="AryResult" />.</summary>
    /// <returns>A failed <see cref="AryResult" /> with the same error details as this instance.</returns>
    /// <exception cref="AryInvalidOperationException">Thrown when attempting to convert a successful result to a failure.</exception>
    public AryResult ToFailure()
        => IsSuccess
            ? throw new AryInvalidOperationException(
                message: "Cannot convert a successful AryResult<T> to AryResult failure."
            )
            : AryResult.Failure(error: Error);
}
