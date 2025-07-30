using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TZZ.Common.Interfaces;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Infrastructure.SQL;

public sealed class ZachZoneDbContext : IdentityDbContext<User, Role, int>, IDatabaseService
{
  public ZachZoneDbContext(DbContextOptions<ZachZoneDbContext> options) : base(options) { }

  async Task IDatabaseService.Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
    => await AddAsync(entity, cancellationToken);

  void IDatabaseService.Remove<TEntity>(TEntity entity) where TEntity : class
    => Remove(entity);

  void IDatabaseService.RemoveRange<TEntity>(IEnumerable<TEntity> entities)
    => RemoveRange(entities);

  async Task IDatabaseService.SaveChanges(CancellationToken cancellationToken)
    => await SaveChangesAsync(cancellationToken);

  IQueryable<TEntity> IDatabaseService.Entity<TEntity>() where TEntity : class
    => Set<TEntity>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfigurationsFromAssembly(typeof(Game).Assembly);

  }

  public Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken)
    => Database.BeginTransactionAsync(cancellationToken);

  public Task CommitTransaction(CancellationToken cancellationToken)
    => Database.CommitTransactionAsync(cancellationToken);
  public Task RollbackTransaction(CancellationToken cancellationToken)
    => Database.RollbackTransactionAsync(cancellationToken);

}