using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Core.PocketPersona.Games.NotificationHandlers;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Games;

public class AddGameCommand : IRequest<ZachZoneCommandResponse>
{
  public required string Name { get; set; }
  public required string PrimaryColor { get; set; }
  public required string SecondaryColor { get; set; }
}

public class AddGameCommandHandler(IDatabaseService dbContext, IPublisher publisher)
  : IRequestHandler<AddGameCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(AddGameCommand request, CancellationToken cancellationToken)
  {
    if (await dbContext.Entity<Game>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
    {
      return ZachZoneCommandResponse.Failure(nameof(AddGameCommand.Name), "The specified game already exists.");
    }

    var game = new Game()
    {
      Name = request.Name,
      PrimaryColor = request.PrimaryColor,
      SecondaryColor = request.SecondaryColor
    };

    await dbContext.Add(game, cancellationToken);

    await dbContext.SaveChanges(cancellationToken);

    await publisher.Publish(new GameAddedNotification(game), cancellationToken);

    return ZachZoneCommandResponse.Success();

  }
}