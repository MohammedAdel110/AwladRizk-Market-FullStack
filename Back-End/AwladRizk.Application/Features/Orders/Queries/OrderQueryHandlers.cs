using AutoMapper;
using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Orders.Queries;

public sealed class GetOrderByNumberQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    : IRequestHandler<GetOrderByNumberQuery, OrderDetailDto?>
{
    public async Task<OrderDetailDto?> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByOrderNumberAsync(request.OrderNumber, cancellationToken);
        if (order is null || order.SessionId != request.SessionId)
        {
            return null;
        }

        var dto = mapper.Map<OrderDetailDto>(order);
        dto.Items = mapper.Map<List<OrderItemDto>>(order.Items);
        dto.Payment = order.Payment is null ? null : mapper.Map<PaymentDto>(order.Payment);
        return dto;
    }
}
