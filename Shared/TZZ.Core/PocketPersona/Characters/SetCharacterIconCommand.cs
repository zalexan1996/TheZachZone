using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TZZ.Common;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Characters;

public class SetCharacterIconCommand : IRequest<ZachZoneCommandResponse>
{
  public int CharacterId { get; set; }
  public required IFormFile File { get; set; }
}

public class SetCharacterIconCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<SetCharacterIconCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(SetCharacterIconCommand request, CancellationToken cancellationToken)
  {
    var character = await dbContext.Entity<Character>()
      .SingleAsync(x => x.Id == request.CharacterId, cancellationToken)
      .ConfigureAwait(false);

    character.ImageBytes.Clear();
    character.ImageBytes = await request.File.ToByteArray().ConfigureAwait(false);

    await dbContext.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }

}