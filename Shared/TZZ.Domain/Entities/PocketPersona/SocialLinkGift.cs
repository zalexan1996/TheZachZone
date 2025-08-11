using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TZZ.Domain.Constants;

namespace TZZ.Domain.Entities.PocketPersona;

public class SocialLinkGift
{
  public int SocialLinkGiftId { get; set; }
  public required string Name { get; set; }
  public required string AcquiredAt { get; set; }
  public int SocialLinkId { get; set; }

  public required SocialLink SocialLink { get; set; }
}

public class SocialLinkGiftEntityTypeConfiguration : IEntityTypeConfiguration<SocialLinkGift>
{
  public void Configure(EntityTypeBuilder<SocialLinkGift> builder)
  {
    builder.ToTable(nameof(SocialLinkGift), SchemaNames.PocketPersona);

    builder.HasKey(x => x.SocialLinkGiftId);

    builder.Property(x => x.SocialLinkGiftId).UseIdentityColumn();

    builder.HasOne(x => x.SocialLink)
      .WithMany(x => x.Gifts)
      .HasForeignKey(x => x.SocialLinkId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}