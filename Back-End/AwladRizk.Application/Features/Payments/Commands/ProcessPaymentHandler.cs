using AutoMapper;
using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Enums;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Payments.Commands;

public sealed class ProcessPaymentHandler(
    IOrderRepository orderRepository,
    IPaymentGateway paymentGateway,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<ProcessPaymentCommand, PaymentDto>
{
    public async Task<PaymentDto> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByOrderNumberAsync(request.OrderNumber, cancellationToken)
            ?? throw new InvalidOperationException("Order not found.");

        var payment = order.Payment ?? throw new InvalidOperationException("Payment record not found.");
        if (payment.Status == PaymentStatus.Completed)
        {
            return mapper.Map<PaymentDto>(payment);
        }

        var result = request.Method switch
        {
            PaymentMethod.Visa => await paymentGateway.ProcessCardPaymentAsync(new CardPaymentRequest
            {
                Amount = payment.Amount,
                OrderNumber = order.OrderNumber,
                CardToken = request.CardToken
            }, cancellationToken),
            PaymentMethod.Fawry => await paymentGateway.GenerateFawryCodeAsync(new FawryPaymentRequest
            {
                Amount = payment.Amount,
                OrderNumber = order.OrderNumber,
                Phone = order.Phone
            }, cancellationToken),
            PaymentMethod.Wallet => await paymentGateway.ProcessWalletPaymentAsync(new WalletPaymentRequest
            {
                Amount = payment.Amount,
                OrderNumber = order.OrderNumber,
                WalletPhone = request.WalletPhone ?? string.Empty,
                Provider = request.WalletProvider ?? "Wallet"
            }, cancellationToken),
            _ => throw new InvalidOperationException("Unsupported payment method.")
        };

        if (result.Success)
        {
            payment.Status = PaymentStatus.Completed;
            payment.TransactionRef = result.TransactionRef;
            payment.PaidAt = DateTime.UtcNow;
            order.Status = OrderStatus.Confirmed;
        }
        else
        {
            payment.Status = PaymentStatus.Failed;
            payment.TransactionRef = result.TransactionRef;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<PaymentDto>(payment);
    }
}
