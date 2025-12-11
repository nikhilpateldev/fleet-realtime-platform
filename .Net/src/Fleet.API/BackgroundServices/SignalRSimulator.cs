using Fleet.API.Hubs;
using Fleet.API.Models;
using Fleet.Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Fleet.API.BackgroundServices;

public class SignalRSimulator : BackgroundService
{
    private readonly IServiceProvider _provider;

    private readonly List<Guid> _vehicleIds = new();
    private readonly List<Guid> _driverIds = new();
    private readonly Dictionary<Guid, TripStatus> _activeTrips = new();

    private double _baseLat = 28.6139;
    private double _baseLng = 77.2090;

    public SignalRSimulator(IServiceProvider provider)
    {
        _provider = provider;

        for (int i = 0; i < 5; i++)
        {
            _vehicleIds.Add(Guid.NewGuid());
            _driverIds.Add(Guid.NewGuid());
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();

            var trackingHub = scope.ServiceProvider.GetRequiredService<IHubContext<TrackingHub, ITrackingClient>>();
            var tripsHub = scope.ServiceProvider.GetRequiredService<IHubContext<TripsHub>>();
            var driversHub = scope.ServiceProvider.GetRequiredService<IHubContext<DriversHub>>();
            var alertsHub = scope.ServiceProvider.GetRequiredService<IHubContext<AlertsHub>>();

            foreach (var vehicleId in _vehicleIds)
            {
                _baseLat += Random.Shared.NextDouble() * 0.001 - 0.0005;
                _baseLng += Random.Shared.NextDouble() * 0.001 - 0.0005;

                await trackingHub.Clients.All.ReceiveLocation(new VehicleLocationUpdate
                {
                    VehicleId = vehicleId,
                    Latitude = _baseLat,
                    Longitude = _baseLng
                });
            }

            foreach (var driverId in _driverIds)
            {
                var isOnline = Random.Shared.Next(0, 10) > 1;

                await driversHub.Clients.All.SendAsync("DriverStatusChanged", new DriverStatusUpdate
                    {
                        DriverId = driverId,
                        IsOnline = isOnline,
                        CurrentStatus = isOnline ? "Driving" : "Offline"
                    }, cancellationToken: stoppingToken);
            }

            var tripId = _activeTrips.Keys.FirstOrDefault();

            if (tripId == Guid.Empty || Random.Shared.Next(0, 10) > 6)
            {
                tripId = Guid.NewGuid();
                _activeTrips[tripId] = TripStatus.Created;

                await tripsHub.Clients.All.SendAsync("TripUpdated",
                    new TripUpdate
                    {
                        TripId = tripId,
                        Status = TripStatus.Created
                    });
            }
            else
            {
                var current = _activeTrips[tripId];

                var next = current switch
                {
                    TripStatus.Created => TripStatus.Assigned,
                    TripStatus.Assigned => TripStatus.InProgress,
                    TripStatus.InProgress => TripStatus.Completed,
                    _ => TripStatus.Completed
                };

                _activeTrips[tripId] = next;

                await tripsHub.Clients.All.SendAsync("TripUpdated", new TripUpdate
                    {
                        TripId = tripId,
                        Status = next
                    }, cancellationToken: stoppingToken);
            }

            if (Random.Shared.Next(0, 10) > 7)
            {
                await alertsHub.Clients.All.SendAsync("AlertPushed",
                    new AlertMessage
                    {
                        Type = "System",
                        Message = "Random health check event"
                    });
            }

            await Task.Delay(3000, stoppingToken);
        }
    }
}
