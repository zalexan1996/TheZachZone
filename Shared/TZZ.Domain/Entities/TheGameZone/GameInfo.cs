using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class GameInfo
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly UploadDate { get; set; }
    public required string[] Categories { get; set; }

    public IList<GameComment> Comments { get; set; } = new List<GameComment>();
}

public class GameInfoConfig : IEntityTypeConfiguration<GameInfo>
{
    public void Configure(EntityTypeBuilder<GameInfo> builder)
    {
        builder.ToTable(nameof(GameInfo), "TGZ");

    }
}