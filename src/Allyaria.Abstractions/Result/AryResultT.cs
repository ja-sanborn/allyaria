namespace Allyaria.Abstractions.Result;

/// <summary>
/// Represents the outcome of an operation that may return a value of type <typeparamref name="T" />. Encapsulates success
/// and failure states, error details, and contextual information.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
/// <remarks>
/// This struct provides a lightweight, immutable result abstraction without heap allocations, following the functional
/// result pattern. For non-generic operations, use <see cref="AryResult" />.
/// </remarks>
public readonly struct AryResult<T>
{
    /// <summary>Initializes a new instance of the <see cref="AryResult{T}" /> struct.</summary>
    /// <param name="isSuccess">Indicates whether the operation succeeded.</param>
    /// <param name="value">The result value, if successful.</param>
    /// <param name="error">The associated <see cref="Exception" />, if any.</param>
    /// <param name="errorCode">An optional machine-readable error code.</param>
    /// <param name="errorMessage">An optional human-readable error message.</param>
    private AryResult(bool isSuccess,
        T? value = default,
        Exception? error = null,
        string? errorCode = null,
        string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;

        if (error is null)
        {
            return;
        }

        ErrorCode = errorCode.OrDefault(defaultValue: string.Empty);
        ErrorMessage = errorMessage.OrDefault(defaultValue: error.Message);
    }

    /// <summary>Gets the <see cref="Exception" /> associated with this result, if any.</summary>
    /// <value>The exception describing the failure cause, or <see langword="null" /> if the operation succeeded.</value>
    public Exception? Error { get; }

    /// <summary>Gets the error code associated with this result, if available.</summary>
    /// <value>A machine-readable error code string, or <see langword="null" /> if the result represents success.</value>
    public string? ErrorCode { get; }

    /// <summary>Gets the human-readable message describing the error.</summary>
    /// <value>A localized message if available, or the exception message if not provided.</value>
    public string? ErrorMessage { get; }

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
    /// <param name="errorCode">An optional machine-readable error code string.</param>
    /// <param name="errorMessage">An optional localized or descriptive message.</param>
    /// <returns>A failed <see cref="AryResult{T}" /> containing the provided error details.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when both <paramref name="error" /> and <paramref name="errorMessage" />
    /// are null.
    /// </exception>
    public static AryResult<T> Failure(Exception? error, string? errorCode = null, string? errorMessage = null)
        => new(
            isSuccess: false,
            error: error ?? new AryException(message: errorMessage.OrDefault(defaultValue: "Unknown error")),
            errorCode: errorCode,
            errorMessage: errorMessage
        );

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
            : AryResult.Failure(error: Error, errorCode: ErrorCode, errorMessage: ErrorMessage);

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T" /> into a successful <see cref="AryResult{T}" />.
    /// </summary>
    /// <param name="value">The value to wrap in a successful result.</param>
    /// <returns>A successful <see cref="AryResult{T}" /> containing the specified value.</returns>
    public static implicit operator AryResult<T>(T value) => Success(value: value);

    /// <summary>Implicitly converts an <see cref="Exception" /> into a failed <see cref="AryResult{T}" />.</summary>
    /// <param name="error">The exception to convert.</param>
    /// <returns>A failed <see cref="AryResult{T}" /> representing the specified exception.</returns>
    public static implicit operator AryResult<T>(Exception error) => Failure(error: error);
}
