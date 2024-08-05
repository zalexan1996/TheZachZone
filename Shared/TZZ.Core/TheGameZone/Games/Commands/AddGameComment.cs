using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Domain.Entities.TheGameZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public record AddGameCommentCommand(int GameInfoId, string Author, string Content) : IRequest<ZachZoneCommand<int>>;

public class AddGameCommentCommandHandler(IDatabaseService dbContext) : IRequestHandler<AddGameCommentCommand, ZachZoneCommand<int>>
{
    public async Task<ZachZoneCommand<int>> Handle(AddGameCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new GameComment()
        {
            Author = request.Author,
            Content = request.Content,
            GameInfoId = request.GameInfoId,
            PostedOn = DateTime.Now,
            GameInfo = await dbContext.Set<GameInfo>().SingleAsync(x => x.Id == request.GameInfoId, cancellationToken)
        };

        await dbContext.Add(comment, cancellationToken);
        await dbContext.SaveChanges(cancellationToken);

        return ZachZoneCommand.Success(comment.Id);
    }
}