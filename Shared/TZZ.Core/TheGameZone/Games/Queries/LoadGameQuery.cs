using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record LoadGameQuery(int Id) : IRequest<ZachZoneCommandResponse<byte[]>>;

public class LoadGameQueryHandler(IDatabaseService dbContext) : IRequestHandler<LoadGameQuery, ZachZoneCommandResponse<byte[]>>
{
  public async Task<ZachZoneCommandResponse<byte[]>> Handle(LoadGameQuery request, CancellationToken cancellationToken)
  {
    var gameInfo = await dbContext.Entity<Game>().SingleAsync(x => x.Id == request.Id, cancellationToken);

    return ZachZoneCommandResponse.Success<byte[]>([]);
  }
}