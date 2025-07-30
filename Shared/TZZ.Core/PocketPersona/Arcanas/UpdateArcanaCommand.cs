using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;

namespace TZZ.Core.PocketPersona.Arcanas;

public class UpdateArcanaCommand : IRequest<ZachZoneCommandResponse>
{
  public int Id { get; set; }
  public required string Name { get; set; }
}

public class UpdateArcanaCommandHandler(IDatabaseService dbContext) : IRequestHandler<UpdateArcanaCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(UpdateArcanaCommand request, CancellationToken cancellationToken)
  {
    var record = await dbContext.Entity<Domain.Entities.PocketPersona.Arcana>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

    if (record is null)
    {
      return ZachZoneCommandResponse.Failure(nameof(UpdateArcanaCommand.Id), "No Arcana exists with the specified Id.");
    }

    record.Name = request.Name;

    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}
