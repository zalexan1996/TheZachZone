using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public class AddGameCommand : IRequest<ZachZoneCommandResponse<int>>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string[] Genres { get; set; }
    public required IFormFile File { get; set; }
}


public class AddGameCommandHandler(IDatabaseService dbContext, ICurrentUserService user) : IRequestHandler<AddGameCommand, ZachZoneCommandResponse<int>>
{
    public async Task<ZachZoneCommandResponse<int>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        if (!user.UserId.HasValue)
        {
            throw new ApplicationException("User is not authenticated.");
        }

        var newGame = new Game()
        {
            Name = request.Name,
            Description = request.Description,
            Genres = dbContext.Set<Genre>().Where(x => request.Genres.Contains(x.Name)).ToList(),
            UploadDate = DateOnly.FromDateTime(DateTime.Now),
            Comments = [],
            AuthorId = user.UserId.Value,
            Author = await dbContext.Set<User>().SingleAsync(x => x.Id == user.UserId.Value, cancellationToken)
        };


        await dbContext.Add(newGame, cancellationToken);
        await dbContext.SaveChanges(cancellationToken);

        var newEntry = await dbContext.Set<Game>().SingleAsync(x => x.Name == request.Name);
        string root = Directory.GetCurrentDirectory();

        string uploadDirectory = "wwwroot/games";

        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        string gamePath = Path.Combine(root, uploadDirectory, newEntry.Id.ToString());
        if (!Directory.Exists(gamePath))
        {
            Directory.CreateDirectory(gamePath);
        }

        string zipPath = Path.Combine(gamePath, "game.zip");

        using (var stream = new FileStream(zipPath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        ZipFile.ExtractToDirectory(zipPath, gamePath);

        File.Delete(zipPath);

        return ZachZoneCommandResponse.Success(newEntry.Id);
    }
}