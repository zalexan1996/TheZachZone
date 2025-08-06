using Microsoft.Extensions.Options;
using System.Globalization;
using TZZ.Common.Configuration;
using TZZ.Core.Common.Services;

namespace TZZ.WebShared.Common.Services;

public class PathLocatorService(IOptions<AppSettings> appSettingsOptions) : IPathLocatorService
{
  public string GetCurrentWorkingDirectory() => Directory.GetCurrentDirectory();
  public string GetProjectRoot() => AppContext.BaseDirectory;
  public string GetStaticFilesPath() => Path.Combine(GetProjectRoot(), appSettingsOptions.Value.StaticFilesFolderName);
  public string GetUploadedGamesDirectory()
    => Path.Combine(GetStaticFilesPath(), "games");

  public string GetUploadedGameDirectory(int gameId)
    => Path.Combine(GetStaticFilesPath(), "games", gameId.ToString());
}
