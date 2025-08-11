using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.PocketPersona;

public class SocialLink
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public int CharacterId { get; set; }
  public int ArcanaId { get; set; }

  public required Character Character { get; set; }
  public required Arcana Arcana { get; set; }
  public IList<SocialLinkDialogue> Dialogues { get; init; } = [];
  public IList<SocialLinkGift> Gifts { get; init; } = [];
}

public class SocialLinkEntityTypeConfiguration : IEntityTypeConfiguration<SocialLink>
{
  public void Configure(EntityTypeBuilder<SocialLink> builder)
  {
    builder.ToTable(nameof(SocialLink), "PP");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id)
      .UseIdentityColumn();

    builder.Property(x => x.Name)
      .HasMaxLength(100);


    builder.HasOne(x => x.Character)
      .WithOne()
      .HasForeignKey(nameof(SocialLink), nameof(SocialLink.CharacterId));

    builder.HasOne(x => x.Arcana)
      .WithMany()
      .HasForeignKey(x => x.ArcanaId);
  }
}
