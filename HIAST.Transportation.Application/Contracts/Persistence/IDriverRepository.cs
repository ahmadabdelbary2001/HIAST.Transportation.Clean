using HIAST.Transportation.Application.DTOs.Driver.Validators;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface IDriverRepository : IGenericRepository<Driver>
{
    Task<DriverWithLineDetails?> GetDriverWithDetailsAsync(int driverId);
    Task<Driver?> GetByLicenseNumberAsync(string licenseNumber);
}