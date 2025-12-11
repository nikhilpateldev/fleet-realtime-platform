using Fleet.Application.Trips.DTOs;
using MediatR;
using System;

namespace Fleet.Application.Trips.Commands;

public record CreateTripCommand(
    Guid VehicleId,
    Guid DriverId,
    string Origin,
    string Destination
) : IRequest<TripDto>;
