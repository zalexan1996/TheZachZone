using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;

namespace TZZ.Core.PocketPersona.Characters;

public class CharacterDto
{
    public int CharacterId { get; set; }
    public required string Name { get; set; }
    public int GameId { get; set; }
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
        var query = dbContext.Set<Domain.Entities.PocketPersona.Character>();

        if (request.Id.HasValue)
        {
            query.Where(x => x.Id == request.Id.Value);
        }
        if (!string.IsNullOrEmpty(request.Name))
        {
            query.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
        }
        if (request.GameId.HasValue)
        {
            query.Where(x => x.GameId == request.GameId.Value);
        }

        var records = await query.Select(x => new CharacterDto()
        {
            CharacterId = x.Id,
            GameId = x.GameId,
            Name = x.Name,
        }).ToListAsync(cancellationToken);

        return ZachZoneCommandResponse.Success(records);
    }
}
