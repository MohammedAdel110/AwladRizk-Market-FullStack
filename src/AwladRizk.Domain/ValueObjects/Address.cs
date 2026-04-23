namespace AwladRizk.Domain.ValueObjects;

/// <summary>
/// Value Object representing a delivery address.
/// Uses record for built-in value equality.
/// </summary>
public record Address(
    string Street,
    string Area,
    string City,
    string Governorate
);
