using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;

namespace TZZ.Core.PocketPersona.Arcanas;

public class ArcanaDto
{
  public int Id { get; set; }
  public required string Name { get; set; }
}
public class GetArcanaQuery : IRequest<ZachZoneCommandResponse<IEnumerable<ArcanaDto>>>
{
}

public class GetArcanaQueryHandler(IDatabaseService dbContext)
  : IRequestHandler<GetArcanaQuery, ZachZoneCommandResponse<IEnumerable<ArcanaDto>>>
{
  public async Task<ZachZoneCommandResponse<IEnumerable<ArcanaDto>>> Handle(GetArcanaQuery request, CancellationToken cancellationToken)
  {
    var results = await dbContext.Entity<TZZ.Domain.Entities.PocketPersona.Arcana>().Select(x => new ArcanaDto()
    {
      Id = x.Id,
      Name = x.Name,
    }).ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(results.AsEnumerable());
  }
}