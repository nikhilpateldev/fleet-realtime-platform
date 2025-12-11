using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Fleet.Application.Common.Interfaces;

public interface IFleetDbContext
{
    DbSet<Vehicle> Vehicles { get; }
    DbSet<Driver> Drivers { get; }
    DbSet<Trip> Trips { get; }
    DbSet<MaintenanceRecord> MaintenanceRecords { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
