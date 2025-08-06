using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Core.PocketPersona.Arcanas.Notifications;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Arcanas;

public class AddArcanaCommand : IRequest<ZachZoneCommandResponse>
{
  public required string Name { get; set; }
}

public class AddArcanaCommandHandler(IDatabaseService dbContext, IPublisher publisher)
  : IRequestHandler<AddArcanaCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(AddArcanaCommand request, CancellationToken cancellationToken)
  {
    if (await dbContext.Entity<Arcana>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
    {
      return ZachZoneCommandResponse.Failure(nameof(AddArcanaCommand.Name), "The specified Arcana already exists.");
    }

    var arcana = new Arcana()
    {
      Name = request.Name,
    };

    await dbContext.Add(arcana, cancellationToken);

    await dbContext.SaveChanges(cancellationToken);

    await publisher.Publish(new AddArcanaNotification(arcana), cancellationToken);
    return ZachZoneCommandResponse.Success();

  }
}