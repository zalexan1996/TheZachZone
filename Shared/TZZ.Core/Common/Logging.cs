using Microsoft.Extensions.Logging;

namespace TZZ.Core.Common;

public static partial class LoggingGenerators
{
  [LoggerMessage(Level = LogLevel.Error, Message = "{commandName} failed with errors: {errors}")]
  public static partial void LogCommandError(this ILogger logger, string commandName, IDictionary<string, string> errors);

  [LoggerMessage(Level = LogLevel.Error, Message = "'{commandName}' threw an exception: {exception}, {innerException}")]
  public static partial void LogCommandException(this ILogger logger, string commandName, string exception, string innerException);
}
