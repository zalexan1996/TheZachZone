using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TZZ.Domain.Entities.Common;

namespace TZZ.Domain.Entities.PocketPersona;

public class Arcana : IEntity
{
  public int Id { get; set; }
  public required string Name { get; set; }
}

public class ArcanaEntityTypeConfiguration : IEntityTypeConfiguration<Arcana>
{
  public void Configure(EntityTypeBuilder<Arcana> builder)
  {
    builder.ToTable(nameof(Arcana), "PP");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).UseIdentityColumn();

    builder.Property(x => x.Name).HasMaxLength(100);

    builder.HasIndex(x => x.Name).IsUnique();
  }
}