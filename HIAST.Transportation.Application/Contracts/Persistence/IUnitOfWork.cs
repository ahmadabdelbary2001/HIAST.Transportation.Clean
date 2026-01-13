using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository EmployeeRepository { get; }
    IDriverRepository DriverRepository { get; }
    IBusRepository BusRepository { get; }
    IStopRepository StopRepository { get; }
    ILineRepository LineRepository { get; }
    ILineSubscriptionRepository LineSubscriptionRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken =  default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

}