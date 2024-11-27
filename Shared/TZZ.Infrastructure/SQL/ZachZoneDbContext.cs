using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;

using TZZ.Domain.Entities.TheGameZone;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Infrastructure.SQL;

public class ZachZoneDbContext : IdentityDbContext<User, Role, int>, IDatabaseService
{
    public ZachZoneDbContext(DbContextOptions<ZachZoneDbContext> options) : base(options) { }
    async Task IDatabaseService.Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class => await AddAsync(entity, cancellationToken);
    void IDatabaseService.Remove<TEntity>(TEntity entity) where TEntity : class => Remove(entity);
    async Task IDatabaseService.SaveChanges(CancellationToken cancellationToken) => await SaveChangesAsync(cancellationToken);
    IQueryable<TEntity> IDatabaseService.Set<TEntity>() where TEntity : class => Set<TEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Game).Assembly);
    }
}