
using TZZ.Common.Interfaces;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Infrastructure.Data.Seeders;

internal sealed class PocketPersonaSeeder(IDatabaseService dbContext) : DataSeeder(dbContext)
{

  public override async Task Seed(CancellationToken cancellationToken)
  {
    await TryAdd<Arcana>(1, new Arcana
    {
      Id = 1,
      Name = "Death"
    }, cancellationToken);
  }
}
