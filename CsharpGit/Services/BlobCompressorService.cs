using System.IO.Compression;

namespace CSharpGit.Services;
/// <summary>
/// Compressor blob class
/// </summary>
public static class BlobCompressorService
{
    
    /// <summary>
    /// Compress File to Blob
    /// </summary>
    /// <param name="filePath">File to compress path</param>
    /// <returns>Array of bytes of memory stream</returns>
    public static async Task<byte[]> CompressFileToBlob(string filePath)
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