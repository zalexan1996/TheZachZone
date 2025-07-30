using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Metadata;

public class GetGenresQuery : IRequest<ZachZoneCommandResponse<IEnumerable<string>>>
{
}

public class GetGenresQueryHandler(IDatabaseService dbContext)
  : IRequestHandler<GetGenresQuery, ZachZoneCommandResponse<IEnumerable<string>>>
{
  public async Task<ZachZoneCommandResponse<IEnumerable<string>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
  {
    var results = await dbContext.Entity<Genre>()
      .Select(x => x.Name)
      .OrderBy(x => x)
      .ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(results.AsEnumerable());
  }
}