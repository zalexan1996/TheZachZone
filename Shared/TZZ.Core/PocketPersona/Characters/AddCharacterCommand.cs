using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Core.PocketPersona.Characters.Notifications;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Characters;

public class AddCharacterCommand : IRequest<ZachZoneCommandResponse>
{
  public required string Name { get; set; }
  public required int GameId { get; set; }
}

public class AddCharacterCommandHandler(IDatabaseService dbContext, IPublisher publisher)
  : IRequestHandler<AddCharacterCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(AddCharacterCommand request, CancellationToken cancellationToken)
  {
    if (await dbContext.Entity<Character>().AnyAsync(x => x.Name.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase), cancellationToken))
    {
      return ZachZoneCommandResponse.Failure(nameof(AddCharacterCommand.Name), "The specified Character already exists.");
    }

    var game = await dbContext.Entity<Game>().SingleOrDefaultAsync(x => x.Id == request.GameId, cancellationToken);

    if (game is null)
    {
      return ZachZoneCommandResponse.Failure(nameof(AddCharacterCommand.GameId), "The specified game does not exist.");
    }

    var character = new Character()
    {
      Name = request.Name,
      GameId = request.GameId,
      Game = game
    };

    await dbContext.Add(character, cancellationToken);

    await dbContext.SaveChanges(cancellationToken);

    await publisher.Publish(new AddCharacterNotification(character), cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}