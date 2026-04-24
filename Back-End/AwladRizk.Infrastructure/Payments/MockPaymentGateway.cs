using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Infrastructure.Payments;

/// <summary>
/// Mock payment gateway for development/testing.
/// Simulates successful payments with random transaction references.
/// Replace with real Paymob/Fawry SDK in production.
/// </summary>
public class MockPaymentGateway : IPaymentGateway
{
    public async Task<PaymentResult> ProcessCardPaymentAsync(CardPaymentRequest request, CancellationToken ct = default)
    {
        // Simulate processing delay
        await Task.Delay(800, ct);

        return new PaymentResult
        {
            Success = true,
            TransactionRef = $"VISA-{Guid.NewGuid().ToString("N")[..12].ToUpper()}",
            Message = "Payment approved"
        };
    }

    public async Task<PaymentResult> GenerateFawryCodeAsync(FawryPaymentRequest request, CancellationToken ct = default)
    {
        // Simulate processing delay
        await Task.Delay(500, ct);

        // Generate a mock Fawry reference code (12 digits in 3 groups)
        var code = $"{Random.Shared.Next(1000, 9999)} {Random.Shared.Next(1000, 9999)} {Random.Shared.Next(1000, 9999)}";

        return new PaymentResult
        {
            Success = true,
            TransactionRef = code,
            Message = "Fawry code generated. Pay at any Fawry outlet."
        };
    }

    public async Task<PaymentResult> ProcessWalletPaymentAsync(WalletPaymentRequest request, CancellationToken ct = default)
    {
        // Simulate processing delay
        await Task.Delay(600, ct);

        return new PaymentResult
        {
            Success = true,
            TransactionRef = $"WALLET-{Guid.NewGuid().ToString("N")[..8].ToUpper()}",
            Message = $"Payment received via {request.Provider}"
        };
    }
}
