using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.PocketPersona;

public class SocialLinkRank
{
    public int Id { get; set; }
    public int Rank { get; set; }
    public string? Requirement { get; set; }
    public int SocialLinkId { get; set; }

    public required SocialLink SocialLink { get; set; }
}

public class SocialLinkRankEntityTypeConfiguration : IEntityTypeConfiguration<SocialLinkRank>
{
    public void Configure(EntityTypeBuilder<SocialLinkRank> builder)
    {
        builder.ToTable(nameof(SocialLinkRank), "PP");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();

        builder.HasOne(x => x.SocialLink)
            .WithMany()
            .HasForeignKey(x => x.SocialLinkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}