using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;

namespace TZZ.Core.PocketPersona.ActivityLog;

public class GetActivityLogsQuery : IRequest<ZachZoneCommandResponse<List<Domain.Entities.PocketPersona.ActivityLog>>>
{
}

public class GetActivityLogsQueryHandler(IDatabaseService dbContext)
  : IRequestHandler<GetActivityLogsQuery, ZachZoneCommandResponse<List<Domain.Entities.PocketPersona.ActivityLog>>>
{
  public async Task<ZachZoneCommandResponse<List<Domain.Entities.PocketPersona.ActivityLog>>> Handle(GetActivityLogsQuery request, CancellationToken cancellationToken)
  {
    var results = await dbContext.Entity<Domain.Entities.PocketPersona.ActivityLog>()
      .OrderByDescending(x => x.Timestamp)
      .ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(results);
  }
}