using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Domain.Entities.Common;

namespace TZZ.Infrastructure.Data;

public interface IDataSeeder
{
  Task Seed(CancellationToken cancellationToken);
}

internal abstract class DataSeeder(IDatabaseService dbContext) : IDataSeeder
{
  public async Task<bool> TryAdd<TEntity>(int id, TEntity entity, CancellationToken cancellationToken)
    where TEntity : class, IEntity
  {
    if (!await dbContext.Entity<TEntity>().AnyAsync(x => x.Id == id, cancellationToken))
    {
      await dbContext.Add(entity, cancellationToken);
    }

    return false;
  }

  public abstract Task Seed(CancellationToken cancellationToken);
}
