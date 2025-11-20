using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class SupervisorRepository : GenericRepository<Supervisor>, ISupervisorRepository
{
    public SupervisorRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Supervisor>> GetSupervisorsWithLinesAsync()
    {
        return await _context.Supervisors
            .Include(s => s.Lines)
            .ToListAsync();
    }
}