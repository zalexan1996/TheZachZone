namespace TZZ.Core.Common.Services;

public interface IPathLocatorService
{
  public string GetCurrentWorkingDirectory();
  public string GetProjectRoot();
  public string GetStaticFilesPath();
  public string GetUploadedGamesDirectory();
  public string GetUploadedGameDirectory(int gameId);
}
