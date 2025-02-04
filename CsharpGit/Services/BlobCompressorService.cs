using System.IO.Compression;

namespace CSharpGit.Services;

public class BlobCompressorService
{
    public static async Task<byte[]> CompressToBlob(string filePath)
    {
        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var memoryStream = new MemoryStream();
        var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
        var readOnlyMemory = new ReadOnlyMemory<byte>(fileBytes);
        var cancellationToken = CancellationToken.None;
        await gzipStream.WriteAsync(readOnlyMemory, cancellationToken);

        return memoryStream.ToArray();
    }
}