using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace TZZ.Common.Shared.Interfaces;

public interface IDatabaseService
{
    public DatabaseFacade Database { get; }
    public IQueryable<TEntity> Set<TEntity>() where TEntity : class;
    public Task Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;
    public void Remove<TEntity>(TEntity entity) where TEntity : class;
    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    public Task SaveChanges(CancellationToken cancellationToken);
    public Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken);
    public Task CommitTransaction(CancellationToken cancellationToken);
    public Task RollbackTransaction(CancellationToken cancellationToken);

}