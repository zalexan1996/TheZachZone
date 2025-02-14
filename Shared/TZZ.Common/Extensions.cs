using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TZZ.Common;

public static class Extensions
{
    public static bool IsNSwagBuild(this IConfigurationRoot configuration)
    {
        bool result = false;
        bool.TryParse(Environment.GetEnvironmentVariable("NSWAG"), out result);
        return result;
    }

    public static async Task<byte[]> ToByteArray(this IFormFile formFile)
    {
        using var memStream = new MemoryStream();

        await formFile.CopyToAsync(memStream);

        return memStream.ToArray();
    }
}