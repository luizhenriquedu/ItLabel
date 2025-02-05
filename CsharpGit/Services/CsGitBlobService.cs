using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace CSharpGit.Services;
/// <summary>
/// CsGit Blob class
/// </summary>
public static class CsGitBlobService
{
    
    /// <summary>
    ///     Creates a blob
    /// </summary>
    /// <param name="content">Bytes to be blobed</param>
    /// <returns>Array of bytes of hashed content</returns>
    
    public static byte[] CreateCsGitBlob(byte[] content)
    {
        string header = $"blob {content.Length}\0";
        byte[] headerBytes = Encoding.ASCII.GetBytes(header);

        byte[] fullBlobData = new byte[headerBytes.Length + content.Length];
        
        Buffer.BlockCopy(headerBytes, 0, fullBlobData, 0, headerBytes.Length);
        Buffer.BlockCopy(fullBlobData, 0, fullBlobData, headerBytes.Length,  content.Length);
        var hash = SHA256.HashData(fullBlobData);

        return hash;

    }
}