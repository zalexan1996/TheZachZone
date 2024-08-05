namespace TZZ.Common.Shared.Interfaces;

public interface IDatabaseService
{
    public IQueryable<TEntity> Set<TEntity>() where TEntity : class;
    public Task Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;
    public void Remove<TEntity>(TEntity entity) where TEntity : class;
    public Task SaveChanges(CancellationToken cancellationToken);
}