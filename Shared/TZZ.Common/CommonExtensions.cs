using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TZZ.Common;

public static class CommonExtensions
{
  public static bool IsNSwagBuild(this IConfigurationRoot configuration)
  {
    bool result = false;

    if (!bool.TryParse(Environment.GetEnvironmentVariable("NSWAG"), out result))
    {
      return false;
    }

    return result;
  }

  public static async Task<byte[]> ToByteArray(this IFormFile formFile)
  {
    ArgumentNullException.ThrowIfNull(formFile, nameof(formFile));

    using var memStream = new MemoryStream();

    await formFile.CopyToAsync(memStream).ConfigureAwait(false);

    return memStream.ToArray();
  }
}