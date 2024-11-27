using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public record AddGameCommentCommand(int GameInfoId, string Content) : IRequest<ZachZoneCommandResponse<int>>;

public class AddGameCommentCommandHandler(IDatabaseService dbContext, ICurrentUserService curService) : IRequestHandler<AddGameCommentCommand, ZachZoneCommandResponse<int>>
{
    public async Task<ZachZoneCommandResponse<int>> Handle(AddGameCommentCommand request, CancellationToken cancellationToken)
    {
        var author = await dbContext.Set<User>().Where(x => x.Id == curService.UserId).FirstOrDefaultAsync(cancellationToken);

        if (author is null)
        {
            throw new ArgumentException("User not authenticated.");
        }

        var comment = new Comment()
        {
            Author = author,
            AuthorId = author.Id,
            Content = request.Content,
            GameInfoId = request.GameInfoId,
            PostedOn = DateTime.Now,
            Game = await dbContext.Set<Game>().SingleAsync(x => x.Id == request.GameInfoId, cancellationToken)
        };

        await dbContext.Add(comment, cancellationToken);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success(comment.Id);
    }
}