using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO.Compression;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheGameZone.Games.Commands;

public class AddGameCommand : IRequest<ZachZoneCommandResponse<int>>
{
  public required string Name { get; init; }
  public required string Description { get; init; }
  public required IList<string> Genres { get; init; } = [];
  public required IFormFile File { get; init; }
}


public class AddGameCommandHandler(IDatabaseService dbContext, ICurrentUserService user) : IRequestHandler<AddGameCommand, ZachZoneCommandResponse<int>>
{
  public async Task<ZachZoneCommandResponse<int>> Handle(AddGameCommand request, CancellationToken cancellationToken)
  {
    if (!user.UserId.HasValue)
    {
      throw new InvalidOperationException("You must be logged in to add a game.");
    }

    var newGame = new Game()
    {
      Name = request.Name,
      Description = request.Description,
      Genres = dbContext.Entity<Genre>().Where(x => request.Genres.Contains(x.Name)).ToList(),
      UploadDate = DateOnly.FromDateTime(DateTime.Now),
      Comments = [],
      AuthorId = user.UserId.Value,
      Author = await dbContext.Entity<User>().SingleAsync(x => x.Id == user.UserId.Value, cancellationToken)
    };


    await dbContext.Add(newGame, cancellationToken);
    await dbContext.SaveChanges(cancellationToken);

    var newEntry = await dbContext.Entity<Game>()
      .SingleAsync(x => x.Name == request.Name, cancellationToken);

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
      await request.File.CopyToAsync(stream, cancellationToken);
    }

    ZipFile.ExtractToDirectory(zipPath, gamePath);

    File.Delete(zipPath);

    return ZachZoneCommandResponse.Success(newEntry.Id);
  }
}