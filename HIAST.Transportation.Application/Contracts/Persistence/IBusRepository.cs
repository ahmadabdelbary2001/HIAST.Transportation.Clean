using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface IBusRepository : IGenericRepository<Bus>
{
    Task<IReadOnlyList<Bus>> GetBusesByStatusAsync(BusStatus status);
    Task<Bus?> GetByLicensePlateAsync(string licensePlate);
}