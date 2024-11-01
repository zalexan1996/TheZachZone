using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public class CommentDto
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public required string AuthorName { get; set; }
    public required string Content { get; set; }
    public int GameInfoId { get; set; }
    public DateTime PostedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}

public record GetGameCommentsQuery(int GameInfoId) : IRequest<ZachZoneCommand<List<CommentDto>>>;

public class GetGameCommentsQueryHandler(IDatabaseService dbContext) : IRequestHandler<GetGameCommentsQuery, ZachZoneCommand<List<CommentDto>>>
{
    public async Task<ZachZoneCommand<List<CommentDto>>> Handle(GetGameCommentsQuery request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Set<GameComment>()
            .Where(c => c.GameInfoId == request.GameInfoId)
            .OrderBy(x => x.PostedOn)
            .Select(x => new CommentDto()
            {
                AuthorId = x.AuthorId,
                AuthorName = $"{x.Author.FirstName} {x.Author.LastName}",
                Content = x.Content,
                GameInfoId = x.GameInfoId,
                PostedOn = x.PostedOn,
                UpdatedOn = x.UpdatedOn,
                Id = x.Id
            })
            .ToListAsync(cancellationToken);

        return ZachZoneCommand.Success(results);
    }
}