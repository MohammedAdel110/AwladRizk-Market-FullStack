using AwladRizk.Domain.Enums;

namespace AwladRizk.Domain.Interfaces;

/// <summary>
/// Payment gateway abstraction.
/// Phase 1: MockPaymentGateway
/// Phase 2+: Paymob, Fawry API, etc. — just implement this interface.
/// </summary>
public interface IPaymentGateway
{
    Task<PaymentResult> ProcessCardPaymentAsync(CardPaymentRequest request, CancellationToken ct = default);
    Task<PaymentResult> GenerateFawryCodeAsync(FawryPaymentRequest request, CancellationToken ct = default);
    Task<PaymentResult> ProcessWalletPaymentAsync(WalletPaymentRequest request, CancellationToken ct = default);
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string TransactionRef { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
}

public class CardPaymentRequest
{
    public decimal Amount { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string? CardToken { get; set; }
}

public class FawryPaymentRequest
{
    public decimal Amount { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class WalletPaymentRequest
{
    public decimal Amount { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string WalletPhone { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty; // VodafoneCash, OrangeCash, EtisalatCash
}
