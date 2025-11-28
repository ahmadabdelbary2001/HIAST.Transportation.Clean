using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Driver.Validators;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<DriverWithLineDetails?> GetDriverWithDetailsAsync(int driverId)
    {
        // We will no longer use Include/ThenInclude. We take full control.
        var query = from driver in _context.Drivers
            where driver.Id == driverId
            // Use a LEFT JOIN to handle drivers with no line.
            join line in _context.Lines on driver.Id equals line.DriverId into driverLines
            from line in driverLines.DefaultIfEmpty() // This makes it a LEFT JOIN
            // If a line exists, join to the bus.
            join bus in _context.Buses on line.BusId equals bus.Id into lineBus
            from bus in lineBus.DefaultIfEmpty()
            select new DriverWithLineDetails
            {
                Driver = driver,
                Line = line,
                Bus = bus
            };

        return await query.FirstOrDefaultAsync();
    }
    
    public async Task<Driver?> GetByLicenseNumberAsync(string licenseNumber)
    {
        return await _context.Drivers
            .FirstOrDefaultAsync(d => d.LicenseNumber == licenseNumber);
    }
}