using Fleet.Domain.Enums;
using System;

namespace Fleet.Domain.Entities;

public class Trip
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Guid DriverId { get; set; }
    public Driver? Driver { get; set; }
    public TripStatus Status { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.AddYears(-2).AddHours(3).AddHours(3);
    public DateTimeOffset? StartedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
}
