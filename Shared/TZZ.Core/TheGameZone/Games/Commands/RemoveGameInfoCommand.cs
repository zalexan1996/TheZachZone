using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public record RemoveGameInfoCommand(int Id) : IRequest<ZachZoneCommandResponse>;

public class RemoveGameInfoCommandHandler(IDatabaseService dbContext) : IRequestHandler<RemoveGameInfoCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(RemoveGameInfoCommand request, CancellationToken cancellationToken)
  {
    var gameInfo = await dbContext.Entity<Game>().SingleAsync(x => x.Id == request.Id, cancellationToken);

    dbContext.Remove(gameInfo);
    await dbContext.SaveChanges(cancellationToken);


    string root = Directory.GetCurrentDirectory();
    string uploadDirectory = Path.Combine(root, "wwwroot", "games", request.Id.ToString());
    if (Directory.Exists(uploadDirectory))
    {
      Directory.Delete(uploadDirectory, true);
    }
    return new ZachZoneCommandResponse();
  }
}