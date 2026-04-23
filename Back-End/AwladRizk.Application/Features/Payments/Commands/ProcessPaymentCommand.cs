using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Enums;

namespace AwladRizk.Application.Features.Payments.Commands;

public record ProcessPaymentCommand(
    string OrderNumber,
    PaymentMethod Method,
    string? CardToken,
    string? WalletPhone,
    string? WalletProvider
) : IRequest<PaymentDto>;
