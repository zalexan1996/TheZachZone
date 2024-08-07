using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.TheGameZone.Games.Commands;

public class AddGameInfoCommand : IRequest<ZachZoneCommand<int>>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string[] Categories { get; set; }
    public required IFormFile File { get; set; }
}


public class AddGameInfoCommandHandler(IDatabaseService dbContext, IPathLocatorService pathLocatorService) : IRequestHandler<AddGameInfoCommand, ZachZoneCommand<int>>
{
    public async Task<ZachZoneCommand<int>> Handle(AddGameInfoCommand request, CancellationToken cancellationToken)
    {
        var newGame = new GameInfo()
        {
            Name = request.Name,
            Description = request.Description,
            Categories = request.Categories,
            UploadDate = DateOnly.FromDateTime(DateTime.Now)
        };


        await dbContext.Add(newGame, cancellationToken);
        await dbContext.SaveChanges(cancellationToken);

        var newEntry = await dbContext.Set<GameInfo>().SingleAsync(x => x.Name == request.Name);
        string root = Directory.GetCurrentDirectory();

        string uploadDirectory = pathLocatorService.GetUploadedGamesDirectory();

        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        string gamePath = pathLocatorService.GetUploadedGameDirectory(newEntry.Id);
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

        return ZachZoneCommand.Success(newEntry.Id);
    }
}