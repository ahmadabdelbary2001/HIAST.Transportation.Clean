using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface ISupervisorRepository : IGenericRepository<Supervisor>
{
    Task<IReadOnlyList<Supervisor>> GetSupervisorsWithLinesAsync();
}