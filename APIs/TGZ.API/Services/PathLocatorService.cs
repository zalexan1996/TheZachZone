using Microsoft.Extensions.Options;
using TZZ.Common.Configuration;
using TZZ.Core.Shared.Services;

namespace TGZ.API.Services;

public class PathLocatorService(IOptions<AppSettings> appSettingsOptions) : IPathLocatorService
{
    public string GetCurrentWorkingDirectory() => Directory.GetCurrentDirectory();
    public string GetProjectRoot() => AppContext.BaseDirectory;
    public string GetStaticFilesPath() => Path.Combine(GetProjectRoot(), appSettingsOptions.Value.StaticFilesFolderName);
    public string GetUploadedGameDirectory(int gameId) => Path.Combine(GetUploadedGamesDirectory(), gameId.ToString());
    public string GetUploadedGamesDirectory() => Path.Combine(GetStaticFilesPath(), "games");
}
