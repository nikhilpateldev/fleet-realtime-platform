using System;

namespace Fleet.Application.Vehicles.DTOs;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsActive { get; set; }
}
