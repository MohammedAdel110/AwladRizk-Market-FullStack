using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AwladRizk.Domain.Enums;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.API.Controllers;

[ApiController]
[Route("api/admin/orders")]
[Authorize(Roles = "Admin")]
public sealed class AdminOrdersController(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrders(
        [FromQuery] string? statuses,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        OrderStatus[]? parsedStatuses = null;
        if (!string.IsNullOrWhiteSpace(statuses))
        {
            var parts = statuses.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var list = new List<OrderStatus>();
            foreach (var p in parts)
            {
                if (Enum.TryParse<OrderStatus>(p, true, out var st))
                {
                    list.Add(st);
                }
            }
            parsedStatuses = list.Count > 0 ? list.ToArray() : null;
        }

        var (items, total) = await orderRepository.GetAdminOrdersAsync(parsedStatuses, page, pageSize, ct);
        var dtoItems = items.Select(o => new AdminOrderListItemDto
        {
            OrderId = o.Id,
            OrderNumber = o.OrderNumber,
            CreatedAt = o.CreatedAt,
            CustomerName = string.IsNullOrWhiteSpace(o.CustomerName) ? "—" : o.CustomerName,
            Phone = o.Phone,
            TotalAmount = o.GrandTotal,
            Status = o.Status.ToString(),
            PaymentMethod = o.Payment?.Method.ToString() ?? string.Empty,
            PaymentStatus = o.Payment?.Status.ToString() ?? string.Empty,
            DeliveryAddress = $"{o.DeliveryStreet}, {o.DeliveryArea}, {o.DeliveryCity}, {o.DeliveryGovernorate}"
        }).ToList();

        return Ok(new
        {
            items = dtoItems,
            totalCount = total,
            page,
            pageSize
        });
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusBody body, CancellationToken ct = default)
    {
        if (!Enum.TryParse<OrderStatus>(body.Status, true, out var status))
        {
            return BadRequest(new { message = "Invalid order status." });
        }

        var order = await orderRepository.GetByIdAsync(id, ct);
        if (order is null) return NotFound();

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;
        orderRepository.Update(order);
        await unitOfWork.SaveChangesAsync(ct);

        return NoContent();
    }
}

public sealed class UpdateOrderStatusBody
{
    public string Status { get; set; } = "Pending";
}

public sealed class AdminOrderListItemDto
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string CustomerName { get; set; } = "—";
    public string Phone { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
}

