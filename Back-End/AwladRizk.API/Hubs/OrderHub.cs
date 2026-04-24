using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AwladRizk.API.Hubs;

public interface IOrderClient
{
    Task ReceiveNewOrder(NewOrderNotification payload);
}

public sealed record NewOrderNotification(
    int OrderId,
    string CustomerName,
    decimal TotalAmount
);

[Authorize(Roles = "Admin")]
public sealed class OrderHub : Hub<IOrderClient>
{
    public const string AdminsGroup = "admins";

    public override async Task OnConnectedAsync()
    {
        if (Context.User?.IsInRole("Admin") == true)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, AdminsGroup);
        }

        await base.OnConnectedAsync();
    }
}

