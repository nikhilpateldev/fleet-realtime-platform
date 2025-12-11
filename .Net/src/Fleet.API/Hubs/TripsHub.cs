using Fleet.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Fleet.API.Hubs;

public class TripsHub : Hub
{
    public async Task BroadcastTripUpdate(TripUpdate update)
    {
        await Clients.All.SendAsync("TripUpdated", update);
    }
}
