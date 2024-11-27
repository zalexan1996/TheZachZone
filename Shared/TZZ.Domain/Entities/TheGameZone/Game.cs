using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly UploadDate { get; set; }
    public required ICollection<Genre> Genres { get; set; }  

    public required ICollection<Comment> Comments { get; set; }
}

public class GameInfoConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable(nameof(Game), "TGZ");

        builder.HasMany(x => x.Genres)
            .WithMany();
    }
}