using Fleet.Application.Vehicles.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Fleet.Application.Vehicles.Queries;

public record GetVehiclesQuery() : IRequest<IReadOnlyList<VehicleDto>>;
