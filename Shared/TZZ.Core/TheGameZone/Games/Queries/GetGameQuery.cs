using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public class GameDto
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public IEnumerable<string> Genres { get; set; } = [];
  public DateOnly UploadDate { get; set; }
  public int AuthorId { get; set; }
  public required string Author { get; set; }
}
public record GetGameQuery(int? Id = null, string? Name = null, string? Genre = null) : IRequest<ZachZoneCommandResponse<List<GameDto>>>;

public class GetGameQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameQuery, ZachZoneCommandResponse<List<GameDto>>>
{
  public async Task<ZachZoneCommandResponse<List<GameDto>>> Handle(GetGameQuery request, CancellationToken cancellationToken)
  {
    var results = await dbContext.Entity<Game>()
      .Where(x => request.Id == null || x.Id == request.Id)
      .Where(x => request.Name == null || x.Name == request.Name)
      .Where(x => request.Genre == null || x.Genres.Any(c => request.Genre == c.Name))
      .OrderByDescending(x => x.UploadDate)
      .Select(x => new GameDto
      {
        Id = x.Id,
        Name = x.Name,
        Description = x.Description,
        Genres = x.Genres.Select(x => x.Name),
        UploadDate = x.UploadDate,
        AuthorId = x.AuthorId,
        Author = x.Author.DisplayName
      })
      .ToListAsync(cancellationToken);

    return ZachZoneCommandResponse.Success(results);
  }
}