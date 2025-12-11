using Fleet.API.Models;

namespace Fleet.API.Hubs;

public interface ITrackingClient
{
    Task ReceiveLocation(VehicleLocationUpdate update);
}
