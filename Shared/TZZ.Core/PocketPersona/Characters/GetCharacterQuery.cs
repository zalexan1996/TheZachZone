using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;

namespace TZZ.Core.PocketPersona.Characters;

public class CharacterDto
{
  public int CharacterId { get; set; }
  public required string Name { get; set; }
  public int GameId { get; set; }
  public required string ImageData { get; set; }
}
public class GetCharacterQuery : IRequest<ZachZoneCommandResponse<List<CharacterDto>>>
{
  public int? Id { get; set; }
  public string? Name { get; set; }
  public int? GameId { get; set; }
}

public class GetCharacterQueryHandler(IDatabaseService dbContext)
  : IRequestHandler<GetCharacterQuery, ZachZoneCommandResponse<List<CharacterDto>>>
{
  public async Task<ZachZoneCommandResponse<List<CharacterDto>>> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
  {
    var query = dbContext.Entity<Domain.Entities.PocketPersona.Character>();

    if (request.Id.HasValue)
    {
      query = query.Where(x => x.Id == request.Id.Value);
    }
    if (!string.IsNullOrEmpty(request.Name))
    {
      query = query.Where(x => x.Name.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase));
    }
    if (request.GameId.HasValue)
    {
      query = query.Where(x => x.GameId == request.GameId.Value);
    }

    var records = await query.Select(x => new CharacterDto()
    {
      CharacterId = x.Id,
      GameId = x.GameId,
      Name = x.Name,
      ImageData = Convert.ToBase64String(x.ImageBytes.ToArray())
    }).ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(records);
  }
}
