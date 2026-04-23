namespace AwladRizk.Domain.ValueObjects;

/// <summary>
/// Value Object representing a monetary amount.
/// Uses record for built-in value equality.
/// </summary>
public record Money(decimal Amount, string Currency = "EGP")
{
    public static Money Zero => new(0);
    public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount, a.Currency);
    public static Money operator -(Money a, Money b) => new(a.Amount - b.Amount, a.Currency);
    public static Money operator *(Money price, int quantity) => new(price.Amount * quantity, price.Currency);

    public override string ToString() => $"{Amount:N2} {Currency}";
}
