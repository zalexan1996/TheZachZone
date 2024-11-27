using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class Genre
{
    public int GenreId { get; set; }
    public required string Name { get; set; }
}

public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable(nameof(Genre), "TGZ");

        builder.HasKey(x => x.GenreId);

        builder.Property(x => x.GenreId).UseIdentityColumn();

        builder.Property(x => x.Name).HasMaxLength(100);
    }
}