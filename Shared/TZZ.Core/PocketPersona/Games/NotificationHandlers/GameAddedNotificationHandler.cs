using MediatR;
using TZZ.Core.PocketPersona.ActivityLog.Services;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Games.NotificationHandlers;

public record GameAddedNotification(Game Game) : INotification;
public class GameAddNotificationHandler(ActivityLogService activity) : INotificationHandler<GameAddedNotification>
{
  public async Task Handle(GameAddedNotification notification, CancellationToken cancellationToken)
  {
    await activity.AddEntry(ActivityLogEntityTypes.Game, "Added", notification.Game.Id, cancellationToken)
      .ConfigureAwait(false);
  }
}