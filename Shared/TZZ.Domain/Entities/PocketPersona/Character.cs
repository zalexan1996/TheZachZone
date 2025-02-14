using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.PocketPersona;

public class Character
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int GameId { get; set; }
    public required Game Game { get; set; }
}

public class CharacterEntityTypeConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable(nameof(Character), "PP");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasOne(x => x.Game)
            .WithMany()
            .HasForeignKey(x => x.GameId);
    }
}