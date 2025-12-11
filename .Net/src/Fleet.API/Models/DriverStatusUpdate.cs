namespace Fleet.API.Models;

public class DriverStatusUpdate
{
    public Guid DriverId { get; set; }
    public bool IsOnline { get; set; }
    public string? CurrentStatus { get; set; }
}
