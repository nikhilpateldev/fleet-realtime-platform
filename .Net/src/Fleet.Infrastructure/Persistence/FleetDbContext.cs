using Fleet.Application.Common.Interfaces;
using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infrastructure.Persistence;

public class FleetDbContext : DbContext, IFleetDbContext
{
    public FleetDbContext(DbContextOptions<FleetDbContext> options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<MaintenanceRecord> MaintenanceRecords => Set<MaintenanceRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>(b =>
        {
            b.HasKey(v => v.Id);
            b.Property(v => v.RegistrationNumber).IsRequired().HasMaxLength(32);
        });

        modelBuilder.Entity<Driver>(b =>
        {
            b.HasKey(d => d.Id);
            b.Property(d => d.Code).IsRequired().HasMaxLength(32);
        });

        modelBuilder.Entity<Trip>(b =>
        {
            b.HasKey(t => t.Id);
            b.HasOne(t => t.Vehicle)
                .WithMany(v => v.Trips)
                .HasForeignKey(t => t.VehicleId);

            b.HasOne(t => t.Driver)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DriverId);
        });

        modelBuilder.Entity<MaintenanceRecord>(b =>
        {
            b.HasKey(m => m.Id);
            b.HasOne(m => m.Vehicle)
                .WithMany(v => v.MaintenanceRecords)
                .HasForeignKey(m => m.VehicleId);
        });
    }
}
