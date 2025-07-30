using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Metadata;

public class RemoveGenreCommand : IRequest<ZachZoneCommandResponse>
{
  public required string Name { get; set; }
}

public class RemoveGenreCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<RemoveGenreCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
  {
    var genre = await dbContext.Entity<Genre>().SingleOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

    if (genre is null)
    {
      return ZachZoneCommandResponse.Failure(nameof(RemoveGenreCommand.Name), "The specified genre does not exist.");
    }

    var impact = await dbContext.Entity<Game>().CountAsync(x => x.Genres.Any(g => g.Name == request.Name), cancellationToken);

    dbContext.Remove(genre);
    await dbContext.SaveChanges(cancellationToken);

    return new ZachZoneCommandResponse()
    {
      Infos =
      {
        {
          "Summary", $"Genre '{request.Name}' was removed."
        },
        {
          "Impact", $"{impact} games were affected."
        }
      }
    };
  }
}
