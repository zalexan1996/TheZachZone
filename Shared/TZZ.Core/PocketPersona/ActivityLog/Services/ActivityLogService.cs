using TZZ.Common.Interfaces;
using TZZ.Core.Common.Services;

namespace TZZ.Core.PocketPersona.ActivityLog.Services;

public class ActivityLogService(IDatabaseService dbContext, ICurrentUserService user)
{
  public async Task AddEntry(string entityType, string activityType, int entityId, CancellationToken cancellationToken)
  {
    await dbContext.Add(new Domain.Entities.PocketPersona.ActivityLog()
    {
      Activity = activityType,
      EntityId = entityId,
      EntityType = entityType,
      UserId = user.UserId,
      Timestamp = DateTime.Now
    }, cancellationToken);

    await dbContext.SaveChanges(cancellationToken);
  }
}
