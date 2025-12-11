using System;
using System.Collections.Generic;

namespace Fleet.Domain.Entities;

public class Driver
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public string? CurrentStatus { get; set; }

    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
