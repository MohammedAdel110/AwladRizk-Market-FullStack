namespace AwladRizk.Application.Exceptions;


public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
}


public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key)
        : base($"{entityName} with key '{key}' was not found.") { }
}


public class StockConflictException : Exception
{
    public StockConflictException(string productName)
        : base($"Stock for '{productName}' has been updated by another transaction. Please refresh and try again.") { }
}

public class EmptyCartException : Exception
{
    public EmptyCartException()
        : base("Cannot place an order with an empty cart.") { }
}
