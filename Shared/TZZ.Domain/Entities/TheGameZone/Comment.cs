using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Domain.Entities.TheGameZone;

public class Comment
{
  public int Id { get; set; }
  public int AuthorId { get; set; }
  public int GameInfoId { get; set; }
  public required string Content { get; set; }
  public DateTime PostedOn { get; set; }
  public DateTime? UpdatedOn { get; set; }

  public required User Author { get; set; }
  public required Game Game { get; set; }
}

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
  public void Configure(EntityTypeBuilder<Comment> builder)
  {
    builder.ToTable(nameof(Comment), "TGZ");

    builder.HasOne(x => x.Game)
      .WithMany(x => x.Comments)
      .HasForeignKey(x => x.GameInfoId);

    builder.HasOne(x => x.Author)
      .WithMany()
      .HasForeignKey(x => x.AuthorId);
  }
}