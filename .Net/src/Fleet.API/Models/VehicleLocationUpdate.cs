namespace Fleet.API.Models;

public class VehicleLocationUpdate
{
    public Guid VehicleId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow.AddYears(-2).AddHours(3);
}
