using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<Driver?> GetByLicenseNumberAsync(string licenseNumber)
    {
        return await _context.Drivers
            .FirstOrDefaultAsync(d => d.LicenseNumber == licenseNumber);
    }
}