using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Domain.Enums;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class BusRepository : GenericRepository<Bus>, IBusRepository
{
    public BusRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Bus>> GetBusesByStatusAsync(BusStatus status)
    {
        return await _context.Buses
            .Where(b => b.Status == status)
            .ToListAsync();
    }

    public async Task<Bus?> GetByLicensePlateAsync(string licensePlate)
    {
        return await _context.Buses
            .FirstOrDefaultAsync(b => b.LicensePlate == licensePlate);
    }
}