using MediatR;
using TZZ.Core.PocketPersona.ActivityLog.Services;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Arcanas.Notifications;

public record AddArcanaNotification(Arcana Arcana) : INotification;

public class AddArcanaNotificationHandler(ActivityLogService activity) : INotificationHandler<AddArcanaNotification>
{
  public async Task Handle(AddArcanaNotification notification, CancellationToken cancellationToken)
  {
    await activity.AddEntry(
      entityType: ActivityLogEntityTypes.Arcana,
      activityType: "Added",
      entityId: notification.Arcana.Id,
      cancellationToken: cancellationToken).ConfigureAwait(false);
  }
}