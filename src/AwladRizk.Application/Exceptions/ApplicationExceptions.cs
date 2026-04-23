namespace AwladRizk.Application.Exceptions;

/// <summary>
/// Thrown when FluentValidation fails in the pipeline.
/// </summary>
public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
}

/// <summary>
/// Thrown when a requested entity is not found.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key)
        : base($"{entityName} with key '{key}' was not found.") { }
}

/// <summary>
/// Thrown when a stock concurrency conflict occurs.
/// </summary>
public class StockConflictException : Exception
{
    public StockConflictException(string productName)
        : base($"Stock for '{productName}' has been updated by another transaction. Please refresh and try again.") { }
}

/// <summary>
/// Thrown when cart is empty at checkout.
/// </summary>
public class EmptyCartException : Exception
{
    public EmptyCartException()
        : base("Cannot place an order with an empty cart.") { }
}
