using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Domain.Entities.TheGameZone;

public class Review
{
  public int Id { get; set; }
  public int AuthorId { get; set; }
  public int GameId { get; set; }
  public required string Content { get; set; }
  public DateTime CreatedOn { get; set; }
  public DateTime? LastUpdated { get; set; }

  public required User Author { get; set; }
  public required Game Game { get; set; }
}

public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
{
  public void Configure(EntityTypeBuilder<Review> builder)
  {
    builder.ToTable(nameof(Review), "TGZ");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).UseIdentityColumn();

    builder.Property(x => x.Content)
      .IsRequired();

    builder.HasOne(x => x.Author)
      .WithMany()
      .HasForeignKey(x => x.AuthorId);

    builder.HasOne(x => x.Game)
      .WithMany()
      .HasForeignKey(x => x.GameId);

    builder.HasIndex(["AuthorId", "GameId"])
      .IsUnique();
  }
}