using Fleet.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Fleet.API.Hubs;

public class TrackingHub : Hub<ITrackingClient>
{
    public async Task StreamLocation(VehicleLocationUpdate update)
    {
        await Clients.Group(update.VehicleId.ToString())
            .ReceiveLocation(update);
    }

    public override async Task OnConnectedAsync()
    {
        var vehicleId = Context.GetHttpContext()?.Request.Query["vehicleId"].ToString();
        if (!string.IsNullOrWhiteSpace(vehicleId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, vehicleId);
        }

        await base.OnConnectedAsync();
    }
}
