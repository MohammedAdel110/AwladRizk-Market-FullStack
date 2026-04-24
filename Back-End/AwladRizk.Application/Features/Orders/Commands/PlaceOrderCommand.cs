using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Enums;

namespace AwladRizk.Application.Features.Orders.Commands;

/// <summary>
/// Places an order from the current cart.
/// Cart data is read from Redis, order is persisted to SQL, cart is cleared.
/// </summary>
public record PlaceOrderCommand(
    string SessionId,
    string CustomerName,
    string Street,
    string Area,
    string City,
    string Governorate,
    string Phone,
    string? Notes,
    PaymentMethod PaymentMethod,
    string? CardToken,
    string? WalletPhone,
    string? WalletProvider
) : IRequest<OrderDetailDto>;
