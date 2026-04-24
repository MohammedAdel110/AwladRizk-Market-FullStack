using AutoMapper;
using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Enums;
using AwladRizk.Domain.Interfaces;
using AwladRizk.Domain.ValueObjects;

namespace AwladRizk.Application.Features.Orders.Commands;

public sealed class PlaceOrderHandler(
    ICartService cartService,
    IProductRepository productRepository,
    IOrderRepository orderRepository,
    IRepository<Payment> paymentRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<PlaceOrderCommand, OrderDetailDto>
{
    public async Task<OrderDetailDto> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartService.GetCartAsync(request.SessionId, cancellationToken);
        if (cart.Items.Count == 0)
        {
            throw new InvalidOperationException("Cart is empty.");
        }

        var products = await productRepository.GetByIdsAsync(
            cart.Items.Select(i => i.ProductId),
            cancellationToken);

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var order = new Order
            {
                SessionId = request.SessionId,
                Status = OrderStatus.Pending,
                CustomerName = request.CustomerName.Trim(),
                Phone = request.Phone.Trim(),
                Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim()
            };
            order.SetDeliveryAddress(new Address(
                request.Street.Trim(),
                request.Area.Trim(),
                request.City.Trim(),
                request.Governorate.Trim()));

            order.OrderNumber = await GenerateUniqueOrderNumberAsync(cancellationToken);

            var subtotal = 0m;
            foreach (var item in cart.Items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    throw new InvalidOperationException($"Product {item.ProductId} not found.");
                }

                var stockDeducted = await productRepository.TryDeductStockAsync(product.Id, item.Quantity, cancellationToken);
                if (!stockDeducted)
                {
                    throw new InvalidOperationException($"Insufficient stock for product {product.NameEn}.");
                }

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    ProductNameSnapshot = product.NameAr
                });

                subtotal += product.Price * item.Quantity;
            }

            order.SubTotal = subtotal;
            order.DeliveryFee = subtotal >= 200m ? 0m : 15m;
            order.GrandTotal = order.SubTotal + order.DeliveryFee;

            await orderRepository.AddAsync(order, cancellationToken);

            var payment = new Payment
            {
                Order = order,
                Method = request.PaymentMethod,
                Amount = order.GrandTotal,
                Status = PaymentStatus.Pending
            };

            await paymentRepository.AddAsync(payment, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            await cartService.ClearAsync(request.SessionId, cancellationToken);

            var dto = mapper.Map<OrderDetailDto>(order);
            dto.Items = mapper.Map<List<OrderItemDto>>(order.Items);
            dto.Payment = mapper.Map<PaymentDto>(payment);
            return dto;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    private async Task<string> GenerateUniqueOrderNumberAsync(CancellationToken ct)
    {
        for (var attempt = 0; attempt < 5; attempt++)
        {
            var orderNumber = Order.GenerateOrderNumber();
            var existing = await orderRepository.GetByOrderNumberAsync(orderNumber, ct);
            if (existing is null)
            {
                return orderNumber;
            }
        }

        throw new InvalidOperationException("Could not generate a unique order number.");
    }
}
