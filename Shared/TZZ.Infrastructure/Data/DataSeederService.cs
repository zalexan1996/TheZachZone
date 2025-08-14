namespace TZZ.Infrastructure.Data;

public class DataSeederService(IEnumerable<IDataSeeder> seeders)
{
  public async Task Seed(CancellationToken cancellationToken)
  {
    foreach (var seeder in seeders)
    {
      await seeder.Seed(cancellationToken);
    }
  }
}
