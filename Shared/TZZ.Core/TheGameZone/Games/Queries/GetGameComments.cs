using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public record CommentDto(int Id, string Author, string Content, int GameInfoId, DateTime PostedOn, DateTime? UpdatedOn);

public record GetGameCommentsQuery(int GameInfoId) : IRequest<ZachZoneCommand<List<CommentDto>>>;

public class GetGameCommentsQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameCommentsQuery, ZachZoneCommand<List<CommentDto>>>
{
    public async Task<ZachZoneCommand<List<CommentDto>>> Handle(GetGameCommentsQuery request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Set<GameComment>()
            .Where(c => c.GameInfoId == request.GameInfoId)
            .OrderBy(x => x.PostedOn)
            .Select(x => new CommentDto(x.Id, x.Author, x.Content, x.GameInfoId, x.PostedOn, x.UpdatedOn))
            .ToListAsync(cancellationToken);

        return ZachZoneCommand.Success(results);
    }
}