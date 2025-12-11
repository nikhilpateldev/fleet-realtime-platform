using Fleet.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Fleet.API.Hubs;

public class AlertsHub : Hub
{
    public async Task PushAlert(AlertMessage alert)
    {
        await Clients.All.SendAsync("AlertPushed", alert);
    }
}
