using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface IDriverRepository : IGenericRepository<Driver>
{
    Task<Driver?> GetByLicenseNumberAsync(string licenseNumber);
}