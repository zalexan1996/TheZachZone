using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.Search;

public record SearchResultDto(string EntityType, int EntityId);
public record SearchQuery(string term, int? gameId) : IRequest<ZachZoneCommandResponse<List<SearchResultDto>>>;

public class SearchQueryHandler(IDatabaseService dbContext)
  : IRequestHandler<SearchQuery, ZachZoneCommandResponse<List<SearchResultDto>>>
{
  public async Task<ZachZoneCommandResponse<List<SearchResultDto>>> Handle(SearchQuery request, CancellationToken cancellationToken)
  {
    var arcanaItems = await dbContext.Entity<Arcana>()
      .Where(a => a.Name.Contains(request.term, StringComparison.CurrentCultureIgnoreCase))
      .Select(x => new SearchResultDto(ActivityLogEntityTypes.Arcana, x.Id))
      .ToListAsync(cancellationToken);

    var gameItems = await dbContext.Entity<Game>()
      .Where(g => g.Name.Contains(request.term, StringComparison.CurrentCultureIgnoreCase))
      .Select(x => new SearchResultDto(ActivityLogEntityTypes.Game, x.Id))
      .ToListAsync(cancellationToken);

    var characterItems = await dbContext.Entity<Character>()
      .Where(c => c.Name.Contains(request.term, StringComparison.CurrentCultureIgnoreCase))
      .Select(x => new SearchResultDto(ActivityLogEntityTypes.Character, x.Id))
      .ToListAsync(cancellationToken);

    var items = arcanaItems
      .Concat(gameItems)
      .Concat(characterItems).ToList();

    return ZachZoneCommandResponse.Success(items);
  }
}