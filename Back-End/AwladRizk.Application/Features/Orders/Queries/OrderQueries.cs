using MediatR;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Features.Orders.Queries;

public record GetOrderByNumberQuery(string OrderNumber, string SessionId) : IRequest<OrderDetailDto?>;
