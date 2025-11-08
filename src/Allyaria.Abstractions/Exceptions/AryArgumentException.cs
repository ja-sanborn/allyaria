namespace Allyaria.Abstractions.Exceptions;

/// <summary>Represents errors related to invalid or unexpected argument values.</summary>
public sealed class AryArgumentException : AryException
{
    /// <summary>Initializes a new instance of the <see cref="AryArgumentException" /> class.</summary>
    public AryArgumentException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public AryArgumentException(string? message)
        : base(message: message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message and inner
    /// exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, Exception? innerException)
        : base(message: message, innerException: innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message and argument
    /// name.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    public AryArgumentException(string? message, string? argName)
        : base(message: message)
        => ArgName = argName;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// name, and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, string? argName, Exception? innerException)
        : base(message: message, innerException: innerException)
        => ArgName = argName;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message and argument
    /// value.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argValue">The argument value.</param>
    public AryArgumentException(string? message, object? argValue)
        : base(message: message)
        => ArgValue = argValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// value, and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argValue">The argument value.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, object? argValue, Exception? innerException)
        : base(message: message, innerException: innerException)
        => ArgValue = argValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// name, and argument value.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="argValue">The argument value.</param>
    public AryArgumentException(string? message, string? argName, object? argValue)
        : base(message: message)
    {
        ArgName = argName;
        ArgValue = argValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AryArgumentException" /> class with a specified error message, argument
    /// name, argument value, and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="argName">The argument name.</param>
    /// <param name="argValue">The argument value.</param>
    /// <param name="innerException">The inner exception.</param>
    public AryArgumentException(string? message, string? argName, object? argValue, Exception? innerException)
        : base(message: message, innerException: innerException)
    {
        ArgName = argName;
        ArgValue = argValue;
    }

    /// <summary>Gets the name of the argument that caused the exception.</summary>
    public string? ArgName { get; }

    /// <summary>Gets the value of the argument that caused the exception.</summary>
    public object? ArgValue { get; }
}
