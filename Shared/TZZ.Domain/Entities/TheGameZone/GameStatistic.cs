using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class GameStatistic
{
  public int Id { get; set; }
  public int GameId { get; set; }
  public DateTime PlayedOn { get; set; }
  public required Game Game { get; set; }
}

public class GameStatisticConfig : IEntityTypeConfiguration<GameStatistic>
{
  public void Configure(EntityTypeBuilder<GameStatistic> builder)
  {
    builder.ToTable(nameof(GameStatistic), "TGZ");

    builder.HasOne(x => x.Game)
      .WithMany()
      .HasForeignKey(x => x.GameId);
  }
}