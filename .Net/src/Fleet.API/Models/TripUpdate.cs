using Fleet.Domain.Enums;

namespace Fleet.API.Models;

public class TripUpdate
{
    public Guid TripId { get; set; }
    public TripStatus Status { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow.AddYears(-2).AddHours(3);
}
