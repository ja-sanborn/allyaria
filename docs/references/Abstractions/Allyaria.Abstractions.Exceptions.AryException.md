# Allyaria.Abstractions.Exceptions.AryException

`AryException` is the base exception type for all Allyaria applications. It provides a unified exception hierarchy to
ensure consistent error handling, logging, and localization across Allyaria projects. All custom exception types should
derive from this class.

---

## Constructors

`AryException()`
Initializes a new instance of the `AryException` class.

* Exceptions: *None*

`AryException(string? message)`
Initializes a new instance of the `AryException` class with a specified error message.

* Exceptions: *None*

`AryException(string? message, Exception? innerException)`
Initializes a new instance of the `AryException` class with a specified error message and an inner exception that caused
this exception.

* Exceptions: *None*

---

## Properties

| Name        | Type             | Description                                                             |
|-------------|------------------|-------------------------------------------------------------------------|
| `Timestamp` | `DateTimeOffset` | Gets the timestamp indicating when this exception instance was created. |

---

## Methods

*None*

---

## Operators

*None*

---

## Events

*None*

---

## Exceptions

*None*

---

## Behavior Notes

* The `Timestamp` property automatically captures the UTC creation time of the exception instance.
* This base type is intended for all custom Allyaria exception classes to ensure a consistent hierarchy and enable
  centralized exception handling.

---

## Examples

### Minimal Example

```csharp
throw new AryException("A general Allyaria error occurred.");
```

### Expanded Example

```csharp
try
{
    throw new InvalidOperationException("Invalid operation detected.");
}
catch (Exception ex)
{
    throw new AryException("A failure occurred within Allyaria processing.", ex);
}
```

> *Rev Date: 2025-10-06*
