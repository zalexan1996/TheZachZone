using MediatR;
using TZZ.Core.PocketPersona.ActivityLog.Services;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Characters.Notifications;

public record AddCharacterNotification(Character Character) : INotification;

public class AddCharacterNotificationHandler(ActivityLogService activity) : INotificationHandler<AddCharacterNotification>
{
  public async Task Handle(AddCharacterNotification notification, CancellationToken cancellationToken)
  {
    await activity.AddEntry(
      entityType: ActivityLogEntityTypes.Character,
       activityType: "Added",
       entityId: notification.Character.Id,
       cancellationToken: cancellationToken).ConfigureAwait(false);
  }
}