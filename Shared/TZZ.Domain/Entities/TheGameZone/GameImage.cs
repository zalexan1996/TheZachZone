using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class GameImage
{
    public int Id { get; set; }
    public int GameInfoId { get; set; }
    public byte[] Data { get; set; } = [];
    public DateTime UploadedOn { get; set; }

    public required GameInfo GameInfo { get; set; }
}

public class GameImageConfiguration : IEntityTypeConfiguration<GameImage>
{
    public void Configure(EntityTypeBuilder<GameImage> builder)
    {
        builder.ToTable(nameof(GameImage), "TGZ");
        builder.HasOne(x => x.GameInfo)
            .WithMany()
            .HasForeignKey(x => x.GameInfoId);
    }
}