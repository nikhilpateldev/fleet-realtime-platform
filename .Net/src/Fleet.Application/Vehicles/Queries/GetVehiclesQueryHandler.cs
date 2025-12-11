using Fleet.Application.Common.Interfaces;
using Fleet.Application.Vehicles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fleet.Application.Vehicles.Queries;

public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IReadOnlyList<VehicleDto>>
{
    private readonly IFleetDbContext _context;

    public GetVehiclesQueryHandler(IFleetDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .AsNoTracking()
            .Select(v => new VehicleDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Model = v.Model,
                Make = v.Make,
                Year = v.Year,
                IsActive = v.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}
