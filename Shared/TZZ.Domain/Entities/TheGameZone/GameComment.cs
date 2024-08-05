using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class GameComment
{
    public int Id { get; set; }
    public required string Author { get; set; }
    public int GameInfoId { get; set; }
    public required string Content { get; set; }
    public DateTime PostedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }

    public required GameInfo GameInfo { get; set; }
}

public class CommentConfiguration : IEntityTypeConfiguration<GameComment>
{
    public void Configure(EntityTypeBuilder<GameComment> builder)
    {
        builder.ToTable(nameof(GameComment), "TGZ");

        builder.HasOne(x => x.GameInfo)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.GameInfoId);
    }
}