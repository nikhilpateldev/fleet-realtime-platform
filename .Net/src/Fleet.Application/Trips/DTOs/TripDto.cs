using Fleet.Domain.Enums;
using System;

namespace Fleet.Application.Trips.DTOs;

public class TripDto
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public Guid DriverId { get; set; }
    public TripStatus Status { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
}
