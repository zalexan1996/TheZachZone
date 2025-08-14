using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TZZ.Domain.Entities.Common;

namespace TZZ.Domain.Entities.PocketPersona;

public static class ActivityLogEntityTypes
{
  public const string Character = nameof(Character);
  public const string Arcana = nameof(Arcana);
  public const string Game = nameof(Game);
  public const string SocialLink = nameof(SocialLink);
}

public class ActivityLog : IEntity
{
  public int Id { get; set; }
  public required string Activity { get; set; }
  public required string EntityType { get; set; }
  public int EntityId { get; set; }
  public DateTime Timestamp { get; set; }
  public int? UserId { get; set; }
  public TheZachZone.User? User { get; set; }
}

public class ActivityLogEntityTypeConfiguration : IEntityTypeConfiguration<ActivityLog>
{
  public void Configure(EntityTypeBuilder<ActivityLog> builder)
  {
    builder.ToTable(nameof(ActivityLog), "PocketPersona");

    builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    builder.Property(x => x.Timestamp).HasDefaultValueSql("GETDATE()");
  }
}