using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Queries;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public int AuthorId { get; set; }
    public required string Author { get; set; }
}
public class GetReviewsQuery : IRequest<ZachZoneCommandResponse<IEnumerable<ReviewDto>>>
{
    public int? GameId { get; set; }
    public int? ReviewId { get; set; }
}

public class GetReviewsQueryHandler (IDatabaseService dbContext)
    : IRequestHandler<GetReviewsQuery, ZachZoneCommandResponse<IEnumerable<ReviewDto>>>
{
    public async Task<ZachZoneCommandResponse<IEnumerable<ReviewDto>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Set<Review>();

        if (request.GameId.HasValue)
        {
            query.Where(x => x.GameId == request.GameId);
        }

        if (request.ReviewId.HasValue)
        {
            query.Where(x => x.Id == request.ReviewId);
        }

        var records = await query
            .Select(x => new ReviewDto()
            {
                Author = x.Author.DisplayName,
                AuthorId = x.Author.Id,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                ReviewId = x.Id
            })
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync(cancellationToken);

        return ZachZoneCommandResponse.Success(records.AsEnumerable());
    }
}
