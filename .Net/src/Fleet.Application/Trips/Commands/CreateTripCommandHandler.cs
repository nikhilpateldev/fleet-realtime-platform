using Fleet.Application.Common.Interfaces;
using Fleet.Application.Trips.DTOs;
using Fleet.Domain.Entities;
using Fleet.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fleet.Application.Trips.Commands;

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
{
    private readonly IFleetDbContext _context;

    public CreateTripCommandHandler(IFleetDbContext context)
    {
        _context = context;
    }

    public async Task<TripDto> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var entity = new Trip
        {
            Id = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            DriverId = request.DriverId,
            Origin = request.Origin,
            Destination = request.Destination,
            Status = TripStatus.Created,
            CreatedAt = DateTimeOffset.UtcNow.AddYears(-2).AddHours(3)
        };

        _context.Trips.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new TripDto
        {
            Id = entity.Id,
            VehicleId = entity.VehicleId,
            DriverId = entity.DriverId,
            Status = entity.Status,
            Origin = entity.Origin,
            Destination = entity.Destination
        };
    }
}
