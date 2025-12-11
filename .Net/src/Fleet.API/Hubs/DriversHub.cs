using Fleet.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Fleet.API.Hubs;

public class DriversHub : Hub
{
    public async Task DriverStatusChanged(DriverStatusUpdate update)
    {
        await Clients.All.SendAsync("DriverStatusChanged", update);
    }
}
