using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.PocketPersona;

public class SocialLinkDialogue
{
  public int Id { get; set; }
  public required string Text { get; set; }
  public int Rank { get; set; }
  public int Order { get; set; }
  public string? ExtraRequirement { get; set; }

  public int SocialLinkId { get; set; }

  public required SocialLink SocialLink { get; set; }
}

public class SocialLinkDialogueEntityTypeConfiguration : IEntityTypeConfiguration<SocialLinkDialogue>
{
  public void Configure(EntityTypeBuilder<SocialLinkDialogue> builder)
  {
    builder.ToTable(nameof(SocialLinkDialogue), "PP");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).UseIdentityColumn();

    builder.HasOne(x => x.SocialLink)
      .WithMany(x => x.Dialogues)
      .HasForeignKey(x => x.SocialLinkId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasIndex(x => new { x.Rank, x.Order })
      .IsUnique();
  }
}