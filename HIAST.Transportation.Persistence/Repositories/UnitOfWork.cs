using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Persistence.DatabaseContext;

namespace HIAST.Transportation.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TransportationDbContext _context;
    private IEmployeeRepository? _employeeRepository;
    private IDriverRepository? _driverRepository;
    private ISupervisorRepository? _supervisorRepository;
    private IBusRepository? _busRepository;
    private IStopRepository? _stopRepository;
    private ILineRepository? _lineRepository;
    private ILineSubscriptionRepository? _lineSubscriptionRepository;

    public UnitOfWork(TransportationDbContext context)
    {
        _context = context;
    }

    public IEmployeeRepository EmployeeRepository => 
        _employeeRepository ??= new EmployeeRepository(_context);

    public IDriverRepository DriverRepository => 
        _driverRepository ??= new DriverRepository(_context);

    public ISupervisorRepository SupervisorRepository => 
        _supervisorRepository ??= new SupervisorRepository(_context);

    public IBusRepository BusRepository => 
        _busRepository ??= new BusRepository(_context);

    public IStopRepository StopRepository => 
        _stopRepository ??= new StopRepository(_context);

    public ILineRepository LineRepository => 
        _lineRepository ??= new LineRepository(_context);
    
    public ILineSubscriptionRepository LineSubscriptionRepository => 
        _lineSubscriptionRepository ??= new LineSubscriptionRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}