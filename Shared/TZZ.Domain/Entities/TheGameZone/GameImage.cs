using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class GameImage
{
  public int Id { get; set; }
  public int GameId { get; set; }
  public IList<byte> Data { get; init; } = [];
  public DateTime UploadedOn { get; set; }

  public required Game Game { get; set; }
}

public class GameImageConfiguration : IEntityTypeConfiguration<GameImage>
{
  public void Configure(EntityTypeBuilder<GameImage> builder)
  {
    builder.ToTable(nameof(GameImage), "TGZ");

    builder.HasOne(x => x.Game)
      .WithMany()
      .HasForeignKey(x => x.GameId);
  }
}