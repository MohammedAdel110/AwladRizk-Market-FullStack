using AwladRizk.Domain.Common;
using AwladRizk.Domain.Enums;

namespace AwladRizk.Domain.Entities;

public class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? TransactionRef { get; set; }
    public decimal Amount { get; set; }
    public DateTime? PaidAt { get; set; }
}
