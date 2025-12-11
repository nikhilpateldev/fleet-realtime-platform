using System;

namespace Fleet.Domain.Entities;

public class MaintenanceRecord
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public DateTimeOffset ScheduledAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCritical { get; set; }
}
