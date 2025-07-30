using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks;

public class DeleteSocialLinkCommand : IRequest<ZachZoneCommandResponse>
{
  public int Id { get; set; }
}

public class DeleteSocialLinkCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<DeleteSocialLinkCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(DeleteSocialLinkCommand request, CancellationToken cancellationToken)
  {
    var record = await dbContext.Entity<SocialLink>().SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

    if (record is null)
    {
      return ZachZoneCommandResponse.Failure(nameof(DeleteSocialLinkCommand.Id), "No SocialLink exists with the specified Id.");
    }

    dbContext.Remove(record);
    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}