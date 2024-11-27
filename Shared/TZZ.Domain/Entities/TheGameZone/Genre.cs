using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.TheGameZone;

public class Genre
{
    public required string Name { get; set; }
}

public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable(nameof(Genre), "TGZ");

        builder.HasKey(x => x.Name);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}